#region

using System;
using System.Linq;
using comrade.Core.UsuarioSistemaCore;
using comrade.Domain.Bases;
using comrade.Domain.Models;
using comrade.Infrastructure.Bases;
using comrade.Infrastructure.DataAccess;

#endregion

namespace comrade.Infrastructure.Repositories
{
    public class UsuarioSistemaRepository : Repository<UsuarioSistema>, IUsuarioSistemaRepository
    {
        private readonly ComradeContext _context;

        public UsuarioSistemaRepository(ComradeContext context)
            : base(context)
        {
            _context = context ??
                       throw new ArgumentNullException(nameof(context));
        }


        public IQueryable<LookupEntity> BuscarPorNome(string nome)
        {
            var result = Db.UsuarioSistemas
                .Where(x => x.Situacao &&
                            x.Nome.Contains(nome)).Take(30)
                .OrderBy(x => x.Nome)
                .Select(s => new LookupEntity {Key = s.Id, Value = s.Nome});

            return result;
        }
    }
}