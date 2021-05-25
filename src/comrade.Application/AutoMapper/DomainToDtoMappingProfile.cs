#region

using AutoMapper;
using comrade.Application.Bases;
using comrade.Application.Dtos;
using comrade.Application.Dtos.AirplaneDtos;
using comrade.Application.Dtos.UsuarioSistemaDtos;
using comrade.Domain.Bases;
using comrade.Domain.Models;

#endregion

namespace comrade.Application.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Entity, EntityDto>();
            CreateMap<LookupEntity, LookupDto>();

            CreateMap<Airplane, AirplaneEditarDto>();
            CreateMap<Airplane, AirplaneDto>();

            CreateMap<UsuarioSistema, UsuarioSistemaEditarDto>();
            CreateMap<UsuarioSistema, UsuarioSistemaDto>();

            CreateMap<UsuarioSistema, AutenticacaoDto>()
                .ForMember(dest => dest.Chave, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Senha));
        }
    }
}