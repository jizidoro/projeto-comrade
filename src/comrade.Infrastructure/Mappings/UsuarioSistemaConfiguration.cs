#region

using comrade.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace comrade.Infrastructure.Mappings
{
    public class UsuarioSistemaConfiguration : IEntityTypeConfiguration<UsuarioSistema>
    {
        public void Configure(EntityTypeBuilder<UsuarioSistema> builder)
        {
            builder.Property(b => b.Id).HasColumnName("USSI_SQ_USUARIO_SISTEMA").IsRequired();
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Email).HasDatabaseName("IX_USUARIO_SISTEMA_EMAIL").IsUnique();
            builder.HasIndex(c => c.Matricula).HasDatabaseName("IX_USUARIO_SISTEMA_MATRICULA").IsUnique();
        }
    }
}