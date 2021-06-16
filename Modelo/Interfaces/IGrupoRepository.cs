using CoBRA.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CoBRA.Domain.Interfaces
{
    public interface IGrupoRepository
    {
        List<Grupo> ObterGrupos();
        Grupo ObterPorId(int id);
        string Adicionar(Grupo grupo);
        string Editar(Grupo grupo);
        void Ativar(Grupo grupo);
        void AdicionarMenusGrupo(Grupo grupo);
        void AtualizarMenusGrupo(Grupo grupo);
        int ObterIdGrupoPorIdGrupoAD(Guid idGrupoAd);
    }
}
