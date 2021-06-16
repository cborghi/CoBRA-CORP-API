using Cobra.Console.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.Console
{
    public class ExecutaScript
    {
        private readonly RepositoryBase _repositoryBase;
        private readonly GravaLog _gravaLog;

        public ExecutaScript(RepositoryBase repositoryBase, GravaLog gravaLog)
        {
            _repositoryBase = repositoryBase;
            _gravaLog = gravaLog;
        }

        public async Task ExecutarScript()
        {
            SqlTransaction transaction = null;

            try
            {
                using (var connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string script = _repositoryBase.BuscarArquivoConsulta("ExecutaScript");

                        using (SqlCommand command = new SqlCommand(script, connection, transaction))
                        {
                            await command.ExecuteNonQueryAsync();
                        }

                        await transaction.CommitAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                await _gravaLog.GravarLog(ex);
            }


        }
    }
}
