using OtoparkSistemi.Models;

namespace OtoparkSistemi.Services.Interfaces
{
    public interface IAbonelikService
    {
        Task<List<Abonelik>> GetAllAsync(bool? aktifMi = null);
        Task<Abonelik?> GetByIdAsync(int id);
        Task<Abonelik?> GetAktifAbonelikByAracIdAsync(int aracId);
        Task<Abonelik> CreateAsync(Abonelik abonelik);
        Task<Abonelik> UpdateAsync(Abonelik abonelik);
        Task GuncelleAbonelikDurumlariniAsync();
    }
}
