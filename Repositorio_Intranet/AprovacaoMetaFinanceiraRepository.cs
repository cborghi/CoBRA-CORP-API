using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace CoBRA.Infra.Intranet
{
    public class AprovacaoMetaFinanceiraRepository : BaseRepository, IAprovacaoMetaFinanceiraRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public AprovacaoMetaFinanceiraRepository(IBaseRepositoryPainelMeta baseRepository, IConfiguration configuration) : base(configuration)
        {
            _baseRepository = baseRepository;
        }

        public async Task AprovacaoPainelMeta(PainelMetaFinanceira painel)
        {
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _baseRepository.BuscarArquivoConsulta("AprovacaoPainelMetaFinanceira");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ID_META", painel.IdMetaFinanceira);
                            command.Parameters.AddWithValue("@ID_STATUS_APROVACAO", painel.IdStatus);
                            command.Parameters.AddWithValue("@OBSERVACAO", painel.Observacao);
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

