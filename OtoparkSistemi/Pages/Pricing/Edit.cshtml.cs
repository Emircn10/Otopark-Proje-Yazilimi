using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Pricing
{
    public class EditModel : PageModel
    {
        private readonly ITarifeService _tarifeService;
        [BindProperty] public Tarife Tarife { get; set; } = new();

        public EditModel(ITarifeService tarifeService) => _tarifeService = tarifeService;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var tarife = await _tarifeService.GetByIdAsync(id);
            if (tarife == null) return NotFound();
            Tarife = tarife;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _tarifeService.UpdateAsync(Tarife);
            TempData["Basari"] = "Tarife güncellendi.";
            return RedirectToPage("/Pricing/Index");
        }
    }
}
