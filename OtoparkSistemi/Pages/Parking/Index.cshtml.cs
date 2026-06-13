using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Parking
{
    public class IndexModel : PageModel
    {
        private readonly IGirisKayitService _kayitService;
        public List<GirisKayit> AktifParklar { get; set; } = new();
        public List<GirisKayit> TumKayitlar { get; set; } = new();
        public string Tab { get; set; } = "aktif";

        public IndexModel(IGirisKayitService kayitService) => _kayitService = kayitService;

        public async Task OnGetAsync(string? tab)
        {
            Tab = tab ?? "aktif";
            AktifParklar = await _kayitService.GetAktifParklar();
            TumKayitlar = await _kayitService.GetAllAsync();
        }
    }
}
