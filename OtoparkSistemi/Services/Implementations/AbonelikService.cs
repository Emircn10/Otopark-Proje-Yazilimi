using Microsoft.EntityFrameworkCore;
using OtoparkSistemi.Data;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Services.Implementations
{
    public class AbonelikService : IAbonelikService
    {
        private readonly AppDbContext _context;
        public AbonelikService(AppDbContext context) => _context = context;

        public async Task<List<Abonelik>> GetAllAsync(bool? aktifMi = null)
        {
            var query = _context.Abonelikler.Include(a => a.Arac).AsQueryable();
            if (aktifMi.HasValue) query = query.Where(a => a.AktifMi == aktifMi.Value);
            return await query.OrderByDescending(a => a.BaslangicTarihi).ToListAsync();
        }

        public async Task<Abonelik?> GetByIdAsync(int id) =>
            await _context.Abonelikler.Include(a => a.Arac).FirstOrDefaultAsync(a => a.AbonelikId == id);

        public async Task<Abonelik?> GetAktifAbonelikByAracIdAsync(int aracId) =>
            await _context.Abonelikler
                .Where(a => a.AracId == aracId && a.AktifMi && a.BitisTarihi >= DateTime.Now)
                .FirstOrDefaultAsync();

        public async Task<Abonelik> CreateAsync(Abonelik abonelik)
        {
            abonelik.AktifMi = abonelik.BitisTarihi >= DateTime.Now;
            _context.Abonelikler.Add(abonelik);
            await _context.SaveChangesAsync();
            return abonelik;
        }

        public async Task<Abonelik> UpdateAsync(Abonelik abonelik)
        {
            // AktifMi'yi kullanıcının seçimine bırak, otomatik override yok
            _context.Abonelikler.Update(abonelik);
            await _context.SaveChangesAsync();
            return abonelik;
        }

        public async Task GuncelleAbonelikDurumlariniAsync()
        {
            var surecenler = await _context.Abonelikler
                .Where(a => a.AktifMi && a.BitisTarihi < DateTime.Now)
                .ToListAsync();
            foreach (var a in surecenler) a.AktifMi = false;
            await _context.SaveChangesAsync();
        }
    }
}
