using System.Net;
using System.Text.RegularExpressions;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.SendMail;
using RSW.Shared.Interfaces;

public class GraphMailService
{
    private readonly IConfiguration _config;
    private readonly IEmailConfigService _emailConfigService;

    public GraphMailService(IConfiguration config, IEmailConfigService emailConfigService)
    {
        _config = config;
        _emailConfigService = emailConfigService;
    }

    public async Task SendWithLayoutAsync(
        string toAddress,
        string templateName,                   // bv. "ConfirmEmailEmail" (DB)
        IDictionary<string, string> tokens,    // tokens voor de DB-bodytemplate
        string? subjectOverride = null)
    {
        // --- Auth/Graph ---
        var g = _config.GetSection("Graph");
        var credential  = new ClientSecretCredential(g["tenantId"], g["clientId"], g["clientSecret"]);
        var graphClient = new GraphServiceClient(credential, new[] { "https://graph.microsoft.com/.default" });
        var fromAddress = g["fromAddress"] ?? "noreply@regiodelangstraat.nl";

        // --- 1) BODY uit DB renderen ---
        var cfg = await _emailConfigService.GetByNameAsync(templateName)
                  ?? throw new InvalidOperationException($"Template '{templateName}' niet gevonden in DB.");

        var subject = string.IsNullOrWhiteSpace(subjectOverride) ? cfg.Subject : subjectOverride;

        // Tokens in de body (DB-template) HTML-encoden: veilig in tekst en href/src-attributen.
        var renderedBody = ApplyTemplate(cfg.Template, tokens, encodeValues: true, throwOnMissingToken: true);

        // --- 2) Lay-out van disk laden ---
        var layoutPath = Path.Combine(AppContext.BaseDirectory, "Templates", "EmailTemplate.html");
        if (!File.Exists(layoutPath))
            throw new FileNotFoundException("Generieke e-mail lay-out niet gevonden.", layoutPath);

        var layoutHtml = await File.ReadAllTextAsync(layoutPath);

        // --- 3) Lay-out tokens invullen ---
        var layoutTokens = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["Subject"] = subject,                         // moet ge-escaped in <title> en aria-label
            ["Content"] = renderedBody,                    // RAW HTML
            ["year"]    = DateTime.UtcNow.Year.ToString()  // ge-escaped oké
        };

        var finalHtml = ApplyTemplateSelective(
            layoutHtml,
            layoutTokens,
            rawKeys: new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Content" }, // alleen Content raw
            throwOnMissingToken: false
        );

        // --- 4) Verzenden ---
        var body = new SendMailPostRequestBody
        {
            Message = new Message
            {
                Subject = subject, // plain subject
                Body = new ItemBody { ContentType = BodyType.Html, Content = finalHtml },
                ToRecipients = new List<Recipient>
                {
                    new() { EmailAddress = new EmailAddress { Address = toAddress } }
                }
            },
            SaveToSentItems = true
        };

        await graphClient.Users[fromAddress].SendMail.PostAsync(body);
    }

    // Standaard templating (alles encoden)
    private static string ApplyTemplate(
        string template,
        IDictionary<string, string> tokens,
        bool encodeValues,
        bool throwOnMissingToken)
    {
        if (string.IsNullOrEmpty(template)) return template;

        var map = new Dictionary<string, string>(tokens, StringComparer.OrdinalIgnoreCase);
        var rx  = new Regex(@"\{\{\s*(?<key>[A-Za-z0-9_.\-]+)\s*\}\}", RegexOptions.Compiled);

        return rx.Replace(template, m =>
        {
            var key = m.Groups["key"].Value;
            if (!map.TryGetValue(key, out var val))
            {
                if (throwOnMissingToken) throw new KeyNotFoundException($"Token '{{{{{key}}}}}' ontbreekt.");
                return m.Value;
            }
            return encodeValues ? WebUtility.HtmlEncode(val) : val;
        });
    }

    // Selectieve templating (sommige keys raw, de rest encoden)
    private static string ApplyTemplateSelective(
        string template,
        IDictionary<string, string> tokens,
        ISet<string> rawKeys,
        bool throwOnMissingToken)
    {
        if (string.IsNullOrEmpty(template)) return template;

        var map = new Dictionary<string, string>(tokens, StringComparer.OrdinalIgnoreCase);
        var rx  = new Regex(@"\{\{\s*(?<key>[A-Za-z0-9_.\-]+)\s*\}\}", RegexOptions.Compiled);

        return rx.Replace(template, m =>
        {
            var key = m.Groups["key"].Value;
            if (!map.TryGetValue(key, out var val))
            {
                if (throwOnMissingToken) throw new KeyNotFoundException($"Token '{{{{{key}}}}}' ontbreekt.");
                return m.Value;
            }

            // Content raw; alle andere tokens encoden
            return rawKeys.Contains(key) ? val : WebUtility.HtmlEncode(val);
        });
    }
}
