using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class CampoOrigemAppService : ICampoOrigemAppService
    {
        private readonly IMapper _mapper;
        private readonly ICampoOrigemRepository _campoOrigemRepository;

        public CampoOrigemAppService(IMapper mapper, ICampoOrigemRepository campoOrigemRepository)
        {
            _mapper = mapper;
            _campoOrigemRepository = campoOrigemRepository;
        }

        public async Task<IEnumerable<CampoOrigemViewModel>> BuscarCampoOrigem(TabelaOrigemViewModel tabela)
        {
            IEnumerable<CampoOrigemViewModel> campos = _mapper.Map<IEnumerable<CampoOrigemViewModel>>(await _campoOrigemRepository.BuscarTabelaOrigem(
                _mapper.Map<TabelaOrigem>(tabela)));

            return campos;
        }

        public async Task<IEnumerable<CampoOrigemViewModel>> ListarCampoOrigem()
        {
            IEnumerable<CampoOrigemViewModel> campos = _mapper.Map<IEnumerable<CampoOrigemViewModel>>(
                await _campoOrigemRepository.ListarCampoOrigem()
                );

            return campos;
        }

    }
}
