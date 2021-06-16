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
    public class RequisicaoElvisAppService : IRequisicaoElvisAppService 
    {
        private readonly IMapper _mapper;
        private readonly IRequisicaoABDRepository _requisicaoABDRepository;
        private readonly IFornecedoresProtheusRepository _fornecedoresProtheusRepository;
        private readonly IServicoRepository _servicoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmailService _emailService;
        private readonly IInicializacaoAppService _inicializacaoAppService;
        private readonly IRegraAppService _regraAppService;

        public RequisicaoElvisAppService(IMapper mapper, IRegraAppService regraAppService, IEmailService emailService, IInicializacaoAppService inicializacaoAppService, IRequisicaoABDRepository requisicaoABDRepository, IServicoRepository servicoRepository, IUsuarioRepository usuarioRepository, IFornecedoresProtheusRepository fornecedoresProtheusRepository)
        {
            _mapper = mapper;
            _requisicaoABDRepository = requisicaoABDRepository;
            _servicoRepository = servicoRepository;
            _usuarioRepository = usuarioRepository;
            _fornecedoresProtheusRepository = fornecedoresProtheusRepository;
            _emailService = emailService;
            _inicializacaoAppService = inicializacaoAppService;
            _regraAppService = regraAppService;
        }

        public int InsertRequisicao(RequisicaoGeradaViewModel requisicao, bool elvis)
        {
            var req = _mapper.Map<RequisicaoGerada>(requisicao);

            return _requisicaoABDRepository.InsertRequisicao(req, elvis);
        }

        public int InsertRequisicaoObra(int requisicao, string obra, decimal valor)
        {
            return _requisicaoABDRepository.InsertRequisicaoObra(requisicao, obra, valor);
        }

        public int InsertRequisicaoServico(int requisicao, string servico)
        {
            return _requisicaoABDRepository.InsertRequisicaoServico(requisicao, servico); ;
        }

        public List<FornecedorViewModel> ObterFornecedores(string nome)
        {
            return _mapper.Map<List<FornecedorViewModel>>(_fornecedoresProtheusRepository.ObterFornecedores(nome));
        }

        public List<RequisicaoObraViewModel> ObterRequisicaoObra(int requisicao, string obra)
        {
            return _mapper.Map<List<RequisicaoObraViewModel>>(_requisicaoABDRepository.ObterRequisicaoObra(requisicao,obra));
        }

        public object ObterRequisicaoPorNota(string nota, int id)
        {
            var requisicoes = _mapper.Map<List<RequisicaoElvisViewModel>>(_requisicaoABDRepository.ObterRequisicaoPorNota(nota));
            var usuario = _usuarioRepository.Obter(id);
            var servicos = ObterServicosPorCentroCusto(usuario.Id);

            var requisicoesGroup = requisicoes.GroupBy(x => x.Nota).Select(
            c => new
            {
                ValorTotal = c.Sum(y => y.Valor),
            })
            .ToList();

            return new
            {
                requisicoes,
                requisicoesGroup,
                servicos
            };
        }

        public object ObterRequisicaoGeradaPorNota(string nota, int id)
        {
            var requisicoes = _mapper.Map<List<RequisicaoElvisViewModel>>(_requisicaoABDRepository.ObterRequisicaoGeradaPorNota(nota));
            var requisicoesGroup = requisicoes.GroupBy(x => x.Nota).Select(
            c => new
            {
                ValorTotal = c.Sum(y => y.Valor),
            })
            .ToList();

            return new
            {
                requisicoes,
                requisicoesGroup
            };
        }

        public object ObterRequisicoes(string tipo)
        {
            var requisicoes = _requisicaoABDRepository.ObterRequisicoes(tipo);

            var requisicoesGroup = requisicoes.GroupBy(x => x.Nota).Select(
                c => new
                {
                    c.First().Nota,
                    ValorTotal = c.Sum(y => y.Valor),
                    c.First().Tipo,
                    c.First().Titulo
                }).ToList();

            return requisicoesGroup;
        }

        public object ObterRequisicoesPorID(int ID)
        {
            var requisicoes = _requisicaoABDRepository.ObterRequisicoesPorID(ID);

            var requisicoesGroup = requisicoes.GroupBy(x => x.Nota).Select(
                c => new
                {
                    c.First().Nota,
                    ValorTotal = c.Sum(y => y.Valor),
                    c.First().Tipo,
                    c.First().Fornecedor
                }).ToList();

            return requisicoesGroup;
        }
        
        public List<ServicoViewModel> ObterServicosPorCentroCusto(int idUsuario)
        {
            Usuario usuario = _usuarioRepository.Obter(idUsuario);

            return _mapper.Map<List<ServicoViewModel>>(_servicoRepository.ObterServicoPorCentroCusto(usuario.Grupo.CentroCusto));
        }

        public int UpdateRequisicaoObra(int requisicao, string obra, decimal valor)
        {
            return _requisicaoABDRepository.UpdateRequisicaoObra(requisicao, obra, valor);
        }

        public int UpdateRequisicaoObraElvis(int contador, int requisicao, int usuarioId)
        {
            return _requisicaoABDRepository.UpdateRequisicaoObraElvis(contador, requisicao, usuarioId);
        }

        public int GerarRequisicao(RequisicaoGeradaViewModel requisicao, bool elvis)
        {
            List<RequisicaoObraViewModel> requisicaoObra = new List<RequisicaoObraViewModel>();

            var req = _mapper.Map<RequisicaoGerada>(requisicao);

            var requisicaoGerada = _requisicaoABDRepository.InsertRequisicao(req, elvis);

            if (requisicaoGerada > 0)
            {
                //Envia email para o supervisor da area.
                var email = new EmailViewModel();
                var regra = new Regra
                {
                    Descricao = ""
                };
                //if (requisicao.Usuario.Grupo.Departamento.ToUpper().Contains("ICONOGRAFIA"))
                //    regra = _regraAppService.ObterRegraPorId(1);
                //else if (requisicao.Usuario.Grupo.Departamento.ToUpper().Equals("DA"))
                //    regra = _regraAppService.ObterRegraPorId(2);
                //else if (requisicao.Usuario.Grupo.Departamento.ToUpper().Contains("ARTE"))
                //    regra = _regraAppService.ObterRegraPorId(3);

                if (regra != null)
                {

                    string path = String.Concat(AppDomain.CurrentDomain.BaseDirectory, @"CorpoHtml\RequisicaoElvis\CorpoRequisicaoGeradaElvis.html");
                    String corpoEmail = File.ReadAllText(path, Encoding.UTF8);
                    
                    corpoEmail = corpoEmail.Replace("__emissao__", DateTime.Now.ToString());
                    corpoEmail = corpoEmail.Replace("__solicitante__", requisicao.Usuario.Nome);
                    corpoEmail = corpoEmail.Replace("__requisicao__", requisicaoGerada.ToString());
                    corpoEmail = corpoEmail.Replace("__fornecedor__", requisicao.Fornecedor.Nome);
                    corpoEmail = corpoEmail.Replace("__valor__", requisicao.ValorTotal.ToString("C3", CultureInfo.CurrentCulture));

                    email.Assunto = "Requisição N°" + requisicaoGerada.ToString();
                    email.Destinatario = regra.Descricao + "; " + requisicao.Usuario.Email;
                    email.CorpoEmail = corpoEmail;
                    _emailService.EnviarEmail(email);
                }



                //Atualiza as tabelas de obra e serviço.
                foreach (RequisicaoElvisViewModel requisicaoABD in requisicao.RequisicaoABD)
                {
                    _requisicaoABDRepository.UpdateRequisicaoObraElvis(requisicaoABD.Contador, requisicaoGerada, requisicao.Usuario.Id);
                }

                _requisicaoABDRepository.InsertRequisicaoServico(requisicaoGerada, requisicao.Servico);

                foreach (RequisicaoElvisViewModel requisicaoABD in requisicao.RequisicaoABD)
                {
                    requisicaoObra = _mapper.Map<List<RequisicaoObraViewModel>>(_requisicaoABDRepository.ObterRequisicaoObra(requisicaoGerada, requisicaoABD.Obra));

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
    }
}
