using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class AcompanhamentoMetaConsultorAppService : IAcompanhamentoMetaConsultorAppService
    {

        private readonly IMapper _mapper;
        private readonly IResultadoPagamentoMetaRepository _resultadoPagamentoMetaRepository;
        private readonly IPainelMetaAnualRepository _painelMetaAnualRepository;
        private readonly IPainelMetaRealRepository _painelMetaRealRepository;
        private readonly IPainelMetaFinanceiraRepository _painelMetaFinanceiraRepository;
        private readonly ICadastroMetaFinanceiraRepository _cadastroMetaFinanceiraRepository;
        private readonly IPainelRepository _painelRepository;

        public AcompanhamentoMetaConsultorAppService(IMapper mapper,
            IPainelMetaFinanceiraRepository painelMetaFinanceiraRepository,
            IPainelMetaAnualRepository painelMetaAnualRepository,
            IResultadoPagamentoMetaRepository resultadoPagamentoMetaRepository,
            IPainelMetaRealRepository painelMetaRealRepository,
            ICadastroMetaFinanceiraRepository cadastroMetaFinanceiraRepository,
            IPainelRepository painelRepository)
        {
            _mapper = mapper;
            _painelMetaAnualRepository = painelMetaAnualRepository;
            _painelMetaFinanceiraRepository = painelMetaFinanceiraRepository;
            _painelMetaRealRepository = painelMetaRealRepository;
            _resultadoPagamentoMetaRepository = resultadoPagamentoMetaRepository;
            _cadastroMetaFinanceiraRepository = cadastroMetaFinanceiraRepository;
            _painelRepository = painelRepository;
        }

        public async Task<AcompanhamentoMetaConsultorViewModel> ObterAcompanhamento(string idUsuario, string idStatus, Guid? PeriodoId)
        {
            Guid? usuario = null;
            Guid? status = null;

            if (!string.IsNullOrEmpty(idUsuario))
            {
                usuario = new Guid(idUsuario);
            }

            if (!string.IsNullOrEmpty(idStatus))
            {
                status = new Guid(idStatus);
            }

            List<PainelMetaAnualViewModel> painelMetasAnuais = CalculoPotencial(null, null, null, null, null, usuario, status, PeriodoId); //var painelIndividual = _painelMetaAnualRepository.GestaoPessoaConsultaAprovacaoMetaAnual(new Guid(id)).Result;
            var painelMetaFinanceira = await _painelMetaFinanceiraRepository.BuscarMetaFinanceiraUsuario(new Guid(idUsuario));
            var dadosPessoa = painelMetasAnuais.LastOrDefault();

            var acompanhamento = new AcompanhamentoMetaConsultorViewModel()
            {
                Id = dadosPessoa?.IdUsuario.ToString(),
                Cargo = dadosPessoa?.Cargo,
                Nome = dadosPessoa?.Nome,
                Nota = "",
                Uf = "Uf",
                ListaLinhaMetaIndividual = _mapper.Map<List<AcompanhamentoMetaConsultorMetaIndividualViewModel>>(_mapper.Map<IEnumerable<PainelMetaAnual>>(painelMetasAnuais)) //painelIndividual
            };

            acompanhamento.ValorPotencialGanho = new ValorPotencialGanhoViewModel();

            if (dadosPessoa != null)
            {
                acompanhamento.ValorPotencialGanho.MetaReal = dadosPessoa.valorRecebimento;
                acompanhamento.ValorPotencialGanho.ReceitaTabela = dadosPessoa.valorMetaReceitaLiquida;
                acompanhamento.ValorPotencialGanho.AtingidoMetaReal = dadosPessoa.realAtingido;
                acompanhamento.ValorPotencialGanho.AtingidoReceitaTabela = dadosPessoa.porcentagemPagamento;
                acompanhamento.ValorPotencialGanho.GanhoMetaReal = dadosPessoa.ganhoReal;
                acompanhamento.ValorPotencialGanho.GanhoReceitaTabela = dadosPessoa.ganhoReceita;
                acompanhamento.ValorPotencialGanho.AntecipadoMetaReal = dadosPessoa.antecipado;
                acompanhamento.ValorPotencialGanho.AntecipadoReceitaTabela = dadosPessoa.antecipado;
                acompanhamento.ValorPotencialGanho.TotalReceberMetaReal = dadosPessoa.totalReceberReal;
                acompanhamento.ValorPotencialGanho.TotalReceberReceitaTabela = dadosPessoa.totalReceberReceita;
            };

            acompanhamento.PesoTotal = acompanhamento.ListaLinhaMetaIndividual.Sum(x => x.Peso);
            acompanhamento.RealAtingido = acompanhamento.ListaLinhaMetaIndividual.Sum(x => Convert.ToDecimal(x.Percentual));
            acompanhamento.ListaLinhaMetaIndividual = acompanhamento.ListaLinhaMetaIndividual.OrderBy(item => item.Indicador).ToList();


            return acompanhamento;
        }

        public async Task<CabecalhoRelatorioPainelViewModel> BuscarDadosCabecalhoRelatorio(Guid idUsuario)
        {
            CabecalhoRelatorioPainel cabecalho = await _painelRepository.BuscarDadosCabecalhoRelatorio(idUsuario);
            int tempoProporcao = CalcularTempoProporcao(cabecalho);

            var viewModel = new CabecalhoRelatorioPainelViewModel
            {
                DataAdmissao = Convert.ToDateTime(cabecalho.DataAdmissao.ToString("dd/MM/yyyy")),
                Periodo = $"{cabecalho.DataInicioCampanha.ToString("dd/MM/yyyy")} - {cabecalho.DataFimCampanha.ToString("dd/MM/yyyy")}",
                NomeUsuario = cabecalho.NomeUsuario,
                Filial = cabecalho.Filial,
                Proporcao = tempoProporcao < 0 ? $"12 / 12 avos" : $"{tempoProporcao} / 12 avos",
                Cargo = cabecalho.Cargo,
                RegiaoAtuacao = await _painelMetaAnualRepository.BuscarRegiaoUsuario(new PainelMetaAnual
                {
                    IdUsuario = idUsuario
                })
            };

            return viewModel;
        }

        private int CalcularTempoProporcao(CabecalhoRelatorioPainel cabecalho)
        {
            DateTime? dataInicio;
            DateTime? dataFim;
            int tempoProporcao = 0;

            if (cabecalho.DataAdmissao < cabecalho.DataInicioCampanha)
                tempoProporcao = 12;

            else
            {

                if (cabecalho.DataDemissao == DateTime.MinValue)
                {
                    tempoProporcao = AjustaMesAno(cabecalho.DataFimCampanha) - AjustaMesAno(cabecalho.DataAdmissao);
                }
                else
                {
                    tempoProporcao = AjustaMesAno(cabecalho.DataDemissao) - AjustaMesAno(cabecalho.DataInicioCampanha);
                }


            }

            dataInicio = cabecalho.DataInicioCampanha;
            dataFim = cabecalho.DataAdmissao;

            if (dataFim.Value.Day <= 14)
            {
                tempoProporcao++;
            }

            return tempoProporcao;
        }

        private int AjustaMesAno(DateTime? d)
        {
            return d.Value.Year * 12 + d.Value.Month;
        }

        public List<PainelMetaAnualViewModel> CalculoPotencial(Guid? MetaAnualId, Guid? MetaId, Guid? LinhaMetaId, Guid? GrupoPainelId, Guid? CargoId, Guid? UsuarioId, Guid? StatusId, Guid? PeriodoId)
        {
            int pesoTotal = 0;
            decimal? realAtingido = 0;

            List<PainelMetaAnualViewModel> painelMetasAnuais = _mapper.Map<IEnumerable<PainelMetaAnualViewModel>>(_painelMetaAnualRepository.ConsultaPainelMetaAnual(MetaAnualId, MetaId, LinhaMetaId, GrupoPainelId, CargoId, UsuarioId, StatusId, PeriodoId).Result).ToList();

            var nome = "";

            foreach (PainelMetaAnualViewModel painelMetaAnual
                in painelMetasAnuais)
            {
                if (nome == "")
                {
                    nome = painelMetaAnual.Nome;
                }
                else if (nome != painelMetaAnual.Nome)
                {
                    realAtingido = 0;
                    pesoTotal = 0;
                    nome = painelMetaAnual.Nome;
                }

                try
                {
                    pesoTotal += painelMetaAnual.Peso;

                    painelMetaAnual.valorMinimoPercentual = painelMetaAnual.ValorMinimo > 0 && painelMetaAnual.Total > 0
                                ? painelMetaAnual.ValorMinimo * (Convert.ToDecimal(painelMetaAnual.Total) / 100)
                                : 0;
                    painelMetaAnual.valorMaximoPercentual = painelMetaAnual.ValorMaximo > 0 && painelMetaAnual.Total > 0
                                ? painelMetaAnual.ValorMaximo * (Convert.ToDecimal(painelMetaAnual.Total) / 100)
                                : 0;

                    List<PainelMetaRealViewModel> painelMetasReais = _mapper.Map<IEnumerable<PainelMetaRealViewModel>>(_painelMetaRealRepository.ConsultaPainelMetaReal(new Guid(), painelMetaAnual.IdUsuario, painelMetaAnual.IdLinhaMeta).Result).ToList();

                    foreach (PainelMetaRealViewModel painelMetaReal in painelMetasReais)
                    {
                        painelMetaAnual.realizado = painelMetaAnual.realizado == null ? 0 : painelMetaReal.Realizado;
                        painelMetaAnual.realizadoPercentual = painelMetaReal.Realizado;
                        painelMetaAnual.ponderado = painelMetaReal.Realizado;

                        if (!painelMetaAnual.IdUnidadeMedida.Equals("6E355000-98CE-406A-B158-91E9602CB6FD"))
                        {
                            painelMetaAnual.realizadoPercentual = Convert.ToDecimal(painelMetaAnual.Total) > 0 ? (Convert.ToDecimal(painelMetaReal.Realizado) / Convert.ToDecimal(painelMetaAnual.Total)) * 100 : 0;
                            painelMetaAnual.ponderado = painelMetaAnual.realizadoPercentual / 100;
                            painelMetaAnual.valorMinimoPercentual = painelMetaAnual.ValorMinimo > 0 && painelMetaAnual.Total > 0
                                ? painelMetaAnual.ValorMinimo * (Convert.ToDecimal(painelMetaAnual.Total) / 100)
                                : 0;
                            painelMetaAnual.valorMaximoPercentual = painelMetaAnual.ValorMaximo > 0 && painelMetaAnual.Total > 0
                                ? painelMetaAnual.ValorMaximo * (Convert.ToDecimal(painelMetaAnual.Total) / 100)
                                : 0;
                        }

                        if (painelMetaAnual.Indicador.Equals("Divulgação de Livro do Mestre") || painelMetaAnual.Indicador.Equals("Desconto Médio"))
                        {
                            if (painelMetaAnual.realizadoPercentual <= 100)
                            {
                                painelMetaAnual.ponderado = 1;
                            }
                            else if (painelMetaAnual.realizadoPercentual > 100)
                            {
                                painelMetaAnual.ponderado = 0;
                            }
                        }
                        else
                        {
                            if (painelMetaAnual.realizadoPercentual < painelMetaAnual.ValorMinimo)
                            {
                                painelMetaAnual.ponderado = 0;
                            }
                            else if (painelMetaAnual.realizadoPercentual > painelMetaAnual.ValorMaximo)
                            {
                                painelMetaAnual.ponderado = painelMetaAnual.ValorMaximo / 100;
                            }
                        }

                        painelMetaAnual.ponderado = painelMetaAnual.ponderado;
                        painelMetaAnual.percentual = painelMetaAnual.realizado * painelMetaAnual.Peso;



                        realAtingido += painelMetaAnual.percentual;

                        if (realAtingido >= 50)
                        {
                            painelMetaAnual.realAtingido = realAtingido;

                            if (realAtingido >= 139)
                            {
                                painelMetaAnual.realAtingido = 140;
                            }

                            int RealAtingido = painelMetaAnual.realAtingido.ToString().Substring(0, painelMetaAnual.realAtingido.ToString().Length - 3) == "" ? 0 : Convert.ToInt32(painelMetaAnual.realAtingido.ToString().Substring(0, painelMetaAnual.realAtingido.ToString().Length - 3));

                            ResultadoPagamentoMetaViewModel resultadoPagamentoMeta = _mapper.Map<ResultadoPagamentoMetaViewModel>(_resultadoPagamentoMetaRepository.ConsultarResultadoPagamentoMeta(RealAtingido).Result);

                            painelMetaAnual.porcentagemPagamento = resultadoPagamentoMeta != null ? resultadoPagamentoMeta.PorcentagemPagamento : 0;
                        }

                    }

                    MetaFinanceiraViewModel metafinanceira = _mapper.Map<MetaFinanceiraViewModel>(_cadastroMetaFinanceiraRepository.ConsultarCadastroMetaFinanceira(painelMetaAnual.IdUsuario, new Guid()).Result);

                    if (metafinanceira != null)
                    {
                        painelMetaAnual.valorRecebimento = Convert.ToDecimal(metafinanceira.ValorRecebimento);

                        var dif = (Convert.ToDecimal(metafinanceira.MetaReceitaLiquidaCalc)
                            / Convert.ToDecimal(metafinanceira.MetaReceitaLiquida))
                            * Convert.ToDecimal(metafinanceira.ValorRecebimento);
                        if ((Convert.ToDecimal(metafinanceira.MetaReceitaLiquidaCalc)
                            / Convert.ToDecimal(metafinanceira.MetaReceitaLiquida) > 1))
                        {
                            painelMetaAnual.valorRecebimentoCalculado = dif;
                        }
                        else
                        {
                            painelMetaAnual.valorRecebimentoCalculado = Convert.ToDecimal(metafinanceira.MetaReceitaLiquida);
                        }

                        painelMetaAnual.valorMetaReceitaLiquida = Convert.ToDecimal(metafinanceira.MetaReceitaLiquida);

                        int porcentagemMinimaPagamento = _painelRepository.BuscarPorcentagemMinimaPagamento();

                        if (realAtingido <= porcentagemMinimaPagamento)
                            painelMetaAnual.ganhoReal = 0;
                        else
                            painelMetaAnual.ganhoReal = (metafinanceira.ValorRecebimento * _painelRepository.BuscarPorcentagemPagamento(realAtingido)) / 100;


                        painelMetaAnual.ganhoReceita = (Convert.ToDecimal(metafinanceira.ValorRecebimento) * Convert.ToDecimal(painelMetaAnual.porcentagemPagamento)) / 100;
                        painelMetaAnual.antecipado = (Convert.ToDecimal(metafinanceira.ValorRecebimento) * 40) / 100;
                        painelMetaAnual.totalReceberReal = painelMetaAnual.ganhoReal - painelMetaAnual.antecipado;
                        painelMetaAnual.totalReceberReceita = painelMetaAnual.ganhoReceita - painelMetaAnual.antecipado;
                        painelMetaAnual.valorMetaReceitaLiquidaAtualizada = Convert.ToDecimal(metafinanceira.MetaReceitaLiquidaCalc);
                    }


                    painelMetaAnual.porcentagemPagamento = painelMetaAnual.porcentagemPagamento;
                    painelMetaAnual.pesoTotal = pesoTotal;
                    painelMetaAnual.realAtingido = realAtingido;
                    decimal value;

                    painelMetaAnual.Total = painelMetaAnual.Total;

                    if (painelMetaAnual.realizado == null)
                    {
                        painelMetaAnual.realizado = 0;
                    }

                    painelMetaAnual.percentual = painelMetaAnual.percentual == null ? 0 : painelMetaAnual.percentual;
                    painelMetaAnual.ponderado = painelMetaAnual.ponderado == null ? 0 : painelMetaAnual.ponderado;

                    painelMetasAnuais = painelMetasAnuais.OrderBy(item => item.Indicador).ToList();


                    if (painelMetaAnual.realizado < painelMetaAnual.ValorMinimo)
                    {
                        painelMetaAnual.ponderado = 0;
                    }
                    else if (painelMetaAnual.realizado < painelMetaAnual.ValorMaximo)
                    {
                        if (painelMetaAnual.ValorMaximo != 0 && painelMetaAnual.Total != 0)
                        {
                            painelMetaAnual.ponderado = painelMetaAnual.ValorMaximo / painelMetaAnual.Total;
                        }
                        else
                        {
                            painelMetaAnual.ponderado = 0;
                        }
                    }
                    else
                    {
                        if (painelMetaAnual.realizado != 0 && painelMetaAnual.Total != 0)
                        {
                            painelMetaAnual.ponderado = painelMetaAnual.realizado / painelMetaAnual.Total;
                        }
                        else
                        {
                            painelMetaAnual.ponderado = 0;
                        }
                    }
                }

                catch (Exception ex)
                {
                    return null;
                }
            }

            foreach (var item in painelMetasAnuais)
            {
                item.pesoTotal = painelMetasAnuais.Sum(a => a.Peso);
                item.realAtingido = painelMetasAnuais.Sum(a => a.percentual);
            }

            return painelMetasAnuais;
        }

        public PainelMetaAnualGrupoViewModel CalculoPotencialGrupo(Guid? GrupoId, Guid? CargoId, Guid? PeriodoId)
        {
            List<PainelMetaAnualViewModel> painelMetasAnuais = CalculoPotencial(null, null, null, GrupoId, CargoId, null, null, PeriodoId);

            var pmag = new PainelMetaAnualGrupoViewModel();
            var nome = "";

            if (painelMetasAnuais.Count > 0)
            {
                pmag.Cargo = painelMetasAnuais[0].Cargo;
                pmag.RegiaoAtuacao = painelMetasAnuais[0].RegiaoAtuacao;
                pmag.Filial = painelMetasAnuais[0].Filial;
                pmag.Periodo = DateTime.Now.Month == 6 ? "01/06/" + DateTime.Now.Year.ToString() + " a 31/05/" + DateTime.Now.AddYears(1).Year.ToString() :
                    "01/06/" + DateTime.Now.AddYears(-1).Year.ToString() + " a 31/05/" + DateTime.Now.Year.ToString();

                pmag.LstMetasGrupo = new List<MetasGrupoViewModel>();
                int pt = 0;
                foreach (var item in painelMetasAnuais.Where(a => a.Nome == painelMetasAnuais[0].Nome))
                {
                    MetasGrupoViewModel metas = new MetasGrupoViewModel
                    {
                        IdMeta = item.IdMeta,
                        Indicador = item.Indicador,
                        Meta = item.Meta,
                        Peso = item.Peso
                    };
                    pt = pt + item.Peso;

                    pmag.LstMetasGrupo.Add(metas);
                }

                foreach (var item in pmag.LstMetasGrupo)
                {
                    item.PesoTotal = pt;
                }

                pmag.LstUsuarioGrupo = new List<UsuarioGrupoViewModel>();
                decimal? ptd = 0;
                foreach (var item in painelMetasAnuais)
                {
                    if (string.IsNullOrEmpty(nome) || nome != item.Nome)
                    {
                        var novoCli = new UsuarioGrupoViewModel
                        {
                            Nome = item.Nome,
                            DataAdmissao = item.DataAdmissao,
                            DataDemissao = item.DataDemissao,
                            Proporcao = item.Proporcao,
                            Regiao = item.RegiaoAtuacao,
                            LstUsuarioMetasGrupo = new List<UsuarioMetasGrupoViewModel>()
                        };

                        UsuarioMetasGrupoViewModel ug = new UsuarioMetasGrupoViewModel
                        {
                            idMeta = item.IdMeta,
                            ValorMinimo = item.ValorMinimo,
                            Total = item.Total,
                            ValorMaximo = item.ValorMaximo,
                            realizado = item.realizado,
                            ponderado = item.ponderado,
                            percentual = item.percentual == null ? 0 : item.percentual,
                            realAtingido = item.realAtingido,
                            valorRecebimento = item.valorRecebimento,
                            valorMetaReceitaLiquida = item.valorMetaReceitaLiquida,
                            valorMetaReceitaLiquidaAtualizada = item.valorMetaReceitaLiquidaAtualizada,
                            porcentagemPagamento = item.porcentagemPagamento,
                            ganhoReal = item.ganhoReal,
                            ganhoReceita = item.ganhoReceita,
                            antecipado = item.antecipado,
                            totalReceberReal = item.totalReceberReal,
                            totalReceberReceita = item.totalReceberReceita,
                            UnidadeMedida = item.UnidadeMedida,
                            PesoTotal = item.percentual == null ? 0 : item.percentual,
                            valorMaximoPercentual = item.valorMaximoPercentual,
                            valorMinimoPercentual = item.valorMinimoPercentual,
                            valorRecebimentoCalculado = item.valorRecebimentoCalculado
                        };
                        ptd = item.percentual == null ? 0 : item.percentual;
                        novoCli.LstUsuarioMetasGrupo.Add(ug);

                        nome = item.Nome;
                        pmag.LstUsuarioGrupo.Add(novoCli);
                    }
                    else
                    {
                        UsuarioMetasGrupoViewModel ug = new UsuarioMetasGrupoViewModel
                        {
                            idMeta = item.IdMeta,
                            ValorMinimo = item.ValorMinimo,
                            Total = item.Total,
                            ValorMaximo = item.ValorMaximo,
                            realizado = item.realizado,
                            ponderado = item.ponderado,
                            percentual = item.percentual == null ? 0 : item.percentual,
                            realAtingido = item.realAtingido,
                            valorRecebimento = item.valorRecebimento,
                            valorMetaReceitaLiquida = item.valorMetaReceitaLiquida,
                            valorMetaReceitaLiquidaAtualizada = item.valorMetaReceitaLiquidaAtualizada,
                            porcentagemPagamento = item.porcentagemPagamento,
                            ganhoReal = item.ganhoReal,
                            ganhoReceita = item.ganhoReceita,
                            antecipado = item.antecipado,
                            totalReceberReal = item.totalReceberReal,
                            totalReceberReceita = item.totalReceberReceita,
                            UnidadeMedida = item.UnidadeMedida
                        };
                        ptd = ptd + (item.percentual == null ? 0 : item.percentual);
                        ug.PesoTotal = ptd;
                        ug.valorMaximoPercentual = item.valorMaximoPercentual;
                        ug.valorMinimoPercentual = item.valorMinimoPercentual;
                        pmag.LstUsuarioGrupo.Where(a => a.Nome == item.Nome).FirstOrDefault().LstUsuarioMetasGrupo.Add(ug);
                        pmag.LstUsuarioGrupo.Where(a => a.Nome == item.Nome).FirstOrDefault().LstUsuarioMetasGrupo[0].realAtingido = item.realAtingido;
                        pmag.LstUsuarioGrupo.Where(a => a.Nome == item.Nome).FirstOrDefault().LstUsuarioMetasGrupo[0].porcentagemPagamento = item.porcentagemPagamento;
                        pmag.LstUsuarioGrupo.Where(a => a.Nome == item.Nome).FirstOrDefault().LstUsuarioMetasGrupo[0].ganhoReal = item.ganhoReal;
                        pmag.LstUsuarioGrupo.Where(a => a.Nome == item.Nome).FirstOrDefault().LstUsuarioMetasGrupo[0].ganhoReceita = item.ganhoReceita;
                        pmag.LstUsuarioGrupo.Where(a => a.Nome == item.Nome).FirstOrDefault().LstUsuarioMetasGrupo[0].totalReceberReal = item.totalReceberReal;
                        pmag.LstUsuarioGrupo.Where(a => a.Nome == item.Nome).FirstOrDefault().LstUsuarioMetasGrupo[0].totalReceberReceita = item.totalReceberReceita;
                    }
                }
            }

            return pmag;
        }
    }
}
