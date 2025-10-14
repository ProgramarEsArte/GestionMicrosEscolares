using System.Threading.Tasks;
using CompraGamer.Api.Data;
using CompraGamer.Api.Models;
using CompraGamer.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CompraGamer.Api.Tests
{
    public class ChoferServiceTests
    {
        private GestionMicrosContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<GestionMicrosContext>()
                .UseInMemoryDatabase(databaseName: "TestChoferDb")
                .Options;
            var db = new GestionMicrosContext(options);
            return db;
        }

        [Fact]
        public async Task CreateReadUpdateDelete_Chofer_CRUD()
        {
            var db = CreateDbContext();
            var service = new EfChoferService(db);

            var chofer = new Chofer { Dni = "90000000", Nombre = "Test", Apellido = "Driver" };
            await service.CreateAsync(chofer);

            var fetched = await service.GetByIdAsync("90000000");
            Assert.NotNull(fetched);
            Assert.Equal("Test", fetched!.Nombre);

            chofer.Nombre = "Updated";
            var ok = await service.UpdateAsync(chofer);
            Assert.True(ok);

            var updated = await service.GetByIdAsync("90000000");
            Assert.Equal("Updated", updated!.Nombre);

            var deleted = await service.DeleteAsync("90000000");
            Assert.True(deleted);

            var afterDelete = await service.GetByIdAsync("90000000");
            Assert.Null(afterDelete);
        }
    }
}
