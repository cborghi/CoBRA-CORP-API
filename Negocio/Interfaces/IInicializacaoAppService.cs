using CoBRA.Application.ViewModels;
using System.Collections.Generic;

namespace CoBRA.Application.Interfaces
{
    public interface IInicializacaoAppService
    {
        object ObterUsuario(int id);
        List<MenuViewModel> ObterMenu();
        List<EstadoViewModel> ObterEstados();
        List<PeriodoViewModel> ObterPeriodos();
        List<UsuarioViewModel> ObterRamais();
        List<UsuarioViewModel> ObterAniversariantes();
        List<UsuarioViewModel> ObterAdmissoes();
        string ObterPrevisaoDoTempo(string latitude, string longetude);
    }
}
