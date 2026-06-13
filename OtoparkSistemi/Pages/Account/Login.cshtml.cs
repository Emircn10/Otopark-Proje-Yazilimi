using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using System.ComponentModel.DataAnnotations;

namespace OtoparkSistemi.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        [BindProperty, Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        public string KullaniciAdi { get; set; } = string.Empty;

        [BindProperty, Required(ErrorMessage = "Şifre zorunludur.")]
        public string Sifre { get; set; } = string.Empty;

        public string? HataMesaji { get; set; }

        public LoginModel(SignInManager<ApplicationUser> signInManager)
            => _signInManager = signInManager;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            if (!ModelState.IsValid) return Page();

            var result = await _signInManager.PasswordSignInAsync(KullaniciAdi, Sifre, false, false);
            if (result.Succeeded)
                return LocalRedirect(returnUrl ?? "/");

            HataMesaji = "Kullanıcı adı veya şifre hatalı.";
            return Page();
        }
    }
}
