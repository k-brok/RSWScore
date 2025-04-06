using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Radzen;
using RSW.WebApp.Components;
using RSW.WebApp.Components.Account;
using RSW.WebApp.Data;
using RSW.WebApp.Entities;
using RSW.WebApp.Helpers;
using RSW.WebApp.Interface.Repositories;
using RSW.WebApp.Repositories;
using RSW.WebApp.Services;

namespace RSW.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            //builder.Services.AddAuthentication(options =>
            //    {
            //        options.DefaultScheme = IdentityConstants.ApplicationScheme;
            //        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            //    })
            //    .AddIdentityCookies();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            Console.WriteLine(connectionString);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            builder.Services.AddScoped<IAssociationRepository, AssociationRepository>();
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IEditionRepository, EditionRepository>();
            builder.Services.AddScoped<IScoreRepository, ScoreRepository>();
            builder.Services.AddScoped<IPatrolRepository, PatrolRepository>();
            builder.Services.AddScoped<IScoutRepository, ScoutRepository>();
            builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            builder.Services.AddScoped<ICriteriaRepository, CriteriaRepository>();
            builder.Services.AddScoped<ISubGroupRepository, SubGroupRepository>();
            builder.Services.AddScoped<ISignupCodeRepository, SignupCodeRepository>();
            builder.Services.AddScoped<IJurySlotRepository, JurySlotRepository>();
            builder.Services.AddScoped<IWebSettingRepository, WebSettingRepository>();

            builder.Services.AddScoped<InitData>();
            builder.Services.AddScoped<GenerateExampleData>();

            builder.Services.AddScoped<ScoreCalculationService>();
            builder.Services.AddScoped<PdfService>();
            builder.Services.AddSingleton<CurrentEditionService>();

            builder.Services.AddScoped<UserStorage>();

            builder.Services.AddSingleton<SettingsService>();
            builder.Services.AddSingleton<TimeZoneService>();
            builder.Services.AddSingleton<RefreshCurrentScores>();
            //builder.Services.AddHostedService(sp => sp.GetRequiredService<RefreshCurrentScores>());


            builder.Services.AddRadzenComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapAdditionalIdentityEndpoints(); // Moet vóór Razor Components

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
            
            if (app.Environment.IsDevelopment())
            {
                app.Services.CreateScope().ServiceProvider.GetRequiredService<GenerateExampleData>().GenerateSampleDataAsync();
            }
            app.Services.CreateScope().ServiceProvider.GetRequiredService<CurrentEditionService>().Edition = await app.Services.CreateScope().ServiceProvider.GetRequiredService<IEditionRepository>().GetActive();
            app.Services.CreateScope().ServiceProvider.GetRequiredService<CurrentEditionService>().Categories = await app.Services.CreateScope().ServiceProvider.GetRequiredService<ICategoryRepository>().GetAllAsync();

            app.Services.CreateScope().ServiceProvider.GetRequiredService<InitData>().Initialize();
            app.Services.CreateScope().ServiceProvider.GetRequiredService<SettingsService>().Settings = await app.Services.CreateScope().ServiceProvider.GetRequiredService<IWebSettingRepository>().GetAllAsync();

            app.Run();
        }
    }
}
