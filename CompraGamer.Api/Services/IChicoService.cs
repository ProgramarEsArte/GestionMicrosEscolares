using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompraGamer.Api.Models;

namespace CompraGamer.Api.Services
{
    public interface IChicoService
    {
        Task<IEnumerable<Chico>> GetAllAsync();
        Task<Chico?> GetByIdAsync(string id);
        Task<Chico> CreateAsync(Chico item);
        Task<bool> UpdateAsync(Chico item);
        Task<bool> DeleteAsync(string id);
    }
}
