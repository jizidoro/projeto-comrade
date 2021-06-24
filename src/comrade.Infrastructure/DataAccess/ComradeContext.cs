#region

using comrade.Domain.Models;
using comrade.Domain.Models.Views;
using comrade.Infrastructure.Mappings;
using comrade.Infrastructure.Mappings.Views;
using Microsoft.EntityFrameworkCore;

#endregion

namespace comrade.Infrastructure.DataAccess
{
    public class ComradeContext : DbContext
    {
        private const string JsonPath = "comrade.Infrastructure.SeedData";

        public ComradeContext(DbContextOptions<ComradeContext> options)
            : base(options)
        {
        }

        // Tabelas
        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<UsuarioSistema> UsuarioSistemas { get; set; }

        // Views
        public DbSet<VwUsuarioSistemaPermissao> VUsuarioSistemaPermissoes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tabelas
            modelBuilder.ApplyConfiguration(new AirplaneConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioSistemaConfiguration());

            // Views
            modelBuilder.ApplyConfiguration(new VwUsuarioSistemaPermissaoConfiguration());
        }
    }
}