#region

using comrade.Application.Dtos.AirplaneDtos;

#endregion

namespace comrade.Application.Validations.AirplaneValitations
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