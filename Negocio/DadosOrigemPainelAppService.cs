using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class DadosOrigemPainelAppService : IDadosOrigemPainelAppService
    {
        private readonly IMapper _mapper;
        private readonly IDadosOrigemPainelRepository _dadosOrigemRepository;

        public DadosOrigemPainelAppService(IMapper mapper, IDadosOrigemPainelRepository dadosOrigemRepository)
        {
            _mapper = mapper;
            _dadosOrigemRepository = dadosOrigemRepository;
        }

        public async Task<IEnumerable<DadosOrigemPainelViewModel>> ListarDadosOrigemPainel()
        {
            IEnumerable<DadosOrigemPainelViewModel> origens = _mapper.Map<IEnumerable<DadosOrigemPainelViewModel>>(await _dadosOrigemRepository.ListarDadosOrigemPainel());
            return origens;
        }
    }
}
