using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class ObraOrcamentoAppService : IObraOrcamentoAppService
    {
        private readonly IMapper _mapper;
        IObraOrcamentoRepository _obraOrcamentoRepository;

        public ObraOrcamentoAppService(IMapper mapper, IObraOrcamentoRepository obraOrcamentoRepository)
        {
            _mapper = mapper;
            _obraOrcamentoRepository = obraOrcamentoRepository;
        }

        public async Task<IEnumerable<ObraOrcamentoViewModel>> ListarObraOrcamento()
        {
            IEnumerable<ObraOrcamentoViewModel> obras = _mapper.Map<IEnumerable<ObraOrcamentoViewModel>>(await _obraOrcamentoRepository.ListarObraOrcamento());
            return obras;
        }

        public async Task GravarOrcamentoLivro(ObraOrcamentoViewModel obra) 
        {
            await _obraOrcamentoRepository.GravarOrcamentoLivro(
                    _mapper.Map<ObraOrcamento>(obra)
                );
        }
    }
}
