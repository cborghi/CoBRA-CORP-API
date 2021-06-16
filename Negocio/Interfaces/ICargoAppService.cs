using CoBRA.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace CoBRA.Application.Interfaces
{
    public interface ICargoAppService
    {
        List<CargoViewModel> ObterPorGrupo(Guid idGrupo);
        List<CargosViewModel> Obter();
    }
}
