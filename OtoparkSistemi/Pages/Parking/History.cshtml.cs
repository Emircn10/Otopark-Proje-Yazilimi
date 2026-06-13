using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Parking
{
    public class HistoryModel : PageModel
    {
        private readonly IGirisKayitService _kayitService;
        public List<GirisKayit> Kayitlar { get; set; } = new();
        public DateTime? Baslangic { get; set; }
        public DateTime? Bitis { get; set; }

        public HistoryModel(IGirisKayitService kayitService) => _kayitService = kayitService;

        public async Task OnGetAsync(DateTime? baslangic, DateTime? bitis)
        {
            Baslangic = baslangic;
            Bitis = bitis;
            Kayitlar = await _kayitService.GetAllAsync(baslangic, bitis);
        }
    }
}
