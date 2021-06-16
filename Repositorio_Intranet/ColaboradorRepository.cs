using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class ColaboradorRepository : BaseRepository, IColaboradorRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public ColaboradorRepository(IBaseRepositoryPainelMeta baseRepository, IConfiguration configuration) : base(configuration)
        {
            _baseRepository = baseRepository;
        }

        public List<Colaborador> ObterColaboradores(Guid idCargo)
        {
            try
            {
                List<Colaborador> colaboradores = new List<Colaborador>();

                string query = @"SELECT ID_USUARIO_RM, ID_CARGO, ID_FILIAL, ID_REGIAO_ATUACAO, DATA_INICIO, DATA_FIM, ID_SECAO, NOME, NUMERO_CHAPA FROM TB_USUARIO_RM WHERE ATIVO = 1 AND ID_CARGO = @id AND CODIGO_PROTHEUS IS NOT NULL";

                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@id", Value = idCargo }
                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            colaboradores.Add(new Colaborador()
                            {
                                IdUsuario = !string.IsNullOrEmpty(dataReader["ID_USUARIO_RM"].ToString()) ? Guid.Parse(dataReader["ID_USUARIO_RM"].ToString()) : new Guid(),
                                IdCargo = !string.IsNullOrEmpty(dataReader["ID_CARGO"].ToString()) ? Guid.Parse(dataReader["ID_CARGO"].ToString()) : new Guid(),
                                IdFilial = !string.IsNullOrEmpty(dataReader["ID_FILIAL"].ToString()) ? Guid.Parse(dataReader["ID_FILIAL"].ToString()) : new Guid(),
                                IdRegiao = !string.IsNullOrEmpty(dataReader["ID_REGIAO_ATUACAO"].ToString()) ? Guid.Parse(dataReader["ID_REGIAO_ATUACAO"].ToString()) : new Guid(),
                                DataInicio = !string.IsNullOrEmpty(dataReader["DATA_FIM"].ToString()) ? Convert.ToDateTime(dataReader["DATA_FIM"].ToString()) : DateTime.MinValue,
                                DataFim = !string.IsNullOrEmpty(dataReader["DATA_FIM"].ToString()) ? Convert.ToDateTime(dataReader["DATA_FIM"].ToString()) : DateTime.MinValue,
                                IdSecao = !string.IsNullOrEmpty(dataReader["ID_SECAO"].ToString()) ? Guid.Parse(dataReader["ID_SECAO"].ToString()) : new Guid(),
                                //CodigoFilial = !string.IsNullOrEmpty(dataReader["CODIGO_FILIAL"].ToString()) ? Convert.ToInt32(dataReader["CODIGO_FILIAL"].ToString()) : 0,
                                Nome = !string.IsNullOrEmpty(dataReader["NOME"].ToString()) ? dataReader["NOME"].ToString() : "",
                                NumeroChapa = !string.IsNullOrEmpty(dataReader["NUMERO_CHAPA"].ToString()) ? dataReader["NUMERO_CHAPA"].ToString() : ""
                            });
                        }
                    }
                }

                return colaboradores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Colaborador ObterColaborador(Guid idUsuario)
        {
            try
            {
                Colaborador colaborador = null;

                string query = @"SELECT ID_USUARIO_RM, ID_CARGO, ID_FILIAL, ID_REGIAO_ATUACAO, DATA_INICIO, DATA_FIM, ID_SECAO, CODIGO_FILIAL, NOME, NUMERO_CHAPA FROM TB_USUARIO_RM WHERE ID_USUARIO_RM  = @ID_USUARIO_RM AND CODIGO_PROTHEUS IS NOT NULL";

                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@ID_USUARIO_RM", Value = idUsuario }
                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            colaborador = new Colaborador()
                            {
                                IdUsuario = !string.IsNullOrEmpty(dataReader["ID_USUARIO_RM"].ToString()) ? Guid.Parse(dataReader["ID_USUARIO_RM"].ToString()) : new Guid(),
                                IdCargo = !string.IsNullOrEmpty(dataReader["ID_CARGO"].ToString()) ? Guid.Parse(dataReader["ID_CARGO"].ToString()) : new Guid(),
                                IdFilial = !string.IsNullOrEmpty(dataReader["ID_FILIAL"].ToString()) ? Guid.Parse(dataReader["ID_FILIAL"].ToString()) : new Guid(),
                                IdRegiao = !string.IsNullOrEmpty(dataReader["ID_REGIAO_ATUACAO"].ToString()) ? Guid.Parse(dataReader["ID_REGIAO_ATUACAO"].ToString()) : new Guid(),
                                DataInicio = !string.IsNullOrEmpty(dataReader["DATA_FIM"].ToString()) ? Convert.ToDateTime(dataReader["DATA_FIM"].ToString()) : DateTime.MinValue,
                                DataFim = !string.IsNullOrEmpty(dataReader["DATA_FIM"].ToString()) ? Convert.ToDateTime(dataReader["DATA_FIM"].ToString()) : DateTime.MinValue,
                                IdSecao = !string.IsNullOrEmpty(dataReader["ID_SECAO"].ToString()) ? Guid.Parse(dataReader["ID_SECAO"].ToString()) : new Guid(),
                                CodigoFilial = !string.IsNullOrEmpty(dataReader["CODIGO_FILIAL"].ToString()) ? Convert.ToInt32(dataReader["CODIGO_FILIAL"].ToString()) : 0,
                                Nome = !string.IsNullOrEmpty(dataReader["NOME"].ToString()) ? dataReader["NOME"].ToString() : "",
                                NumeroChapa = !string.IsNullOrEmpty(dataReader["NUMERO_CHAPA"].ToString()) ? dataReader["NUMERO_CHAPA"].ToString() : ""
                            };
                        }
                    }
                }

                return colaborador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Colaborador>> ObterColaboradoresPorSupervisor(Guid idUsuario, Guid? idRegiao)
        {
            try
            {
                SqlDataReader reader = null;

                string consulta = _baseRepository.BuscarArquivoConsulta("BuscarFuncionarioSubordinado");

                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    SqlCommand command = new SqlCommand(consulta, connection);

                    SqlParameter paramHierarquia = new SqlParameter("@ID_USUARIO_RM", SqlDbType.NVarChar);
                    SqlParameter paramRegiao = new SqlParameter("@ID_REGIAO", SqlDbType.NVarChar);

                    paramHierarquia.Value = idUsuario.ToString();
                    paramRegiao.Value = idRegiao != null ? idRegiao.ToString() : "00000000-0000-0000-0000-000000000000";

                    command.Parameters.Add(paramHierarquia);
                    command.Parameters.Add(paramRegiao);

                    try
                    {
                        await connection.OpenAsync();
                        reader = await command.ExecuteReaderAsync();

                        var colaboradores = LerDataReaderColaborador(reader).ToList();

                        return colaboradores;
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IEnumerable<Colaborador> LerDataReaderColaborador(SqlDataReader reader)
        {
            var colaboradores = new List<Colaborador>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Colaborador colaborador = new Colaborador
                    {
                        IdUsuario = !string.IsNullOrEmpty(reader["ID_USUARIO_RM"].ToString()) ? Guid.Parse(reader["ID_USUARIO_RM"].ToString()) : new Guid(),
                        IdCargo = !string.IsNullOrEmpty(reader["ID_CARGO"].ToString()) ? Guid.Parse(reader["ID_CARGO"].ToString()) : new Guid(),
                        IdFilial = !string.IsNullOrEmpty(reader["ID_FILIAL"].ToString()) ? Guid.Parse(reader["ID_FILIAL"].ToString()) : new Guid(),
                        IdRegiao = !string.IsNullOrEmpty(reader["ID_REGIAO_ATUACAO"].ToString()) ? Guid.Parse(reader["ID_REGIAO_ATUACAO"].ToString()) : new Guid(),
                        DataInicio = !string.IsNullOrEmpty(reader["DATA_INICIO"].ToString()) ? Convert.ToDateTime(reader["DATA_INICIO"].ToString()) : DateTime.MinValue,
                        DataFim = !string.IsNullOrEmpty(reader["DATA_FIM"].ToString()) ? Convert.ToDateTime(reader["DATA_FIM"].ToString()) : DateTime.MinValue,
                        IdSecao = !string.IsNullOrEmpty(reader["ID_SECAO"].ToString()) ? Guid.Parse(reader["ID_SECAO"].ToString()) : new Guid(),
                        CodigoFilial = !string.IsNullOrEmpty(reader["CODIGO_FILIAL"].ToString()) ? Convert.ToInt32(reader["CODIGO_FILIAL"].ToString()) : 0,
                        Nome = !string.IsNullOrEmpty(reader["NOME"].ToString()) ? reader["NOME"].ToString() : "",
                        NumeroChapa = !string.IsNullOrEmpty(reader["NUMERO_CHAPA"].ToString()) ? reader["NUMERO_CHAPA"].ToString() : ""
                    };
                    colaboradores.Add(colaborador);
                }
            }

            return colaboradores;
        }
    }
}
