using System.Threading.Tasks;
using CompraGamer.Api.Data;
using CompraGamer.Api.Models;
using CompraGamer.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CompraGamer.Api.Tests
{
    public class ChicoServiceTests
    {
        private GestionMicrosContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<GestionMicrosContext>()
                .UseInMemoryDatabase(databaseName: "TestChicoDb")
                .Options;
            return new GestionMicrosContext(options);
        }

        [Fact]
        public async Task CreateReadUpdateDelete_Chico_CRUD()
        {
            var db = CreateDbContext();
            var service = new EfChicoService(db);

            var chico = new Chico { Dni = "80000000", Nombre = "Kid", Apellido = "Test", MicroPatente = null };
            await service.CreateAsync(chico);

            var fetched = await service.GetByIdAsync("80000000");
            Assert.NotNull(fetched);
            Assert.Equal("Kid", fetched!.Nombre);

            chico.Nombre = "KidUpdated";
            var ok = await service.UpdateAsync(chico);
            Assert.True(ok);

            var updated = await service.GetByIdAsync("80000000");
            Assert.Equal("KidUpdated", updated!.Nombre);

            var deleted = await service.DeleteAsync("80000000");
            Assert.True(deleted);

            var afterDelete = await service.GetByIdAsync("80000000");
            Assert.Null(afterDelete);
        }
    }
}
