#region

using comrade.Infrastructure.DataAccess;
using comrade.UnitTests.Helpers;
using comrade.WebApi.UseCases.V1.UsuarioSistemaApi;

#endregion

namespace comrade.UnitTests.Tests.UsuarioSistemaTests.Bases
{
    public class UsuarioSistemaInjectionController
    {
        private readonly UsuarioSistemaInjectionAppService _usuarioSistemaInjectionAppService = new();

        public UsuarioSistemaController ObterUsuarioSistemaController(ComradeContext context)
        {
            var mapper = MapperHelper.ConfigMapper();
            var usuarioSistemaAppService =
                _usuarioSistemaInjectionAppService.ObterUsuarioSistemaAppService(context, mapper);

            return new UsuarioSistemaController(usuarioSistemaAppService, mapper);
        }
    }
}