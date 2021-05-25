#region

using System;
using System.Threading.Tasks;
using AutoMapper;
using comrade.Application.Bases;
using comrade.Application.Dtos.UsuarioSistemaDtos;
using comrade.Application.Filters;
using comrade.Application.Interfaces;
using comrade.Application.Queries;
using comrade.WebApi.Modules.Common.FeatureFlags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

#endregion

namespace comrade.WebApi.UseCases.V1.UsuarioSistemaApi
{
    // [Authorize]
    [FeatureGate(CustomFeature.UsuarioSistema)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsuarioSistemaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioSistemaAppService _usuarioSistemaAppService;

        public UsuarioSistemaController(
            IUsuarioSistemaAppService usuarioSistemaAppService, IMapper mapper)
        {
            _usuarioSistemaAppService = usuarioSistemaAppService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> Listar([FromQuery] PaginationQuery? paginationQuery)
        {
            try
            {
                PaginationFilter? paginationFilter = null;
                if (paginationQuery != null)
                {
                    paginationFilter = _mapper.Map<PaginationQuery, PaginationFilter>(paginationQuery);
                }

                var result = await _usuarioSistemaAppService.Listar(paginationFilter);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<UsuarioSistemaDto>(e));
            }
        }


        [HttpGet]
        [Route("obter/{id:int}")]
        public async Task<IActionResult> Obter(int id)
        {
            try
            {
                var result = await _usuarioSistemaAppService.Obter(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<UsuarioSistemaDto>(e));
            }
        }

        [Route("incluir")]
        [HttpPost]
        public async Task<IActionResult> Incluir([FromBody] UsuarioSistemaIncluirDto dto)
        {
            try
            {
                var result = await _usuarioSistemaAppService.Incluir(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<UsuarioSistemaDto>(e));
            }
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromBody] UsuarioSistemaEditarDto dto)
        {
            try
            {
                var result = await _usuarioSistemaAppService.Editar(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<UsuarioSistemaDto>(e));
            }
        }

        [HttpDelete]
        [Route("excluir/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var result = await _usuarioSistemaAppService.Excluir(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new SingleResultDto<UsuarioSistemaDto>(e));
            }
        }
    }
}