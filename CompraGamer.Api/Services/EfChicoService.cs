using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompraGamer.Api.Data;
using CompraGamer.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CompraGamer.Api.Services
{
    public class EfChicoService : IChicoService
    {
        private readonly GestionMicrosContext _db;
        public EfChicoService(GestionMicrosContext db) => _db = db;

        public async Task<Chico> CreateAsync(Chico item)
        {
            _db.Chicos.Add(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var c = await _db.Chicos.FindAsync(id);
            if (c == null) return false;
            _db.Chicos.Remove(c);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Chico>> GetAllAsync() => await _db.Chicos.ToListAsync();

        public async Task<Chico?> GetByIdAsync(string id) => await _db.Chicos.FindAsync(id);

        public async Task<bool> UpdateAsync(Chico item)
        {
            var exists = await _db.Chicos.AnyAsync(c => c.Dni == item.Dni);
            if (!exists) return false;
            _db.Chicos.Update(item);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
