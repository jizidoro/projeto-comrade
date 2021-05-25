#region

using comrade.Domain.Models.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace comrade.Infrastructure.Mappings.Views
{
    public class VwUsuarioSistemaPermissaoConfiguration : IEntityTypeConfiguration<VwUsuarioSistemaPermissao>
    {
        public void Configure(EntityTypeBuilder<VwUsuarioSistemaPermissao> builder)
        {
            builder.ToView("VW_USSP_USUARIO_SISTEMA_PERMISSAO").HasNoKey();

            builder.Property(b => b.SqUsuarioSistema).HasColumnName("USSI_SQ_USUARIO_SISTEMA");
        }
    }
}