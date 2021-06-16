using CoBRA.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CoBRA.Domain.Interfaces
{
    public interface IRelatorioRepository
    {
        List<Comissao> Comissao(int? qntMes, int idEstado, int idUsuario, int idHierarquia, string codDivulgador, DateTime? dataInicio = null, DateTime? dataFim = null);
        List<Comissao> ComissaoDetalhes(int? qntMes, int idEstado, string codDivulgador, DateTime? dataInicio = null, DateTime? dataFim = null);
    }
}
