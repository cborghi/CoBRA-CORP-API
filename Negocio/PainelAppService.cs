using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class PainelAppService : IPainelAppService 
    {
        private readonly IMapper _mapper;
        private readonly IPainelRepository _painelRepository;


        public PainelAppService(IMapper mapper, IPainelRepository  painelRepository)
        {
            _mapper = mapper;
            _painelRepository = painelRepository;

        }

        public async Task InserirPainel(CabecalhoPainelMetaViewModel painel)
        {
            await _painelRepository.GravarPainelMeta(_mapper.Map<CabecalhoPainelMeta>(painel));
        }

        public async Task<IEnumerable<CabecalhoPainelMetaViewModel>> BuscarPainel(CabecalhoPainelMetaViewModel painel, Guid? PeriodoId)
        {
            return _mapper.Map<IEnumerable<CabecalhoPainelMetaViewModel>>(
                await _painelRepository.BuscarCabecalhoPainel(_mapper.Map<CabecalhoPainelMeta>(painel), PeriodoId)
                );
        }

        public async Task<IEnumerable<CabecalhoPainelMetaViewModel>> FiltrarPainel(CabecalhoPainelMetaViewModel painel)
        {
            return _mapper.Map<IEnumerable<CabecalhoPainelMetaViewModel>>(
                await _painelRepository.FiltrarCabecalhoPainel(_mapper.Map<CabecalhoPainelMeta>(painel))
                );
        }

        public async Task<IList<LinhaPainelMetaViewModel>> BuscarLinhaPainel(LinhaPainelMetaViewModel painel)
        {
            return _mapper.Map<IList<LinhaPainelMetaViewModel>>(
                await _painelRepository.BuscarLinhaPainel(_mapper.Map<LinhaPainelMeta>(painel))
                );
        }

        public async Task AtualizarLinhaPainel(LinhaPainelMetaViewModel painel)
        {
            await _painelRepository.AtualizarLinhaPainel(_mapper.Map<LinhaPainelMeta>(painel));
        }

        public async Task ExcluirLinhaPainel(LinhaPainelMetaViewModel painel)
        {
            await _painelRepository.ExcluirLinhaPainel(_mapper.Map<LinhaPainelMeta>(painel));
        }

        public async Task EnviarPainelAprovacao(CabecalhoPainelMetaViewModel cabecalho)
        {
            await _painelRepository.EnviarPainelAprovacao(_mapper.Map<CabecalhoPainelMeta>(cabecalho));
        }

        public async Task ExcluirCabecalhoPainel(CabecalhoPainelMetaViewModel cabecalho)
        {
            await _painelRepository.ExcluirCabecalhoPainel(_mapper.Map<CabecalhoPainelMeta>(cabecalho));
        }

        public async Task<byte> ObterPesoTotalMeta(CabecalhoPainelMetaViewModel cabecalho)
        {
            return await _painelRepository.ObterPesoTotalMeta(_mapper.Map<CabecalhoPainelMeta>(cabecalho));
        }

        public async Task<bool> VerificarMetaCadastrada(CabecalhoPainelMetaViewModel cabecalho)
        {
            return await _painelRepository.VerificarMetaCadastrada(_mapper.Map<CabecalhoPainelMeta>(cabecalho));
        }

        public async Task<List<PeriodoCampanhaViewModel>> BuscarPeriodo()
        {
            var dados = await _painelRepository.BuscarPeriodo();
            var retorno = new List<PeriodoCampanhaViewModel>();
            foreach (var item in dados)
            {
                var d = new PeriodoCampanhaViewModel
                {
                    Id_Periodo = item.Id_Periodo,
                    Descricao = item.Descricao,
                    Data_Inicio = item.Data_Inicio,
                    Dat_Fim = item.Dat_Fim
                };

                retorno.Add(d);
            }

            return retorno;
        }
    }
}
