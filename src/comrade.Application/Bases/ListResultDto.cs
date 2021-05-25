#region

using System.Collections.Generic;
using comrade.Application.Utils;
using comrade.Core.Helpers.Messages;
using comrade.Domain.Enums;

#endregion

namespace comrade.Application.Bases
{
    public class ListResultDto<T> : ResultDto, IListResultDto<T>
        where T : Dto
    {
        public ListResultDto()
        {
        }

        public ListResultDto(IList<T> data)
        {
            Data = data;
            Codigo = data == null ? (int) EnumResultadoAcao.ErroNaoEncontrado : (int) EnumResultadoAcao.Sucesso;
            Sucesso = data != null;
            Mensagem = data == null ? MensagensNegocio.ResourceManager.GetString("MSG04") : string.Empty;
        }

        public ListResultDto(int codigo, string menssagem)
        {
            Codigo = codigo;
            Sucesso = false;
            Mensagem = menssagem;
        }

        public ListResultDto(string menssagem)
        {
            Codigo = (int) EnumResultadoAcao.ErroValidacaoNegocio;
            Sucesso = false;
            Mensagem = menssagem;
        }

        public IList<T> Data { get; set; }
    }
}