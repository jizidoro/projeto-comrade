#region

using System.Threading.Tasks;
using AutoMapper;
using comrade.Application.Bases;
using comrade.Application.Dtos;
using comrade.Application.Interfaces;
using comrade.Application.Utils;
using comrade.Core.SecurityCore.Usecase;
using comrade.Core.Views.VBaUsuPermissaoCore;
using comrade.Domain.Models;

#endregion

namespace comrade.Application.Services
{
    public class AutenticacaoAppService : AppService, IAutenticacaoAppService
    {
        private readonly AtualizarSenhaExpiradaUsecase _atualizarSenhaExpiradaUsecase;
        private readonly EsquecerSenhaUsecase _esquecerSenhaUsecase;
        private readonly GerarTokenLoginUsecase _gerarTokenLoginUsecase;
        private readonly IVwUsuarioSistemaPermissaoRepository _vUsuarioSistemaPermissaoRepository;

        public AutenticacaoAppService(IVwUsuarioSistemaPermissaoRepository vUsuarioSistemaPermissaoRepository,
            AtualizarSenhaExpiradaUsecase atualizarSenhaExpiradaUsecase,
            GerarTokenLoginUsecase gerarTokenLoginUsecase, EsquecerSenhaUsecase esquecerSenhaUsecase, IMapper mapper) :
            base(mapper)
        {
            _vUsuarioSistemaPermissaoRepository = vUsuarioSistemaPermissaoRepository;
            _atualizarSenhaExpiradaUsecase = atualizarSenhaExpiradaUsecase;
            _esquecerSenhaUsecase = esquecerSenhaUsecase;
            _gerarTokenLoginUsecase = gerarTokenLoginUsecase;
        }

        public async Task<ISingleResultDto<UsuarioDto>> GerarTokenLoginUsecase(AutenticacaoDto dto)
        {
            var result = await _gerarTokenLoginUsecase.Execute(dto.Chave, dto.Senha);

            if (result.Success)
            {
                var token = new UsuarioDto
                {
                    Token = result.User.Token
                };

                return new SingleResultDto<UsuarioDto>(token);
            }

            return new SingleResultDto<UsuarioDto>(result);
        }

        public async Task<ISingleResultDto<EntityDto>> EsquecerSenha(AutenticacaoDto dto)
        {
            var evento = Mapper.Map<UsuarioSistema>(dto);

            var result = await _esquecerSenhaUsecase.Execute(evento);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> ExpirarSenha(AutenticacaoDto dto)
        {
            var evento = Mapper.Map<UsuarioSistema>(dto);

            var result = await _atualizarSenhaExpiradaUsecase.Execute(evento);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }
    }
}