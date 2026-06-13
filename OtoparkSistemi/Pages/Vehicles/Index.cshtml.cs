using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Vehicles
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAracService _aracService;
        public List<Arac> Araclar { get; set; } = new();
        public string? Arama { get; set; }
        public string? Tip { get; set; }

        public IndexModel(IAracService aracService) => _aracService = aracService;

        public async Task OnGetAsync(string? arama, string? tip)
        {
            Arama = arama; Tip = tip;
            Araclar = await _aracService.GetAllAsync(arama, tip);
        }
    }
}
