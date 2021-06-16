using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using System.Collections.Generic;

namespace CoBRA.Application
{
    public class DepartamentoAppService : IDepartamentoAppService
    {
        private readonly IMapper _mapper;
        private readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoAppService(IMapper mapper, IDepartamentoRepository departamentoRepository)
        {
            _mapper = mapper;
            _departamentoRepository = departamentoRepository;
        }

        public List<DepartamentoViewModel> ObterTodos()
        {
            return _mapper.Map<List<DepartamentoViewModel>>(_departamentoRepository.ObterDepartamentos());
        }
    }
}
