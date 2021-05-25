#region

using AutoMapper;
using comrade.Application.Dtos;
using comrade.Application.Dtos.AirplaneDtos;
using comrade.Application.Dtos.UsuarioSistemaDtos;
using comrade.Domain.Models;

#endregion

namespace comrade.Application.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<AirplaneIncluirDto, Airplane>();
            CreateMap<UsuarioSistemaIncluirDto, UsuarioSistema>();
            CreateMap<AutenticacaoDto, UsuarioSistema>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Chave))
                .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Senha));
        }
    }
}