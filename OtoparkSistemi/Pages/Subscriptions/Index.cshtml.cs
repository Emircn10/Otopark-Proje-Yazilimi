using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Subscriptions
{
    public class IndexModel : PageModel
    {
        private readonly IAbonelikService _abonelikService;
        public List<Abonelik> Abonelikler { get; set; } = new();
        public string Filtre { get; set; } = "aktif";

        public IndexModel(IAbonelikService abonelikService) => _abonelikService = abonelikService;

        public async Task OnGetAsync(string? filtre)
        {
            Filtre = filtre ?? "aktif";
            await _abonelikService.GuncelleAbonelikDurumlariniAsync();
            Abonelikler = await _abonelikService.GetAllAsync(Filtre != "pasif");
        }
    }
}
