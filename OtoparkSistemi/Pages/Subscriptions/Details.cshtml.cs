using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Subscriptions
{
    public class DetailsModel : PageModel
    {
        private readonly IAbonelikService _abonelikService;
        public Abonelik? Abonelik { get; set; }

        public DetailsModel(IAbonelikService abonelikService) => _abonelikService = abonelikService;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Abonelik = await _abonelikService.GetByIdAsync(id);
            if (Abonelik == null) return NotFound();
            return Page();
        }
    }
}
