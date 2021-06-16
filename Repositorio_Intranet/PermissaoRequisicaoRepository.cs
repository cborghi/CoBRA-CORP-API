using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class PermissaoRequisicaoRepository : IPermissaoRequisicaoRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public PermissaoRequisicaoRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IList<PermissaoRequisicao>> ListarPermissaoUsuario(Usuario usuario)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListaUsuarioPermissaoRequisicao");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_USUARIO", ((Object)usuario.Id) ?? DBNull.Value);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderPermissaoUsuario(reader);
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
        public async Task<IList<PermissaoRequisicao>> FiltrarPermissaoUsuario(PermissaoRequisicao permissao)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("FiltraUsuarioPermissaoRequisicao");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@NOME", ((Object)permissao.Usuario.Nome) ?? DBNull.Value);
                command.Parameters.AddWithValue("@ID_USUARIO", ((Object)permissao.Usuario.Id) ?? DBNull.Value);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderPermissaoUsuario(reader);
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

        private IList<PermissaoRequisicao> LerDataReaderPermissaoUsuario(SqlDataReader reader)
        {
            IList<PermissaoRequisicao> linhas = null;

            if (reader.HasRows)
            {
                linhas = new List<PermissaoRequisicao>();
                PermissaoRequisicao linha;

                while (reader.Read())
                {
                    linha = new PermissaoRequisicao
                    {
                        Usuario = new Usuario
                        {
                            Id = Convert.ToInt32(reader["ID_USUARIO"].ToString()),
                            Nome = reader["NOME"].ToString(),
                            Email = reader["EMAIL"].ToString()
                        },
                        AprovaRequisicaoSupervisor = bool.Parse(reader["APROVA_REQUISICAO_SUPERVISOR"].ToString()),
                        ReprovaRequisicaoSupervisor = bool.Parse(reader["REPROVA_REQUISICAO_SUPERVISOR"].ToString()),
                        CancelaRequisicaoSupervisor = bool.Parse(reader["CANCELA_REQUISICAO_SUPERVISOR"].ToString()),
                        AprovaRequisicaoGerente = bool.Parse(reader["APROVA_REQUISICAO_GERENTE"].ToString()),
                        ReprovaRequisicaoGerente = bool.Parse(reader["REPROVA_REQUISICAO_GERENTE"].ToString()),
                        CancelaRequisicaoGerente = bool.Parse(reader["CANCELA_REQUISICAO_GERENTE"].ToString())
                    };

                    linhas.Add(linha);
                }
            }

            return linhas;
        }

        public async Task GravarPermissaoUsuario(PermissaoRequisicao permissao)
        {


            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _baseRepository.BuscarArquivoConsulta("SalvarUsuarioPermissaoRequisicao");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ID_USUARIO", permissao.Usuario.Id);
                            command.Parameters.AddWithValue("@APROVA_REQUISICAO_SUPERVISOR", permissao.AprovaRequisicaoSupervisor);
                            command.Parameters.AddWithValue("@REPROVA_REQUISICAO_SUPERVISOR", permissao.ReprovaRequisicaoSupervisor);
                            command.Parameters.AddWithValue("@CANCELA_REQUISICAO_SUPERVISOR", permissao.CancelaRequisicaoSupervisor);
                            command.Parameters.AddWithValue("@APROVA_REQUISICAO_GERENTE", permissao.AprovaRequisicaoGerente);
                            command.Parameters.AddWithValue("@REPROVA_REQUISICAO_GERENTE", permissao.ReprovaRequisicaoGerente);
                            command.Parameters.AddWithValue("@CANCELA_REQUISICAO_GERENTE", permissao.CancelaRequisicaoGerente);
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
    }
}
