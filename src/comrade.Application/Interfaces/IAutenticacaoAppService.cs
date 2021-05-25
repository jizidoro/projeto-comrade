#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos;
using comrade.Application.Utils;

#endregion

namespace comrade.Application.Interfaces
{
    public interface IAutenticacaoAppService : IAppService
    {
        Task<ISingleResultDto<UsuarioDto>> GerarTokenLoginUsecase(AutenticacaoDto dto);
        Task<ISingleResultDto<EntityDto>> EsquecerSenha(AutenticacaoDto dto);
        Task<ISingleResultDto<EntityDto>> ExpirarSenha(AutenticacaoDto dto);
    }
}