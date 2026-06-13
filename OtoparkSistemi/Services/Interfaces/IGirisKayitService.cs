using OtoparkSistemi.Models;
using OtoparkSistemi.ViewModels;

namespace OtoparkSistemi.Services.Interfaces
{
    public interface IGirisKayitService
    {
        Task<List<GirisKayit>> GetAllAsync(DateTime? baslangic = null, DateTime? bitis = null);
        Task<List<GirisKayit>> GetAktifParklar();
        Task<GirisKayit?> GetByIdAsync(int id);
        Task<GirisKayit?> GetAktifKayitByAracIdAsync(int aracId);
        Task<GirisKayit> AracGirisAsync(int aracId, bool ziyaretciMi = false);
        Task<GirisKayit> AracCikisAsync(int kayitId);
        Task<decimal> HesaplaUcretAsync(int aracId, DateTime giris, DateTime cikis);
        Task<decimal> GetBugunkuGelirAsync();
        Task<int> GetOtoparkdakiSayiAsync();
    }
}
