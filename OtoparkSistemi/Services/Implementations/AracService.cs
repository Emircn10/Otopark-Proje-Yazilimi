using Microsoft.EntityFrameworkCore;
using OtoparkSistemi.Data;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Services.Implementations
{
    public class AracService : IAracService
    {
        private readonly AppDbContext _context;
        public AracService(AppDbContext context) => _context = context;

        public async Task<List<Arac>> GetAllAsync(string? arama = null, string? tip = null)
        {
            var query = _context.Araclar.AsQueryable();
            if (!string.IsNullOrWhiteSpace(arama))
                query = query.Where(a => a.Plaka.Contains(arama) || (a.SahibiAdi != null && a.SahibiAdi.Contains(arama)));
            if (!string.IsNullOrWhiteSpace(tip))
                query = query.Where(a => a.AracTipi == tip);
            return await query.OrderByDescending(a => a.OlusturmaTarihi).ToListAsync();
        }

        public async Task<Arac?> GetByIdAsync(int id) =>
            await _context.Araclar.Include(a => a.Abonelikler).Include(a => a.GirisKayitlari).FirstOrDefaultAsync(a => a.AracId == id);

        public async Task<Arac?> GetByPlakaAsync(string plaka) =>
            await _context.Araclar.FirstOrDefaultAsync(a => a.Plaka.ToUpper() == plaka.ToUpper());

        public async Task<Arac> CreateAsync(Arac arac)
        {
            arac.Plaka = arac.Plaka.ToUpper().Trim();
            arac.OlusturmaTarihi = DateTime.Now;
            _context.Araclar.Add(arac);
            await _context.SaveChangesAsync();
            return arac;
        }

        public async Task<Arac> UpdateAsync(Arac arac)
        {
            arac.Plaka = arac.Plaka.ToUpper().Trim();
            _context.Araclar.Update(arac);
            await _context.SaveChangesAsync();
            return arac;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var arac = await _context.Araclar
                .Include(a => a.Abonelikler)
                .Include(a => a.GirisKayitlari)
                .FirstOrDefaultAsync(a => a.AracId == id);

            if (arac == null) return false;

            // Önce bağlı kayıtları sil (Restrict delete nedeniyle)
            _context.GirisKayitlari.RemoveRange(arac.GirisKayitlari);
            _context.Abonelikler.RemoveRange(arac.Abonelikler);
            _context.Araclar.Remove(arac);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PlakaExistsAsync(string plaka, int? excludeId = null)
        {
            var query = _context.Araclar.Where(a => a.Plaka.ToUpper() == plaka.ToUpper());
            if (excludeId.HasValue) query = query.Where(a => a.AracId != excludeId.Value);
            return await query.AnyAsync();
        }
    }
}
