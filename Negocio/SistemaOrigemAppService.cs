using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class SistemaOrigemAppService : ISistemaOrigemAppService
    {
        private readonly IMapper _mapper;
        private readonly ISistemaOrigemRepository _sistemaOrigemRepository;

        public SistemaOrigemAppService(IMapper mapper, ISistemaOrigemRepository sistemaOrigemRepository)
        {
            _mapper = mapper;
            _sistemaOrigemRepository = sistemaOrigemRepository;
        }

        public async Task<IEnumerable<SistemaOrigemViewModel>> ListarSistemaOrigem()
        {
            IEnumerable<SistemaOrigemViewModel> sistemas = _mapper.Map<IEnumerable<SistemaOrigemViewModel>>(await _sistemaOrigemRepository.ListaSistemaOrigem());
            return sistemas;
        }
    }
}
