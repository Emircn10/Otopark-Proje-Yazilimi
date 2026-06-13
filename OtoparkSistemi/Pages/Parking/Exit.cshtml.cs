using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Services.Interfaces;
using OtoparkSistemi.ViewModels;

namespace OtoparkSistemi.Pages.Parking
{
    public class ExitModel : PageModel
    {
        private readonly IGirisKayitService _kayitService;
        public CikisViewModel? Vm { get; set; }
        [BindProperty] public int KayitId { get; set; }

        public ExitModel(IGirisKayitService kayitService) => _kayitService = kayitService;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var kayit = await _kayitService.GetByIdAsync(id);
            if (kayit == null || kayit.CikisTarihi != null) return NotFound();

            var sure = (int)(DateTime.Now - kayit.GirisTarihi).TotalMinutes;
            var ucret = await _kayitService.HesaplaUcretAsync(kayit.AracId, kayit.GirisTarihi, DateTime.Now);

            KayitId = id;
            Vm = new CikisViewModel
            {
                KayitId = id,
                Plaka = kayit.Arac?.Plaka ?? "",
                AracTipi = kayit.Arac?.AracTipi ?? "",
                GirisTarihi = kayit.GirisTarihi,
                ToplamSureDakika = sure,
                HesaplananUcret = ucret,
                AbonelikKullanildi = kayit.AbonelikKullanildiMi
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var kayit = await _kayitService.AracCikisAsync(KayitId);
            TempData["Basari"] = $"{kayit.Arac?.Plaka} çıkışı tamamlandı. Ücret: ₺{kayit.OdenenUcret?.ToString("N2")}";
            return RedirectToPage("/Parking/Index");
        }
    }
}
