using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class PainelMetaRealAppService : IPainelMetaRealAppService
    {

        private readonly IMapper _mapper;
        private readonly IPainelMetaRealRepository _painelMetaRealRepository;

        public PainelMetaRealAppService(IMapper mapper, IPainelMetaRealRepository painelMetaRealRepository)
        {
            _mapper = mapper;
            _painelMetaRealRepository = painelMetaRealRepository;
        }

        public async Task<IEnumerable<PainelMetaRealViewModel>> ConsultaPainelMetaReal(Guid? MetaRealId, Guid? UsuarioId, Guid? LinhaMetaId)
        {
            IEnumerable<PainelMetaRealViewModel> metareal = _mapper.Map<IEnumerable<PainelMetaRealViewModel>>(await _painelMetaRealRepository.ConsultaPainelMetaReal(MetaRealId, UsuarioId, LinhaMetaId));
            
            return metareal;
        }
    }
}
