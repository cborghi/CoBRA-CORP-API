using CoBRA.Domain.Entities;
using System.Collections.Generic;

namespace CoBRA.Domain.Interfaces
{
    public interface IRequisicaoCompraRepository
    {
        List<RequisicaoCompras> ObterRequisicoes(Usuario usuario);
        List<CentroCusto> ObterCentrosDeCusto();
        RequisicaoCompras ObterRequisicaoPorId(int id);
        List<ParcelaRequisicao> ObterParcelasRequisicao(string requisicaoId);
        Requisicao BuscarRequisicao(int idRequisicao);
        string BuscarServicoRequisicao(int idRequisicao);
        List<Obra> BuscarObraRequisicao(int idRequisicao);
        List<RequisicaoObra> ObterRequisicaoObra(int requisicao, string obra);
        List<RequisicaoObra> ObterRequisicaoGeradaPorNota(string nota);
        int AprovarSupervisorRequisicao(int id, string supervisor, int idAprovador);
        int AprovarGerenteRequisicao(int id, string gerente, int idAprovador);
        int ReprovarSupervisorRequisicao(int id, string supervisor, int idAprovador);
        int ReprovarGerenteRequisicao(int id, string gerente, int idAprovador);
        int InsertRequisicao(RequisicaoGerada requisicao, bool elvis);
        void UpdateRequisicao(RequisicaoAtualizada requisicao);
        int InsertRequisicaoObra(int requisicao, string obra, decimal valor);
        int InsertRequisicaoServico(int requisicao, string servico);
        int InsertParcelaRequisicao(string requisicaoID, ParcelaRequisicao parcela);
        int AtualizarRequisicao(int id, string link);
        int ExcluirRequisicao(int id, string nomeUsuario, string descricao);
        void DeleteParcelasRequisicao(string requisicaoID);
        List<Obra> ObterObras(string Nome);
        void CancelarRequisicao(int idRequisicao, int idUsuario);
        bool VerificarExistenciaNotaFornecedor(string nota, string documento);
    }
}
