using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InternetTenderService.Data;
using InternetTenderService.Models;

namespace InternetTenderService.Pages.Tenders;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _db;
    public DetailsModel(AppDbContext db) => _db = db;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public Tender Tender { get; set; }

    [BindProperty]
    public decimal BidAmount { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Tender = await _db.Tenders
            .Include(t => t.Bids)
                .ThenInclude(b => b.Bidder)
            .FirstOrDefaultAsync(t => t.Id == Id);

        if (Tender == null)
            return RedirectToPage("Index");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToPage("/Account/Login");

        Tender = await _db.Tenders
            .Include(t => t.Bids)
            .FirstOrDefaultAsync(t => t.Id == Id);
        if (Tender == null)
            return RedirectToPage("Index");

        if (Tender.OwnerId == userId)
        {
            ModelState.AddModelError(string.Empty, "Владелец не может подавать заявку на свой тендер.");
            return Page();
        }

        if (BidAmount <= 0)
        {
            ModelState.AddModelError(nameof(BidAmount), "Сумма должна быть больше нуля.");
            return Page();
        }

        var bid = new Bid
        {
            Amount = BidAmount,
            PlacedAt = DateTime.UtcNow,
            TenderId = Id,
            BidderId = userId.Value
        };
        _db.Bids.Add(bid);
        await _db.SaveChangesAsync();

        return RedirectToPage();
    }
}
