using CoBRA.Domain.Entities;
using System.Collections.Generic;

namespace CoBRA.Domain.Interfaces
{
    public interface IInicializacaoRepository
    {
        List<Menu> ObterMenu();
        List<Estado> ObterEstados();
        List<Periodo> ObterPeriodos();
        List<Usuario> ObterRamais();
        string ObterPrevisaoDoTempo(string latitude, string longetude);
    }
}
