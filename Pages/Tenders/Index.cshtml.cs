using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InternetTenderService.Data;
using InternetTenderService.Models;

namespace InternetTenderService.Pages.Tenders;

public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public IndexModel(AppDbContext db) => _db = db;

    public List<Tender> Tenders { get; set; }

    public async Task OnGet()
    {
        Tenders = await _db.Tenders.Include(t => t.Owner).ToListAsync();
    }
}
