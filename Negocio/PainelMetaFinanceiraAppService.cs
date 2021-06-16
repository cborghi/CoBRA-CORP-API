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
    public class PainelMetaFinanceiraAppService : IPainelMetaFinanceiraAppService 
    {
        private readonly IMapper _mapper;
        private readonly IPainelMetaFinanceiraRepository _painelMetaFinanceiraRepository;

        public PainelMetaFinanceiraAppService(IMapper mapper, IPainelMetaFinanceiraRepository painelMetaFinanceiraRepository)
        {
            _mapper = mapper;
            _painelMetaFinanceiraRepository = painelMetaFinanceiraRepository;

        }

        public async Task<PainelMetaFinanceiraViewModel> ListarPainelMetaFinanceiraAprovado()
        {
            try
            {
                var dados = await _painelMetaFinanceiraRepository.BuscarMetaFinanceira("Aprovado");
                var consultores = dados.Where(x => "Consultores Comerciais".Equals(x.Grupo));
                var consultoresPrime = dados.Where(x => "Consultores Comerciais Prime".Equals(x.Grupo));
                var gerentes = dados.Where(x => "Gerentes e Supervisores".Equals(x.Grupo));
                return new PainelMetaFinanceiraViewModel()
                {
                    Status = "Aprovado",
                    ListaLinhaConsultor = _mapper.Map<List<MetaFinanceiraViewModel>>(consultores),
                    ListaLinhaConsultorPrime = _mapper.Map<List<MetaFinanceiraViewModel>>(consultoresPrime),
                    ListaLinhaGerente = _mapper.Map<List<MetaFinanceiraViewModel>>(gerentes),
                    ListaLinhaSupervisor = _mapper.Map<List<MetaFinanceiraViewModel>>(gerentes)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<PainelMetaFinanceiraViewModel> ListarPainelMetaFinanceiraPendente()
        {
            try
            {
                var dados = await _painelMetaFinanceiraRepository.BuscarMetaFinanceira("Pendente");
                var consultores = dados.Where(x => "Consultores Comerciais".Equals(x.Grupo))
                                       .GroupBy(x => new { x.IdUsuario, x.Nome, x.Cargo, x.Situacao })
                                       .Select(x => new PainelMetaFinanceira
                                       {
                                           IdMetaFinanceira = x.FirstOrDefault().IdMetaFinanceira,
                                           IdUsuario = x.FirstOrDefault().IdUsuario,
                                           IdLinhaMeta = x.FirstOrDefault().IdLinhaMeta,
                                           IdStatus = x.FirstOrDefault().IdStatus,
                                           Nome = x.FirstOrDefault().Nome,
                                           Cargo = x.FirstOrDefault().Cargo,
                                           Grupo = x.FirstOrDefault().Grupo,
                                           Situacao = x.FirstOrDefault().Situacao,
                                           Observacao = x.FirstOrDefault().Observacao,
                                           MetaReceitaLiquida = x.FirstOrDefault().MetaReceitaLiquida,
                                           ValorRecebimento = x.FirstOrDefault().ValorRecebimento,
                                           DataCriacao = x.FirstOrDefault().DataCriacao,
                                           DataAlteracao = x.FirstOrDefault().DataAlteracao,
                                           Regiao = x.FirstOrDefault().Regiao
                                          
                                       });
                var consultoresPrime = dados.Where(x => "Consultores Comerciais Prime".Equals(x.Grupo))
                                       .GroupBy(x => new { x.IdUsuario, x.Nome, x.Cargo, x.Situacao })
                                       .Select(x => new PainelMetaFinanceira
                                       {
                                           IdMetaFinanceira = x.FirstOrDefault().IdMetaFinanceira,
                                           IdUsuario = x.FirstOrDefault().IdUsuario,
                                           IdLinhaMeta = x.FirstOrDefault().IdLinhaMeta,
                                           IdStatus = x.FirstOrDefault().IdStatus,
                                           Nome = x.FirstOrDefault().Nome,
                                           Cargo = x.FirstOrDefault().Cargo,
                                           Grupo = x.FirstOrDefault().Grupo,
                                           Situacao = x.FirstOrDefault().Situacao,
                                           Observacao = x.FirstOrDefault().Observacao,
                                           MetaReceitaLiquida = x.FirstOrDefault().MetaReceitaLiquida,
                                           ValorRecebimento = x.FirstOrDefault().ValorRecebimento,
                                           DataCriacao = x.FirstOrDefault().DataCriacao,
                                           DataAlteracao = x.FirstOrDefault().DataAlteracao,
                                           Regiao = x.FirstOrDefault().Regiao

                                       });
                var gerentes = dados.Where(x => "Gerentes e Supervisores".Equals(x.Grupo))
                                       .GroupBy(x => new { x.IdUsuario, x.Nome, x.Cargo, x.Situacao })
                                       .Select(x => new PainelMetaFinanceira
                                       {
                                           IdMetaFinanceira = x.FirstOrDefault().IdMetaFinanceira,
                                           IdUsuario = x.FirstOrDefault().IdUsuario,
                                           IdLinhaMeta = x.FirstOrDefault().IdLinhaMeta,
                                           IdStatus = x.FirstOrDefault().IdStatus,
                                           Nome = x.FirstOrDefault().Nome,
                                           Cargo = x.FirstOrDefault().Cargo,
                                           Grupo = x.FirstOrDefault().Grupo,
                                           Situacao = x.FirstOrDefault().Situacao,
                                           Observacao = x.FirstOrDefault().Observacao,
                                           MetaReceitaLiquida = x.FirstOrDefault().MetaReceitaLiquida,
                                           ValorRecebimento = x.FirstOrDefault().ValorRecebimento,
                                           DataCriacao = x.FirstOrDefault().DataCriacao,
                                           DataAlteracao = x.FirstOrDefault().DataAlteracao,
                                           Regiao = x.FirstOrDefault().Regiao
                                       });
                return new PainelMetaFinanceiraViewModel()
                {
                    Status = "Pendente",
                    ListaLinhaConsultor = _mapper.Map<List<MetaFinanceiraViewModel>>(consultores),
                    ListaLinhaConsultorPrime = _mapper.Map<List<MetaFinanceiraViewModel>>(consultoresPrime),
                    ListaLinhaGerente = _mapper.Map<List<MetaFinanceiraViewModel>>(gerentes),
                    ListaLinhaSupervisor = _mapper.Map<List<MetaFinanceiraViewModel>>(gerentes)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

    }
}
