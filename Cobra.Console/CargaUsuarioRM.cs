using Cobra.Console.Base;
using Cobra.Console.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cobra.Console
{
    public class CargaUsuarioRM
    {
        private readonly RepositoryBase _repositoryBase;

        public CargaUsuarioRM(RepositoryBase repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task ExecutarCargaUsuarioIntermediaria()
        {
			try
			{

                List<UsuarioRM> usuarios = await ListarUsuariosRM();

                foreach (var usuario in usuarios)
                {
                    await GravarUsuarioIntermediario(usuario);
                }
			}
			catch (Exception ex)
			{
				throw ex;
			}
        }

        public async Task ExecutarCargaUsuarioFinal()
        {
            List<UsuarioRM> usuarios = await ListarUsuariosIntermediarios();
            var usuariosTratados = new List<UsuarioRM>();
            var usuariosFiltrados = new List<UsuarioRM>();
            UsuarioRM usuarioAtual = null;
            
            foreach (var usuario in usuarios)
            {
                foreach (var usuarioFiltrado in usuarios)
                {
                    if (usuarioFiltrado.Cpf.Equals(usuario.Cpf))
                    {
                        usuariosFiltrados.Add(usuarioFiltrado);
                    }
                }
                
                if (usuariosFiltrados.Any(u => u.DataDemissao == DateTime.MinValue))
                    usuarioAtual = usuariosFiltrados.Where(u => u.DataDemissao == DateTime.MinValue).First();
                else
                    usuarioAtual = usuariosFiltrados.OrderByDescending(u => u.DataDemissao).First();
                
                if(!usuariosTratados.Any(u => u.Cpf.Equals(usuarioAtual.Cpf)))
                    usuariosTratados.Add(usuarioAtual);

                usuariosFiltrados = new List<UsuarioRM>();
            }

            await GravarUsuarioFinal(usuariosTratados);
        }

        private async Task<List<UsuarioRM>> ListarUsuariosRM() 
		{
            SqlDataReader reader = null;

            string consulta = _repositoryBase.BuscarArquivoConsulta("ListarUsuariosRM");

            using (SqlConnection connection = new SqlConnection(_repositoryBase.ConexaoCorpore))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    return LerDataReaderUsuarioRM(reader);
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

        private async Task<List<UsuarioRM>> ListarUsuariosIntermediarios()
        {
            SqlDataReader reader = null;

            string consulta = "SELECT * FROM TB_USUARIO_RM_INTEGRACAO";

            using (SqlConnection connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    return LerDataReaderUsuarioIntermediario(reader);
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

        private List<UsuarioRM> LerDataReaderUsuarioRM(SqlDataReader reader) 
		{
            var usuarios = new List<UsuarioRM>();
            UsuarioRM usuario = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    usuario = new UsuarioRM();
                    usuario.CodigoFilial  = int.TryParse(reader["CODFILIAL"].ToString(), out int filial) ? filial : -1;
                    usuario.NomeFilial = reader["NOME_FILIAL"].ToString();
                    usuario.Chapa = reader["CHAPA"].ToString();
                    usuario.Nome = reader["NOME"].ToString();
                    usuario.CodigoFuncao = reader["CODFUNCAO"].ToString();
                    usuario.Funcao = reader["FUNCAO"].ToString();
                    usuario.CodigoSecao = reader["CODSECAO"].ToString();
                    usuario.DescricaoSecao = reader["DESCRICAO_SECAO"].ToString();
                    usuario.Cpf = reader["CPF"].ToString();
                    usuario.DataInicio = Convert.ToDateTime(reader["DATAADMISSAO"]);
                    usuario.DataDemissao = DateTime.TryParse(reader["DATADEMISSAO"].ToString(), out DateTime dataDemissao) ? dataDemissao : DateTime.MinValue;
                    usuarios.Add(usuario);
                }
            }

            return usuarios;
        }

        private List<UsuarioRM> LerDataReaderUsuarioIntermediario(SqlDataReader reader)
        {
            var usuarios = new List<UsuarioRM>();
            UsuarioRM usuario = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    usuario = new UsuarioRM();
                    usuario.CodigoFilial = int.TryParse(reader["CODIGO_FILIAL"].ToString(), out int filial) ? filial : -1;
                    usuario.NomeFilial = reader["NOME_FILIAL"].ToString();
                    usuario.Chapa = reader["CHAPA"].ToString();
                    usuario.Nome = reader["NOME"].ToString();
                    usuario.CodigoFuncao = reader["CODFUNCAO"].ToString();
                    usuario.Funcao = reader["FUNCAO"].ToString();
                    usuario.CodigoSecao = reader["CODSECAO"].ToString();
                    usuario.DescricaoSecao = reader["DESCRICAO_SECAO"].ToString();
                    usuario.DataInicio = Convert.ToDateTime(reader["DATA_INICIO"]);
                    usuario.DataDemissao = DateTime.TryParse(reader["DATA_DEMISSAO"].ToString(), out DateTime dataDemissao) ? dataDemissao : DateTime.MinValue;
                    usuario.Cpf = reader["CPF"].ToString();
                    usuarios.Add(usuario);
                }
            }

            return usuarios;
        }

        private async Task GravarUsuarioIntermediario(UsuarioRM usuario)
        {
            using (SqlConnection connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
            {
                await connection.OpenAsync();

                string script = _repositoryBase.BuscarArquivoConsulta("InserirIClienteRMIntermediario");

                using (SqlCommand command = new SqlCommand(script, connection))
                {
                    command.Parameters.AddWithValue("@CODIGO_FILIAL", ((Object)usuario.CodigoFilial) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@NOME_FILIAL", ((Object)usuario.NomeFilial) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CHAPA", ((Object)usuario.Chapa) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@NOME", ((Object)usuario.Nome) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CODFUNCAO", ((Object)usuario.CodigoFuncao) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FUNCAO", ((Object)usuario.Funcao) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CODSECAO", ((Object)usuario.CodigoSecao) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DESCRICAO_SECAO", ((Object)usuario.DescricaoSecao) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DATA_INICIO", ((Object)usuario.DataInicio) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DATA_DEMISSAO", ((Object)VerificarDataDemissao(usuario.DataDemissao)) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CPF", ((Object)usuario.Cpf) ?? DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task GravarUsuarioIntermediario(List<UsuarioRM> usuarios)
        {
            SqlTransaction transaction = null;

            try
            {
                using (var connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string script = _repositoryBase.BuscarArquivoConsulta("InserirIClienteRMIntermediario");

                        using (SqlCommand command = new SqlCommand(script, connection, transaction))
                        {
                            command.Parameters.Add("@CODIGO_FILIAL", SqlDbType.Int);
                            command.Parameters.Add("@NOME_FILIAL", SqlDbType.Int);
                            command.Parameters.Add("@CHAPA", SqlDbType.VarChar);
                            command.Parameters.Add("@NOME", SqlDbType.VarChar);
                            command.Parameters.Add("@CODFUNCAO", SqlDbType.VarChar);
                            command.Parameters.Add("@FUNCAO", SqlDbType.VarChar);
                            command.Parameters.Add("@CODSECAO", SqlDbType.VarChar);
                            command.Parameters.Add("@DESCRICAO_SECAO", SqlDbType.VarChar);
                            command.Parameters.Add("@DATA_INICIO", SqlDbType.DateTime);
                            command.Parameters.Add("@CPF", SqlDbType.VarChar);

                            foreach (var usuario in usuarios)
                            {
                                command.Parameters["@CODIGO_FILIAL"].Value = usuario.CodigoFilial;
                                command.Parameters["@NOME_FILIAL"].Value = usuario.NomeFilial;
                                command.Parameters["@CHAPA"].Value = usuario.Chapa;
                                command.Parameters["@NOME"].Value = usuario.Nome;
                                command.Parameters["@CODFUNCAO"].Value = usuario.CodigoFuncao;
                                command.Parameters["@FUNCAO"].Value = usuario.Funcao;
                                command.Parameters["@CODSECAO"].Value = usuario.CodigoSecao;
                                command.Parameters["@DESCRICAO_SECAO"].Value = usuario.DescricaoSecao;
                                await command.ExecuteNonQueryAsync();
                            }
                        }

                        await transaction.CommitAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                throw ex;
            }
        }

        private async Task GravarUsuarioFinal(List<UsuarioRM> usuarios)
        {
            SqlTransaction transaction = null;

            try
            {
                using (var connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string script = _repositoryBase.BuscarArquivoConsulta("InserirClienteRMFinal")
                            .Replace("__BANCO__", _repositoryBase.NomeBancoProtheus);

                        using (SqlCommand command = new SqlCommand(script, connection, transaction))
                        {
                            command.Parameters.Add("@NOME", SqlDbType.VarChar);
                            command.Parameters.Add("@NUMERO_CHAPA", SqlDbType.VarChar);
                            command.Parameters.Add("@CODIGO_SECAO", SqlDbType.VarChar);
                            command.Parameters.Add("@DESCRICAO_CARGO", SqlDbType.VarChar);
                            command.Parameters.Add("@CODIGO_FILIAL", SqlDbType.Int);
                            command.Parameters.Add("@DATA_INICIO", SqlDbType.DateTime2);
                            command.Parameters.Add("@DATA_FIM", SqlDbType.DateTime2);
                            command.Parameters.Add("@CPF", SqlDbType.VarChar);

                            foreach (var usuario in usuarios)
                            {
                                command.Parameters["@NOME"].Value = usuario.Nome;
                                command.Parameters["@NUMERO_CHAPA"].Value = usuario.Chapa;
                                command.Parameters["@CODIGO_SECAO"].Value = usuario.CodigoSecao;
                                command.Parameters["@DESCRICAO_CARGO"].Value = usuario.Funcao;
                                command.Parameters["@CODIGO_FILIAL"].Value = usuario.CodigoFilial;
                                command.Parameters["@DATA_INICIO"].Value = usuario.DataInicio;
                                command.Parameters["@DATA_FIM"].Value = usuario.DataDemissao;
                                command.Parameters["@CPF"].Value = usuario.Cpf;
                                await command.ExecuteNonQueryAsync();
                            }
                        }

                        await transaction.CommitAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                throw ex;
            }
        }

        public DateTime? VerificarDataDemissao(DateTime data)
        {
            if (data == DateTime.MinValue)
                return null;

            return data;
        }


    }

    
}

