using System;
using System.Collections.Generic;
using CoBRA.Domain.Entities;

namespace CoBRA.Domain.Interfaces {
    public interface IRelatorioProtheusRepository {
        List<Comissao> Comissao(int? qntMes, int idEstado, int idUsuario, int idHierarquia, string codDivulgador, DateTime? dataInicio = null, DateTime? dataFim = null);
        List<Comissao> ComissaoDetalhes(int? qntMes, int idEstado, string divulgador, DateTime? dataInicio = null, DateTime? dataFim = null);
    }
}