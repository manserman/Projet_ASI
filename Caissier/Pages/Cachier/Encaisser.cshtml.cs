using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Data;
using ProjetASI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Mime;
using System.IO;
using Microsoft.AspNetCore.SignalR;
using ProjetASI.Hubs;

namespace ProjetASI.Pages.Cachier
{
    public class EncaisserModel : PageModel
    {
        [BindProperty]
        public Commande commande { get; set; }
        [BindProperty]
        public int sommedejapayee { get; set; } = 0;
        private readonly IHubContext<CommandeHub> _hubContext;
        private readonly DBContext _context;
        public EncaisserModel(DBContext context, IHubContext<CommandeHub> hubcontext)
        {
            _context = context;
            _hubContext = hubcontext;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            commande =await _context.Commandes.FirstOrDefaultAsync(cde => cde.ID == id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            commande = await _context.Commandes.FirstOrDefaultAsync(cde => cde.ID == id);
            commande.isPaid = true;
            _context.Attach(commande).State = EntityState.Modified;
            var table = _context.Tables.FirstOrDefault(t => t.ID == commande.tableID);
            table.occuppe = false;
            _context.Attach(table).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("CommandePayee");
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostFactureAsync(int id)
        {
        
                var commande = _context.Commandes
                    .Include(c => c.Articles)
                    .ThenInclude(lgn => lgn.article)
                    .FirstOrDefault(c => c.ID == id);

                if (commande == null)
                {
                    return NotFound();
                }

                var document = new Document(PageSize.A4, 50, 50, 25, 25);
            var stream = new MemoryStream();
                var writer = PdfWriter.GetInstance(document, stream);

                document.Open();

                // Ajouter le titre et la date de la facture
                var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
                var subTitleFont= FontFactory.GetFont("Arial", 14, Font.BOLD);
               var title = new Paragraph($"Facture de la commande n°{commande.ID}", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);
                document.Add(new Paragraph($"Date d'édition: {DateTime.Now.ToShortDateString()}\n\n"));

                // Ajouter les détails de la commande
                var infoFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);
                document.Add(new Paragraph($"Date de commande: {commande.datecomm.ToShortDateString()}", infoFont));
            // Ajouter les articles commandés
            document.Add(new Paragraph("Articles:", subTitleFont));
            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            PdfPCell cell1 = new PdfPCell(new Phrase("Nom de l'article"));
            PdfPCell cell2 = new PdfPCell(new Phrase("Quantité"));
            PdfPCell cell3 = new PdfPCell(new Phrase("Prix"));
            PdfPCell cell4 = new PdfPCell(new Phrase("TVA"));

            table.AddCell(cell1);
            table.AddCell(cell2);
            table.AddCell(cell3);
            table.AddCell(cell4);

            foreach (var article in commande.Articles)
            {
                table.AddCell(article.article.nom);
                table.AddCell(article.quantite.ToString());
                table.AddCell(article.prix.ToString());
                table.AddCell((article.prix*0.2/1.2).ToString());
            }

          

            document.Add(table); // ajouter la table dans le document PDF

            document.Add(new Paragraph($"Prix total: {commande.prix}", subTitleFont));
            document.Add(new Paragraph($"TVA Total: {commande.prix * 0.2 / 1.2}", subTitleFont));
            document.Close();

            var contentDisposition = new ContentDisposition
            {
                FileName = $"Facture_Commande_{commande.ID}.pdf",
                Inline = false
            };

            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return File(stream.ToArray(), "application/pdf");
        }
    }
    }

