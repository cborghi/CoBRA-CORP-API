using Cobra.Console.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.Console
{
    public class GravaLog
    {
        private readonly RepositoryBase _repositoryBase;

        public GravaLog(RepositoryBase repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }


        public async Task GravarLog(Exception ex)
        {
            using (SqlConnection connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
            {
                const int ID_SISTEMA_COBRA = 1;
                const int ID_TIPO_LOG_ERRO = 2;
                await connection.OpenAsync();

                string script = _repositoryBase.BuscarArquivoConsulta("GravarLog");

                using (SqlCommand command = new SqlCommand(script, connection))
                {
                    command.Parameters.AddWithValue("@TIPO_LOG", ((Object)ID_TIPO_LOG_ERRO) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@USUARIO", ((Object) "0") ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DATA_LOG", ((Object) DateTime.Now) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DESCRICAO", ((Object) $"Erro: {ex.Message}. Local: {ex.StackTrace}") ?? DBNull.Value);
                    command.Parameters.AddWithValue("@IP", ((Object) "CONSOLE INTEGRAÇÃO") ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TIPO_SISTEMA", ((Object) ID_SISTEMA_COBRA) ?? DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }

        }
    }
}
