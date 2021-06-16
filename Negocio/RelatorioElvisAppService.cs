using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class RelatorioElvisAppService : IRelatorioElvisAppService
    {
        private readonly IMapper _mapper;
        private readonly IRelatorioElvisRepository _relatorioAppService;

        public RelatorioElvisAppService(IMapper mapper, IRelatorioElvisRepository relatorioAppService)
        {
            _mapper = mapper;
            _relatorioAppService = relatorioAppService;
        }

        public async Task<IList<RelatorioElvisViewModel>> ListarRelatorio()
        {
            return _mapper.Map<IList<RelatorioElvisViewModel>>(await _relatorioAppService.ListarItensRelatorio());
        }

        public async Task<IList<RelatorioElvisViewModel>> FiltrarRelatorio(FiltroRelatorioElvisViewModel filtro)
        {
            filtro.ParametroPesquisa = filtro.ParametroPesquisa ?? new ParametroPesquisaViewModel()
            {
                QuantidadeMaximaItensPesquisa = 10,
                NumeroPagina = 0,
                Ordenacao = "titulo_az"
            };

            return _mapper.Map<IList<RelatorioElvisViewModel>>(await _relatorioAppService.FiltrarItensRelatorio(
                _mapper.Map<FiltroRelatorioElvis>(filtro)));
        }

        public async Task<IList<ObraAndamentoViewModel>> FiltrarRelatorioObrasAndamento(FiltroObraAndamentoViewModel filtro)
        {
            filtro.ParametroPesquisa = filtro.ParametroPesquisa ?? new ParametroPesquisaViewModel()
            {
                QuantidadeMaximaItensPesquisa = 10,
                NumeroPagina = 0,
                Ordenacao = "titulo_az"
            };

            return _mapper.Map<IList<ObraAndamentoViewModel>>(await _relatorioAppService.FiltrarRelatorioObrasAndamento(
                _mapper.Map<FiltroObraAndamento>(filtro)
                ));
        }

        public async Task<IList<FiltroRelatorioElvisViewModel>> CarregarPrograma() {
            return _mapper.Map<IList<FiltroRelatorioElvisViewModel>>(await _relatorioAppService.CarregarPrograma());
        }

        public async Task<IList<FiltroRelatorioElvisViewModel>> CarregarProgramaObraAndamento()
        {
            return _mapper.Map<IList<FiltroRelatorioElvisViewModel>>(await _relatorioAppService.CarregarProgramaObraAndamento());
        }

        public async Task<IList<FiltroRelatorioElvisViewModel>> CarregarDisciplina() {
            return _mapper.Map<IList<FiltroRelatorioElvisViewModel>>(await _relatorioAppService.CarregarDisciplina());
        }
              
        public async Task<IList<FiltroRelatorioElvisViewModel>> CarregarColecao() {
            return _mapper.Map<IList<FiltroRelatorioElvisViewModel>>(await _relatorioAppService.CarregarColecao());
        }
               
        public async Task<IList<FiltroRelatorioElvisViewModel>> CarregarAnoEscolar() {
            return _mapper.Map<IList<FiltroRelatorioElvisViewModel>>(await _relatorioAppService.CarregarAnoEscolar());
        }

    }
}
