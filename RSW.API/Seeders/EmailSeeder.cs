using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;

namespace RSW.API.Seeders
{
    public class EmailSeeder
    {
        private readonly AppDbContext _context;

        public EmailSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedEmails()
        {
            if (await _context.EmailConfigs.FirstOrDefaultAsync(e => e.Name == "ResetPasswordEmail") == null)
            {
                var ResetPasswordEmail = new EmailConfig
                {
                    Id = Guid.NewGuid(),
                    Name = "ResetPasswordEmail",
                    Subject = "Stel je wachtwoord opnieuw in",
                    Template = @"
                        <h1>Wachtwoord opnieuw instellen</h1>

                        <p>Hoi {{name}},</p>

                        <p>
                        We hebben een verzoek ontvangen om het wachtwoord voor je account te wijzigen. 
                        Klik op de knop hieronder om een nieuw wachtwoord in te stellen. 
                        De link verandert automatisch na <strong>{{expiryMinutes}} minuten</strong>.
                        </p>

                        <div class=""btn-wrap"">
                        <a href=""{{resetLink}}"" class=""btn"" target=""_blank"" rel=""noopener"">
                            Wachtwoord opnieuw instellen
                        </a>
                        </div>

                        <p class=""note"">
                        Werkt de knop niet? Kopieer en plak deze link in je browser:
                        </p>

                        <p class=""muted"">
                        <a class=""inline"" href=""{{resetLink}}"" target=""_blank"" rel=""noopener"">
                            {{resetLink}}
                        </a>
                        </p>

                        <p>
                        Als jij hier niet om hebt gevraagd, kun je deze e-mail veilig negeren — 
                        er verandert dan niets aan je account. 
                        Wil je hulp of heb je vragen, neem contact op via 
                        <a href=""mailto:{{supportEmail}}"" class=""inline"">{{supportEmail}}</a>.
                        </p>

                        <p>
                        Met vriendelijke groet,<br>
                        Team RegioDeLangstraat
                        </p>"
                };
                _context.EmailConfigs.Add(ResetPasswordEmail);
            }

            if (await _context.EmailConfigs.FirstOrDefaultAsync(e => e.Name == "ConfirmEmailEmail") == null)
            {
                var ConfirmEmailEmail = new EmailConfig
                {
                    Id = Guid.NewGuid(),
                    Name = "ConfirmEmailEmail",
                    Subject = "Bevestig je e-mailadres",
                    Template = @"
                <h2>Welkom bij RSW, {{name}}!</h2>
                <p>Bedankt voor je registratie. Klik op de onderstaande link om je e-mailadres te bevestigen:</p>
                <p><a href='{{confirmationLink}}'>E-mail bevestigen</a></p>
                <p>Heb je dit account niet aangemaakt? Neem dan contact op via {{supportEmail}}.</p>
                <br/>"
                };
                _context.EmailConfigs.Add(ConfirmEmailEmail);
            }

            if (await _context.EmailConfigs.FirstOrDefaultAsync(e => e.Name == "ConfirmChangeEmailEmail") == null)
            {
                var ConfirmEmailEmail = new EmailConfig
                {
                    Id = Guid.NewGuid(),
                    Name = "ConfirmEmailEmail",
                    Subject = "Bevestig je nieuwe e-mailadres",
                    Template = @"
                <h2>Beste {{name}}!</h2>
                <p>Klik op de onderstaande link voor het activeren van je nieuwe emailadres:</p>
                <p><a href='{{confirmationLink}}'>E-mail bevestigen</a></p>
                <p>Heb je je email iet gewijzigd? Neem dan contact op via {{supportEmail}}.</p>
                <br/>"
                };
                _context.EmailConfigs.Add(ConfirmEmailEmail);
            }

            if (await _context.EmailConfigs.FirstOrDefaultAsync(e => e.Name == "AdminRequest-GroupLink") == null)
            {
                var ConfirmEmailEmail = new EmailConfig
                {
                    Id = Guid.NewGuid(),
                    Name = "AdminRequest-GroupLink",
                    Subject = "Nieuwe koppelingsaanvraag (account → Groep)",
                    Template = @"
                        <p>Er is een nieuwe koppelingsaanvraag ingediend.</p>
                        <ul>
                        <li><b>Gebruiker:</b> {{name}} ({{email}})</li>
                        <li><b>UserId:</b> {{userId}}</li>
                        <li><b>Groep:</b> {{unitName}} {{associationName}}</li>
                        <li><b>Bericht:</b> {{remarks}}</li>
                        <li><b>AanvraagId:</b> {{RequestId}}</li>
                        <li><b>Ingediend (UTC):</b> {{RequestDateUtc}}</li>
                        </ul>
                        <p>Deze aanvraag kan later via het beheerscherm worden <i>geaccepteerd</i> of <i>geweigerd</i>.</p>"
                };
                _context.EmailConfigs.Add(ConfirmEmailEmail);
            }

            if (await _context.EmailConfigs.FirstOrDefaultAsync(e => e.Name == "GroupLink-Approved") == null)
            {
                var ConfirmEmailEmail = new EmailConfig
                {
                    Id = Guid.NewGuid(),
                    Name = "GroupLink-Approved",
                    Subject = "Nieuwe koppelingsaanvraag (account → Groep)",
                    Template = @"
                        <h1>Aanvraag goedgekeurd</h1>

                        <p>Hoi {{name}},</p>

                        <p>
                        Goed nieuws — je aanvraag om gekoppeld te worden aan 
                        <b>{{unitName}} ({{associationName}})</b> is goedgekeurd op 
                        <strong>{{ApprovalDateUtc}} (UTC)</strong>.
                        </p>

                        <ul>
                        <li><b>Gebruiker:</b> {{name}} ({{email}})</li>
                        <li><b>UserId:</b> {{userId}}</li>
                        <li><b>Unit (association):</b> {{unitName}} ({{associationName}})</li>
                        <li><b>AanvraagId:</b> {{RequestId}}</li>
                        <li><b>Opmerking:</b> {{remarks}}</li>
                        </ul>

                        <p>
                        Je hebt nu toegang tot de gekoppelde gegevens binnen deze unit.
                        </p>

                        <p>
                        Met vriendelijke groet,<br>
                        Team RegioDeLangstraat
                        </p>"
                };
                _context.EmailConfigs.Add(ConfirmEmailEmail);
            }
            
            if (await _context.EmailConfigs.FirstOrDefaultAsync(e => e.Name == "GroupLink-Rejected") == null)
            {
                var ConfirmEmailEmail = new EmailConfig
                {
                    Id = Guid.NewGuid(),
                    Name = "GroupLink-Rejected",
                    Subject = "Nieuwe koppelingsaanvraag (account → Groep)",
                    Template = @"
                        <h1>Aanvraag afgekeurd</h1>

                        <p>Hoi {{name}},</p>

                        <p>
                        Je aanvraag om gekoppeld te worden aan 
                        <b>{{unitName}} ({{associationName}})</b> is helaas afgekeurd op 
                        <strong>{{ApprovalDateUtc}} (UTC)</strong>.
                        </p>

                        <ul>
                        <li><b>Gebruiker:</b> {{name}} ({{email}})</li>
                        <li><b>UserId:</b> {{userId}}</li>
                        <li><b>Unit (association):</b> {{unitName}} ({{associationName}})</li>
                        <li><b>AanvraagId:</b> {{RequestId}}</li>
                        <li><b>Opmerking:</b> {{remarks}}</li>
                        </ul>

                        <p>
                        Wil je weten waarom of een nieuwe aanvraag doen? Neem contact op met de beheerder van {{associationName}} 
                        voor meer informatie.
                        </p>

                        <p>
                        Met vriendelijke groet,<br>
                        Team RegioDeLangstraat
                        </p>"
                };
                _context.EmailConfigs.Add(ConfirmEmailEmail);
            }
            
            await _context.SaveChangesAsync();
        }
    }
}