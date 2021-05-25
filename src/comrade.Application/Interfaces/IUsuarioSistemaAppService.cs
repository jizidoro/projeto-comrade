#region

using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos.UsuarioSistemaDtos;
using comrade.Application.Filters;
using comrade.Application.Utils;

#endregion

namespace comrade.Application.Interfaces
{
    public interface IUsuarioSistemaAppService : IAppService
    {
        Task<IPageResultDto<UsuarioSistemaDto>> Listar(PaginationFilter paginationFilter = null);
        Task<ListResultDto<LookupDto>> BuscarPorNome(string nome);
        Task<ISingleResultDto<UsuarioSistemaDto>> Obter(int id);
        Task<ISingleResultDto<EntityDto>> Incluir(UsuarioSistemaIncluirDto dto);
        Task<ISingleResultDto<EntityDto>> Editar(UsuarioSistemaEditarDto dto);
        Task<ISingleResultDto<EntityDto>> Excluir(int id);
    }
}