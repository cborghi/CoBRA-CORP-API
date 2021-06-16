using CoBRA.Domain.Entities;
using System.Collections.Generic;

namespace CoBRA.Domain.Interfaces
{
    public interface IMenuRepository
    {
        List<Menu> ObterMenus(bool submenus);
        List<Menu> ObterIdsMenus(List<Menu> listaMenus);
        List<Menu> ObterMenusPorGrupoId(int IdGrupo);
    }
}
