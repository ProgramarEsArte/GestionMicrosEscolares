using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompraGamer.Api.Models;

namespace CompraGamer.Api.Services
{
    public class MicroEscolarService : IMicroEscolarService
    {
        // key: patente (char(7))
        private readonly ConcurrentDictionary<string, MicroEscolar> _store = new();

        public MicroEscolarService()
        {
            var m1 = new MicroEscolar { Patente = "ABC1234", ChoferDni = null };
            _store[m1.Patente] = m1;
        }

        public Task<MicroEscolar> CreateAsync(MicroEscolar item)
        {
            if (string.IsNullOrWhiteSpace(item.Patente)) throw new ArgumentException("Patente required");
            _store[item.Patente] = item;
            return Task.FromResult(item);
        }

        public Task<bool> DeleteAsync(string id)
        {
            return Task.FromResult(_store.TryRemove(id, out _));
        }

        public Task<IEnumerable<MicroEscolar>> GetAllAsync()
        {
            return Task.FromResult(_store.Values.AsEnumerable());
        }

        public Task<MicroEscolar?> GetByIdAsync(string id)
        {
            _store.TryGetValue(id, out var item);
            return Task.FromResult(item);
        }

        public Task<bool> UpdateAsync(MicroEscolar item)
        {
            if (string.IsNullOrWhiteSpace(item.Patente)) return Task.FromResult(false);
            if (!_store.ContainsKey(item.Patente)) return Task.FromResult(false);
            _store[item.Patente] = item;
            return Task.FromResult(true);
        }
    }
}
