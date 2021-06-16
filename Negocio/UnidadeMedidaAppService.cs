using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class UnidadeMedidaAppService : IUnidadeMedidaAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnidadeMedidaRepository _unidadeMedidaRepository;

        public UnidadeMedidaAppService(IMapper mapper, IUnidadeMedidaRepository unidadeMedidaRepository)
        {
            _mapper = mapper;
            _unidadeMedidaRepository = unidadeMedidaRepository;
        }

        public async Task<IEnumerable<UnidadeMedidaViewModel>> ListarUnidadeMedida()
        {
            IEnumerable<UnidadeMedidaViewModel> unidades = _mapper.Map<IEnumerable<UnidadeMedidaViewModel>>(await _unidadeMedidaRepository.ListaUnidadeMedida());
            return unidades;
        }
    }
}
