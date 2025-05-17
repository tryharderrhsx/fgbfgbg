using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InternetTenderService.Data;
using InternetTenderService.Models;

namespace InternetTenderService.Pages.Tenders;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public CreateModel(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [BindProperty]
    public Tender Tender { get; set; } = new Tender();

    [BindProperty]
    public IFormFile? Image { get; set; }

public async Task<IActionResult> OnPostAsync()
{
    if (!ModelState.IsValid)
    {
        ModelState.AddModelError("", "Модель невалидна");
        return Page();
    }

    if (Image != null && Image.Length > 0)
    {
        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
        Directory.CreateDirectory(uploadsFolder); // на всякий случай

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await Image.CopyToAsync(stream);
        }

        Tender.ImagePath = "/uploads/" + fileName;
    }

    Tender.CreatedAt = DateTime.UtcNow;
    Tender.OwnerId = HttpContext.Session.GetInt32("UserId") ?? 0;

    _context.Tenders.Add(Tender);
    await _context.SaveChangesAsync();

    return RedirectToPage("Index");
}
}
