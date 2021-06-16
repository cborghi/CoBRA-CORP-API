using Cobra.Console.Base;
using Cobra.Console.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Cobra.Console
{
    public class IntegraOrigemDados
    {
        private readonly RepositoryBase _repositoryBase;
        private Dictionary<Guid, string> _scripts;

        public IntegraOrigemDados(RepositoryBase repositoryBase)
        {
            _repositoryBase = repositoryBase;
            _scripts = new Dictionary<Guid, string>();
        }

        public async Task ExecutarIntegracaoOrigemDados() 
        {
            //BUSCAR META USUARIO
           IList<MetaUsuario> metaUsuarios = await BuscarMetasUsuario();

            //CARREGAR SCRIPT
            await CarregarScriptsOrigem();

            string script = string.Empty;
            decimal valor = default;
            decimal porcentagemIntegracao = 0;
            decimal qtdIntegrados = 0;

            foreach (var metaUsuario in metaUsuarios)
            {
                script = _scripts[metaUsuario.IdOrigem];
                valor = await CarregarDadosOrigemUsuario(script, metaUsuario.IdUsuario);
                await GravarDadosOrigem(valor, metaUsuario);
                
                qtdIntegrados++;
                porcentagemIntegracao = ((qtdIntegrados / metaUsuarios.Count) * 100);
                System.Console.WriteLine($"{Math.Round(porcentagemIntegracao)} %");
            }
        }


        private async Task<decimal> CarregarDadosOrigemUsuario(string script, Guid idUsuario) 
        {
            SqlDataReader reader = null;

            using (SqlConnection connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
            {
                SqlCommand command = new SqlCommand(script, connection);
                try
                {
                    await connection.OpenAsync();
                    
                    command.Parameters.AddWithValue("@TB_USUARIOS", ConverterListaFuncionario(await BuscarFuncionarioSubordinado(idUsuario)));
                    command.Parameters["@TB_USUARIOS"].TypeName = "dbo.TY_USUARIO";

                    reader = await command.ExecuteReaderAsync();

                    return LerDataReaderDadoOrigem(reader);
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

        private decimal LerDataReaderDadoOrigem(SqlDataReader reader)
        {
            var random = new Random();
            decimal valorMeta = default;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    valorMeta = Decimal.TryParse(reader["VALOR"].ToString(), out decimal valor) ? valor : 0;
                }
            }

            if (valorMeta == 0)
                valorMeta = random.Next(1, 99);

            return valorMeta;
        }

        private async Task GravarDadosOrigem(decimal valor, MetaUsuario metaUsuario) 
        {
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _repositoryBase.BuscarArquivoConsulta("GravarDadosOrigem");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ID_USUARIO_RM", metaUsuario.IdUsuario);
                            command.Parameters.AddWithValue("@ID_ORIGEM", metaUsuario.IdOrigem);
                            command.Parameters.AddWithValue("@ID_LINHA_META", metaUsuario.IdLinhaMeta);
                            command.Parameters.AddWithValue("@VALOR", valor);
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

        private async Task CarregarScriptsOrigem() 
        {
            SqlDataReader reader = null;

            string consulta = _repositoryBase.BuscarArquivoConsulta("CarregarScriptOrigem");

            using (SqlConnection connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    LerDataReaderScriptOrigem(reader);
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

        private void LerDataReaderScriptOrigem(SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _scripts.Add(Guid.Parse(reader["ID_ORIGEM"].ToString()), reader["SCRIPT"].ToString());
                }
            }
        }

        private async Task<IList<MetaUsuario>> BuscarMetasUsuario() 
        {
            SqlDataReader reader = null;

            string consulta = _repositoryBase.BuscarArquivoConsulta("BuscarMetaUsuario");

            using (SqlConnection connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    return LerDataReaderUsuarioMeta(reader);
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

        private DataTable ConverterListaFuncionario(IList<Usuario> usuarios) 
        {
            var tabela = new DataTable();
            DataRow linha = null;
            tabela.Columns.Add(new DataColumn("ID_USUARIO", typeof(Guid)));

            foreach (Usuario usuario in usuarios)
            {
                linha = tabela.NewRow();

                linha["ID_USUARIO"] = usuario.IdUsuario;
                tabela.Rows.Add(linha);
            }

            return tabela;
        }

        private async Task<IList<Usuario>> BuscarFuncionarioSubordinado(Guid idUsuario)
        {
            SqlDataReader reader = null;

            string consulta = _repositoryBase.BuscarArquivoConsulta("BuscarFuncionarioSubordinado");

            using (SqlConnection connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                try
                {
                    command.Parameters.AddWithValue("@ID_USUARIO", idUsuario);
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    return  LerDataReaderFuncionario(reader, idUsuario);
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

        private IList<Usuario> LerDataReaderFuncionario(SqlDataReader reader, Guid idUsuario)
        {
            var funcionarios = new List<Usuario>();
            Usuario funcionario = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    funcionario = new Usuario();
                    funcionario.IdUsuario = Guid.Parse(reader["ID_USUARIO"].ToString());
                    funcionarios.Add(funcionario);
                }
            }
            else
            {
                funcionarios.Add(new Usuario { IdUsuario = idUsuario });
            }

            return funcionarios;
        }

        private IList<MetaUsuario> LerDataReaderUsuarioMeta(SqlDataReader reader)
        {
            var metasUsuario = new List<MetaUsuario>();
            MetaUsuario metaUsuario = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    metaUsuario = new MetaUsuario();
                    metaUsuario.IdOrigem = Guid.Parse(reader["ID_ORIGEM"].ToString());
                    metaUsuario.IdLinhaMeta = Guid.Parse(reader["ID_LINHA_META"].ToString());
                    metaUsuario.IdUsuario = Guid.Parse(reader["ID_USUARIO_RM"].ToString());
                    metasUsuario.Add(metaUsuario);
                }
            }

            return metasUsuario;
        }
    }
}
