using CoBRA.Application.ViewModels;
using System.Collections.Generic;

namespace CoBRA.Application.Interfaces
{
    public interface IMenuAppService
    {
        List<MenuViewModel> ObterTodos(bool submenus = true);
        List<MenuViewModel> RecuperarIdsMenus(List<MenuViewModel> listaMenus);
        List<MenuViewModel> ObterMenusPorGrupo(int grupoId);
    }
}
