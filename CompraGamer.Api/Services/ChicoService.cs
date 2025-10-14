using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompraGamer.Api.Models;

namespace CompraGamer.Api.Services
{
    public class ChicoService : IChicoService
    {
        // key: dni (char(8))
        private readonly ConcurrentDictionary<string, Chico> _store = new();

        public ChicoService()
        {
            var c1 = new Chico { Dni = "11111111", Nombre = "Juan", Apellido = "Perez", MicroPatente = null };
            var c2 = new Chico { Dni = "22222222", Nombre = "Ana", Apellido = "Gomez", MicroPatente = null };
            _store[c1.Dni] = c1;
            _store[c2.Dni] = c2;
        }

        public Task<Chico> CreateAsync(Chico item)
        {
            // Expect caller to set Dni (as in SQL schema)
            if (string.IsNullOrWhiteSpace(item.Dni)) throw new ArgumentException("Dni is required");
            _store[item.Dni] = item;
            return Task.FromResult(item);
        }

        public Task<bool> DeleteAsync(string id)
        {
            return Task.FromResult(_store.TryRemove(id, out _));
        }

        public Task<IEnumerable<Chico>> GetAllAsync()
        {
            return Task.FromResult(_store.Values.AsEnumerable());
        }

        public Task<Chico?> GetByIdAsync(string id)
        {
            _store.TryGetValue(id, out var c);
            return Task.FromResult(c);
        }

        public Task<bool> UpdateAsync(Chico item)
        {
            if (string.IsNullOrWhiteSpace(item.Dni)) return Task.FromResult(false);
            if (!_store.ContainsKey(item.Dni)) return Task.FromResult(false);
            _store[item.Dni] = item;
            return Task.FromResult(true);
        }
    }
}
