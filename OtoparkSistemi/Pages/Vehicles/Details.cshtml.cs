using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Vehicles
{
    public class DetailsModel : PageModel
    {
        private readonly IAracService _aracService;
        public Arac? Arac { get; set; }

        public DetailsModel(IAracService aracService) => _aracService = aracService;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Arac = await _aracService.GetByIdAsync(id);
            if (Arac == null) return NotFound();
            return Page();
        }
    }
}
