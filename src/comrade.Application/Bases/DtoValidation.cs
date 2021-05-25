#region

using FluentValidation;

#endregion

namespace comrade.Application.Bases
{
    public class DtoValidation<TDto> : AbstractValidator<TDto>
        where TDto : EntityDto
    {
        public const string CampoObrigatorio = "CAMPO_OBRIGATORIO";
        public const string TamanhoEspecificoCampo = "TAMANHO_ESPECIFICO_CAMPO";
        public const string TamanhoIntervaloCampo = "TAMANHO_INTERVALO_CAMPO";
        public const string CampoDataMaiorQueHoje = "CAMPO_DATA_MAIOR_QUE_HOJE";
    }
}