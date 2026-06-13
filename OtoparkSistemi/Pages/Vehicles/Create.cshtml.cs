using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Vehicles
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IAracService _aracService;
        [BindProperty] public Arac Arac { get; set; } = new();

        public CreateModel(IAracService aracService) => _aracService = aracService;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (await _aracService.PlakaExistsAsync(Arac.Plaka))
            {
                ModelState.AddModelError("Arac.Plaka", "Bu plaka zaten kayıtlı.");
                return Page();
            }

            await _aracService.CreateAsync(Arac);
            TempData["Basari"] = $"{Arac.Plaka} plakalı araç başarıyla eklendi.";
            return RedirectToPage("/Vehicles/Index");
        }
    }
}
