#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using comrade.Application.Bases;
using comrade.Application.Dtos.AirplaneDtos;
using comrade.Application.Filters;
using comrade.Application.Interfaces;
using comrade.Application.Utils;
using comrade.Application.Validations.AirplaneValitation;
using comrade.Core.AirplaneCore;
using comrade.Core.AirplaneCore.Usecase;
using comrade.Domain.Models;
using Microsoft.EntityFrameworkCore;

#endregion

namespace comrade.Application.Services
{
    public class AirplaneAppService : AppService, IAirplaneAppService
    {
        private readonly AirplaneEditarUsecase _editarAirplaneUsecase;
        private readonly AirplaneExcluirUsecase _excluirAirplaneUsecase;
        private readonly AirplaneIncluirUsecase _incluirAirplaneUsecase;
        private readonly IAirplaneRepository _repository;

        public AirplaneAppService(IAirplaneRepository repository,
            AirplaneEditarUsecase editarAirplaneUsecase,
            AirplaneIncluirUsecase incluirAirplaneUsecase,
            AirplaneExcluirUsecase excluirAirplaneUsecase,
            IMapper mapper)
            : base(mapper)
        {
            _repository = repository;
            _editarAirplaneUsecase = editarAirplaneUsecase;
            _incluirAirplaneUsecase = incluirAirplaneUsecase;
            _excluirAirplaneUsecase = excluirAirplaneUsecase;
        }

        public async Task<IPageResultDto<AirplaneDto>> Listar(PaginationFilter paginationFilter = null)
        {
            List<AirplaneDto> lista;
            if (paginationFilter == null)
            {
                lista = await Task.Run(() => _repository.GetAll()
                    .ProjectTo<AirplaneDto>(Mapper.ConfigurationProvider)
                    .ToListAsync());

                return new PageResultDto<AirplaneDto>(lista);
            }

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            lista = await Task.Run(() => _repository.GetAll().Skip(skip).Take(paginationFilter.PageSize)
                .ProjectTo<AirplaneDto>(Mapper.ConfigurationProvider)
                .ToListAsync());

            return new PageResultDto<AirplaneDto>(lista);
        }

        public async Task<ISingleResultDto<AirplaneDto>> Obter(int id)
        {
            var entity = await _repository.GetById(id);
            var dto = Mapper.Map<AirplaneDto>(entity);
            return new SingleResultDto<AirplaneDto>(dto);
        }

        public async Task<ISingleResultDto<EntityDto>> Incluir(AirplaneIncluirDto dto)
        {
            var validator = new AirplaneIncluirValidation();

            var results = await validator.ValidateAsync(dto);

            if (!results.IsValid)
            {
                var listaErros = results.Errors.Select(x => x.ErrorMessage);
                return new SingleResultDto<EntityDto>(listaErros);
            }

            var evento = Mapper.Map<Airplane>(dto);

            var result = await _incluirAirplaneUsecase.Execute(evento);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> Editar(AirplaneEditarDto dto)
        {
            var validator = new AirplaneEditarValidation();

            var results = await validator.ValidateAsync(dto);

            if (!results.IsValid)
            {
                var listaErros = results.Errors.Select(x => x.ErrorMessage);
                return new SingleResultDto<EntityDto>(listaErros);
            }

            var evento = Mapper.Map<Airplane>(dto);

            var result = await _editarAirplaneUsecase.Execute(evento);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> Excluir(int id)
        {
            var result = await _excluirAirplaneUsecase.Execute(id);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }
    }
}