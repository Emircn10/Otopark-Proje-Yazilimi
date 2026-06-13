using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Parking
{
    [Authorize]
    public class EntryModel : PageModel
    {
        private readonly IAracService _aracService;
        private readonly IGirisKayitService _kayitService;
        private readonly IAbonelikService _abonelikService;

        [BindProperty, Required(ErrorMessage = "Plaka zorunludur.")]
        public string Plaka { get; set; } = string.Empty;

        public string? Mesaj { get; set; }
        public string? MesajTuru { get; set; }
        public bool AbonelikVar { get; set; }
        public List<string> KayitliPlakalar { get; set; } = new();

        public EntryModel(IAracService aracService, IGirisKayitService kayitService, IAbonelikService abonelikService)
        {
            _aracService = aracService;
            _kayitService = kayitService;
            _abonelikService = abonelikService;
        }

        public async Task OnGetAsync()
        {
            var araclar = await _aracService.GetAllAsync();
            KayitliPlakalar = araclar.Select(a => a.Plaka).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var araclar = await _aracService.GetAllAsync();
            KayitliPlakalar = araclar.Select(a => a.Plaka).ToList();

            if (!ModelState.IsValid) return Page();

            var arac = await _aracService.GetByPlakaAsync(Plaka);
            if (arac == null)
            {
                MesajTuru = "danger";
                Mesaj = $"'{Plaka}' plakalı araç kayıtlı değil. Kayıtlı olmayan araçlar için Ziyaretçi Girişi kullanın.";
                return Page();
            }

            var aktifKayit = await _kayitService.GetAktifKayitByAracIdAsync(arac.AracId);
            if (aktifKayit != null)
            {
                MesajTuru = "warning";
                Mesaj = $"{arac.Plaka} zaten otoparkta! (Kayıt #{aktifKayit.KayitId})";
                return Page();
            }

            var abonelik = await _abonelikService.GetAktifAbonelikByAracIdAsync(arac.AracId);
            AbonelikVar = abonelik != null;

            await _kayitService.AracGirisAsync(arac.AracId);
            TempData["Basari"] = $"{arac.Plaka} girişi kaydedildi.{(AbonelikVar ? " (Aktif abonelik – ücretsiz)" : "")}";
            return RedirectToPage("/Parking/Index");
        }
    }
}
