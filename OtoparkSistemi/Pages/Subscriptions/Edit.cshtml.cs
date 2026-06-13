using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Subscriptions
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IAbonelikService _abonelikService;
        [BindProperty] public Abonelik Abonelik { get; set; } = new();

        public EditModel(IAbonelikService abonelikService) => _abonelikService = abonelikService;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var abonelik = await _abonelikService.GetByIdAsync(id);
            if (abonelik == null) return NotFound();
            Abonelik = abonelik;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _abonelikService.UpdateAsync(Abonelik);
            TempData["Basari"] = "Abonelik güncellendi.";
            return RedirectToPage("/Subscriptions/Index");
        }
    }
}
