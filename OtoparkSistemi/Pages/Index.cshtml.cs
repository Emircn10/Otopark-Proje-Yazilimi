using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Services.Interfaces;
using OtoparkSistemi.ViewModels;

namespace OtoparkSistemi.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IGirisKayitService _kayitService;
        private readonly IAbonelikService _abonelikService;

        public DashboardViewModel Vm { get; set; } = new();

        public IndexModel(IGirisKayitService kayitService, IAbonelikService abonelikService)
        {
            _kayitService = kayitService;
            _abonelikService = abonelikService;
        }

        public async Task OnGetAsync()
        {
            await _abonelikService.GuncelleAbonelikDurumlariniAsync();

            var sonKayitlar = await _kayitService.GetAllAsync();
            var aktif = await _kayitService.GetAktifParklar();

            Vm = new DashboardViewModel
            {
                OtoparkdakiAracSayisi = aktif.Count,
                BugunkuGelir = await _kayitService.GetBugunkuGelirAsync(),
                AktifAbonelikSayisi = (await _abonelikService.GetAllAsync(true)).Count,
                ToplamKapasite = 50,
                SonKayitlar = sonKayitlar.Take(10).Select(k => new SonKayitViewModel
                {
                    KayitId = k.KayitId,
                    Plaka = k.Arac?.Plaka ?? "-",
                    AracTipi = k.Arac?.AracTipi ?? "-",
                    GirisTarihi = k.GirisTarihi,
                    CikisTarihi = k.CikisTarihi,
                    OdenenUcret = k.OdenenUcret,
                    OdemeDurumu = k.OdemeDurumu
                }).ToList()
            };
        }
    }
}
