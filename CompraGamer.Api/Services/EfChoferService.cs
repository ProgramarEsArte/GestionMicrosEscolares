using System.Collections.Generic;
using System.Threading.Tasks;
using CompraGamer.Api.Data;
using CompraGamer.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CompraGamer.Api.Services
{
    public class EfChoferService : IChoferService
    {
        private readonly GestionMicrosContext _db;
        public EfChoferService(GestionMicrosContext db) => _db = db;

        public async Task<Chofer> CreateAsync(Chofer item)
        {
            _db.Choferes.Add(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var f = await _db.Choferes.FindAsync(id);
            if (f == null) return false;
            _db.Choferes.Remove(f);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Chofer>> GetAllAsync() => await _db.Choferes.ToListAsync();

        public async Task<Chofer?> GetByIdAsync(string id) => await _db.Choferes.FindAsync(id);

        public async Task<bool> UpdateAsync(Chofer item)
        {
            var exists = await _db.Choferes.AnyAsync(c => c.Dni == item.Dni);
            if (!exists) return false;
            _db.Choferes.Update(item);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
