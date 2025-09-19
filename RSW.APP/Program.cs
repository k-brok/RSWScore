using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using RSW.Shared.Mapper;

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

        var apiBaseUrl = builder.Configuration["ApiBaseUrl"];
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
