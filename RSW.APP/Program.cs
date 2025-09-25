using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using RSW.APP.Services;
using RSW.Shared.Interfaces;
using RSW.APP.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace RSW.APP;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddRadzenComponents();

        builder.Services.AddScoped<IAssociationService, AssociationService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<ICriteriaService, CriteriaService>();
        builder.Services.AddScoped<IEditionService, EditionService>();
        builder.Services.AddScoped<IGroupService, GroupService>();
        builder.Services.AddScoped<IJurySlotService, JurySlotService>();
        builder.Services.AddScoped<IPatrolService, PatrolService>();
        builder.Services.AddScoped<IScoreService, ScoreService>();
        builder.Services.AddScoped<IScoutService, ScoutService>();
        builder.Services.AddScoped<ISignupCodeService, SignupCodeService>();
        builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
        builder.Services.AddScoped<ISubGroupService, SubGroupService>();
        builder.Services.AddScoped<IWebSettingService, WebSettingService>();
        builder.Services.AddScoped<IVolunteerTaskService, VolunteerTaskService>();
        builder.Services.AddScoped<IVolunteerAssignmentService, VolunteerAssignmentService>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<ITokenStorage, BrowserTokenStorage>();
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
        builder.Services.AddScoped<JwtAuthenticationStateProvider>();

        builder.Services.AddAuthorizationCore();

        var apiBaseUrl = builder.Configuration["ApiBaseUrl"];
        builder.Services.AddScoped(sp =>
        {
            var client = new HttpClient(new JwtAuthorizationMessageHandler(sp.GetRequiredService<ITokenStorage>()))
            {
                BaseAddress = new Uri(apiBaseUrl)
            };
            return client;
        });

        builder.Services.AddTransient<JwtAuthorizationMessageHandler>();

        await builder.Build().RunAsync();
    }
}
