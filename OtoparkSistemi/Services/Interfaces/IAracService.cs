using OtoparkSistemi.Models;

namespace OtoparkSistemi.Services.Interfaces
{
    public interface IAracService
    {
        Task<List<Arac>> GetAllAsync(string? arama = null, string? tip = null);
        Task<Arac?> GetByIdAsync(int id);
        Task<Arac?> GetByPlakaAsync(string plaka);
        Task<Arac> CreateAsync(Arac arac);
        Task<Arac> UpdateAsync(Arac arac);
        Task<bool> DeleteAsync(int id);
        Task<bool> PlakaExistsAsync(string plaka, int? excludeId = null);
    }
}
