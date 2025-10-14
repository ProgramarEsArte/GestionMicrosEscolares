using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompraGamer.Api.Models;

namespace CompraGamer.Api.Services
{
    public interface IMicroEscolarService
    {
        Task<IEnumerable<MicroEscolar>> GetAllAsync();
        Task<MicroEscolar?> GetByIdAsync(string id);
        Task<MicroEscolar> CreateAsync(MicroEscolar item);
        Task<bool> UpdateAsync(MicroEscolar item);
        Task<bool> DeleteAsync(string id);
    }
}
