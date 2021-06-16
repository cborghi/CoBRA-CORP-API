using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class PainelMetaAnualRepository : IPainelMetaAnualRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public PainelMetaAnualRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IEnumerable<PainelMetaAnual>> ConsultaPainelMetaAnual(Guid? MetaAnualId, Guid? MetaId, Guid? LinhaMetaId, Guid? GrupoPainelId, Guid? CargoId, Guid? UsuarioId, Guid? StatusId, Guid? PeriodoId)
        {
            SqlDataReader reader = null;

            string consulta = _baseRepository.BuscarArquivoConsulta("ConsultaPainelMetaAnual");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                SqlParameter paramIdMetaAnual = new SqlParameter("@ID_META_ANUAL", SqlDbType.UniqueIdentifier);
                SqlParameter paramIdMeta = new SqlParameter("@ID_META", SqlDbType.UniqueIdentifier);
                SqlParameter paramIdLinhaMeta = new SqlParameter("@ID_LINHA_META", SqlDbType.UniqueIdentifier);
                SqlParameter paramIdGrupoPainel = new SqlParameter("@ID_GRUPO_PAINEL", SqlDbType.UniqueIdentifier);
                SqlParameter paramIdCargo = new SqlParameter("@ID_CARGO", SqlDbType.UniqueIdentifier);
                SqlParameter paramIdUsuario = new SqlParameter("@ID_USUARIO_RM", SqlDbType.UniqueIdentifier);
                SqlParameter paramIdStatus = new SqlParameter("@ID_STATUS", SqlDbType.UniqueIdentifier);
                SqlParameter paramIdPeriodo = new SqlParameter("@ID_PERIODO", SqlDbType.UniqueIdentifier);

                paramIdMetaAnual.Value = !string.IsNullOrEmpty(MetaAnualId.GetValueOrDefault().ToString()) && MetaAnualId != null ? MetaAnualId : new Guid();
                paramIdMeta.Value = !string.IsNullOrEmpty(MetaId.GetValueOrDefault().ToString()) && MetaId != null ? MetaId : new Guid();
                paramIdLinhaMeta.Value = !string.IsNullOrEmpty(LinhaMetaId.GetValueOrDefault().ToString()) && LinhaMetaId != null ? LinhaMetaId : new Guid();
                paramIdGrupoPainel.Value = !string.IsNullOrEmpty(GrupoPainelId.GetValueOrDefault().ToString()) && GrupoPainelId != null ? GrupoPainelId : new Guid();
                paramIdCargo.Value = !string.IsNullOrEmpty(CargoId.GetValueOrDefault().ToString()) && CargoId != null ? CargoId : new Guid();
                paramIdUsuario.Value = !string.IsNullOrEmpty(UsuarioId.GetValueOrDefault().ToString()) && UsuarioId != null ? UsuarioId : new Guid();
                paramIdStatus.Value = !string.IsNullOrEmpty(StatusId.GetValueOrDefault().ToString()) && StatusId != null ? StatusId : new Guid();
                paramIdPeriodo.Value = !string.IsNullOrEmpty(PeriodoId.GetValueOrDefault().ToString()) && PeriodoId != null ? PeriodoId : new Guid();

                command.Parameters.Add(paramIdMetaAnual);
                command.Parameters.Add(paramIdMeta);
                command.Parameters.Add(paramIdLinhaMeta);
                command.Parameters.Add(paramIdGrupoPainel);
                command.Parameters.Add(paramIdCargo);
                command.Parameters.Add(paramIdUsuario);
                command.Parameters.Add(paramIdStatus);
                command.Parameters.Add(paramIdPeriodo);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    IEnumerable<PainelMetaAnual> item = await LerDataReaderMetaAnual(reader);

                    if (MetaAnualId == null && MetaId == null && LinhaMetaId == null && GrupoPainelId == null && CargoId == null && UsuarioId == null && StatusId == null)
                    {
                        return item.GroupBy(x => new { x.IdUsuario, x.Nome, x.Cargo, x.Situacao })
                                   .Select(x => new PainelMetaAnual
                                   {
                                       IdMetaAnual = new Guid(),
                                       IdUsuario = x.FirstOrDefault().IdUsuario,
                                       IdLinhaMeta = new Guid(),
                                       IdUnidadeMedida = new Guid(),
                                       Nome = x.FirstOrDefault().Nome,
                                       Cargo = x.FirstOrDefault().Cargo,
                                       Grupo = "",
                                       Situacao = x.FirstOrDefault().Situacao,
                                       Meta = "",
                                       Indicador = "",
                                       Peso = 0,
                                       UnidadeMedida = "",
                                       Total = 0,
                                       ValorMinimo = 0,
                                       ValorMaximo = 0,
                                       IdCargo = x.FirstOrDefault().IdCargo,
                                       Observacao = "",
                                       IdStatus = new Guid(),
                                       DataCriacao = DateTime.MinValue,
                                       DataAlteracao = DateTime.MinValue,
                                       Regiao = x.FirstOrDefault().Regiao,
                                       RegiaoAtuacao = x.FirstOrDefault().Regiao                                   })
                                   .Distinct();
                    }
                    else
                    {
                        return item;
                    }
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

        public async Task<IEnumerable<PainelMetaAnual>> GestaoPessoaConsultaPainelMetaAnual(Guid? metaAnualId, Guid? statusId, Guid? usuarioId)
        {
            SqlDataReader reader = null;

            string consulta = _baseRepository.BuscarArquivoConsulta("ConsultaGestaoPessoaPainelMetaAnual");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                SqlParameter paramIdMetaAnual = new SqlParameter("@ID_META_ANUAL", SqlDbType.UniqueIdentifier);
                SqlParameter paramIdStatus = new SqlParameter("@ID_STATUS", SqlDbType.UniqueIdentifier);
                SqlParameter paramIdUsuario = new SqlParameter("@ID_USUARIO_RM", SqlDbType.UniqueIdentifier);

                paramIdMetaAnual.Value = !string.IsNullOrEmpty(metaAnualId.GetValueOrDefault().ToString()) && metaAnualId != null ? metaAnualId : new Guid();
                paramIdStatus.Value = !string.IsNullOrEmpty(statusId.GetValueOrDefault().ToString()) && statusId != null ? statusId : new Guid();
                paramIdUsuario.Value = !string.IsNullOrEmpty(usuarioId.GetValueOrDefault().ToString()) && usuarioId != null ? usuarioId : new Guid();

                command.Parameters.Add(paramIdMetaAnual);
                command.Parameters.Add(paramIdStatus);
                command.Parameters.Add(paramIdUsuario);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    IEnumerable<PainelMetaAnual> item = await LerDataReaderMetaIndividual(reader);

                    return item.GroupBy(x => new { x.IdUsuario, x.Nome, x.Cargo, x.Situacao })
                               .Select(x => new PainelMetaAnual
                               {
                                   IdMetaAnual = x.FirstOrDefault().IdMetaAnual,
                                   IdUsuario = x.FirstOrDefault().IdUsuario,
                                   IdLinhaMeta = x.FirstOrDefault().IdLinhaMeta,
                                   IdUnidadeMedida = x.FirstOrDefault().IdUnidadeMedida,
                                   Nome = x.FirstOrDefault().Nome,
                                   Cargo = x.FirstOrDefault().Cargo,
                                   Grupo = x.FirstOrDefault().Grupo,
                                   Situacao = x.FirstOrDefault().Situacao,
                                   Meta = x.FirstOrDefault().Meta,
                                   Indicador = x.FirstOrDefault().Indicador,
                                   Peso = x.FirstOrDefault().Peso,
                                   UnidadeMedida = x.FirstOrDefault().UnidadeMedida,
                                   Total = x.FirstOrDefault().Total,
                                   ValorMinimo = x.FirstOrDefault().ValorMinimo,
                                   ValorMaximo = x.FirstOrDefault().ValorMaximo,
                                   IdStatus = x.FirstOrDefault().IdStatus,
                                   DataCriacao = x.FirstOrDefault().DataCriacao,
                                   DataAlteracao = x.FirstOrDefault().DataAlteracao,
                                   Regiao = x.FirstOrDefault().Regiao
                               })
                               .Distinct();

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

        public async Task<IEnumerable<PainelMetaAnual>> GestaoPessoaConsultaAprovacaoMetaAnual(Guid? usuarioId)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ConsultaGestaoPessoaAprovacaoMetaAnual");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                SqlParameter paramIdUsuario = new SqlParameter("@ID_USUARIO_RM", SqlDbType.UniqueIdentifier)
                {
                    Value = !string.IsNullOrEmpty(usuarioId.GetValueOrDefault().ToString()) && usuarioId != null ? usuarioId : new Guid()
                };

                command.Parameters.Add(paramIdUsuario);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    IEnumerable<PainelMetaAnual> item = await LerDataReaderMetaIndividual(reader);

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

        private async Task<IEnumerable<PainelMetaAnual>> LerDataReaderMetaAnual(SqlDataReader reader)
        {
            var metas = new List<PainelMetaAnual>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    PainelMetaAnual meta = new PainelMetaAnual
                    {
                        IdMetaAnual = !string.IsNullOrEmpty(reader["ID_META_ANUAL"].ToString()) ? Guid.Parse(reader["ID_META_ANUAL"].ToString()) : new Guid(),
                        IdUsuario = Guid.Parse(reader["ID_USUARIO_RM"].ToString()),
                        IdLinhaMeta = Guid.Parse(reader["ID_LINHA_META"].ToString()),
                        IdUnidadeMedida = !string.IsNullOrEmpty(reader["ID_UNIDADE_MEDIDA_PAINEL"].ToString()) ? Guid.Parse(reader["ID_UNIDADE_MEDIDA_PAINEL"].ToString()) : new Guid(),
                        Nome = reader["NOME"].ToString(),
                        Cargo = reader["CARGO"].ToString(),
                        Grupo = reader["GRUPO_PAINEL"].ToString(),
                        Situacao = reader["SITUACAO"].ToString(),
                        Meta = reader["META"].ToString(),
                        IdMeta = Guid.Parse(reader["ID_META"].ToString()),
                        Indicador = reader["INDICADOR"].ToString(),
                        Peso = Convert.ToInt32(reader["PESO"].ToString()),
                        UnidadeMedida = reader["UNIDADE_MEDIDA"].ToString(),
                        Total = !string.IsNullOrEmpty(reader["META_ANUAL"].ToString()) ? Convert.ToDecimal(reader["META_ANUAL"].ToString()) : 0,
                        ValorMinimo = !string.IsNullOrEmpty(reader["VALOR_MINIMO"].ToString()) ? Convert.ToDecimal(reader["VALOR_MINIMO"].ToString()) : 0,
                        ValorMaximo = !string.IsNullOrEmpty(reader["VALOR_MAXIMO"].ToString()) ? Convert.ToDecimal(reader["VALOR_MAXIMO"].ToString()) : 0,
                        DataCriacao = !string.IsNullOrEmpty(reader["DATA_CRIACAO"].ToString()) ? DateTime.Parse(reader["DATA_CRIACAO"].ToString()) : DateTime.MinValue,
                        DataAlteracao = !string.IsNullOrEmpty(reader["DATA_ALTERACAO"].ToString()) ? DateTime.Parse(reader["DATA_ALTERACAO"].ToString()) : DateTime.MinValue,
                        IdStatus = !string.IsNullOrEmpty(reader["ID_STATUS"].ToString()) ? Guid.Parse(reader["ID_STATUS"].ToString()) : new Guid()
                    };
                    meta.Regiao = await BuscarRegiaoUsuario(meta);
                    meta.RegiaoAtuacao = reader["REGIAO"].ToString();
                    meta.DataAdmissao = !string.IsNullOrEmpty(reader["ADMISSAO"].ToString()) ? Convert.ToDateTime(reader["ADMISSAO"].ToString()) : DateTime.MinValue;
                    meta.DataDemissao = !string.IsNullOrEmpty(reader["DEMISSAO"].ToString()) ? Convert.ToDateTime(reader["DEMISSAO"].ToString()) : DateTime.MinValue;

                    metas.Add(meta);
                }
            }

            return metas;
        }

        private async Task<IEnumerable<PainelMetaAnual>> LerDataReaderMetaIndividual(SqlDataReader reader)
        {
            var metas = new List<PainelMetaAnual>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    PainelMetaAnual meta = new PainelMetaAnual
                    {
                        IdMetaAnual = !string.IsNullOrEmpty(reader["ID_META_ANUAL"].ToString()) ? Guid.Parse(reader["ID_META_ANUAL"].ToString()) : new Guid(),
                        IdUsuario = Guid.Parse(reader["ID_USUARIO_RM"].ToString()),
                        IdLinhaMeta = Guid.Parse(reader["ID_LINHA_META"].ToString()),
                        IdUnidadeMedida = !string.IsNullOrEmpty(reader["ID_UNIDADE_MEDIDA_PAINEL"].ToString()) ? Guid.Parse(reader["ID_UNIDADE_MEDIDA_PAINEL"].ToString()) : new Guid(),
                        Nome = reader["NOME"].ToString(),
                        Cargo = reader["CARGO"].ToString(),
                        Grupo = reader["GRUPO_PAINEL"].ToString(),
                        Situacao = reader["SITUACAO"].ToString(),
                        Meta = reader["META"].ToString(),
                        Indicador = reader["INDICADOR"].ToString(),
                        Peso = Convert.ToInt32(reader["PESO"].ToString()),
                        UnidadeMedida = reader["UNIDADE_MEDIDA"].ToString(),
                        Total = !string.IsNullOrEmpty(reader["META_ANUAL"].ToString()) ? Convert.ToDecimal(reader["META_ANUAL"].ToString()) : 0,
                        ValorMinimo = !string.IsNullOrEmpty(reader["VALOR_MINIMO"].ToString()) ? Convert.ToDecimal(reader["VALOR_MINIMO"].ToString()) : 0,
                        ValorMaximo = !string.IsNullOrEmpty(reader["VALOR_MAXIMO"].ToString()) ? Convert.ToDecimal(reader["VALOR_MAXIMO"].ToString()) : 0,
                        DataCriacao = !string.IsNullOrEmpty(reader["DATA_CRIACAO"].ToString()) ? DateTime.Parse(reader["DATA_CRIACAO"].ToString()) : DateTime.MinValue,
                        DataAlteracao = !string.IsNullOrEmpty(reader["DATA_ALTERACAO"].ToString()) ? DateTime.Parse(reader["DATA_ALTERACAO"].ToString()) : DateTime.MinValue,
                        IdStatus = !string.IsNullOrEmpty(reader["ID_STATUS"].ToString()) ? Guid.Parse(reader["ID_STATUS"].ToString()) : new Guid()
                    };
                    meta.Regiao = await BuscarRegiaoUsuario(meta);

                    metas.Add(meta);
                }
            }

            return metas;
        }

        public async Task SalvarPainelMetaAnual(PainelMetaAnual meta)
        {
            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _baseRepository.BuscarArquivoConsulta("IncluiPainelMetaAnual");

                        if (!meta.IdMetaAnual.Equals(new Guid("00000000-0000-0000-0000-000000000000")))
                        {
                            consulta = _baseRepository.BuscarArquivoConsulta("AlteraPainelMetaAnual");
                        }

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ID_META_ANUAL", meta.IdMetaAnual);
                            command.Parameters.AddWithValue("@ID_USUARIO_RM", meta.IdUsuario);
                            command.Parameters.AddWithValue("@ID_LINHA_META", meta.IdLinhaMeta);
                            command.Parameters.AddWithValue("@ID_UNIDADE_MEDIDA_PAINEL", meta.IdUnidadeMedida);
                            command.Parameters.AddWithValue("@TOTAL", meta.Total);
                            command.Parameters.AddWithValue("@VALOR_MINIMO", meta.ValorMinimo);
                            command.Parameters.AddWithValue("@VALOR_MAXIMO", meta.ValorMaximo);
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

        public Task<string> BuscarRegiaoUsuario(PainelMetaAnual meta)
        {
            return _baseRepository.BuscarRegiaoUsuario(meta);
        }
    }
}
