using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Vehicles
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IAracService _aracService;
        [BindProperty] public Arac Arac { get; set; } = new();

        public EditModel(IAracService aracService) => _aracService = aracService;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var arac = await _aracService.GetByIdAsync(id);
            if (arac == null) return NotFound();
            Arac = arac;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            if (await _aracService.PlakaExistsAsync(Arac.Plaka, Arac.AracId))
            {
                ModelState.AddModelError("Arac.Plaka", "Bu plaka başka bir araçta kayıtlı.");
                return Page();
            }
            await _aracService.UpdateAsync(Arac);
            TempData["Basari"] = "Araç bilgileri güncellendi.";
            return RedirectToPage("/Vehicles/Index");
        }
    }
}
