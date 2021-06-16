using Cobra.Console.Base;
using Cobra.Console.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Cobra.Console
{
    public class IntegraConsultorRegiao
    {
        private readonly RepositoryBase _repositoryBase;

        public IntegraConsultorRegiao(RepositoryBase repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task IntegrarUsuarioRegiaoAtuacao()
        {
            IList<Usuario> usuarios = await ListarUsuarios();

            foreach (var usuario in usuarios)
            {
                await GravarUsuarioRegiaoAtuacao(usuario);
            }

        }

        private async Task<List<Usuario>> ListarUsuarios()
        {
            SqlDataReader reader = null;

            string consulta = _repositoryBase.BuscarArquivoConsulta("BuscarConsultorAreaAtuacao");

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

        private async Task GravarUsuarioRegiaoAtuacao(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
            {
                await connection.OpenAsync();

                string script = _repositoryBase.BuscarArquivoConsulta("GravarConsultorAreaAtuacao")
                    .Replace("__BANCO__", _repositoryBase.NomeBancoProtheus);

                using (SqlCommand command = new SqlCommand(script, connection))
                {
                    command.Parameters.AddWithValue("@ID_USUARIO", ((Object)usuario.IdUsuario) ?? DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
