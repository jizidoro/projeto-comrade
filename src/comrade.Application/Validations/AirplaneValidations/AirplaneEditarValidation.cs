#region

using comrade.Application.Dtos.AirplaneDtos;

#endregion

namespace comrade.Application.Validations.AirplaneValidations
{
    public class AirplaneEditarValidation : AirplaneValidation<AirplaneEditarDto>
    {
        public AirplaneEditarValidation()
        {
            ValidarId();
            ValidarCodigo();
            ValidarModelo();
            ValidarQuantidadePassageiro();
        }
    }
}