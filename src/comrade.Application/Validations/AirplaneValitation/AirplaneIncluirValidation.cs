#region

using comrade.Application.Dtos.AirplaneDtos;

#endregion

namespace comrade.Application.Validations.AirplaneValitation
{
    public class AirplaneIncluirValidation : AirplaneValidation<AirplaneIncluirDto>
    {
        public AirplaneIncluirValidation()
        {
            ValidarCodigo();
            ValidarModelo();
            ValidarQuantidadePassageiro();
        }
    }
}