using System.Threading.Tasks;
using CompraGamer.Api.Data;
using CompraGamer.Api.Models;
using CompraGamer.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CompraGamer.Api.Tests
{
    public class MicroEscolarServiceTests
    {
        private GestionMicrosContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<GestionMicrosContext>()
                .UseInMemoryDatabase(databaseName: "TestMicroDb")
                .Options;
            return new GestionMicrosContext(options);
        }

        [Fact]
        public async Task CreateReadUpdateDelete_Micro_CRUD()
        {
            var db = CreateDbContext();
            var service = new EfMicroEscolarService(db);

            var micro = new MicroEscolar { Patente = "TTT1111", ChoferDni = null };
            await service.CreateAsync(micro);

            var fetched = await service.GetByIdAsync("TTT1111");
            Assert.NotNull(fetched);
            Assert.Equal("TTT1111", fetched!.Patente);

            micro.ChoferDni = "90000000";
            var ok = await service.UpdateAsync(micro);
            Assert.True(ok);

            var updated = await service.GetByIdAsync("TTT1111");
            Assert.Equal("90000000", updated!.ChoferDni);

            var deleted = await service.DeleteAsync("TTT1111");
            Assert.True(deleted);

            var afterDelete = await service.GetByIdAsync("TTT1111");
            Assert.Null(afterDelete);
        }
    }
}
