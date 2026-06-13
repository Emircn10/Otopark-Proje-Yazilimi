using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OtoparkSistemi.Pages.Parking
{
    [Authorize]
    public class WalkInModel : PageModel
    {
        private readonly IAracService _aracService;
        private readonly IGirisKayitService _kayitService;

        [BindProperty, Required(ErrorMessage = "Plaka zorunludur.")]
        public string Plaka { get; set; } = string.Empty;

        [BindProperty, Required(ErrorMessage = "Araç tipi zorunludur.")]
        public string AracTipi { get; set; } = string.Empty;

        [BindProperty] public string? SahibiAdi { get; set; }
        [BindProperty] public string? SahibiTelefon { get; set; }

        public string? Mesaj { get; set; }
        public string? MesajTuru { get; set; }

        public WalkInModel(IAracService aracService, IGirisKayitService kayitService)
        {
            _aracService = aracService;
            _kayitService = kayitService;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Plaka temizle
            Plaka = Plaka.Trim().ToUpper().Replace(" ", "");

            // Araç kayıtlı mı?
            var arac = await _aracService.GetByPlakaAsync(Plaka);

            if (arac == null)
            {
                // Ziyaretçi kaydı oluştur
                arac = new Arac
                {
                    Plaka = Plaka,
                    AracTipi = AracTipi,
                    SahibiAdi = SahibiAdi ?? "Ziyaretçi",
                    SahibiTelefon = SahibiTelefon,
                    OlusturmaTarihi = DateTime.Now
                };
                await _aracService.CreateAsync(arac);
            }

            // Zaten içeride mi?
            var aktifKayit = await _kayitService.GetAktifKayitByAracIdAsync(arac.AracId);
            if (aktifKayit != null)
            {
                MesajTuru = "warning";
                Mesaj = $"{arac.Plaka} zaten otoparkta! (Kayıt #{aktifKayit.KayitId})";
                return Page();
            }

            await _kayitService.AracGirisAsync(arac.AracId, ziyaretciMi: true);
            TempData["Basari"] = $"Ziyaretçi {arac.Plaka} girişi kaydedildi.";
            return RedirectToPage("/Parking/Index");
        }
    }
}
