using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class GrupoPainelAppService : IGrupoPainelAppService
    {

        private readonly IMapper _mapper;
        private readonly IGrupoPainelRepository _grupoPainelRepository;

        public GrupoPainelAppService(IMapper mapper, IGrupoPainelRepository grupoPainelRepository)
        {
            _mapper = mapper;
            _grupoPainelRepository = grupoPainelRepository;
        }

        public async Task<IEnumerable<GrupoPainelViewModel>> ListarGrupoPainel(Guid? StatusId)
        {
            var grupos = _mapper.Map<IEnumerable<GrupoPainelViewModel>>(await _grupoPainelRepository.ListaGrupoPainel(StatusId));
            return grupos;
        }

    }
}
