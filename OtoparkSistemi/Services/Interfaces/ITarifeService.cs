using OtoparkSistemi.Models;

namespace OtoparkSistemi.Services.Interfaces
{
    public interface ITarifeService
    {
        Task<List<Tarife>> GetAllAsync();
        Task<Tarife?> GetByIdAsync(int id);
        Task<Tarife?> GetGecerliTarifeAsync(string aracTipi);
        Task<Tarife> CreateAsync(Tarife tarife);
        Task<Tarife> UpdateAsync(Tarife tarife);
        Task<bool> DeleteAsync(int id);
    }
}
