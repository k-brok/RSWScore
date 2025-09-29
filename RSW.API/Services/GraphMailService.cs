using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.SendMail;

namespace RSW.API.Services;

public class GraphMailService
{
    private readonly IConfiguration _config;

    public GraphMailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendAsync(string toAddress, string subject, string content)
    {
        var graphSection = _config.GetSection("Graph");
        string? tenantId = graphSection["tenantId"];
        string? clientId = graphSection["clientId"];
        string? clientSecret = graphSection["clientSecret"];

        // Authenticate
        var credential = new ClientSecretCredential(
            tenantId,
            clientId,
            clientSecret
        );

        var graphClient = new GraphServiceClient(credential, new[] { "https://graph.microsoft.com/.default" });

        // Bouw het bericht
        var requestBody = new SendMailPostRequestBody
        {
            Message = new Message
            {
                Subject = subject,
                Body = new ItemBody
                {
                    ContentType = BodyType.Html, // of Text
                    Content = content,
                },
                ToRecipients = new List<Recipient>
                {
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = toAddress,
                        },
                    },
                },
            },
            SaveToSentItems = true,
        };

        // Versturen vanuit jouw noreply account
        await graphClient.Users["noreply@regiodelangstraat.nl"]
            .SendMail
            .PostAsync(requestBody);
    }
}
