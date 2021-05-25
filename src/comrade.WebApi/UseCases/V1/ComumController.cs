#region

using System;
using System.Threading.Tasks;
using comrade.Application.Bases;
using comrade.Application.Dtos.AirplaneDtos;
using comrade.Application.Dtos.UsuarioSistemaDtos;
using comrade.Application.Interfaces;
using comrade.Domain.Models;
using comrade.WebApi.Modules.Common.FeatureFlags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement.Mvc;

#endregion

namespace comrade.WebApi.UseCases.V1
{
    [FeatureGate(CustomFeature.Comum)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ComumController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUsuarioSistemaAppService _usuarioSistemaAppService;

        public ComumController(IServiceProvider serviceProvider, IUsuarioSistemaAppService usuarioSistemaAppService)
        {
            _serviceProvider = serviceProvider;
            _usuarioSistemaAppService = usuarioSistemaAppService;
        }


        [HttpGet]
        [Route("lookup-usuario-sistema")]
        public async Task<IActionResult> GetLookupUsuarioSistema()
        {
            try
            {
                var service = _serviceProvider.GetService<ILookupServiceApp<UsuarioSistema>>();

                var result = await service?.ObterLookup()!;

                return Ok(new ListResultDto<LookupDto>(result));
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<AirplaneDto>(e));
            }
        }

        [HttpGet]
        [Route("lookup-usuario-sistema-por-nome/{nome}")]
        public async Task<IActionResult> GetLookupUsuarioSistemaPorNone(string nome)
        {
            try
            {
                var result = await _usuarioSistemaAppService.BuscarPorNome(nome);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<UsuarioSistemaDto>(e));
            }
        }
    }
}