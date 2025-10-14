using Microsoft.EntityFrameworkCore;
using CompraGamer.Api.Models;

namespace CompraGamer.Api.Data
{
    public class GestionMicrosContext : DbContext
    {
        public GestionMicrosContext(DbContextOptions<GestionMicrosContext> options) : base(options)
        {
        }

        public DbSet<Chico> Chicos { get; set; } = null!;
        public DbSet<Chofer> Choferes { get; set; } = null!;
        public DbSet<MicroEscolar> Microescolares { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tablas y claves según script.sql
            modelBuilder.Entity<Chico>(eb =>
            {
                eb.ToTable("chico");
                eb.HasKey(c => c.Dni);
                eb.Property(c => c.Dni).HasColumnName("dni").HasMaxLength(8).IsFixedLength();
                eb.Property(c => c.Nombre).HasColumnName("nombre").HasMaxLength(50);
                eb.Property(c => c.Apellido).HasColumnName("apellido").HasMaxLength(50);
                eb.Property(c => c.MicroPatente).HasColumnName("micro_patente").HasMaxLength(7).IsFixedLength();
                eb.HasOne<MicroEscolar>()
                  .WithMany()
                  .HasForeignKey(c => c.MicroPatente)
                  .HasConstraintName("chico_ibfk_1");
            });

            modelBuilder.Entity<Chofer>(eb =>
            {
                eb.ToTable("chofer");
                eb.HasKey(c => c.Dni);
                eb.Property(c => c.Dni).HasColumnName("dni").HasMaxLength(8).IsFixedLength();
                eb.Property(c => c.Nombre).HasColumnName("nombre").HasMaxLength(50);
                eb.Property(c => c.Apellido).HasColumnName("apellido").HasMaxLength(50);
            });

            modelBuilder.Entity<MicroEscolar>(eb =>
            {
                eb.ToTable("microescolar");
                eb.HasKey(m => m.Patente);
                eb.Property(m => m.Patente).HasColumnName("patente").HasMaxLength(7).IsFixedLength();
                eb.Property(m => m.ChoferDni).HasColumnName("chofer_dni").HasMaxLength(8).IsFixedLength();
                eb.HasOne<Chofer>()
                  .WithMany()
                  .HasForeignKey(m => m.ChoferDni)
                  .HasConstraintName("microescolar_ibfk_1");
            });
        }
    }
}
