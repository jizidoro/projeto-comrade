#region

using System.Threading.Tasks;
using comrade.Application.Dtos;
using comrade.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace comrade.WebApi.UseCases.V1.LoginApi
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAutenticacaoAppService _autenticacaoAppService;

        public TokenController(
            IAutenticacaoAppService autenticacaoAppService
        )
        {
            _autenticacaoAppService = autenticacaoAppService;
        }


        [HttpPost]
        [Route("token")]
        public async Task<ActionResult> Token([FromBody] AutenticacaoDto dto)
        {
            var result = await _autenticacaoAppService.GerarTokenLoginUsecase(dto);

            return Ok(result);
        }
    }
}