using Cobra.Console.Base;
using Cobra.Console.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Cobra.Console
{
    public class IntegraConsultorHierarquia
    {
        private readonly RepositoryBase _repositoryBase;

        public IntegraConsultorHierarquia(RepositoryBase repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task IntregrarUsuarioHierarquia()
        {
            IList<Usuario> usuarios = await ListarUsuarios();
            await LimparUsuarioHierarquia();

            foreach (var usuario in usuarios)
            {
                await GravarUsuarioHierarquia(usuario);
            }
        }

        private async Task<List<Usuario>> ListarUsuarios()
        {
            SqlDataReader reader = null;

            string consulta = "SELECT ID_USUARIO_RM FROM TB_USUARIO_RM WHERE ATIVO = 1";

            using (SqlConnection connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    return LerDataReaderUsuario(reader);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        await connection.CloseAsync();

                    await command.DisposeAsync();
                }
            }

        }

        private List<Usuario> LerDataReaderUsuario(SqlDataReader reader)
        {
            var usuarios = new List<Usuario>();
            Usuario usuario = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    usuario = new Usuario();
                    usuario.IdUsuario = Guid.Parse(reader["ID_USUARIO_RM"].ToString());

                    usuarios.Add(usuario);
                }
            }

            return usuarios;
        }

        private async Task GravarUsuarioHierarquia(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
            {
                await connection.OpenAsync();

                string script = _repositoryBase.BuscarArquivoConsulta("GravarUsuarioHierarquia")
                    .Replace("__BANCO__", _repositoryBase.NomeBancoProtheus);

                using (SqlCommand command = new SqlCommand(script, connection))
                {
                    command.Parameters.AddWithValue("@ID_USUARIO", ((Object)usuario.IdUsuario) ?? DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task LimparUsuarioHierarquia()
        {
            using (SqlConnection connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
            {
                await connection.OpenAsync();

                string script = "DELETE FROM TB_GERENTE_SUPERVISOR; DELETE FROM TB_SUPERVISOR_CONSULTOR";

                using (SqlCommand command = new SqlCommand(script, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
