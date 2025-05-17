using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InternetTenderService.Data;
using InternetTenderService.Models;
using Microsoft.EntityFrameworkCore;

namespace InternetTenderService.Pages.Tenders;

public class EditModel : PageModel
{
    private readonly AppDbContext _db;
    public EditModel(AppDbContext db) => _db = db;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
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

        var tenderToUpdate = await _db.Tenders.FirstOrDefaultAsync(t => t.Id == Id);
        if (tenderToUpdate == null) return RedirectToPage("Index");
        if (tenderToUpdate.OwnerId != userId) return Forbid();

        if (!ModelState.IsValid) return Page();

        tenderToUpdate.Title = Tender.Title;
        tenderToUpdate.Description = Tender.Description;
        await _db.SaveChangesAsync();

        return RedirectToPage("Details", new { id = Id });
    }
}