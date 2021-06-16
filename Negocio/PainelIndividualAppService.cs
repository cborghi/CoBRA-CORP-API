using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class PainelIndividualAppService : IPainelIndividualAppService 
    {
        private readonly IMapper _mapper;
        private readonly IPainelMetaAnualRepository _painelMetaAnualRepository;

        public PainelIndividualAppService(IMapper mapper,
            IPainelMetaAnualRepository painelMetaAnualRepository)
        {
            _mapper = mapper;
            _painelMetaAnualRepository = painelMetaAnualRepository;
        }

        public async Task<PainelIndividualViewModel> ListarPainelIndividualAprovado()
        {
            var dados = await _painelMetaAnualRepository.GestaoPessoaConsultaPainelMetaAnual(
                null, new Guid("32342EC1-0B81-4861-987E-2A3E2C1ACEF2"), null
            );

            var consultores = dados.Where(x => "Consultores Comerciais".Equals(x.Grupo));
            var consultoresPrime = dados.Where(x => "Consultores Comerciais Prime".Equals(x.Grupo));
            var gerentes = dados.Where(x => "Gerentes e Supervisores".Equals(x.Grupo));

            return new PainelIndividualViewModel()
            {
                Status = "Aprovado",
                ListaLinhaConsultor = _mapper.Map<List<MetaIndividualViewModel>>(consultores),
                ListaLinhaConsultorPrime = _mapper.Map<List<MetaIndividualViewModel>>(consultoresPrime),
                ListaLinhaGerente = _mapper.Map<List<MetaIndividualViewModel>>(gerentes),
                ListaLinhaSupervisor = _mapper.Map<List<MetaIndividualViewModel>>(gerentes)
            };
        }

        public async Task<PainelIndividualViewModel> ListarPainelIndividualPendente()
        {
            var dados = await _painelMetaAnualRepository.GestaoPessoaConsultaPainelMetaAnual(
                null, new Guid("70D0CC08-227F-4BC4-9805-19B184E16D4B"), null
                );
            var consultores = dados.Where(x => "Consultores Comerciais".Equals(x.Grupo));
            var consultoresPrime = dados.Where(x => "Consultores Comerciais Prime".Equals(x.Grupo));
            var gerentes = dados.Where(x => "Gerentes e Supervisores".Equals(x.Grupo));
            return new PainelIndividualViewModel()
            {
                Status = "Pendente",
                ListaLinhaConsultor = _mapper.Map<List<MetaIndividualViewModel>>(consultores),
                ListaLinhaConsultorPrime = _mapper.Map<List<MetaIndividualViewModel>>(consultoresPrime),
                ListaLinhaGerente = _mapper.Map<List<MetaIndividualViewModel>>(gerentes),
                ListaLinhaSupervisor = _mapper.Map<List<MetaIndividualViewModel>>(gerentes)
            };
        }

    }
}
