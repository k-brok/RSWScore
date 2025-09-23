using Microsoft.AspNetCore.Builder;

namespace Chunkk.JWT.Server.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseChunkkJwtCORS(this WebApplication app)
    {
        app.UseCors("AllowBlazorClient");
        return app;
    }
}
