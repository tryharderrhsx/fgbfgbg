using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InternetTenderService.Data;
using InternetTenderService.Models;
using Microsoft.EntityFrameworkCore;

namespace InternetTenderService.Pages.Tenders;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _db;
    public DeleteModel(AppDbContext db) => _db = db;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public Tender Tender { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToPage("/Account/Login");

        Tender = await _db.Tenders.FirstOrDefaultAsync(t => t.Id == Id);
        if (Tender == null) return RedirectToPage("Index");
        if (Tender.OwnerId != userId) return Forbid();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToPage("/Account/Login");

        Tender = await _db.Tenders.FirstOrDefaultAsync(t => t.Id == Id);
        if (Tender == null) return RedirectToPage("Index");
        if (Tender.OwnerId != userId) return Forbid();

        _db.Tenders.Remove(Tender);
        await _db.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}
