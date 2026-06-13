using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Pages.Pricing
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ITarifeService _tarifeService;
        public List<Tarife> Tarifeler { get; set; } = new();

        public IndexModel(ITarifeService tarifeService) => _tarifeService = tarifeService;

        public async Task OnGetAsync() => Tarifeler = await _tarifeService.GetAllAsync();
    }
}
