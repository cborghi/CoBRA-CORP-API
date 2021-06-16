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
    public class PainelMetaAnualAppService : IPainelMetaAnualAppService
    {

        private readonly IMapper _mapper;
        private readonly IPainelMetaAnualRepository _painelMetaAnualRepository;

        public PainelMetaAnualAppService(IMapper mapper, IPainelMetaAnualRepository painelMetaAnualRepository)
        {
            _mapper = mapper;
            _painelMetaAnualRepository = painelMetaAnualRepository;
        }

        public async Task<IEnumerable<PainelMetaAnualViewModel>> ConsultaPainelMetaAnual(Guid? MetaAnualId, Guid? MetaId, Guid? LinhaMetaId, Guid? GrupoPainelId, Guid? CargoId, Guid? UsuarioId, Guid? StatusId, Guid? PeriodoId)
        {
            IEnumerable<PainelMetaAnualViewModel> metaanual = _mapper.Map<IEnumerable<PainelMetaAnualViewModel>>(await _painelMetaAnualRepository.ConsultaPainelMetaAnual(MetaAnualId, MetaId, LinhaMetaId, GrupoPainelId, CargoId, UsuarioId, StatusId, PeriodoId));
            return metaanual;
        }

        public async Task SalvarPainelMetaAnual(PainelMetaAnualViewModel meta)
        {
            await _painelMetaAnualRepository.SalvarPainelMetaAnual(_mapper.Map<PainelMetaAnual>(meta));
        }
    }
}
