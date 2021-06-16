using System;

namespace CoBRA.Application.Interfaces
{
    public interface IRelatorioAppService
    {
        object ObterRelatorioComissao(int? qntMes, int idEstado, int idUsuario, int idHierarquia, string codDivulgador, DateTime? dataInicio, DateTime? dataFim, string idDivulgador);
        object ObterRelatorioComissaoDetalhes(int? qntMes, int idEstado, int idHierarquia, string codDivulgador, DateTime? dataInicio = null, DateTime? dataFim = null, int idDivulgador = 0);
        object ObterRelatorioComissaoProtheus(int? qntMes, int idEstado, int idUsuario, int idHierarquia, string codDivulgador, DateTime? dataInicio, DateTime? dataFim, string idDivulgador);
        object ObterRelatorioComissaoDetalhesProtheus(int? qntMes, int idEstado, int idHierarquia, string codDivulgador, DateTime? dataInicio = null, DateTime? dataFim = null, int idDivulgador = 0);
        byte[] GerarExcelRelatorioComissao(int? qntMes, int idEstado, int idHierarquia, string codDivulgador, DateTime? dataInicio = null, DateTime? dataFim = null, int idDivulgador = 0);
        byte[] GerarExecelRelatorioComissaoProtheus(int? qntMes, int idEstado, int idHierarquia, string codDivulgador, DateTime? dataInicio = null, DateTime? dataFim = null, int idDivulgador = 0);
    }
}
