using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class ResultadoPagamentoMetaAppService : IResultadoPagamentoMetaAppService
    {

        private readonly IMapper _mapper;
        private readonly IResultadoPagamentoMetaRepository _resultadoPagamentoMetaRepository;

        public ResultadoPagamentoMetaAppService(IMapper mapper, IResultadoPagamentoMetaRepository resultadoPagamentoMetaRepository)
        {
            _mapper = mapper;
            _resultadoPagamentoMetaRepository = resultadoPagamentoMetaRepository;
        }

        public async Task<ResultadoPagamentoMetaViewModel> ConsultarResultadoPagamentoMeta(int Percentual)
        {
            ResultadoPagamentoMetaViewModel ResultadoPagamentoMetaViewModel = _mapper.Map<ResultadoPagamentoMetaViewModel>(await _resultadoPagamentoMetaRepository.ConsultarResultadoPagamentoMeta(Percentual));
            return ResultadoPagamentoMetaViewModel;
        }

    }
}
