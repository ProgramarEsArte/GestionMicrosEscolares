using System.Collections.Generic;
using System.Threading.Tasks;
using CompraGamer.Api.Data;
using CompraGamer.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CompraGamer.Api.Services
{
    public class EfMicroEscolarService : IMicroEscolarService
    {
        private readonly GestionMicrosContext _db;
        private readonly ILogger<EfMicroEscolarService> _logger;

        public EfMicroEscolarService(GestionMicrosContext db, ILogger<EfMicroEscolarService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<MicroEscolar> CreateAsync(MicroEscolar item)
        {
            _db.Microescolares.Add(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var m = await _db.Microescolares.FindAsync(id);
            if (m == null) return false;
            _db.Microescolares.Remove(m);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MicroEscolar>> GetAllAsync()
        {
            _logger.LogInformation("Obteniendo lista de micros con conteo de chicos");
            var micros = await _db.Microescolares.ToListAsync();
            
            foreach (var micro in micros)
            {
                var count = await _db.Chicos.CountAsync(c => c.MicroPatente == micro.Patente);
                micro.CantidadChicos = count;
                _logger.LogInformation("Micro {Patente} tiene {Count} chicos", micro.Patente, count);
            }
            
            return micros;
        }

        public async Task<MicroEscolar?> GetByIdAsync(string id) => await _db.Microescolares.FindAsync(id);

        public async Task<bool> UpdateAsync(MicroEscolar item)
        {
            var exists = await _db.Microescolares.AnyAsync(m => m.Patente == item.Patente);
            if (!exists) return false;
            _db.Microescolares.Update(item);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
