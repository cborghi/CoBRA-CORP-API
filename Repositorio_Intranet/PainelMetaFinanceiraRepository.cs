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
    public class PainelMetaFinanceiraRepository : BaseRepository, IPainelMetaFinanceiraRepository
    {

        private readonly IBaseRepositoryPainelMeta _baseRepository;


        public PainelMetaFinanceiraRepository(IBaseRepositoryPainelMeta baseRepository, IConfiguration configuration) : base(configuration)
        {
            _baseRepository = baseRepository;

        }

        public async Task<PainelMetaFinanceira> BuscarMetaFinanceira(Guid idMetaFinanceira)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ConsultaAprovacaoMetaFinanceira");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                SqlParameter paramIdMetaFinanceira = new SqlParameter("@ID_META_FINANCEIRA", SqlDbType.UniqueIdentifier)
                {
                    Value = idMetaFinanceira
                };

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

        public async Task<PainelMetaFinanceira> BuscarMetaFinanceiraUsuario(Guid usuarioId)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ConsultaMetaFinanceiraUsuario");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                SqlParameter paramIdUsuario = new SqlParameter("@ID_USUARIO_RM", SqlDbType.UniqueIdentifier)
                {
                    Value = usuarioId
                };

                command.Parameters.Add(paramIdUsuario);

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

        public async Task<IEnumerable<PainelMetaFinanceira>> BuscarMetaFinanceira(string statusMetaFinanceira)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ConsultaPainelMetaFinanceira");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                SqlParameter paramIdStatusMeta = new SqlParameter("@DESCRICAO_STATUS_META", SqlDbType.NVarChar)
                {
                    Value = statusMetaFinanceira
                };

                command.Parameters.Add(paramIdStatusMeta);

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
                        //meta.IdLinhaMeta = Guid.Parse(reader["ID_LINHA_META"].ToString());
                        Nome = reader["NOME"].ToString(),
                        Cargo = reader["CARGO"].ToString(),
                        Grupo = reader["GRUPO_PAINEL"].ToString(),
                        Situacao = reader["SITUACAO"].ToString(),
                        Observacao = reader["OBSERVACAO"].ToString(),
                        MetaReceitaLiquida = !string.IsNullOrEmpty(reader["META_RECEITA_LIQUIDA"].ToString()) ? Convert.ToDecimal(reader["META_RECEITA_LIQUIDA"].ToString()) : 0,
                        ValorRecebimento = !string.IsNullOrEmpty(reader["VALOR_RECEBIMENTO"].ToString()) ? Convert.ToDecimal(reader["VALOR_RECEBIMENTO"].ToString()) : 0,
                        DataCriacao = !string.IsNullOrEmpty(reader["DATA_CRIACAO"].ToString()) ? DateTime.Parse(reader["DATA_CRIACAO"].ToString()) : DateTime.MinValue,
                        DataAlteracao = !string.IsNullOrEmpty(reader["DATA_ALTERACAO"].ToString()) ? DateTime.Parse(reader["DATA_ALTERACAO"].ToString()) : DateTime.MinValue,
                        IdStatus = !string.IsNullOrEmpty(reader["ID_STATUS"].ToString()) ? Guid.Parse(reader["ID_STATUS"].ToString()) : new Guid()
                    };
                    meta.Regiao = await _baseRepository.BuscarRegiaoUsuario(new PainelMetaAnual { IdUsuario = meta.IdUsuario });
                    metas.Add(meta);
                }
            }

            return metas;
        }
    }
}

