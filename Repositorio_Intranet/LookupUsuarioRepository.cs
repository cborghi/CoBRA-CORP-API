using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class LookupUsuarioRepository : BaseRepository, ILookupUsuarioRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;
        public LookupUsuarioRepository(IBaseRepositoryPainelMeta baseRepository, IConfiguration configuration) : base(configuration)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IEnumerable<LookupUsuario>> ObterLookupUsuariosAcompanhamento(string idCargo, string idRegiao, string idUsuario)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ConsultaUsuarioAcompanhamento");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                SqlParameter paramCargo = new SqlParameter("@ID_CARGO", SqlDbType.NVarChar);
                SqlParameter paramRegiao = new SqlParameter("@ID_REGIAO", SqlDbType.NVarChar);
                SqlParameter paramUsuario = new SqlParameter("@ID_USUARIO_RM", SqlDbType.NVarChar);

                paramCargo.Value = (!string.IsNullOrEmpty(idCargo) && !idCargo.Equals("undefined")) ? idCargo : "00000000-0000-0000-0000-000000000000";
                paramRegiao.Value = !string.IsNullOrEmpty(idRegiao) ? idRegiao : "00000000-0000-0000-0000-000000000000";
                paramUsuario.Value = !string.IsNullOrEmpty(idUsuario) ? idUsuario : "00000000-0000-0000-0000-000000000000";

                command.Parameters.Add(paramCargo);
                command.Parameters.Add(paramRegiao);
                command.Parameters.Add(paramUsuario);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    var usuarios = LerDataReaderLookupUsuario(reader);

                    return usuarios;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();

                    command.Dispose();
                }
            }
        }

        public async Task<IEnumerable<LookupUsuario>> ObterLookupUsuariosAcompanhamentoGeral(string idCargo, string idRegiao)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ConsultaUsuarioAcompanhamentoGeral");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                SqlParameter paramCargo = new SqlParameter("@ID_CARGO", SqlDbType.NVarChar);
                SqlParameter paramRegiao = new SqlParameter("@ID_REGIAO", SqlDbType.NVarChar);
                
                paramCargo.Value = (!string.IsNullOrEmpty(idCargo) && !idCargo.Equals("undefined")) ? idCargo : "00000000-0000-0000-0000-000000000000";
                paramRegiao.Value = !string.IsNullOrEmpty(idRegiao) ? idRegiao : "00000000-0000-0000-0000-000000000000";
                
                command.Parameters.Add(paramCargo);
                command.Parameters.Add(paramRegiao);
                
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    var usuarios = LerDataReaderLookupUsuario(reader);

                    return usuarios;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();

                    command.Dispose();
                }
            }
        }

        public async Task<IEnumerable<LookupUsuario>> ObterLookupUsuarioAcompanhamento(string idUsuario)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ConsultaUsuarioAcompanhamentoIndividual");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);


                SqlParameter paramUsuario = new SqlParameter("@ID_USUARIO_RM", SqlDbType.NVarChar)
                {
                    Value = !string.IsNullOrEmpty(idUsuario) ? idUsuario : "00000000-0000-0000-0000-000000000000"
                };

                command.Parameters.Add(paramUsuario);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    var usuarios = LerDataReaderLookupUsuario(reader);

                    return usuarios;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();

                    command.Dispose();
                }
            }
        }

        private IEnumerable<LookupUsuario> LerDataReaderLookupUsuario(SqlDataReader reader)
        {
            var usuarios = new List<LookupUsuario>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    LookupUsuario usuario = new LookupUsuario
                    {
                        UsuarioId = Guid.Parse(reader["ID_USUARIO_RM"].ToString()),
                        Nome = reader["NOME"].ToString()
                    };
                    usuarios.Add(usuario);
                }
            }

            return usuarios;
        }
    }
}
