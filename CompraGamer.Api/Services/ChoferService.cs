using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompraGamer.Api.Models;

namespace CompraGamer.Api.Services
{
    public class ChoferService : IChoferService
    {
        private readonly ConcurrentDictionary<string, Chofer> _store = new();

        public ChoferService()
        {
            var c1 = new Chofer { Dni = "33333333", Nombre = "Pedro", Apellido = "Lopez" };
            _store[c1.Dni] = c1;
        }

        public Task<Chofer> CreateAsync(Chofer item)
        {
            if (string.IsNullOrWhiteSpace(item.Dni)) throw new ArgumentException("Dni required");
            _store[item.Dni] = item;
            return Task.FromResult(item);
        }

        public Task<bool> DeleteAsync(string id)
        {
            return Task.FromResult(_store.TryRemove(id, out _));
        }

        public Task<IEnumerable<Chofer>> GetAllAsync()
        {
            return Task.FromResult(_store.Values.AsEnumerable());
        }

        public Task<Chofer?> GetByIdAsync(string id)
        {
            _store.TryGetValue(id, out var item);
            return Task.FromResult(item);
        }

        public Task<bool> UpdateAsync(Chofer item)
        {
            if (string.IsNullOrWhiteSpace(item.Dni)) return Task.FromResult(false);
            if (!_store.ContainsKey(item.Dni)) return Task.FromResult(false);
            _store[item.Dni] = item;
            return Task.FromResult(true);
        }
    }
}
