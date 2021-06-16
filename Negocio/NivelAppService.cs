using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using System.Collections.Generic;

namespace CoBRA.Application
{
    public class NivelAppService : INivelAppService
    {
        private readonly IMapper _mapper;
        private readonly INivelRepository _nivelRepository;

        public NivelAppService(IMapper mapper, INivelRepository nivelRepository)
        {
            _mapper = mapper;
            _nivelRepository = nivelRepository;
        }

        public List<NivelViewModel> Obter()
        {
            var niveis = _mapper.Map<List<NivelViewModel>>(_nivelRepository.Obter());
            return niveis;
        }
    }
}
