using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using RSW.Application.Interfaces;
using RSW.Infrastructure.Services;
using RSW.Infrastructure.Data;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(o =>
            o.UseSqlite(config.GetConnectionString("Default")));

        services.AddScoped<IAssociationService, AssociationService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICriteriaService, CriteriaService>();
        services.AddScoped<IEditionService, EditionService>();
        services.AddScoped<IUnitService, UnitService>();
        services.AddScoped<IJurySlotService, JurySlotService>();
        services.AddScoped<IPatrolService, PatrolService>();
        services.AddScoped<IScoreService, ScoreService>();
        services.AddScoped<IScoutService, ScoutService>();
        services.AddScoped<ISignupCodeService, SignupCodeService>();
        services.AddScoped<ISubCategoryService, SubCategoryService>();
        services.AddScoped<ISubGroupService, SubGroupService>();
        services.AddScoped<IWebSettingService, WebSettingService>();
        services.AddScoped<IVolunteerTaskService, VolunteerTaskService>();
        services.AddScoped<IVolunteerAssignmentService, VolunteerAssignmentService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmailConfigService, EmailConfigService>();

        services.AddScoped<IUnitLinkRequestService, UnitLinkWorkflow>();
        
        return services;
    }
}