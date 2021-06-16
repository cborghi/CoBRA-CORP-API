using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;


namespace CoBRA.Infra.Intranet
{
    public class CadastroMetaFinanceiraRepository : BaseRepository, ICadastroMetaFinanceiraRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public CadastroMetaFinanceiraRepository(IBaseRepositoryPainelMeta baseRepository, IConfiguration configuration) : base(configuration)
        {
            _baseRepository = baseRepository;

        }

        public async Task<IEnumerable<PainelMetaFinanceira>> ListarMetaFinanceira(Guid? IdPeriodo)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarMetaFinanceira");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                SqlParameter paramIdPeriodo = new SqlParameter("@ID_PERIODO", SqlDbType.UniqueIdentifier)
                {
                    Value = IdPeriodo
                };

                command.Parameters.Add(paramIdPeriodo);
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    IEnumerable<PainelMetaFinanceira> metasFinanceiras = await LerDataReaderMetaFinanceira(reader);

                    return metasFinanceiras;
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


        public async Task<IEnumerable<PainelMetaFinanceira>> BuscarCadastroMetaFinanceira()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ConsultaCadastroMetaFinanceira");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    IEnumerable<PainelMetaFinanceira> item = await LerDataReaderMetaFinanceira(reader);

                    return item;
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

        public async Task<PainelMetaFinanceira> ConsultarCadastroMetaFinanceira(Guid? UsuarioId, Guid? MetaFinanceiraId)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("PesquisaCadastroMetaFinanceira");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                SqlParameter paramIdUsuario = new SqlParameter("@ID_USUARIO_RM", SqlDbType.UniqueIdentifier);
                SqlParameter paramIdMetaFinanceira = new SqlParameter("@ID_META_FINANCEIRA", SqlDbType.UniqueIdentifier);

                paramIdUsuario.Value = !string.IsNullOrEmpty(UsuarioId.GetValueOrDefault().ToString()) && UsuarioId != null ? UsuarioId : new Guid();
                paramIdMetaFinanceira.Value = !string.IsNullOrEmpty(MetaFinanceiraId.GetValueOrDefault().ToString()) && MetaFinanceiraId != null ? MetaFinanceiraId : new Guid();

                command.Parameters.Add(paramIdUsuario);
                command.Parameters.Add(paramIdMetaFinanceira);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    IEnumerable<PainelMetaFinanceira> item = await LerDataReaderMetaFinanceira(reader);

                    return item.FirstOrDefault();
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

        private async Task<IEnumerable<PainelMetaFinanceira>> LerDataReaderMetaFinanceira(SqlDataReader reader)
        {
            var metas = new List<PainelMetaFinanceira>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    PainelMetaFinanceira meta = new PainelMetaFinanceira
                    {
                        IdMetaFinanceira = !string.IsNullOrEmpty(reader["ID_META_FINANCEIRA"].ToString()) ? Guid.Parse(reader["ID_META_FINANCEIRA"].ToString()) : new Guid(),
                        IdUsuario = Guid.Parse(reader["ID_USUARIO_RM"].ToString()),
                        Nome = !string.IsNullOrEmpty(reader["NOME"].ToString()) ? reader["NOME"].ToString() : "",
                        Cargo = !string.IsNullOrEmpty(reader["CARGO"].ToString()) ? reader["CARGO"].ToString() : "",
                        Situacao = !string.IsNullOrEmpty(reader["SITUACAO"].ToString()) ? reader["SITUACAO"].ToString() : "",
                        MetaReceitaLiquida = !string.IsNullOrEmpty(reader["META_RECEITA_LIQUIDA"].ToString()) ? Convert.ToDecimal(reader["META_RECEITA_LIQUIDA"].ToString()) : 0,
                        ValorRecebimento = !string.IsNullOrEmpty(reader["VALOR_RECEBIMENTO"].ToString())
            ? Convert.ToDecimal(reader["VALOR_RECEBIMENTO"].ToString())
            : 0,
                        DataCriacao = !string.IsNullOrEmpty(reader["DATA_CRIACAO"].ToString()) ? DateTime.Parse(reader["DATA_CRIACAO"].ToString()) : DateTime.MinValue,
                        DataAlteracao = !string.IsNullOrEmpty(reader["DATA_ALTERACAO"].ToString()) ? DateTime.Parse(reader["DATA_ALTERACAO"].ToString()) : DateTime.MinValue,
                        IdStatus = !string.IsNullOrEmpty(reader["ID_STATUS"].ToString()) ? Guid.Parse(reader["ID_STATUS"].ToString()) : new Guid()
                    };
                    meta.Regiao = await _baseRepository.BuscarRegiaoUsuario(new PainelMetaAnual { IdUsuario = meta.IdUsuario });
                    meta.MetaReceitaLiquidaCalc = !string.IsNullOrEmpty(reader["META_RECEITA_LIQUIDA_CALC"].ToString()) ? Convert.ToDecimal(reader["META_RECEITA_LIQUIDA_CALC"].ToString()) : 0;

                    metas.Add(meta);
                }
            }

            return metas;
        }

        public async Task GravarMetaFinanceira(PainelMetaFinanceira meta)
        {
            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _baseRepository.BuscarArquivoConsulta("IncluiPainelMetaFinanceira");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ID_META_FINANCEIRA", meta.IdMetaFinanceira);
                            command.Parameters.AddWithValue("@ID_USUARIO_RM", meta.IdUsuario);
                            command.Parameters.AddWithValue("@ID_LINHA_META", meta.IdLinhaMeta);
                            command.Parameters.AddWithValue("@VALOR_RECEBIMENTO", Convert.ToDecimal(meta.ValorRecebimento));
                            command.Parameters.AddWithValue("@META_RECEITA_LIQUIDA", Convert.ToDecimal(meta.MetaReceitaLiquida));
                            command.Parameters.AddWithValue("@ID_STATUS", meta.IdStatus);

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

        public async Task ModificarMetaFinanceira(PainelMetaFinanceira meta)
        {
            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _baseRepository.BuscarArquivoConsulta("AlteraPainelMetaFinanceira");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ID_META_FINANCEIRA", meta.IdMetaFinanceira);
                            command.Parameters.AddWithValue("@ID_USUARIO_RM", meta.IdUsuario);
                            command.Parameters.AddWithValue("@ID_LINHA_META", meta.IdLinhaMeta);
                            command.Parameters.AddWithValue("@VALOR_RECEBIMENTO", meta.ValorRecebimento);
                            command.Parameters.AddWithValue("@META_RECEITA_LIQUIDA", meta.MetaReceitaLiquida);
                            command.Parameters.AddWithValue("@ID_STATUS", meta.IdStatus);
                            command.Parameters.AddWithValue("@META_RECEITA_LIQUIDA_CALC", meta.MetaReceitaLiquidaCalc == null ? 0 : meta.MetaReceitaLiquidaCalc);

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

