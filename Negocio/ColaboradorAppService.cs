using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class ColaboradorAppService : IColaboradorAppService
    {
        private readonly IMapper _mapper;
        private readonly IColaboradorRepository _colaboradorRepository;

        public ColaboradorAppService(IMapper mapper, IColaboradorRepository colaboradorRepository)
        {
            _mapper = mapper;
            _colaboradorRepository = colaboradorRepository;
        }

        public List<ColaboradorViewModel> ObterPorCargo(Guid idCargo)
        {
            var colaboradores = _mapper.Map<List<ColaboradorViewModel>>(_colaboradorRepository.ObterColaboradores(idCargo));
            return colaboradores;
        }

        public async Task<List<ColaboradorViewModel>> ObterPorSupervisor(Guid idUsuario, Guid? idRegiao)
        {
            var colaboradores = _mapper.Map<List<ColaboradorViewModel>>(await _colaboradorRepository.ObterColaboradoresPorSupervisor(idUsuario, idRegiao));
            colaboradores.Add(_mapper.Map<ColaboradorViewModel>(_colaboradorRepository.ObterColaborador(idUsuario)));
            return colaboradores;
        }
    }
}
