using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using RSW.APP.Services;
using RSW.Shared.Interfaces;
using RSW.Shared.Mapper;
using Chunkk.JWT.Client;

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

        builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfile));

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

        var apiBaseUrl = builder.Configuration["ApiBaseUrl"];
        builder.Services.AddChunkkJwt(new Uri(apiBaseUrl));

        builder.Services.AddScoped(sp =>
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(apiBaseUrl!)
            };
            return client;
        });

        await builder.Build().RunAsync();
    }
}
