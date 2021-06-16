using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace CoBRA.Infra.Intranet
{
    public class AprovacaoGrupoRepository : BaseRepository, IAprovacaoGrupoRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public AprovacaoGrupoRepository(IBaseRepositoryPainelMeta baseRepository, IConfiguration configuration): base(configuration)
        {
            _baseRepository = baseRepository;
        }

        public async Task AprovacaoPainelMeta(CabecalhoPainelMeta cabecalho)
        {
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _baseRepository.BuscarArquivoConsulta("AprovacaoPainelMeta");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ID_META", cabecalho.Id);
                            command.Parameters.AddWithValue("@STATUS", cabecalho.StatusPainel.Descricao);
                            command.Parameters.AddWithValue("@OBSERVACAO", cabecalho.Observacao);
                            await command.ExecuteNonQueryAsync();
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
    }
}

