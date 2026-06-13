using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Subscriptions
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IAbonelikService _abonelikService;
        private readonly IAracService _aracService;

        [BindProperty] public Abonelik Abonelik { get; set; } = new();
        public List<SelectListItem> AracListesi { get; set; } = new();

        public CreateModel(IAbonelikService abonelikService, IAracService aracService)
        {
            _abonelikService = abonelikService;
            _aracService = aracService;
        }

        public async Task OnGetAsync()
        {
            var araclar = await _aracService.GetAllAsync();
            AracListesi = araclar.Select(a => new SelectListItem { Value = a.AracId.ToString(), Text = $"{a.Plaka} – {a.SahibiAdi}" }).ToList();
            Abonelik.BaslangicTarihi = DateTime.Today;
            Abonelik.BitisTarihi = DateTime.Today.AddMonths(1);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var araclar = await _aracService.GetAllAsync();
                AracListesi = araclar.Select(a => new SelectListItem { Value = a.AracId.ToString(), Text = $"{a.Plaka} – {a.SahibiAdi}" }).ToList();
                return Page();
            }
            await _abonelikService.CreateAsync(Abonelik);
            TempData["Basari"] = "Abonelik başarıyla oluşturuldu.";
            return RedirectToPage("/Subscriptions/Index");
        }
    }
}
