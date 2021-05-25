#region

using System.Linq;
using comrade.Core.Views.VBaUsuPermissaoCore;
using comrade.Domain.Models.Views;
using comrade.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

#endregion

namespace comrade.Infrastructure.Repositories.Views
{
    public class VwUsuarioSistemaPermissaoRepository : IVwUsuarioSistemaPermissaoRepository
    {
        protected readonly ComradeContext Db;
        protected readonly DbSet<VwUsuarioSistemaPermissao> DbSet;

        public VwUsuarioSistemaPermissaoRepository(ComradeContext context)
        {
            Db = context;
            DbSet = Db.Set<VwUsuarioSistemaPermissao>();
        }


        public IQueryable<VwUsuarioSistemaPermissao> ListarPorSqUsuarioSistema(int sqUsuarioSistema)
        {
            var permissoes = Db.VUsuarioSistemaPermissoes
                .AsQueryable();

            return permissoes;
        }
    }
}