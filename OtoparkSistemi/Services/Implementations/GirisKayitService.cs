using Microsoft.EntityFrameworkCore;
using OtoparkSistemi.Data;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Services.Implementations
{
    public class GirisKayitService : IGirisKayitService
    {
        private readonly AppDbContext _context;
        private readonly IAbonelikService _abonelikService;
        private readonly ITarifeService _tarifeService;

        public GirisKayitService(AppDbContext context, IAbonelikService abonelikService, ITarifeService tarifeService)
        {
            _context = context;
            _abonelikService = abonelikService;
            _tarifeService = tarifeService;
        }

        public async Task<List<GirisKayit>> GetAllAsync(DateTime? baslangic = null, DateTime? bitis = null)
        {
            var query = _context.GirisKayitlari.Include(g => g.Arac).AsQueryable();
            if (baslangic.HasValue) query = query.Where(g => g.GirisTarihi >= baslangic.Value);
            if (bitis.HasValue) query = query.Where(g => g.GirisTarihi <= bitis.Value.AddDays(1));
            return await query.OrderByDescending(g => g.GirisTarihi).ToListAsync();
        }

        public async Task<List<GirisKayit>> GetAktifParklar() =>
            await _context.GirisKayitlari.Include(g => g.Arac)
                .Where(g => g.CikisTarihi == null)
                .OrderByDescending(g => g.GirisTarihi)
                .ToListAsync();

        public async Task<GirisKayit?> GetByIdAsync(int id) =>
            await _context.GirisKayitlari.Include(g => g.Arac).FirstOrDefaultAsync(g => g.KayitId == id);

        public async Task<GirisKayit?> GetAktifKayitByAracIdAsync(int aracId) =>
            await _context.GirisKayitlari
                .Where(g => g.AracId == aracId && g.CikisTarihi == null)
                .FirstOrDefaultAsync();

        public async Task<GirisKayit> AracGirisAsync(int aracId, bool ziyaretciMi = false)
        {
            var abonelik = await _abonelikService.GetAktifAbonelikByAracIdAsync(aracId);
            var kayit = new GirisKayit
            {
                AracId = aracId,
                GirisTarihi = DateTime.Now,
                AbonelikKullanildiMi = abonelik != null && !ziyaretciMi,
                ZiyaretciMi = ziyaretciMi,
                OdemeDurumu = "Bekliyor"
            };
            _context.GirisKayitlari.Add(kayit);
            await _context.SaveChangesAsync();
            return kayit;
        }

        public async Task<GirisKayit> AracCikisAsync(int kayitId)
        {
            var kayit = await _context.GirisKayitlari.Include(g => g.Arac).FirstOrDefaultAsync(g => g.KayitId == kayitId)
                        ?? throw new InvalidOperationException("Kayıt bulunamadı.");

            kayit.CikisTarihi = DateTime.Now;
            var sure = kayit.CikisTarihi.Value - kayit.GirisTarihi;
            kayit.ToplamSure = (int)sure.TotalMinutes;

            if (kayit.AbonelikKullanildiMi)
            {
                kayit.OdenenUcret = 0;
                kayit.OdemeDurumu = "Abonelik";
            }
            else
            {
                kayit.OdenenUcret = await HesaplaUcretAsync(kayit.AracId, kayit.GirisTarihi, kayit.CikisTarihi.Value);
                kayit.OdemeDurumu = "Ödendi";
            }

            await _context.SaveChangesAsync();
            return kayit;
        }

        public async Task<decimal> HesaplaUcretAsync(int aracId, DateTime giris, DateTime cikis)
        {
            var arac = await _context.Araclar.FindAsync(aracId);
            if (arac == null) return 0;

            var tarife = await _tarifeService.GetGecerliTarifeAsync(arac.AracTipi);
            if (tarife == null) return 0;

            // Başlayan saate göre yukarı yuvarla (60 dakika = 1 saat, 61 dakika = 2 saat)
            var dakika = (cikis - giris).TotalMinutes;
            var saatler = (int)Math.Ceiling(dakika / 60.0);
            if (saatler < 1) saatler = 1; // minimum 1 saat

            decimal ucret;

            if (saatler <= 1)
            {
                // 0-1 saat: sabit ücret
                ucret = tarife.Ilk1SaatUcret;
            }
            else if (saatler <= 3)
            {
                // 1-3 saat: ilk saat + sonraki saatler
                ucret = tarife.Ilk1SaatUcret + (saatler - 1) * tarife.Saat1_3Ucret;
            }
            else
            {
                // 3+ saat: ilk saat + 2 saat (1-3 arası) + kalan saatler
                ucret = tarife.Ilk1SaatUcret + 2 * tarife.Saat1_3Ucret + (saatler - 3) * tarife.Saat3PlusSaatlik;
            }

            return Math.Min(Math.Round(ucret, 2), tarife.GunlukMaksimum);
        }

        public async Task<decimal> GetBugunkuGelirAsync()
        {
            var bugun = DateTime.Today;
            return await _context.GirisKayitlari
                .Where(g => g.CikisTarihi.HasValue && g.CikisTarihi.Value.Date == bugun && g.OdenenUcret.HasValue)
                .SumAsync(g => g.OdenenUcret!.Value);
        }

        public async Task<int> GetOtoparkdakiSayiAsync() =>
            await _context.GirisKayitlari.CountAsync(g => g.CikisTarihi == null);
    }
}
