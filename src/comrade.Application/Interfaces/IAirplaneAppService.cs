#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos.AirplaneDtos;
using comrade.Application.Filters;
using comrade.Application.Utils;

#endregion

namespace comrade.Application.Interfaces
{
    public interface IAirplaneAppService : IAppService
    {
        Task<IPageResultDto<AirplaneDto>> Listar(PaginationFilter paginationFilter = null);
        Task<ISingleResultDto<AirplaneDto>> Obter(int id);
        Task<ISingleResultDto<EntityDto>> Incluir(AirplaneIncluirDto dto);
        Task<ISingleResultDto<EntityDto>> Editar(AirplaneEditarDto dto);
        Task<ISingleResultDto<EntityDto>> Excluir(int id);
    }
}