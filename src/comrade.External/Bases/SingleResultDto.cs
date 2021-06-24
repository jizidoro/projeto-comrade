#region

using System;
using System.Collections.Generic;
using System.Linq;
using comrade.Domain.Bases;
using comrade.Domain.Enums;
using comrade.External.Utils;

#endregion

namespace comrade.External.Bases
{
    public class SingleResultDto<TDto> : ResultDto, ISingleResultDto<TDto>
        where TDto : Dto
    {
        public SingleResultDto(TDto data)
        {
            Codigo = data == null ? (int) EnumResultadoAcao.ErroNaoEncontrado : (int) EnumResultadoAcao.Sucesso;
            Sucesso = data != null;
            Data = data;
        }

        public SingleResultDto()
        {
            Codigo = (int) EnumResultadoAcao.ErroNaoEncontrado;
            Sucesso = false;
            Data = null;
        }

        public SingleResultDto(Exception ex)
        {
            Codigo = (int) EnumResultadoAcao.ErroServidor;
            Sucesso = false;
            Mensagem = ex.Message;
        }

        public SingleResultDto(IEnumerable<string> listaErros)
        {
            var enumerable = listaErros.ToList();
            Codigo = enumerable.Any() ? (int) EnumResultadoAcao.ErroValidacaoNegocio : (int) EnumResultadoAcao.Sucesso;
            Sucesso = false;
            Mensagens = enumerable.ToList();
        }

        public SingleResultDto(int codigo, bool sucesso, string mensagem)
        {
            Codigo = codigo;
            Sucesso = sucesso;
            Mensagem = mensagem;
        }

        public TDto Data { get; private set; }

    }
}