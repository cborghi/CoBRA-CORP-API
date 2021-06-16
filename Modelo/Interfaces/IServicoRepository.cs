using CoBRA.Domain.Entities;
using System.Collections.Generic;

namespace CoBRA.Domain.Interfaces
{
    public interface IServicoRepository
    {

        List<Servico> ObterServico();
        List<Servico> ObterServicoPorCentroCusto(string centroCusto);
        Servico ObterServicoPorTipo(string tipo);
    }
}
