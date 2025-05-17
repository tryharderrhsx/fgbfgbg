using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InternetTenderService.Data;
using InternetTenderService.Models;
using System.Linq;

namespace InternetTenderService.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _context;

        public RegisterModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Имя пользователя и пароль обязательны.";
                return Page();
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Username == Username);
            if (existingUser != null)
            {
                ErrorMessage = "Пользователь с таким именем уже существует.";
                return Page();
            }

            var user = new User
            {
                Username = Username,
                PasswordHash = Password // В реальности — хэшировать!
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);

            return RedirectToPage("/Tenders/Index");
        }
    }
}
