using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class TabelaOrigemAppService : ITabelaOrigemAppService
    {
        private readonly IMapper _mapper;
        private readonly ITabelaOrigemRepository _tabelaOrigemRepository;

        public TabelaOrigemAppService(IMapper mapper, ITabelaOrigemRepository tabelaOrigemRepository)
        {
            _mapper = mapper;
            _tabelaOrigemRepository = tabelaOrigemRepository;
        }

        public async Task<IEnumerable<TabelaOrigemViewModel>> BuscarTabelaOrigem(SistemaOrigemViewModel sistema)
        {
            IEnumerable<TabelaOrigemViewModel> tabelas = _mapper.Map<IEnumerable<TabelaOrigemViewModel>>(await _tabelaOrigemRepository.BuscarTabelaOrigem(
                _mapper.Map<SistemaOrigem>(sistema)));

            return tabelas;
        }

        public async Task<IEnumerable<TabelaOrigemViewModel>> ListarTabelaOrigem()
        {
            IEnumerable<TabelaOrigemViewModel> tabelas = _mapper.Map<IEnumerable<TabelaOrigemViewModel>>(
                await _tabelaOrigemRepository.ListarTabelaOrigem()
                );

            return tabelas;
        }
    }
}
