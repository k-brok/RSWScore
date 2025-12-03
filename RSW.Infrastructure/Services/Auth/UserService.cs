using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RSW.Infrastructure.Data;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

namespace RSW.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public UserService(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await Task.FromResult(_userManager.Users.ToList());
        }

        public async Task<ApplicationUser?> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<ApplicationUser> CreateAsync(ApplicationUser model, string password)
        {
            var result = await _userManager.CreateAsync(model, password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Could not create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return model;
        }

        public async Task<ApplicationUser?> UpdateAsync(string id, ApplicationUser model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            // Update velden die je toestaat
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;
            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            user.Unit = model.Unit;
            user.UnitId = model.UnitId;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Could not update user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return user;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<IList<string>?> GetRolesAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<VolunteerAssignment>?> GetVolunteerAssignmentsAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            return await _context.VolunteerAssignments.Where(V => V.UserId == id).ToListAsync();
        }
    }
}
