using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class RegiaoConsultorAppService : IRegiaoConsultorAppService
    {
        private readonly IMapper _mapper;
        private readonly IRegiaoConsultorRepository _regiaoConsultorRepository;

        public RegiaoConsultorAppService(IMapper mapper, IRegiaoConsultorRepository regiaoConsultorRepository)
        {
            _mapper = mapper;
            _regiaoConsultorRepository = regiaoConsultorRepository;
        }

        public async Task<IList<RegiaoConsultorViewModel>> ListarRegiaoConsutor(Guid idUsuarioRM)
        {
            var regioes = _mapper.Map<IList<RegiaoConsultorViewModel>>(
                await _regiaoConsultorRepository.BuscarRegiaoConsultor(idUsuarioRM));

            return regioes;
        }

        public async Task<IList<RegiaoConsultorViewModel>> ListarRegiaoConsutor()
        {
            var regioes = _mapper.Map<IList<RegiaoConsultorViewModel>>(
                await _regiaoConsultorRepository.ListarRegiaoConsultor());

            return regioes;
        }

    }
}
