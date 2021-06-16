using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class AmarracaoAppService : IAmarracaoAppService
    {
        private readonly IMapper _mapper;
        private readonly IAmarracaoRepository _amarracaoRepository;

        public AmarracaoAppService(IAmarracaoRepository amarracaoRepository, IMapper mapper)
        {
            _amarracaoRepository = amarracaoRepository;
            _mapper = mapper;
        }

        public List<RegraPagamentoViewModel> ListarRegraPagamento()
        {
            List<RegraPagamento> lst = _amarracaoRepository.ListarRegraPagamento();

            List<RegraPagamentoViewModel> retorno = new List<RegraPagamentoViewModel>();
            foreach (var item in lst)
            {
                RegraPagamentoViewModel r = new RegraPagamentoViewModel();
                r.IdRegraPagamento = item.IdRegraPagamento;
                r.DescricaoRegraPagamento = item.DescricaoRegraPagamento;
                retorno.Add(r);
            }

            return retorno;
        }

        public List<PrazoValidadeViewModel> ListarPrazoValidade()
        {
            List<PrazoValidade> lst = _amarracaoRepository.ListarPrazoValidade();

            List<PrazoValidadeViewModel> retorno = new List<PrazoValidadeViewModel>();
            foreach (var item in lst)
            {
                PrazoValidadeViewModel r = new PrazoValidadeViewModel();
                r.IdPrazoValidade = item.IdPrazoValidade;
                r.DescricaoPrazoValidade = item.DescricaoPrazoValidade;
                retorno.Add(r);
            }

            return retorno;
        }

        public List<BloqueioPagamentoViewModel> ListarBloqueioPagamento()
        {
            List<BloqueioPagamento> lst = _amarracaoRepository.ListarBloqueioPagamento();

            List<BloqueioPagamentoViewModel> retorno = new List<BloqueioPagamentoViewModel>();
            foreach (var item in lst)
            {
                BloqueioPagamentoViewModel r = new BloqueioPagamentoViewModel();
                r.IdBloqueioPagamento = item.IdBloqueioPagamento;
                r.DescricaoBloqueioPagamento = item.DescricaoBloqueioPagamento;
                retorno.Add(r);
            }

            return retorno;
        }

        public List<TipoContratoViewModel> ListarTipoContrato()
        {
            List<TipoContrato> lst = _amarracaoRepository.ListarTipoContrato();

            List<TipoContratoViewModel> retorno = new List<TipoContratoViewModel>();
            foreach (var item in lst)
            {
                TipoContratoViewModel r = new TipoContratoViewModel();
                r.IdTipoContrato = item.IdTipoContrato;
                r.DescricaoTipoContrato = item.DescricaoTipoContrato;
                retorno.Add(r);
            }

            return retorno;
        }

        public List<TipoParticipacaoViewModel> ListarTipoParticipacao()
        {
            List<TipoParticipacao> lst = _amarracaoRepository.ListarTipoParticipacao();

            List<TipoParticipacaoViewModel> retorno = new List<TipoParticipacaoViewModel>();
            foreach (var item in lst)
            {
                TipoParticipacaoViewModel r = new TipoParticipacaoViewModel();
                r.IdTipoParticipacao = item.IdTipoParticipacao;
                r.DescricaoTipoParticipacao = item.DescricaoTipoParticipacao;
                retorno.Add(r);
            }

            return retorno;
        }

        public async Task<int> SalvarAmarracao(AmarracaoViewModel amarracao, int usuario)
        {
            return 0;
        }
    }
}
