using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RSW.Infrastructure.Data;
using RSW.Domain.Dto;
using RSW.Domain.Entities;
using RSW.Domain.Enums;
using RSW.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace RSW.Infrastructure.Services;

public class UnitLinkWorkflow : IUnitLinkRequestService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitService _unitService;           // bestaand servicecontract
    private readonly IAssociationService _assocService;   // bestaand servicecontract
    private readonly IConfiguration _cfg;
    private readonly GraphMailService _graphMailService;

    public UnitLinkWorkflow(
        AppDbContext db,
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        IUnitService unitService,
        IAssociationService assocService,
        IConfiguration cfg,
        GraphMailService graphMailService)
    {
        _db = db;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _unitService = unitService;
        _assocService = assocService;
        _cfg = cfg;
        _graphMailService = graphMailService;
    }

    public async Task<CreateUnitLinkRequestResponse> CreateAsync(CreateUnitLinkRequestRequest req)
    {
        // 1) Huidige gebruiker
        var currentUserId = req.UserId ?? _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(currentUserId))
            return new CreateUnitLinkRequestResponse { Success = false, Message = "Geen geldige gebruiker." };

        var user = await _userManager.FindByIdAsync(currentUserId);
        if (user == null)
            return new CreateUnitLinkRequestResponse { Success = false, Message = "Gebruiker niet gevonden." };

        // 2) Unit validatie
        var unit = await _unitService.GetByIdAsync(req.UnitId);
        if (unit == null)
            return new CreateUnitLinkRequestResponse { Success = false, Message = "Groep (Unit) niet gevonden." };
        var assoc = await _assocService.GetByIdAsync(unit.AssociationId);

        // 3) Dubbele open aanvraag voorkomen
        var exists = await _db.UnitLinkRequests
            .AnyAsync(x => x.UserId == currentUserId && x.UnitId == req.UnitId && x.Status == UnitLinkRequestStatus.Pending);
        if (exists)
            return new CreateUnitLinkRequestResponse { Success = false, Message = "Er is al een openstaande aanvraag voor deze Groep." };

        // 4) Opslaan
        var entity = new PendingUnitLinkRequest
        {
            UserId = currentUserId,
            UnitId = req.UnitId,
            Message = req.Message
        };
        _db.UnitLinkRequests.Add(entity);
        await _db.SaveChangesAsync();

        // 5) Notificatie naar organisatie (nu al per mail)AdminRequest-GroupLink
        var orgMail = "kasperbrok@gmail.com";

        var tokens = new Dictionary<string, string>
        {
            ["name"] = user.UserName ?? "gebruiker",
            ["email"] = user.Email!,
            ["userId"] = user.Id,
            ["unitName"] = unit.Name!,
            ["associationName"] = assoc != null ? $"({assoc.Name})" : "",
            ["remarks"] = System.Net.WebUtility.HtmlEncode(req.Message ?? "-"),
            ["RequestId"] = entity.Id.ToString(),
            ["RequestDateUtc"] = entity.CreatedUtc.ToString("yyyy-MM-dd HH:mm:ss")
        };

        await _graphMailService.SendWithLayoutAsync(
            toAddress: orgMail!,
            templateName: "AdminRequest-GroupLink",
            tokens: tokens
        );

        // 6) Response
        return new CreateUnitLinkRequestResponse
        {
            Success = true,
            Message = "Aanvraag ingediend.",
            Request = new UnitLinkRequestDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                UnitId = entity.UnitId,
                UnitName = unit.Name,
                AssociationName = assoc?.Name,
                CreatedUtc = entity.CreatedUtc,
                Status = entity.Status
            }
        };
    }

    public async Task<IEnumerable<UnitLinkRequestDto>> GetMineAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);;
        if (string.IsNullOrWhiteSpace(userId)) return Enumerable.Empty<UnitLinkRequestDto>();

        var query = from r in _db.UnitLinkRequests
                    where r.UserId == userId
                    join u in _db.Units on r.UnitId equals u.Id   // als Units in dezelfde DbContext zitten
                    into uu
                    from u in uu.DefaultIfEmpty()
                    select new UnitLinkRequestDto
                    {
                        Id = r.Id,
                        UserId = r.UserId,
                        UnitId = r.UnitId,
                        UnitName = u != null ? u.Name : null,
                        AssociationName = null, // evt. bijvullen via join of later opvragen
                        CreatedUtc = r.CreatedUtc,
                        Status = r.Status
                    };

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<List<PendingUnitLinkRequest>> GetAllAsync()
    {
        return await _db.UnitLinkRequests
                .ToListAsync();
    }

    public async Task<bool> CancelAsync(Guid requestId)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);;
        if (string.IsNullOrWhiteSpace(userId)) return false;

        var r = await _db.UnitLinkRequests.FirstOrDefaultAsync(x => x.Id == requestId && x.UserId == userId);
        if (r == null || r.Status != UnitLinkRequestStatus.Pending) return false;

        r.Status = UnitLinkRequestStatus.Cancelled;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ApproveAsync(Guid requestId)
    {
        var r = await _db.UnitLinkRequests.FirstOrDefaultAsync(x => x.Id == requestId);
        if (r == null || r.Status != UnitLinkRequestStatus.Pending) return false;

        var user = await _userManager.FindByIdAsync(r.UserId);
        if (user == null)
            return false;

        r.Status = UnitLinkRequestStatus.Approved;

        var Unit = await _unitService.GetByIdAsync(r.UnitId);
        if (Unit == null)
            return false;

        user.UnitId = r.UnitId;
        await _userManager.UpdateAsync(user);

        await _db.SaveChangesAsync();

        var tokens = new Dictionary<string, string>
        {
            ["name"] = user.UserName ?? "gebruiker",
            ["email"] = user.Email!,
            ["userId"] = user.Id,
            ["remarks"] = System.Net.WebUtility.HtmlEncode(r.Message ?? "-"),
            ["unitName"] = Unit.Name!,
            ["associationName"] = Unit.AssociationName ?? "",
            ["RequestId"] = r.Id.ToString(),
            ["ApprovalDateUtc"] = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
        };

        await _graphMailService.SendWithLayoutAsync(
            toAddress: user.Email!,
            templateName: "GroupLink-Approved",
            tokens: tokens
        );

        return true;
    }

    public async Task<bool> RejectAsync(Guid requestId)
    {
        var r = await _db.UnitLinkRequests.FirstOrDefaultAsync(x => x.Id == requestId);
        if (r == null || r.Status != UnitLinkRequestStatus.Pending) return false;

        var user = await _userManager.FindByIdAsync(r.UserId);
        if (user == null)
            return false;

        r.Status = UnitLinkRequestStatus.Rejected;
        var Unit = await _unitService.GetByIdAsync(r.UnitId);
        if (Unit == null)
            return false;
        
        Console.WriteLine("Unit found: " + Unit.Name);

        await _db.SaveChangesAsync();

        var tokens = new Dictionary<string, string>
        {
            ["name"] = user.UserName ?? "gebruiker",
            ["email"] = user.Email!,
            ["userId"] = user.Id,
            ["remarks"] = System.Net.WebUtility.HtmlEncode(r.Message ?? "-"),
            ["unitName"] = Unit.Name!,
            ["associationName"] = Unit.AssociationName ?? "",
            ["RequestId"] = r.Id.ToString(),
            ["ApprovalDateUtc"] = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
        };

        await _graphMailService.SendWithLayoutAsync(
            toAddress: user.Email!,
            templateName: "GroupLink-Rejected",
            tokens: tokens
        );

        return true;
    }
}
