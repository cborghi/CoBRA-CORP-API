using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace CoBRA.Application
{
    public class CargoAppService : ICargoAppService
    {
        private readonly IMapper _mapper;
        private readonly ICargoRepository _cargoRepository;

        public CargoAppService(IMapper mapper, ICargoRepository cargoRepository)
        {
            _mapper = mapper;
            _cargoRepository = cargoRepository;
        }

        public List<CargoViewModel> ObterPorGrupo(Guid idGrupo)
        {
            return _mapper.Map<List<CargoViewModel>>(_cargoRepository.ObterCargos(idGrupo));
        }

        public List<CargosViewModel> Obter()
        {
            return _mapper.Map<List<CargosViewModel>>(_cargoRepository.Obter());
        }
    }
}
