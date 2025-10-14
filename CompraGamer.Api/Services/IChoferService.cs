using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompraGamer.Api.Models;

namespace CompraGamer.Api.Services
{
    public interface IChoferService
    {
        Task<IEnumerable<Chofer>> GetAllAsync();
        Task<Chofer?> GetByIdAsync(string id);
        Task<Chofer> CreateAsync(Chofer item);
        Task<bool> UpdateAsync(Chofer item);
        Task<bool> DeleteAsync(string id);
    }
}
