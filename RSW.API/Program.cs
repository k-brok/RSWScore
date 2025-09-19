using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.API.Endpoints;
using RSW.API.Services;
using RSW.Shared.Interfaces;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RSW.Shared.Mapper;

namespace RSW.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options
                .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine, LogLevel.Information)
        );

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

        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSignalR();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapAssociationEndpoints();
        app.MapCategoryEndpoints();
        app.MapCriteriaEndpoints();
        app.MapEditionEndpoints();
        app.MapGroupEndpoints();
        app.MapJurySlotEndpoints();
        app.MapPatrolEndpoints();
        app.MapScoreEndpoints();
        app.MapScoutEndpoints();
        app.MapSignupCodeEndpoints();
        app.MapSubCategoryEndpoints();
        app.MapSubGroupEndpoints();
        app.MapWebSettingEndpoints();

        app.MapHub<UpdatesHub>("/hubs/updates");

        app.Run();
    }
}
