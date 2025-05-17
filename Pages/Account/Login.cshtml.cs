using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InternetTenderService.Data;
using InternetTenderService.Models;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace InternetTenderService.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _db;
        public LoginModel(AppDbContext db) => _db = db;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            // Хеширование введённого пароля
            var hash = Convert.ToBase64String(
                SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Password))
            );

            // Ищем пользователя
            var user = _db.Users
                .FirstOrDefault(u => u.Username == Username && u.PasswordHash == hash);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
                return Page();
            }

            // Сохраняем UserId и Username в сессии
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);

    return RedirectToPage("/Tenders/Index");
        }
    }
}
