using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CoBRA.Infra.Intranet
{
    public class BaseRepository
    {
        private IConfiguration _configuration;
        
        public BaseRepository(IConfiguration config)
        {
            _configuration = config;
        }

        public SqlConnection GetConnection()
        {
            var sqlConn = _configuration.GetConnectionString("ConnEBSA");
            SqlConnection connection = new SqlConnection(sqlConn);
            
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }

        protected SqlCommand GetCommand(SqlConnection connection, string commandText, CommandType commandType)
        {
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection)
            {
                CommandType = commandType
            };
            return command;
        }

        protected SqlParameter GetParameter(string parameter, object value)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, value != null ? value : DBNull.Value)
            {
                Direction = ParameterDirection.Input
            };
            return parameterObject;
        }

        protected SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, type); ;

            if (type == SqlDbType.NVarChar || type == SqlDbType.VarChar || type == SqlDbType.NText || type == SqlDbType.Text)
            {
                parameterObject.Size = -1;
            }

            parameterObject.Direction = parameterDirection;

            if (value != null)
            {
                parameterObject.Value = value;
            }
            else
            {
                parameterObject.Value = DBNull.Value;
            }

            return parameterObject;
        }

        protected int ExecuteNonQuery(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            int returnValue = -1;

            try
            {
                using (SqlConnection connection = this.GetConnection())
                {
                    DbCommand cmd = this.GetCommand(connection, procedureName, commandType);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }
                    returnValue = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
        }

        protected object ExecuteScalar(string procedureName, List<SqlParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            int returnValue;

            try
            {
                using (SqlConnection connection = this.GetConnection())
                {
                    SqlCommand cmd = this.GetCommand(connection, procedureName, commandType);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                //LogException("Failed to ExecuteScalar for " + procedureName, ex, parameters);
                throw;
            }

            return returnValue;
        }

        protected SqlDataReader GetDataReader(string procedureName, List<SqlParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            SqlDataReader ds;

            try
            {
                SqlConnection connection = this.GetConnection();
                {
                    SqlCommand cmd = this.GetCommand(connection, procedureName, commandType);
                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    ds = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception ex)
            {
                //LogException("Failed to GetDataReader for " + procedureName, ex, parameters);
                throw;
            }

            return ds;
        }

        protected bool GrupoCadastrado(string descricao, int idGrupo = 0)
        {
            try
            {
                string query = idGrupo > 0 ? @"SELECT DESCRICAO FROM GRUPO WHERE ID_GRUPO = @idGrupo" : @"SELECT DESCRICAO FROM GRUPO WHERE DESCRICAO = @descricao";

                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@descricao", Value = descricao },
                    new SqlParameter() { ParameterName = "@idGrupo", Value = idGrupo }
                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null)
                    {
                        if (dataReader.HasRows)
                        {
                            return true;
                        };
                    }
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        protected string ApiKeyPrevisaoDoTempo()
        {
            return _configuration["APIKEYPrevisaoDoTempo:URL"];
        }

        protected bool ProdEnvironment()
        {
            return _configuration["Environment"]=="Prod";
        }


    }
}
