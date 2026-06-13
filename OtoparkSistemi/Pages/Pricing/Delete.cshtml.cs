using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Pricing
{
    public class DeleteModel : PageModel
    {
        private readonly ITarifeService _tarifeService;
        public Tarife? Tarife { get; set; }

        public DeleteModel(ITarifeService tarifeService) => _tarifeService = tarifeService;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Tarife = await _tarifeService.GetByIdAsync(id);
            if (Tarife == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _tarifeService.DeleteAsync(id);
            TempData["Basari"] = "Tarife silindi.";
            return RedirectToPage("/Pricing/Index");
        }
    }
}
