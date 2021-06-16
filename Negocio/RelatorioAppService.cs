using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using static CoBRA.Domain.Enum.Enum;

namespace CoBRA.Application
{
    public class RelatorioAppService : IRelatorioAppService
    {
        private readonly IMapper _mapper;
        private readonly IRelatorioRepository _relatorioRepository;
        private readonly IRelatorioProtheusRepository _relatorioProtheusRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public RelatorioAppService(IMapper mapper, IRelatorioRepository relatorioRepository, IRelatorioProtheusRepository relatorioProtheusRepository, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _relatorioRepository = relatorioRepository;
            _relatorioProtheusRepository = relatorioProtheusRepository;
            _usuarioRepository = usuarioRepository;
        }

        public object ObterRelatorioComissao(int? qntMes, int idEstado, int idUsuario, int idHierarquia, string codDivulgador, DateTime? dataInicio, DateTime? dataFim, string idDivulgador)
        {
            List<ComissaoViewModel> listaComissao = new List<ComissaoViewModel>();

            if (idHierarquia == (int)Hierarquia.GerenteComercial || idHierarquia == (int)Hierarquia.SupervisorComercial || idHierarquia == (int)Hierarquia.Diretoria)
            {
                listaComissao = _mapper.Map<List<ComissaoViewModel>>(_relatorioRepository.Comissao(qntMes, idEstado, idUsuario, idHierarquia, codDivulgador, dataInicio, dataFim));
            }
            else if (idHierarquia == (int)Hierarquia.Divulgador)
            {
                listaComissao = _mapper.Map<List<ComissaoViewModel>>(_relatorioRepository.Comissao(qntMes, idEstado, idUsuario, idHierarquia, idDivulgador));
            }
            
            return new
            {
                relatorio = listaComissao
            };
        }

        public object ObterRelatorioComissaoDetalhes(int? qntMes, int idEstado, int idHierarquia, string codDivulgador, DateTime? dataInicio = null, DateTime? dataFim = null, int idDivulgador = 0)
        {
            List<ComissaoViewModel> relatorio = new List<ComissaoViewModel>();

            if ((idHierarquia == (int)Hierarquia.GerenteComercial || idHierarquia == (int)Hierarquia.SupervisorComercial || idHierarquia == (int)Hierarquia.Diretoria) && !String.IsNullOrEmpty(codDivulgador))
            {
                relatorio = _mapper.Map<List<ComissaoViewModel>>(_relatorioRepository.ComissaoDetalhes(qntMes, idEstado, codDivulgador, dataInicio, dataFim));
            }
            else if (idDivulgador > 0)
            {
                var usuario = _usuarioRepository.Obter(idDivulgador);

                relatorio = _mapper.Map<List<ComissaoViewModel>>(_relatorioRepository.ComissaoDetalhes(qntMes, idEstado, usuario.CodUsuario, dataInicio, dataFim));
            }

            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("pt");

            var totalizadorMensal = relatorio
                                   .OrderBy(x => x.DataPagamento)
                                   .GroupBy(x => x.DataPagamento.Month)
                                   .Select(c => new
                                   {
                                       MesDescricao = cultureInfo.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(c.Key)),
                                       ValorNF = c.FirstOrDefault().ValorNF,
                                       ValorRecebido = c.Sum(y => y.ValorRecebido),
                                       ValorReceber = c.Sum(y => y.ValorReceber),
                                       PagoComissao = c.Sum(y => y.PagoComissao),
                                       AReceberComissao = c.Sum(y => y.AReceberComissao)
                                   }).ToList();


            return new {
                totalizadorMensal,
                relatorio
            };
        }


        public object ObterRelatorioComissaoProtheus(int? qntMes, int idEstado, int idUsuario, int idHierarquia, string codDivulgador, DateTime? dataInicio, DateTime? dataFim, string idDivulgador) {
            List<ComissaoViewModel> relatorioComissao = new List<ComissaoViewModel>();

            if (idHierarquia == (int)Hierarquia.GerenteComercial || idHierarquia == (int)Hierarquia.SupervisorComercial || idHierarquia == (int)Hierarquia.Diretoria) {

                relatorioComissao = _mapper.Map<List<ComissaoViewModel>>(_relatorioProtheusRepository.Comissao(qntMes, idEstado, idUsuario, idHierarquia, codDivulgador, dataInicio, dataFim));

            } else if (idHierarquia == (int)Hierarquia.Divulgador) {

                relatorioComissao = _mapper.Map<List<ComissaoViewModel>>(_relatorioProtheusRepository.Comissao(qntMes, idEstado, idUsuario, idHierarquia, idDivulgador));

            }

            return relatorioComissao;


        }

        public object ObterRelatorioComissaoDetalhesProtheus(int? qntMes, int idEstado, int idHierarquia, string codDivulgador, DateTime? dataInicio = null, DateTime? dataFim = null, int idDivulgador = 0) {

            List<ComissaoViewModel> relatorio = new List<ComissaoViewModel>();

            if (( idHierarquia == (int)Hierarquia.GerenteComercial || idHierarquia == (int)Hierarquia.SupervisorComercial || idHierarquia == (int)Hierarquia.Diretoria ) && !String.IsNullOrEmpty(codDivulgador)) {

                relatorio = _mapper.Map<List<ComissaoViewModel>>(_relatorioProtheusRepository.ComissaoDetalhes(qntMes, idEstado, codDivulgador, dataInicio, dataFim));
                

            } else if (idDivulgador > 0) {
                var usuario = _usuarioRepository.Obter(idDivulgador);
                
                relatorio = _mapper.Map<List<ComissaoViewModel>>(_relatorioProtheusRepository.ComissaoDetalhes(qntMes, idEstado, usuario.CodUsuario, dataInicio, dataFim));
                
            }

            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("pt");

            var totalizadorMensal = relatorio
                                   .OrderBy(x => x.DataPagamento)
                                   .GroupBy(x => x.DataPagamento.Month )
                                   .Select(c => new {
                                       MesDescricao = cultureInfo.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(c.Key)),
                                       ValorNF = c.Sum(y => y.ValorNF),
                                       ValorRecebido = c.Sum(y => y.ValorRecebido),
                                       //ValorReceber = c.Sum(y => y.ValorReceber),
                                       //PagoComissao = c.Sum(y => y.PagoComissao),
                                       AReceberComissao = c.Sum(y => y.AReceberComissao)
                                   }).ToList();
            
            //var totalizadorMensal = relatorio
            //                       //.OrderBy(x => x.DataPagamento)
            //                       .GroupBy(x => x.CodDivulgador)
            //                       .Select(c => new {
            //                           //MesDescricao = cultureInfo.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName()),
            //                           ValorNF = c.Sum(y => y.ValorNF),
            //                           ValorRecebido = c.Sum(y => y.ValorRecebido),
            //                           PagoComissao = c.Sum(y => y.PagoComissao),
            //                       }).ToList();


            return new {
                totalizadorMensal,
                relatorio
            };
        }


        public byte[] GerarExcelRelatorioComissao(int? qntMes, int idEstado, int idHierarquia, string codDivulgador, DateTime? dataInicio = null, DateTime? dataFim = null, int idDivulgador = 0)
        {

            List<ComissaoViewModel> relatorio = new List<ComissaoViewModel>();

            if ((idHierarquia == (int)Hierarquia.GerenteComercial || idHierarquia == (int)Hierarquia.SupervisorComercial || idHierarquia == (int)Hierarquia.Diretoria) && !String.IsNullOrEmpty(codDivulgador))
            {
                relatorio = _mapper.Map<List<ComissaoViewModel>>(_relatorioRepository.ComissaoDetalhes(qntMes, idEstado, codDivulgador, dataInicio, dataFim));
            }
            else if (idDivulgador > 0)
            {
                var usuario = _usuarioRepository.Obter(idDivulgador);

                relatorio = _mapper.Map<List<ComissaoViewModel>>(_relatorioRepository.ComissaoDetalhes(qntMes, idEstado, usuario.CodUsuario, dataInicio, dataFim));
            }

            var csv = new StringBuilder();
            string linha = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12}", 
            "CLIENTE", "COD_PROMOCIONAL", "NF", "RATEIO", "EMISSAO", "TIPO_VENDA", "PARCELA", 
            "FORMATO", "VALOR_RECEBIDO", "VALOR_RECEBER", "PAGO_COMISSAO", "RECEBER_COMISSAO", "TRANSFERENCIA");

            csv.AppendLine(linha);
            foreach (var item in relatorio)
            {
                string cliente = item.Cliente;
                string codPromocional = item.CodPromocional;
                string notaFiscal = item.NF;
                string rateio = item.Rateio;
                string emissao = item.Emissao.ToString("dd/MM/yyyy");
                string tipoVenda = item.TipoVenda;
                string parcela = item.Parcela;
                string formato = item.Formato;
                string valorRecebido = item.ValorRecebido.ToString();
                string valorReceber = item.ValorReceber.ToString();
                string pagoComissao = item.PagoComissao.ToString();
                string receberComissao = item.AReceberComissao.ToString();
                string transferencia = item.Transferencia;

                linha = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12}",
                cliente, codPromocional, notaFiscal, rateio, emissao, tipoVenda, parcela, formato, valorRecebido, valorReceber, pagoComissao, receberComissao, transferencia);

                csv.AppendLine(linha);
            }

            byte[] arquivo = ASCIIEncoding.ASCII.GetBytes(csv.ToString());

            return arquivo;
        }

        public byte[] GerarExecelRelatorioComissaoProtheus(int? qntMes, int idEstado, int idHierarquia, string codDivulgador, DateTime? dataInicio = null, DateTime? dataFim = null, int idDivulgador = 0) {

            List<ComissaoViewModel> relatorio = new List<ComissaoViewModel>();

            if (( idHierarquia == (int)Hierarquia.GerenteComercial || idHierarquia == (int)Hierarquia.SupervisorComercial || idHierarquia == (int)Hierarquia.Diretoria ) && !String.IsNullOrEmpty(codDivulgador)) {
                
                relatorio = _mapper.Map<List<ComissaoViewModel>>(_relatorioProtheusRepository.ComissaoDetalhes(qntMes, idEstado, codDivulgador, dataInicio, dataFim));
            
            } else if (idDivulgador > 0) {

                var usuario = _usuarioRepository.Obter(idDivulgador);
                
                relatorio = _mapper.Map<List<ComissaoViewModel>>(_relatorioProtheusRepository.ComissaoDetalhes(qntMes, idEstado, usuario.CodUsuario, dataInicio, dataFim));

            }

            var csv = new StringBuilder();

            string linha = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
            "CLIENTE", "COD_PROMOCIONAL", "NF", "EMISSAO", "TIPO_VENDA", "PARCELA",
            "FORMATO", "VALOR_RECEBIDO", "PAGO_COMISSAO", "TRANSFERENCIA");

            csv.AppendLine(linha);
            
            foreach (var item in relatorio) {
                string cliente = item.Cliente;
                string codPromocional = item.CodPromocional;
                string notaFiscal = item.NF;
                //string rateio = item.Rateio;
                string emissao = item.Emissao.ToString("dd/MM/yyyy");
                string tipoVenda = item.TipoVenda;
                string parcela = item.Parcela;
                string formato = item.Formato;
                string valorRecebido = item.ValorRecebido.ToString();
                //string valorReceber = item.ValorReceber.ToString();
                string pagoComissao = item.PagoComissao.ToString();
                //string receberComissao = item.AReceberComissao.ToString();
                string transferencia = item.Transferencia;

                linha = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
                cliente, codPromocional, notaFiscal, emissao, tipoVenda, parcela, formato, valorRecebido, pagoComissao, transferencia);

                csv.AppendLine(linha);
            }

            byte[] arquivo = ASCIIEncoding.ASCII.GetBytes(csv.ToString());

            return arquivo;

        }

    }
}
