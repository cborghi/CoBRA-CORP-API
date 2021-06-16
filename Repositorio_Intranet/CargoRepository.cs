using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class CargoRepository : BaseRepository, ICargoRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public CargoRepository(IBaseRepositoryPainelMeta baseRepository, IConfiguration configuration): base(configuration)
        {
            _baseRepository = baseRepository;
        }

        public List<Cargos> Obter()
        {
            try
            {
                List<Cargos> cargos = new List<Cargos>();

                string query = @"SELECT ID_CARGO, DESCRICAO, ATIVO, DATA_CRIACAO FROM CARGO";

                using (DbDataReader dataReader = GetDataReader(query.ToString(), null, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            cargos.Add(new Cargos()
                            {
                                Id = Convert.ToInt32(dataReader["ID_CARGO"]),
                                Descricao = dataReader["DESCRICAO"].ToString(),
                                Ativo = Convert.ToBoolean(dataReader["ATIVO"]),
                                DataCriacao = Convert.ToDateTime(dataReader["DATA_CRIACAO"])
                            });
                        }
                    }
                }

                return cargos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Cargo> ObterCargos(Guid idGrupo)
        {
            try
            {
                List<Cargo> cargos = new List<Cargo>();

                string query = @"SELECT ID_CARGO, DESCRICAO, ID_HIERARQUIA, ID_GRUPO_PAINEL, DT_CADASTRO FROM TB_CARGO WHERE ID_GRUPO_PAINEL = @id AND ATIVO = 1";

                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@id", Value = idGrupo }
                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            cargos.Add(new Cargo()
                            {
                                IdCargo = Guid.Parse(dataReader["ID_CARGO"].ToString()),
                                Descricao = dataReader["DESCRICAO"].ToString(),
                                IdHierarquia = Guid.Parse(dataReader["ID_HIERARQUIA"].ToString()),
                                IdGrupo = Guid.Parse(dataReader["ID_GRUPO_PAINEL"].ToString()),
                                DtCadastro = Convert.ToDateTime(dataReader["DT_CADASTRO"].ToString())
                            });
                        }
                    }
                }

                return cargos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Cargo>> ObterCargosAcompanhamento(int idUsuario)
        {
            var protheusAmbiente = ProdEnvironment() ? "[Protheus]" : "[Protheus_Dev]";

            string consulta = _baseRepository.BuscarArquivoConsulta("ConsultaCargoAcompanhamento")
                .Replace("__PROTHEUS__", protheusAmbiente);

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                SqlParameter paramHierarquia = new SqlParameter("@ID_USUARIO", SqlDbType.NVarChar)
                {
                    Value = idUsuario.ToString()
                };

                command.Parameters.Add(paramHierarquia);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    var cargos = LerDataReaderCargo(reader);

                    return cargos;
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

        public async Task<IEnumerable<Cargo>> ListarCargosAcompanhamento()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarCargoAcompanhamento");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    var cargos = LerDataReaderCargo(reader);

                    return cargos;
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

        private IEnumerable<Cargo> LerDataReaderCargo(SqlDataReader reader)
        {
            var cargos = new List<Cargo>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Cargo cargo = new Cargo
                    {
                        IdCargo = Guid.Parse(reader["ID_CARGO"].ToString()),
                        Descricao = reader["DESCRICAO"].ToString(),
                        IdHierarquia = Guid.Parse(reader["ID_HIERARQUIA"].ToString()),
                        IdGrupo = Guid.Parse(reader["ID_GRUPO_PAINEL"].ToString()),
                        DtCadastro = Convert.ToDateTime(reader["DT_CADASTRO"].ToString())
                    };
                    cargos.Add(cargo);
                }
            }

            return cargos;
        }


        public async Task<Guid> ObterIdUsuarioRM(int idUsuario)
        {
            var protheusAmbiente = ProdEnvironment() ? "[Protheus]" : "[Protheus_Dev]";

            string consulta = _baseRepository.BuscarArquivoConsulta("ObterIdUsuarioRM")
                .Replace("__PROTHEUS__", protheusAmbiente);

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                SqlParameter param = new SqlParameter("@ID_USUARIO", SqlDbType.Int)
                {
                    Value = idUsuario
                };

                command.Parameters.Add(param);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    var idUsuarioRM = LerDataReaderIdRM(reader);

                    return idUsuarioRM;
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

        private Guid LerDataReaderIdRM(SqlDataReader reader)
        {
            
            Guid idUsuarioRM = default;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    idUsuarioRM = Guid.TryParse(reader["ID_USUARIO_RM"].ToString(), out Guid idUsuario) ? idUsuario : new Guid();
                }
            }

            return idUsuarioRM;
        }

        
    }
}
