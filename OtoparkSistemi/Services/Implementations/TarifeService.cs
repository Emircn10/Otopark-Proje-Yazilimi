using Microsoft.EntityFrameworkCore;
using OtoparkSistemi.Data;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;

namespace OtoparkSistemi.Services.Implementations
{
    public class TarifeService : ITarifeService
    {
        private readonly AppDbContext _context;
        public TarifeService(AppDbContext context) => _context = context;

        public async Task<List<Tarife>> GetAllAsync() =>
            await _context.Tarifeler.OrderBy(t => t.AracTipi).ToListAsync();

        public async Task<Tarife?> GetByIdAsync(int id) =>
            await _context.Tarifeler.FindAsync(id);

        public async Task<Tarife?> GetGecerliTarifeAsync(string aracTipi) =>
            await _context.Tarifeler
                .Where(t => t.AracTipi == aracTipi
                    && t.GecerlilikBaslangic <= DateTime.Now
                    && (t.GecerlilikBitis == null || t.GecerlilikBitis >= DateTime.Now))
                .OrderByDescending(t => t.GecerlilikBaslangic)
                .FirstOrDefaultAsync();

        public async Task<Tarife> CreateAsync(Tarife tarife)
        {
            _context.Tarifeler.Add(tarife);
            await _context.SaveChangesAsync();
            return tarife;
        }

        public async Task<Tarife> UpdateAsync(Tarife tarife)
        {
            _context.Tarifeler.Update(tarife);
            await _context.SaveChangesAsync();
            return tarife;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tarife = await _context.Tarifeler.FindAsync(id);
            if (tarife == null) return false;
            _context.Tarifeler.Remove(tarife);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
