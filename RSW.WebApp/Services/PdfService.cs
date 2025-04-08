using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Fonts;
using RSW.WebApp.Entities;
using PdfSharp.Snippets.Font;
using Microsoft.AspNetCore.Components.Forms;
using PdfSharp.Drawing.Layout;
using RSW.WebApp.Interface.Repositories;
using System.Drawing.Imaging;
using System.Drawing;
using ZXing;
using ZXing.Rendering;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using ZXing.ImageSharp.Rendering;
using RSW.WebApp.Repositories;
using Microsoft.AspNetCore.Components;

namespace RSW.WebApp.Services
{
    public class PdfService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ScoreCalculationService scoreCalculationService;
        private readonly IGroupRepository groupRepository;
        private readonly IJurySlotRepository JurySlotRepository;
        private readonly NavigationManager _nav;
        public PdfService(ICategoryRepository icategoryRepository, ScoreCalculationService _scoreCalculationService, IGroupRepository _groupRepository, IJurySlotRepository _JurySlotRepository, NavigationManager navigationManager)
        {
            categoryRepository = icategoryRepository;
            scoreCalculationService = _scoreCalculationService;
            groupRepository = _groupRepository;
            JurySlotRepository = _JurySlotRepository;
            _nav = navigationManager;
        }

        // **1. Aangepaste functie voor één patrouille**
        public async Task<byte[]> GeneratePdfAsync(Edition edition, Patrol patrol)
        {
            await Task.Delay(20);
            using (var memoryStream = new MemoryStream())
            {
                PdfDocument document = new PdfDocument();

                document.Info.Title = $"RSW score form {edition.RSWStartDate.Year.ToString()} - {patrol.Name}";

                await GeneratePatrolPdfPage(document, edition, patrol);

                document.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }

        // **2. Nieuwe functie voor meerdere patrouilles**
        public async Task<byte[]> GenerateMultiplePatrolsPdfAsync(Edition edition, Group GroupFilter = null)
        {
            List<Patrol> patrols;
            await Task.Delay(20);
            using (var memoryStream = new MemoryStream())
            {
                if(GroupFilter == null)
                {
                    patrols = edition.SubGroups.SelectMany(S => S.patrols).OrderByDescending(P => P.TotalScore).ToList();
                }
                else
                {
                    patrols = edition.SubGroups.SelectMany(S => S.patrols).Where(P => P.GroupId == GroupFilter.Id).OrderByDescending(P => P.TotalScore).ToList();
                }
                PdfDocument document = new PdfDocument();

                document.Info.Title = $"RSW score overzicht {edition.RSWStartDate.Year.ToString()}";

                foreach (var patrol in patrols)
                {
                    await GeneratePatrolPdfPage(document, edition, patrol);
                }

                document.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }

        // **3. Herbruikbare functie voor het genereren van een patrouille-pagina**
        private async Task GeneratePatrolPdfPage(PdfDocument document, Edition edition, Patrol patrol)
        {
            List<Category> categories = await categoryRepository.GetAllAsync();
            if (Capabilities.Build.IsCoreBuild)
                GlobalFontSettings.FontResolver = new FailsafeFontResolver();

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            var width = page.Width;
            var height = page.Height;

            DrawLayout(gfx, width, height, $"Scorelijst RSW {edition.RSWStartDate.Year} - {patrol.Name} ({patrol.Group.Association.Abbreviation})");

            int infoStartY = 80; // Startpositie voor patrouille-informatie
            await DrawPatrolInfo(gfx, patrol, width, infoStartY);

            int tableStartY = infoStartY + 280; // Startpositie voor de tabel onder de patrouille-info
            await DrawScoreTable(gfx, patrol, categories, width, tableStartY);

            DrawFooter(gfx, width, height);
        }

        /// <summary>
        /// Tekent de algemene layout van de PDF, inclusief titel en randdecoratie.
        /// </summary>
        private double DrawLayout(XGraphics gfx, double width, double height, string title)
        {
            XFont titleFont = new XFont("Arial", 24, XFontStyleEx.Bold);
            
            gfx.DrawString(title, titleFont, XBrushes.DarkGreen, new XRect(0, 30, width, 40), XStringFormats.TopCenter);

            // Randdecoratie
            XPen borderPen = new XPen(XColors.SaddleBrown, 5);
            gfx.DrawRectangle(borderPen, 20, 20, width - 40, height - 40);

            return 60;
        }

        /// <summary>
        /// Voegt een inleidende tekst en patrouille-informatie toe aan de PDF.
        /// </summary>
        private async Task DrawPatrolInfo(XGraphics gfx, Patrol patrol, double width, int startY)
        {
            XFont introFont = new XFont("Arial", 12, XFontStyleEx.Italic);
            XFont boldFont = new XFont("Arial", 12, XFontStyleEx.Bold);
            XFont regularFont = new XFont("Arial", 12, XFontStyleEx.Regular);

            Group group = await groupRepository.GetAsync(patrol.GroupId);

            int labelX = 50; // Beginpositie voor labels
            int valueX = 200; // Beginpositie voor waarden (uitlijning)
            int lineHeight = 20;

            // Inleidende tekst
            string introText = $"Wat leuk dat je hebt deelgenomen aan de RSW! Ik hoop dat je een geweldig weekend hebt beleefd en mooie herinneringen hebt gemaakt. Samen met je patrouille heb je allerlei uitdagingen volbracht en je uiterste best gedaan. Jullie hebben uiteindelijk de **{patrol.position}e plaats** behaald, een super prestatie!\n\n" +
                               "Hieronder zie je een overzicht van de scores die jullie hebben gehaald. Elke categorie vertegenwoordigt een onderdeel van de RSW, zoals samenwerking, pionieren of koken. Voor elke categorie zijn er punten te verdienen, en deze worden vergeleken met het maximum aantal mogelijke punten. Daarnaast zie je het percentage van de behaalde score en hoe zwaar die categorie meetelt in het totaal.\n\n" +
                               "De ‘Totaal’-score onderaan laat zien hoe goed jullie het als patrouille hebben gedaan over alle onderdelen heen. Gebruik deze informatie om te zien waar jullie sterk in waren en waar jullie misschien nog kunnen groeien voor de volgende keer.\n\n" +
                               "Hopelijk zien we je volgend jaar weer op de RSW! Veel succes en vooral veel plezier met scouting!";

            // Maak een tekstformatter aan om de tekst netjes te wrappen
            XTextFormatter tf = new XTextFormatter(gfx);

            // Definieer het tekstgebied (startX, startY, breedte, hoogte)
            XRect textRect = new XRect(50, startY, width - 100, 160);

            // Tekst toevoegen met automatische tekstafbreking (wrapping)
            tf.DrawString(introText, introFont, XBrushes.Black, textRect, XStringFormats.TopLeft);

            // Verhoog startY zodat de informatie eronder begint
            startY += 180; // Na de inleidende tekst, zorgen voor ruimte

            // Patrouille-informatie
            DrawBoldLabel(gfx, "Vereniging:", group.Association.Name, boldFont, regularFont, labelX, valueX, startY);
            DrawBoldLabel(gfx, "Groep:", group.Name, boldFont, regularFont, labelX, valueX, startY + lineHeight);
            DrawBoldLabel(gfx, "Naam:", patrol.Name, boldFont, regularFont, labelX, valueX, startY + 2 * lineHeight);
            DrawBoldLabel(gfx, "Nummer:", patrol.Number.ToString(), boldFont, regularFont, labelX, valueX, startY + 3 * lineHeight);
            DrawBoldLabel(gfx, "Plaats:", patrol.position.ToString(), boldFont, regularFont, labelX, valueX, startY + 4 * lineHeight);
        }


        /// <summary>
        /// Tekent een vetgedrukte label gevolgd door normale tekst op één lijn.
        /// </summary>
        private void DrawBoldLabel(XGraphics gfx, string label, string value, XFont boldFont, XFont regularFont, int labelX, int valueX, int y)
        {
            gfx.DrawString(label, boldFont, XBrushes.Black, new XRect(labelX, y, 150, 20), XStringFormats.TopLeft);
            gfx.DrawString(value, regularFont, XBrushes.Black, new XRect(valueX, y, 400, 20), XStringFormats.TopLeft);
        }


        /// <summary>
        /// Genereert de scoretabel met alle categorieën en scores.
        /// </summary>
        private async Task DrawScoreTable(XGraphics gfx, Patrol patrol, List<Category> categories, double width, int startY)
        {
            XFont tableFont = new XFont("Arial", 14, XFontStyleEx.Regular);
            int startX = 50;
            int rowHeight = 30;
            int tableWidth = (int)width - 100;

            // Kolombreedtes
            int columnCategory = (int)(tableWidth * 0.30);
            int columnSmall = (int)(tableWidth * 0.12);
            int columnNormal = (int)(tableWidth * 0.18);

            // Kolomkoppen
            gfx.DrawString("Categorie", tableFont, XBrushes.Black, new XRect(startX, startY, columnCategory, rowHeight), XStringFormats.CenterLeft);
            gfx.DrawString("Punten", tableFont, XBrushes.Black, new XRect(startX + columnCategory, startY, columnSmall, rowHeight), XStringFormats.Center);
            gfx.DrawString("Max", tableFont, XBrushes.Black, new XRect(startX + columnCategory + columnSmall, startY, columnSmall, rowHeight), XStringFormats.Center);
            gfx.DrawString("Percentage", tableFont, XBrushes.Black, new XRect(startX + columnCategory + 2 * columnSmall, startY, columnNormal, rowHeight), XStringFormats.Center);
            gfx.DrawString("Weegfactor", tableFont, XBrushes.Black, new XRect(startX + columnCategory + 2 * columnSmall + columnNormal, startY, columnNormal, rowHeight), XStringFormats.Center);
            gfx.DrawString("Totaal", tableFont, XBrushes.Black, new XRect(startX + columnCategory + 2 * columnSmall + 2 * columnNormal, startY, columnSmall, rowHeight), XStringFormats.Center);

            int tableHeight = startY + rowHeight;
            int endX = startX + columnCategory + 2 * columnSmall + 2 * columnNormal + columnSmall;

            // Horizontale lijnen
            gfx.DrawLine(XPens.Black, startX, startY, endX, startY);
            for (int i = 0; i <= categories.Count + 1; i++)
            {
                gfx.DrawLine(XPens.Black, startX, tableHeight + (i * rowHeight), endX, tableHeight + (i * rowHeight));
            }

            // Verticale lijnen
            int xPos = startX + columnCategory;
            for (int i = 0; i < 5; i++)
            {
                gfx.DrawLine(XPens.Black, xPos, startY, xPos, tableHeight + (categories.Count * rowHeight));
                xPos += (i < 2) ? columnSmall : columnNormal;
            }

            // Data in de tabel
            for (int i = 0; i < categories.Count; i++)
            {
                int currentScore = await scoreCalculationService.CalculatePoints(patrol, categories[i]);
                decimal currentPercentage = await scoreCalculationService.CalculatePercentages(patrol, categories[i]);
                decimal currentTotalPercentage = await scoreCalculationService.CalculateTotalPercentages(patrol, categories[i]);
                int yPosition = tableHeight + (i * rowHeight);

                gfx.DrawString(categories[i].Name, tableFont, XBrushes.Black, new XRect(startX, yPosition, columnCategory, rowHeight), XStringFormats.CenterLeft);
                gfx.DrawString(currentScore.ToString(), tableFont, XBrushes.Black, new XRect(startX + columnCategory, yPosition, columnSmall, rowHeight), XStringFormats.Center);
                gfx.DrawString(categories[i].MaxScore.ToString(), tableFont, XBrushes.Black, new XRect(startX + columnCategory + columnSmall, yPosition, columnSmall, rowHeight), XStringFormats.Center);
                gfx.DrawString(currentPercentage.ToString("0.##") + "%", tableFont, XBrushes.Black, new XRect(startX + columnCategory + 2 * columnSmall, yPosition, columnNormal, rowHeight), XStringFormats.Center);
                gfx.DrawString(categories[i].Weight.ToString() + "%", tableFont, XBrushes.Black, new XRect(startX + columnCategory + 2 * columnSmall + columnNormal, yPosition, columnNormal, rowHeight), XStringFormats.Center);
                gfx.DrawString(currentTotalPercentage.ToString("0.##") + "%", tableFont, XBrushes.Black, new XRect(startX + columnCategory + 2 * columnSmall + 2 * columnNormal, yPosition, columnSmall, rowHeight), XStringFormats.Center);
            }
            // **Totaalberekening**
            decimal PatrolTotalPercentage = await scoreCalculationService.CalculateTotalPercentages(patrol, categories);
            gfx.DrawString("Totaal:", tableFont, XBrushes.Black, new XRect(startX + columnCategory + 2 * columnSmall, tableHeight + (categories.Count * rowHeight), columnNormal, rowHeight), XStringFormats.Center);
            gfx.DrawString(PatrolTotalPercentage.ToString("0.##") + "%", tableFont, XBrushes.Black, new XRect(startX + columnCategory + 2 * columnSmall + 2 * columnNormal, tableHeight + (categories.Count * rowHeight), columnSmall, rowHeight), XStringFormats.Center);
        }

        /// <summary>
        /// Voegt een footer toe aan de PDF.
        /// </summary>
        private void DrawFooter(XGraphics gfx, double width, double height)
        {
            XFont footerFont = new XFont("Arial", 10, XFontStyleEx.Italic);
            gfx.DrawString("Bedankt voor het leuke weekend, groetjes de organisatie!", footerFont, XBrushes.DarkGreen,
                new XRect(0, height - 50, width, 20), XStringFormats.Center);
        }

        // **4. Nieuwe functie voor het genereren van een PDF met een tabel van alle patrouilles**
        public async Task<byte[]> GeneratePatrolsTablePdfAsync(Edition edition)
        {
            await Task.Delay(20);
            using (var memoryStream = new MemoryStream())
            {
                PdfDocument document = new PdfDocument();
                if (Capabilities.Build.IsCoreBuild)
                    GlobalFontSettings.FontResolver = new FailsafeFontResolver();

                document.Info.Title = $"RSW overzicht {edition.RSWStartDate.Year.ToString()} - Patrouilles";

                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                var width = page.Width;
                var height = page.Height;

                // Sorteren op positie
                var sortedPatrols = edition.SubGroups.SelectMany(S => S.patrols).OrderBy(p => p.position).ToList();

                DrawLayout(gfx, width, height, $"RSW Score overzicht {edition.RSWStartDate.Year}");

                // Startpositie voor de tabel
                int tableStartY = 100;
                await DrawPatrolsTable(gfx, sortedPatrols, width, height, tableStartY);

                document.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }
        // Nieuwe functie voor het genereren van een PDF met categorie informatie**
        public async Task<byte[]> GeneratePdfByCategoryAsync(Category category, Edition edition)
        {
            await Task.Delay(20);
            using (var memoryStream = new MemoryStream())
            {
                PdfDocument document = new PdfDocument();
                if (Capabilities.Build.IsCoreBuild)
                    GlobalFontSettings.FontResolver = new FailsafeFontResolver();

                document.Info.Title = $"Score Formulieren";

                // Stel Landscape modus in
                PdfPage page = document.AddPage();
                page.Orientation = PageOrientation.Landscape;

                XGraphics gfx = XGraphics.FromPdfPage(page);
                var width = page.Width;
                var height = page.Height;

                string title = $"Scorelijsten RSW {edition.RSWStartDate.Year}";

                double CurrentY = DrawLayout(gfx, width, height, title);
                await DrawTables(gfx, document, width, height, CurrentY, title, category, edition.SubGroups.ToList(), edition);

                document.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }
        private async Task DrawTables(XGraphics gfx, PdfDocument document, double width, double height, double currentY, string title, Category category, List<SubGroup> subGroups, Edition edition)
        {
            double margin = 30;
            double topMargin = 80;
            double rowHeight = 20;
            double patrolColWidth = 40;
            double bottomMargin = 40;
            bool firstPage = true;

            foreach (SubGroup subGroup in subGroups)
            {
                PdfPage page;
                if (firstPage)
                {
                    page = gfx.PdfPage;
                    firstPage = false;
                }
                else
                {
                    page = document.AddPage();
                    page.Orientation = PageOrientation.Landscape;
                    gfx = XGraphics.FromPdfPage(page);
                    DrawLayout(gfx, width, height, title);
                }

                string code = await JurySlotRepository.GetCode(category, subGroup, edition);

                string url = _nav.BaseUri;
                url += "scoreform?code=";
                url += code;

                double startY = topMargin;
                AddIntroTextAndQRCode(gfx, width, height, margin, startY, url);
                startY += 150; // Ruimte voor QR-code en introductietekst

                // **Sorteer en groepeer subcategories op basis van ruimte**
                List<(SubCategory, double)> subCategoryHeights = new List<(SubCategory, double)>();

                foreach (SubCategory subCategory in category.SubCategories)
                {
                    int patrolCount = subGroup.patrols.Count();
                    double totalPatrolWidth = patrolCount * patrolColWidth;
                    double criteriaColWidth = width - (2 * margin) - totalPatrolWidth;
                    double estimatedHeight = (subCategory.criterias.Count + 3) * rowHeight; // Rijenhoogte

                    subCategoryHeights.Add((subCategory, estimatedHeight));
                }

                // **Slimme plaatsing van tabellen op de pagina**
                double availableHeight = height - bottomMargin - startY;
                List<(SubCategory, double)> currentPageTables = new List<(SubCategory, double)>();

                foreach (var (subCategory, tableHeight) in subCategoryHeights)
                {
                    if (tableHeight > availableHeight && currentPageTables.Count > 0)
                    {
                        // Als de volgende tabel niet past, print eerst de huidige set tabellen
                        DrawSubCategoryTables(gfx, width, height, margin, rowHeight, patrolColWidth, subGroup, currentPageTables, ref startY);

                        // Nieuwe pagina maken
                        PdfPage newPage = document.AddPage();
                        newPage.Orientation = PageOrientation.Landscape;
                        gfx = XGraphics.FromPdfPage(newPage);
                        DrawLayout(gfx, width, height, title);

                        startY = topMargin;
                        availableHeight = height - bottomMargin - startY;
                        currentPageTables.Clear();
                    }

                    currentPageTables.Add((subCategory, tableHeight));
                    availableHeight -= tableHeight;
                }

                // **Laatste groep tabellen printen als er nog iets over is**
                if (currentPageTables.Count > 0)
                {
                    DrawSubCategoryTables(gfx, width, height, margin, rowHeight, patrolColWidth, subGroup, currentPageTables, ref startY);
                }
            }
        }

        // **Helper-functie om tabellen te tekenen**
        private void DrawSubCategoryTables(XGraphics gfx, double width, double height, double margin, double rowHeight, double patrolColWidth, SubGroup subGroup, List<(SubCategory, double)> tables, ref double startY)
        {
            foreach (var (subCategory, _) in tables)
            {
                int patrolCount = subGroup.patrols.Count();
                double totalPatrolWidth = patrolCount * patrolColWidth;
                double criteriaColWidth = width - (2 * margin) - totalPatrolWidth;

                gfx.DrawString($"{subCategory.Name} - {subGroup.Color}",
                    new XFont("Arial", 14, XFontStyleEx.Bold),
                    XBrushes.Black, new XPoint(margin, startY));
                startY += rowHeight;

                double[] columnX = new double[patrolCount + 2];
                columnX[0] = margin;
                columnX[1] = margin + criteriaColWidth;

                for (int i = 2; i < columnX.Length; i++)
                {
                    columnX[i] = columnX[i - 1] + patrolColWidth;
                }

                gfx.DrawLine(XPens.Black, margin, startY, width - margin, startY);

                gfx.DrawString("Criteria",
                    new XFont("Arial", 10, XFontStyleEx.Bold),
                    XBrushes.Black, new XPoint(columnX[0] + 5, startY + rowHeight - 5));

                for (int i = 0; i < patrolCount; i++)
                {
                    gfx.DrawString($"P{subGroup.patrols[i].Number}",
                        new XFont("Arial", 10, XFontStyleEx.Bold),
                        XBrushes.Black, new XPoint(columnX[i + 1] + 5, startY + rowHeight - 5));
                }

                startY += rowHeight;
                gfx.DrawLine(XPens.Black, margin, startY, width - margin, startY);

                foreach (Criteria criteria in subCategory.criterias)
                {
                    double currentY = startY + rowHeight;

                    gfx.DrawString(criteria.Description,
                        new XFont("Arial", 10),
                        XBrushes.Black, new XPoint(columnX[0] + 5, startY + rowHeight - 5));

                    for (int i = 1; i < columnX.Length - 1; i++)
                    {
                        gfx.DrawString("", new XFont("Arial", 10),
                            XBrushes.Black, new XPoint(columnX[i] + 5, startY + rowHeight - 5));
                    }

                    gfx.DrawLine(XPens.Black, margin, currentY, width - margin, currentY);
                    startY += rowHeight;
                }

                foreach (double x in columnX)
                {
                    gfx.DrawLine(XPens.Black, x, startY - (rowHeight * (subCategory.criterias.Count + 1)), x, startY);
                }

                startY += rowHeight;
            }
        }


        // Method to add intro text and a QR code with ZXing.Net
        public void AddIntroTextAndQRCode(XGraphics gfx, double width, double height, double margin, double startY, string url)
        {
            // **Lettertype instellen**
            var fontNormal = new XFont("Arial", 12);
            var fontBold = new XFont("Arial", 12, XFontStyleEx.Bold);

            // **Tekstblok instellen met vaste breedte voor tekstterugloop**
            double textWidth = width - 200; // Beperkt de breedte zodat de tekst niet buiten de pagina valt
            double textHeight = 0; // Wordt bepaald door de tekst
            XRect textRect = new XRect(margin, startY, textWidth, height);

            string introText = "Welkom juryleden!\n" +
                               "Om de beoordelingen correct vast te leggen, vragen we jullie vriendelijk om de QR-code " +
                               "hiernaast te scannen en het scoreformulier online in te vullen. " +
                               "Dit helpt ons bij een snelle en nauwkeurige verwerking van de resultaten.\n" +
                               "Alvast bedankt voor jullie inzet!";

            // **Tekst met terugloop tekenen**
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.DrawString(introText, fontNormal, XBrushes.Black, textRect, XStringFormats.TopLeft);

            // **Hoogte van de tekst bepalen**
            textHeight = gfx.MeasureString(introText, fontNormal).Height + 80; // Extra marge

            // **QR-code genereren en rechts naast de tekst plaatsen**
            var barcodeWriter = new BarcodeWriter<Image<Rgba32>>()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 150,
                    Height = 150,
                    Margin = 0
                },
                Renderer = new ImageSharpRenderer<Rgba32>()
            };

            using (var qrImage = barcodeWriter.Write(url))
            {
                using (var ms = new MemoryStream())
                {
                    qrImage.SaveAsPng(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    var xImage = XImage.FromStream(ms);

                    // **QR-code rechts naast de tekst plaatsen**
                    gfx.DrawImage(xImage, width - 160, startY);
                }
            }

            // **Nieuwe Y-positie onder de tekst en QR-code voor de naamvelden**
            double newY = startY + textHeight;

            // **Lijnen voor juryleden**
            gfx.DrawString("Naam Jury 1:", fontBold, XBrushes.Black, new XPoint(margin, newY));
            gfx.DrawLine(XPens.Black, margin + 100, newY + 3, margin + 300, newY + 3); // Lijn voor naam

            newY += 30;
            gfx.DrawString("Naam Jury 2:", fontBold, XBrushes.Black, new XPoint(margin, newY));
            gfx.DrawLine(XPens.Black, margin + 100, newY + 3, margin + 300, newY + 3); // Lijn voor naam
        }


        // **5. Functie voor het tekenen van de patrouilles-tabel**
        private async Task DrawPatrolsTable(XGraphics gfx, List<Patrol> patrols, double width, double height, int startY)
        {
            XFont tableFont = new XFont("Arial", 12, XFontStyleEx.Regular);
            int startX = 50;
            int rowHeight = ((int)height - 150) / patrols.Count;
            int tableWidth = (int)width - 100;

            // Kolombreedtes
            int columnPosition = (int)(tableWidth * 0.05); // smalle kolom voor positie
            int columnNumber = (int)(tableWidth * 0.05); // smalle kolom voor nummer
            int columnName = (int)(tableWidth * 0.30); // brede kolom voor naam
            int columnGroup = (int)(tableWidth * 0.20); // smalle kolom voor groep
            int columnAssociation = (int)(tableWidth * 0.30); // smalle kolom voor vereniging
            int columnDisqualified = (int)(tableWidth * 0.05); // smalle kolom voor gedisqualificeerd
            int columnYoungest = (int)(tableWidth * 0.05); // smalle kolom voor jongste

            // Kolomkoppen
            gfx.DrawString("Pos.", tableFont, XBrushes.Black, new XRect(startX, startY, columnPosition, rowHeight), XStringFormats.Center);
            gfx.DrawString("Nr.", tableFont, XBrushes.Black, new XRect(startX + columnPosition, startY, columnNumber, rowHeight), XStringFormats.Center);
            gfx.DrawString("Naam", tableFont, XBrushes.Black, new XRect(startX + columnPosition + columnNumber, startY, columnName, rowHeight), XStringFormats.Center);
            gfx.DrawString("Groep", tableFont, XBrushes.Black, new XRect(startX + columnPosition + columnNumber + columnName, startY, columnGroup, rowHeight), XStringFormats.Center);
            gfx.DrawString("Vereniging", tableFont, XBrushes.Black, new XRect(startX + columnPosition + columnNumber + columnName + columnGroup, startY, columnAssociation, rowHeight), XStringFormats.Center);
            gfx.DrawString("Dis.*", tableFont, XBrushes.Black, new XRect(startX + columnPosition + columnNumber + columnName + columnGroup + columnAssociation, startY, columnDisqualified, rowHeight), XStringFormats.Center);
            gfx.DrawString("Y**", tableFont, XBrushes.Black, new XRect(startX + columnPosition + columnNumber + columnName + columnGroup + columnAssociation + columnDisqualified, startY, columnYoungest, rowHeight), XStringFormats.Center);

            // Teken horizontale lijn
            gfx.DrawLine(XPens.Black, startX, startY + rowHeight, startX + columnPosition + columnNumber + columnName + columnGroup + columnAssociation + columnDisqualified + columnYoungest, startY + rowHeight);

            int tableHeight = startY + rowHeight;
            int endX = startX + columnPosition + columnNumber + columnName + columnGroup + columnAssociation + columnDisqualified + columnYoungest;

            // Loop door alle patrouilles om de rijen toe te voegen
            foreach (var patrol in patrols)
            {
                int yPosition = tableHeight;
                Group group = await groupRepository.GetAsync(patrol.GroupId);

                // Positie, Nummer, Naam, Groep, Vereniging
                gfx.DrawString(patrol.position.ToString(), tableFont, XBrushes.Black, new XRect(startX, yPosition, columnPosition, rowHeight), XStringFormats.Center);
                gfx.DrawString(patrol.Number.ToString(), tableFont, XBrushes.Black, new XRect(startX + columnPosition, yPosition, columnNumber, rowHeight), XStringFormats.Center);
                gfx.DrawString($"{patrol.Name}", tableFont, XBrushes.Black, new XRect(startX + columnPosition + columnNumber, yPosition, columnName, rowHeight), XStringFormats.Center);
                gfx.DrawString(group.Name, tableFont, XBrushes.Black, new XRect(startX + columnPosition + columnNumber + columnName, yPosition, columnGroup, rowHeight), XStringFormats.Center);
                gfx.DrawString(group.Association.Name, tableFont, XBrushes.Black, new XRect(startX + columnPosition + columnNumber + columnName + columnGroup, yPosition, columnAssociation, rowHeight), XStringFormats.Center);

                // Kleuren van de cellen afhankelijk van boolean waardes
                //XBrush disqualifiedColor = patrol.IsDisqualified ? XBrushes.Red : XBrushes.Transparent;
                //XBrush youngestColor = patrol.IsYoungest ? XBrushes.LightBlue : XBrushes.Transparent;

                //// Teken achtergrondkleur
                //gfx.DrawRectangle(disqualifiedColor, startX + columnPosition + columnNumber + columnName + columnGroup + columnAssociation, yPosition, columnDisqualified, rowHeight);
                //gfx.DrawRectangle(youngestColor, startX + columnPosition + columnNumber + columnName + columnGroup + columnAssociation + columnDisqualified, yPosition, columnYoungest, rowHeight);

                // Teken een rood bolletje als IsDisqualified true is
                if (patrol.IsDisqualified)
                {
                    double centerX = startX + columnPosition + columnNumber + columnName + columnGroup + columnAssociation + (columnDisqualified / 2);
                    double centerY = yPosition + (rowHeight / 2);
                    double radius = rowHeight * 0.3; // Maak de cirkel een beetje kleiner dan de rijhoogte

                    gfx.DrawEllipse(XBrushes.Red, centerX - radius, centerY - radius, radius * 2, radius * 2);
                }

                // Teken een groen bolletje als IsYoungest true is
                if (patrol.IsYoungest)
                {
                    double centerX = startX + columnPosition + columnNumber + columnName + columnGroup + columnAssociation + columnDisqualified + (columnYoungest / 2);
                    double centerY = yPosition + (rowHeight / 2);
                    double radius = rowHeight * 0.3;

                    gfx.DrawEllipse(XBrushes.Green, centerX - radius, centerY - radius, radius * 2, radius * 2);
                }


                tableHeight += rowHeight;
            }

            // Teken horizontale lijn onderaan de tabel
            gfx.DrawLine(XPens.Black, startX, tableHeight, endX, tableHeight);
        }

    }
}
