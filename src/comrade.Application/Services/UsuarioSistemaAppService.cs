#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using comrade.Application.Bases;
using comrade.Application.Dtos.UsuarioSistemaDtos;
using comrade.Application.Filters;
using comrade.Application.Interfaces;
using comrade.Application.Utils;
using comrade.Application.Validations.BaUsuValitation;
using comrade.Core.UsuarioSistemaCore;
using comrade.Core.UsuarioSistemaCore.Usecase;
using comrade.Domain.Bases;
using comrade.Domain.Models;
using Microsoft.EntityFrameworkCore;

#endregion

namespace comrade.Application.Services
{
    public class UsuarioSistemaAppService : AppService, IUsuarioSistemaAppService
    {
        private readonly UsuarioSistemaEditarUsecase _editarUsuarioSistemaUsecase;
        private readonly UsuarioSistemaExcluirUsecase _excluirUsuarioSistemaUsecase;
        private readonly UsuarioSistemaIncluirUsecase _incluirUsuarioSistemaUsecase;
        private readonly IUsuarioSistemaRepository _repository;

        public UsuarioSistemaAppService(IUsuarioSistemaRepository repository,
            UsuarioSistemaEditarUsecase editarUsuarioSistemaUsecase,
            UsuarioSistemaIncluirUsecase incluirUsuarioSistemaUsecase,
            UsuarioSistemaExcluirUsecase excluirUsuarioSistemaUsecase,
            IMapper mapper)
            : base(mapper)
        {
            _repository = repository;
            _editarUsuarioSistemaUsecase = editarUsuarioSistemaUsecase;
            _incluirUsuarioSistemaUsecase = incluirUsuarioSistemaUsecase;
            _excluirUsuarioSistemaUsecase = excluirUsuarioSistemaUsecase;
        }

        public async Task<IPageResultDto<UsuarioSistemaDto>> Listar(PaginationFilter paginationFilter = null)
        {
            List<UsuarioSistemaDto> lista;
            if (paginationFilter == null)
            {
                lista = await Task.Run(() => _repository.GetAll()
                    .ProjectTo<UsuarioSistemaDto>(Mapper.ConfigurationProvider)
                    .ToListAsync());

                return new PageResultDto<UsuarioSistemaDto>(lista);
            }

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            lista = await Task.Run(() => _repository.GetAll().Skip(skip).Take(paginationFilter.PageSize)
                .ProjectTo<UsuarioSistemaDto>(Mapper.ConfigurationProvider)
                .ToListAsync());

            return new PageResultDto<UsuarioSistemaDto>(paginationFilter, lista);
        }

        public async Task<ListResultDto<LookupDto>> BuscarPorNome(string nome)
        {
            var success = int.TryParse(nome, out var number);
            List<LookupDto> lista = new();

            if (success)
            {
                var entity = await _repository.GetById(number);
                if (entity != null)
                {
                    var dto = Mapper.Map<LookupDto>(new LookupEntity {Key = entity.Id, Value = entity.Nome});
                    lista = new List<LookupDto> {dto};
                }
            }
            else if (!string.IsNullOrEmpty(nome))
            {
                lista = await Task.Run(() => _repository.BuscarPorNome(nome)
                    .ProjectTo<LookupDto>(Mapper.ConfigurationProvider)
                    .ToListAsync());
            }

            return new ListResultDto<LookupDto>(lista);
        }

        public async Task<ISingleResultDto<UsuarioSistemaDto>> Obter(int id)
        {
            var entity = await _repository.GetById(id);
            var dto = Mapper.Map<UsuarioSistemaDto>(entity);
            return new SingleResultDto<UsuarioSistemaDto>(dto);
        }

        public async Task<ISingleResultDto<EntityDto>> Incluir(UsuarioSistemaIncluirDto dto)
        {
            var validator = new UsuarioSistemaIncluirValidation();

            var results = await validator.ValidateAsync(dto);

            if (!results.IsValid)
            {
                var listaErros = results.Errors.Select(x => x.ErrorMessage);
                return new SingleResultDto<EntityDto>(listaErros);
            }

            var evento = Mapper.Map<UsuarioSistema>(dto);

            var result = await _incluirUsuarioSistemaUsecase.Execute(evento);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> Editar(UsuarioSistemaEditarDto dto)
        {
            var validator = new UsuarioSistemaEditarValidation();

            var results = await validator.ValidateAsync(dto);

            if (!results.IsValid)
            {
                var listaErros = results.Errors.Select(x => x.ErrorMessage);
                return new SingleResultDto<EntityDto>(listaErros);
            }

            var evento = Mapper.Map<UsuarioSistema>(dto);

            var result = await _editarUsuarioSistemaUsecase.Execute(evento);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }

        public async Task<ISingleResultDto<EntityDto>> Excluir(int id)
        {
            var result = await _excluirUsuarioSistemaUsecase.Execute(id);

            var resultDto = new SingleResultDto<EntityDto>(result);
            resultDto.SetData(result, Mapper);

            return resultDto;
        }
    }
}