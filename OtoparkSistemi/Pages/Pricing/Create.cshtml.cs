using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Pricing
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ITarifeService _tarifeService;
        [BindProperty] public Tarife Tarife { get; set; } = new();

        public CreateModel(ITarifeService tarifeService) => _tarifeService = tarifeService;

        public void OnGet() { Tarife.GecerlilikBaslangic = DateTime.Today; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _tarifeService.CreateAsync(Tarife);
            TempData["Basari"] = "Tarife oluşturuldu.";
            return RedirectToPage("/Pricing/Index");
        }
    }
}
