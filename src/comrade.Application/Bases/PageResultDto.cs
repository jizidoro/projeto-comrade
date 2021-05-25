#region

using System.Collections.Generic;
using comrade.Application.Filters;
using comrade.Application.Utils;
using comrade.Core.Helpers.Messages;
using comrade.Domain.Enums;

#endregion

namespace comrade.Application.Bases
{
    public class PageResultDto<T> : ResultDto, IPageResultDto<T>
        where T : Dto
    {
        public PageResultDto()
        {
        }

        public PageResultDto(IList<T> data)
        {
            Data = data;
            Codigo = data == null ? (int) EnumResultadoAcao.ErroNaoEncontrado : (int) EnumResultadoAcao.Sucesso;
            Sucesso = data != null;
            Mensagem = data == null ? MensagensNegocio.ResourceManager.GetString("MSG04") : string.Empty;
        }

        public PageResultDto(PaginationFilter pagination, IList<T> data)
        {
            Data = data;
            PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : (int?) null;
            PageSize = pagination.PageNumber >= 1 ? pagination.PageSize : (int?) null;
            NextPage = pagination.PageNumber + 1;
            PreviusPage = pagination.PageNumber > 1 ? pagination.PageNumber - 1 : (int?) null;
            Codigo = data == null ? (int) EnumResultadoAcao.ErroNaoEncontrado : (int) EnumResultadoAcao.Sucesso;
            Sucesso = data != null;
            Mensagem = data == null ? MensagensNegocio.ResourceManager.GetString("MSG04") : string.Empty;
        }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? NextPage { get; set; }
        public int? PreviusPage { get; set; }

        public IList<T> Data { get; set; }
    }
}