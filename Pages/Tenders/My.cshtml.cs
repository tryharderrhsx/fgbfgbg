using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InternetTenderService.Data;
using InternetTenderService.Models;

namespace InternetTenderService.Pages.Tenders
{
    public class MyModel : PageModel
    {
        private readonly AppDbContext _db;
        public MyModel(AppDbContext db) => _db = db;

        public List<Tender> MyTenders { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToPage("/Account/Login");

            MyTenders = await _db.Tenders
                .Where(t => t.OwnerId == userId.Value)
                .ToListAsync();

            return Page();
        }
    }
}
