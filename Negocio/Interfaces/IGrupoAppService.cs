using CoBRA.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace CoBRA.Application.Interfaces
{
    public interface IGrupoAppService
    {
        List<GrupoViewModel> ObterTodos();
        string Adicionar(GrupoViewModel grupo);
        string Editar(GrupoViewModel grupo);
        void AtivarGrupo(GrupoViewModel grupo);
        int ObterIdGrupoPorIdGrupoAD(Guid idGrupoAd);
        GrupoViewModel ObterGrupoPorId(int idGrupo);
    }
}
