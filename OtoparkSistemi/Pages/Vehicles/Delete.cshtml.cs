using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Vehicles
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly IAracService _aracService;
        public Arac? Arac { get; set; }

        public DeleteModel(IAracService aracService) => _aracService = aracService;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Arac = await _aracService.GetByIdAsync(id);
            if (Arac == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var arac = await _aracService.GetByIdAsync(id);
            var plaka = arac?.Plaka ?? "";
            await _aracService.DeleteAsync(id);
            TempData["Basari"] = $"{plaka} plakalı araç silindi.";
            return RedirectToPage("/Vehicles/Index");
        }
    }
}
