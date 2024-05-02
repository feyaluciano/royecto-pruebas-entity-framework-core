using Microsoft.EntityFrameworkCore;
using ProyectoPruebasEntityFrameworkCore.Models;

namespace ProyectoPruebasEntityFrameworkCore
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
        modelBuilder.Entity<Persona>()
                .HasOne(p => p.Provincia)
                .WithMany() 
                .HasForeignKey(p => p.ProvinciaId);       
        }

        public DbSet<Provincia> Provincias => Set<Provincia>();
        public DbSet<Persona> Persona=> Set<Persona>();

    }
}
