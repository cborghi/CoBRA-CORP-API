using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using CoBRA.Infra.CrossCutting.EmailService.Interfaces;
using CoBRA.Infra.CrossCutting.EmailService.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace CoBRA.Application
{
    public class RequisicaoComprasAppService : IRequisicaoComprasAppService
    {
        private readonly IMapper _mapper;
        private readonly IRequisicaoCompraRepository _requisicaoCompraRepository;
        private readonly IRequisicaoABDRepository _requisicaoABDRepository;
        private readonly IServicoRepository _servicoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmailService _emailService;
        private readonly IFornecedoresProtheusRepository _fornecedoresProtheusRepository;


        public RequisicaoComprasAppService(IMapper mapper, IRegraAppService regraAppService, IEmailService emailService, IRequisicaoCompraRepository requisicaoCompraRepository, IRequisicaoABDRepository requisicaoABDRepository, IUsuarioRepository usuarioRepository, IServicoRepository servicoRepository, IFornecedoresProtheusRepository fornecedoresProtheusRepository)
        {
            _mapper = mapper;
            _requisicaoCompraRepository = requisicaoCompraRepository;
            _requisicaoABDRepository = requisicaoABDRepository;
            _usuarioRepository = usuarioRepository;
            _servicoRepository = servicoRepository;
            _emailService = emailService;
            _fornecedoresProtheusRepository = fornecedoresProtheusRepository;
        }

        public RequisicaoComprasViewModel ObterRequisicaoPorId(int id)
        {
            var requisicao = _mapper.Map<RequisicaoComprasViewModel>(_requisicaoCompraRepository.ObterRequisicaoPorId(id));
            if (requisicao.Id > 0)
                requisicao.Servico = _mapper.Map<ServicoViewModel>(_servicoRepository.ObterServicoPorTipo(requisicao.Servico.TipoServico));

            return requisicao;
        }

        public RequisicaoViewModel BuscarRequisicao(int idRequisicao)
        {
            var retorno = new RequisicaoViewModel();
            var requisicao = _requisicaoCompraRepository.BuscarRequisicao(idRequisicao);
            retorno.NotaFiscalCompartilhada = requisicao.NotaFiscalCompartilhada;
            retorno.IdRequisicao = idRequisicao;
            retorno.TipoDocumento = requisicao.TipoDocumento;
            retorno.Nota = requisicao.Nota;
            retorno.Telefone = requisicao.Telefone;
            retorno.Contato = requisicao.Contato;
            retorno.DataPagamento = requisicao.DataPagamento;
            retorno.DataEntrega = requisicao.DataEntrega;
            retorno.Moeda = requisicao.Moeda;
            retorno.Valor = requisicao.Valor;
            retorno.FormaPagamento = requisicao.FormaPagamento;
            retorno.ConfirmaPagamento = requisicao.ConfirmaPagamento;
            retorno.Observacao = requisicao.Observacao;
            retorno.IdElvis = requisicao.IdElvis;
            retorno.Link = requisicao.Link;
            retorno.IntegradoProtheus = requisicao.IntegradoProtheus;
            retorno.ElvisOrigem = requisicao.ElvisOrigem;
            retorno.Cancelada = requisicao.Cancelada;
            retorno.SerieNotaFiscal = requisicao.SerieNotaFiscal;
            retorno.Servico = _requisicaoCompraRepository.BuscarServicoRequisicao(idRequisicao);
            
            var forn = requisicao.Fornecedor.Codigo;
            retorno.Fornecedor = new FornecedorViewModel();
            var fornecedor = _fornecedoresProtheusRepository.ObterFornecedorCodigo(forn);
            retorno.Fornecedor = _mapper.Map<FornecedorViewModel>(fornecedor);
            retorno.RequisicaoABD = new List<ObraViewModel>();
            var obras = _requisicaoCompraRepository.BuscarObraRequisicao(idRequisicao);
            retorno.RequisicaoABD = _mapper.Map<List<ObraViewModel>>(obras);

            return retorno;
        }

        public List<RequisicaoComprasViewModel> ObterRequisicoes(int idUsuario)
        {
            var usuario = _usuarioRepository.Obter(idUsuario);
            return _mapper.Map<List<RequisicaoComprasViewModel>>(_requisicaoCompraRepository.ObterRequisicoes(usuario));
        }

        public List<ServicoViewModel> ObterServicos()
        {
            return _mapper.Map<List<ServicoViewModel>>(_servicoRepository.ObterServico().Where(x => x.TipoServico.Equals("2.23") || x.TipoServico.Equals("3.10") || x.TipoServico.Equals("2.04")).ToList());
        }

        public List<ParcelaRequisicaoViewModel> ObterParcelasRequisicao(string idRequisicao)
        {
            return _mapper.Map<List<ParcelaRequisicaoViewModel>>(_requisicaoCompraRepository.ObterParcelasRequisicao(idRequisicao));
        }

        public int AprovarRequisicao(RequisicaoAprovadaViewModel requisicao)
        {
            if (requisicao.Grupo.Contains("Supervisor"))
            {
                RequisicaoComprasViewModel requisicaoCompra = ObterRequisicaoPorId(requisicao.Id);

                if (requisicaoCompra != null && !_requisicaoCompraRepository.VerificarExistenciaNotaFornecedor(requisicaoCompra.Nota, requisicaoCompra.Fornecedor != null && requisicaoCompra.Fornecedor.Tipo.Equals("J") ? requisicaoCompra.Fornecedor.CGC : requisicaoCompra.Fornecedor.Cpf))
                {
                    return _requisicaoCompraRepository.AprovarSupervisorRequisicao(requisicao.Id, requisicao.Aprovador, requisicao.IdAprovador);
                }
            }
            else if (requisicao.Grupo.Contains("Diretor"))
            {
                return _requisicaoCompraRepository.AprovarGerenteRequisicao(requisicao.Id, requisicao.Aprovador, requisicao.IdAprovador);
            }

            return requisicao.Id;
        }

        public int ReprovarRequisicao(RequisicaoAprovadaViewModel requisicao)
        {
            if (requisicao.Grupo.Contains("Supervisor"))
                return _requisicaoCompraRepository.ReprovarSupervisorRequisicao(requisicao.Id, requisicao.Aprovador, requisicao.IdAprovador);
            else if (requisicao.Grupo.Contains("Diretor"))
                return _requisicaoCompraRepository.ReprovarGerenteRequisicao(requisicao.Id, requisicao.Aprovador, requisicao.IdAprovador);
            else
                return requisicao.Id;
        }

        public int IncluirRequisicao(RequisicaoGerada requisicao, bool elvis)
        {
            return _requisicaoCompraRepository.InsertRequisicao(requisicao, elvis);
        }

        public void AtualizaRequisicao(RequisicaoAtualizada requisicao)
        {
            _requisicaoCompraRepository.UpdateRequisicao(requisicao);
        }

        public int IncluirRequisicaoObra(int requisicao, string obra, decimal valor)
        {
            return _requisicaoCompraRepository.InsertRequisicaoObra(requisicao, obra, valor);
        }

        public int IncluirRequisicaoServico(int requisicao, string servico)
        {
            return _requisicaoCompraRepository.InsertRequisicaoServico(requisicao, servico);
        }

        public int GerarRequisicao(RequisicaoGerada requisicao)
        {
            List<RequisicaoObra> requisicaoObra = new List<RequisicaoObra>();

            var requisicaoGerada = IncluirRequisicao(requisicao, false);

            if (requisicaoGerada > 0)
            {
                if (requisicao.Parcelas != null)
                {
                    foreach (ParcelaRequisicao parcela in requisicao.Parcelas)
                    {
                        _requisicaoCompraRepository.InsertParcelaRequisicao(requisicaoGerada.ToString(), parcela);
                    }
                }
                var email = new EmailViewModel();
                string path = String.Concat(AppDomain.CurrentDomain.BaseDirectory, @"CorpoHtml\RequisicaoCompras\CorpoRequisicaoGeradaCompras.html");
                String corpoEmail = File.ReadAllText(path, Encoding.UTF8);

                corpoEmail = corpoEmail.Replace("__emissao__", DateTime.Now.ToString());
                corpoEmail = corpoEmail.Replace("__solicitante__", requisicao.Usuario.Nome);
                corpoEmail = corpoEmail.Replace("__requisicao__", requisicaoGerada.ToString());
                corpoEmail = corpoEmail.Replace("__fornecedor__", requisicao.Fornecedor.Nome);
                corpoEmail = corpoEmail.Replace("__valor__", requisicao.ValorTotal.ToString("C3", CultureInfo.CurrentCulture));

                email.Assunto = "Requisição N°" + requisicaoGerada.ToString();
                email.Destinatario = requisicao.Usuario.Email;
                email.CorpoEmail = corpoEmail;

                _emailService.EnviarEmail(email);

                IncluirRequisicaoServico(requisicaoGerada, requisicao.Servico);

                foreach (RequisicaoElvis requisicaoABD in requisicao.RequisicaoABD)
                {
                    requisicaoObra = _requisicaoABDRepository.ObterRequisicaoObra(requisicaoGerada, requisicaoABD.Obra);

                    if (requisicaoObra.Count == 0)
                    {
                        _requisicaoABDRepository.InsertRequisicaoObra(requisicaoGerada, requisicaoABD.Obra, requisicaoABD.Valor);
                    }
                    else
                    {
                        _requisicaoABDRepository.UpdateRequisicaoObra(requisicaoGerada, requisicaoABD.Obra, requisicaoABD.Valor);
                    }
                }
            }

            return requisicaoGerada;
        }

        public void UpdateRequisicao(RequisicaoAtualizada requisicao)
        {
            List<RequisicaoObra> requisicaoObra = new List<RequisicaoObra>();

            AtualizaRequisicao(requisicao);

            IncluirRequisicaoServico(requisicao.IdRequisicao, requisicao.Servico);
            _requisicaoABDRepository.ExcluirRequisicaoObra(requisicao.IdRequisicao);

            foreach (RequisicaoElvis requisicaoABD in requisicao.RequisicaoABD)
            {
                _requisicaoABDRepository.InsertRequisicaoObra(requisicao.IdRequisicao, requisicaoABD.Obra, requisicaoABD.Valor);
            }
        }

        public void UpdateParcelasRequisicao(string requisicaoId, List<ParcelaRequisicao> parcelas)
        {
            if (parcelas.Count > 0)
            {
                _requisicaoCompraRepository.DeleteParcelasRequisicao(requisicaoId);

                foreach (ParcelaRequisicao parcela in parcelas)
                {
                    _requisicaoCompraRepository.InsertParcelaRequisicao(requisicaoId, parcela);
                }
            }
        }

        public int AtualizarRequisicao(int id, string link)
        {
            return _requisicaoCompraRepository.AtualizarRequisicao(id, link);
        }

        public int ExcluirRequisicao(RequisicaoExcluidaViewModel requisicao)
        {

            RequisicaoComprasViewModel req = ObterRequisicaoPorId(requisicao.Id);
            EmailViewModel email = new EmailViewModel();
            Usuario usuarioSolicitante = _usuarioRepository.Obter(req.IdSolicitante);

            string path = string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"CorpoHtml\RequisicaoElvis\CorpoRequisicaoExcluida.html");
            string corpoEmail = File.ReadAllText(path, Encoding.UTF8);

            corpoEmail = corpoEmail.Replace("__exclusor__", requisicao.Usuario.Nome);
            corpoEmail = corpoEmail.Replace("__requisicao__", requisicao.Id.ToString());
            corpoEmail = corpoEmail.Replace("__fornecedor__", req.Fornecedor.Nome);
            corpoEmail = corpoEmail.Replace("__emissao__", req.Incluido.ToString());
            corpoEmail = corpoEmail.Replace("__prazo__", req.PrazoPagamento.ToString());
            corpoEmail = corpoEmail.Replace("__nota__", req.Nota);
            corpoEmail = corpoEmail.Replace("__valor__", req.Valor.ToString("C3", CultureInfo.CurrentCulture));
            corpoEmail = corpoEmail.Replace("__motivo__", requisicao.Descricao);

            email.Assunto = "Requisição Excluida N°" + requisicao.Id;
            email.Destinatario = usuarioSolicitante.Email + "; " + requisicao.Usuario.Email;
            email.CorpoEmail = corpoEmail;
            _emailService.EnviarEmail(email);


            return _requisicaoCompraRepository.ExcluirRequisicao(requisicao.Id, requisicao.Usuario.Nome, requisicao.Descricao);
        }

        public List<CentroCustoViewModel> ObterCentrosDeCusto()
        {
            return _mapper.Map<List<CentroCustoViewModel>>(_requisicaoCompraRepository.ObterCentrosDeCusto());
        }

        public List<ObraViewModel> ObterObras(string Nome)
        {
            return _mapper.Map<List<ObraViewModel>>(_requisicaoCompraRepository.ObterObras(Nome));
        }

        public object ObterRequisicaoGeradaPorNota(string nota, int id)
        {
            var requisicoes = _mapper.Map<List<RequisicaoObra>>(_requisicaoCompraRepository.ObterRequisicaoGeradaPorNota(nota));
            var requisicoesGroup = requisicoes.GroupBy(x => x.RequisicaoId).Select(
            c => new
            {
                ValorTotal = c.Sum(y => y.Total),
            })
            .ToList();

            return new
            {
                requisicoes,
                requisicoesGroup
            };
        }

        public void CancelaRequisicao(int idRequisicao, int idUsuario)
        {
            _requisicaoCompraRepository.CancelarRequisicao(idRequisicao, idUsuario);
        }
    }
}
