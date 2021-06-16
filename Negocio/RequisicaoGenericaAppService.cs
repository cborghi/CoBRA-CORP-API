using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using CoBRA.Infra.CrossCutting.EmailService.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CoBRA.Application
{
    public class RequisicaoGenericaAppService : IRequisicaoGenericaAppService
    {
        private readonly IMapper _mapper;
        private readonly IRequisicaoGenericaRepository _requisicaoGenericaRepository;
        private readonly IEmailService _emailService;

        public RequisicaoGenericaAppService(IMapper mapper, IRequisicaoGenericaRepository requisicaoGenericaRepository, IEmailService emailService)
        {
            _mapper = mapper;
            _requisicaoGenericaRepository = requisicaoGenericaRepository;
            _emailService = emailService;
        }

        public object ObterRequisicoes()
        {
            List<RequisicaoGenericaViewModel> requisicoes = _mapper.Map<List<RequisicaoGenericaViewModel>>(_requisicaoGenericaRepository.ObterRequisicoes());

            var requisicoesGroup = requisicoes.GroupBy(x => x.Doc).Select(
                c => new
                {
                    c.First().Doc,
                    ValorTotal = c.Sum(y => y.Valor),
                    c.First().TipoDoc
                }).ToList();

            return requisicoesGroup;
        }

        public int GerarRequisicao(RequisicaoGenericaViewModel requisicao)
        {
            var req = _mapper.Map<RequisicaoGenerica>(requisicao);

            var requisicaoGenerica = _requisicaoGenericaRepository.InserirRequisicao(req);

            if (requisicaoGenerica > 0)
            {
                //var email = new EmailViewModel();
                //var regra = new Regra();

                //regra.Descricao = "";

                //if (regra != null)
                //{

                //    string path = String.Concat(AppDomain.CurrentDomain.BaseDirectory, @"CorpoHtml\RequisicaoElvis\CorpoRequisicaoGenerica.html");
                //    String corpoEmail = File.ReadAllText(path, Encoding.UTF8);
                    
                //    corpoEmail = corpoEmail.Replace("__emissao__", DateTime.Now.ToString());
                //    corpoEmail = corpoEmail.Replace("__solicitante__", requisicao.Usuario.Nome);
                //    corpoEmail = corpoEmail.Replace("__requisicao__", requisicaoGenerica.ToString());
                //    corpoEmail = corpoEmail.Replace("__fornecedor__", requisicao.Fornecedor.Nome);
                //    corpoEmail = corpoEmail.Replace("__valor__", requisicao.ValorTotal.ToString("C3", CultureInfo.CurrentCulture));

                //    email.Assunto = "Requisição N°" + requisicaoGenerica.ToString();
                //    email.Destinatario = regra.Descricao + "; " + requisicao.Usuario.Email;
                //    email.CorpoEmail = corpoEmail;

                //    _emailService.EnviarEmail(email);
                //}

                //_requisicaoGenericaRepository.InserirRequisicao(requisicaoGenerica);

            }

            return requisicaoGenerica;
        }
    }
}
