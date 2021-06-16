using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CoBRA.Infra.Intranet
{
    public class PainelRepository : IPainelRepository, IPainelIndicadorMetaRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public PainelRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;

        }

        public async Task GravarPainelMeta(CabecalhoPainelMeta painelMeta)
        {
            SqlTransaction transaction = null;

            try
            {
                using (var connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {

                        string scriptCabecalho = _baseRepository.BuscarArquivoConsulta("InsertCabecalhoPainel");

                        using (SqlCommand command = new SqlCommand(scriptCabecalho, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ID_GRUPO_PAINEL", ((Object)painelMeta.GrupoPainel.IdGrupo) ?? DBNull.Value);
                            SqlDataReader reader = await command.ExecuteReaderAsync();

                            await reader.ReadAsync();
                            painelMeta.Id = Guid.Parse(reader["ID_META"].ToString());
                            reader.Close();
                        }

                        string scriptLinhas = _baseRepository.BuscarArquivoConsulta("InsertLinhasPainel");

                        using (SqlCommand command = new SqlCommand(scriptLinhas, connection, transaction))
                        {
                            command.Parameters.Add("@ID_META", SqlDbType.UniqueIdentifier);
                            command.Parameters.Add("@INDICADOR", SqlDbType.VarChar);
                            command.Parameters.Add("@META", SqlDbType.VarChar);
                            command.Parameters.Add("@ID_ORIGEM", SqlDbType.UniqueIdentifier);
                            command.Parameters.Add("@PESO", SqlDbType.TinyInt);


                            foreach (var linha in painelMeta.LinhasPainel)
                            {
                                command.Parameters["@ID_META"].Value = painelMeta.Id;
                                command.Parameters["@INDICADOR"].Value = linha.Indicador;
                                command.Parameters["@META"].Value = linha.Meta;
                                command.Parameters["@ID_ORIGEM"].Value = linha.DadosOrigemPainel.IdOrigem;
                                command.Parameters["@PESO"].Value = linha.Peso;
                                await command.ExecuteNonQueryAsync();
                            }
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

        public async Task<byte> ObterPesoTotalMeta(CabecalhoPainelMeta painelMeta)
        {
            try
            {
                using (var connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    string scriptCabecalho = _baseRepository.BuscarArquivoConsulta("ObterPesoTotalPainelGrupo");

                    using (SqlCommand command = new SqlCommand(scriptCabecalho, connection))
                    {
                        command.Parameters.AddWithValue("@ID_GRUPO_PAINEL", ((Object)painelMeta.GrupoPainel.IdGrupo) ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ID_LINHA_META", ((Object)painelMeta.LinhasPainel.FirstOrDefault()?.IdLinhaPainelMeta) ?? DBNull.Value);
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        await reader.ReadAsync();
                        byte pesoTotal = byte.TryParse(reader["PESO_TOTAL"].ToString(), out byte peso) ? peso : byte.MinValue;
                        reader.Close();

                        return pesoTotal;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> VerificarMetaCadastrada(CabecalhoPainelMeta painelMeta)
        {
            try
            {
                using (var connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    string scriptCabecalho = _baseRepository.BuscarArquivoConsulta("VerificarMetaCadastrada");

                    using (SqlCommand command = new SqlCommand(scriptCabecalho, connection))
                    {
                        command.Parameters.AddWithValue("@ID_GRUPO_PAINEL", ((Object)painelMeta.GrupoPainel.IdGrupo) ?? DBNull.Value);
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        await reader.ReadAsync();
                        bool possuiMetaCadastrada = bool.TryParse(reader["POSSUI_META"].ToString(), out bool possuiMeta) ? possuiMeta : false;
                        reader.Close();

                        return possuiMetaCadastrada;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CabecalhoPainelMeta>> BuscarCabecalhoPainel(CabecalhoPainelMeta painelMeta, Guid? PeriodoId)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarCabecalhoPainel");
            Guid? idp = new Guid("00000000-0000-0000-0000-000000000000");
            if(PeriodoId != null)
            {
                idp = PeriodoId;
            }

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_META", ((Object)painelMeta?.Id) ?? DBNull.Value);
                command.Parameters.AddWithValue("@ID_GRUPO_PAINEL", ((Object)painelMeta?.GrupoPainel?.IdGrupo) ?? DBNull.Value);
                command.Parameters.AddWithValue("@ID_STATUS_PAINEL", ((Object)painelMeta?.StatusPainel?.IdStatusPainel) ?? DBNull.Value);
                command.Parameters.AddWithValue("@OBSERVACAO", ((Object)painelMeta?.Observacao) ?? DBNull.Value);
                command.Parameters.AddWithValue("@ID_PERIODO", idp);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return await LerDataReaderCabecalho(reader);
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

        public async Task<IEnumerable<CabecalhoPainelMeta>> FiltrarCabecalhoPainel(CabecalhoPainelMeta painelMeta)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("FiltrarCabecalhoPainel");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_META", ((Object)painelMeta?.Id) ?? DBNull.Value);
                command.Parameters.AddWithValue("@ID_GRUPO_PAINEL", ((Object)painelMeta?.GrupoPainel?.IdGrupo) ?? DBNull.Value);
                command.Parameters.AddWithValue("@ID_STATUS_PAINEL", ((Object)painelMeta?.StatusPainel?.IdStatusPainel) ?? DBNull.Value);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return await LerDataReaderCabecalho(reader);
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

        public async Task<IList<LinhaPainelMeta>> BuscarLinhaPainel(LinhaPainelMeta linha)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarLinhaPainel");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_LINHA_META", ((Object)linha?.IdLinhaPainelMeta) ?? DBNull.Value);
                command.Parameters.AddWithValue("@ID_META", DBNull.Value);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderLinha(reader);
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

        private async Task<IList<LinhaPainelMeta>> BuscarLinhaPainel(CabecalhoPainelMeta cabecalho)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarLinhaPainel");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_META", ((Object)cabecalho?.Id) ?? DBNull.Value);
                command.Parameters.AddWithValue("@ID_LINHA_META", DBNull.Value);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderLinha(reader);
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

        public async Task AtualizarLinhaPainel(LinhaPainelMeta linha)
        {
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _baseRepository.BuscarArquivoConsulta("AtualizarLinhaPainel");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ID_LINHA_META", linha.IdLinhaPainelMeta);
                            command.Parameters.AddWithValue("@META", linha.Meta);
                            command.Parameters.AddWithValue("@INDICADOR", linha.Indicador);
                            command.Parameters.AddWithValue("@PESO", linha.Peso);
                            command.Parameters.AddWithValue("@ID_ORIGEM", linha.DadosOrigemPainel.IdOrigem);
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

        public async Task EnviarPainelAprovacao(CabecalhoPainelMeta cabecalho)
        {
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _baseRepository.BuscarArquivoConsulta("EnviarPainelAprovacao");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ID_GRUPO_PAINEL", cabecalho.GrupoPainel.IdGrupo);
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

        public async Task ExcluirLinhaPainel(LinhaPainelMeta linha)
        {
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _baseRepository.BuscarArquivoConsulta("ExcluirLinhaPainel");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ID_LINHA_META", linha.IdLinhaPainelMeta);
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

        public async Task ExcluirCabecalhoPainel(CabecalhoPainelMeta cabecalho)
        {
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _baseRepository.BuscarArquivoConsulta("ExcluirCabecalhoPainel");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ID_GRUPO_PAINEL", cabecalho.GrupoPainel.IdGrupo);
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

        public async Task<CabecalhoRelatorioPainel> BuscarDadosCabecalhoRelatorio(Guid? idUsuario) 
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarCabecalhoRelatorio");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_USUARIO", ((Object)idUsuario) ?? DBNull.Value);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderCabecalhoRelatorio(reader);
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

        public async Task<IEnumerable<PeriodoCampanha>> BuscarPeriodo()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarPeriodo");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderPeriodoCampanha(reader);
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

        public IEnumerable<PeriodoCampanha> LerDataReaderPeriodoCampanha(SqlDataReader reader)
        {
            Collection<PeriodoCampanha> periodo = null;

            if (reader.HasRows)
            {
                periodo = new Collection<PeriodoCampanha>();
                while (reader.Read())
                {
                    PeriodoCampanha pc = new PeriodoCampanha
                    {
                        Id_Periodo = reader["ID_PERIODO"].ToString(),
                        Descricao = reader["DESCRICAO"].ToString(),
                        Data_Inicio = Convert.ToDateTime(reader["DATA_INICIO"].ToString()),
                        Dat_Fim = Convert.ToDateTime(reader["DATA_FIM"].ToString())
                    };


                    periodo.Add(pc);
                }
            }

            return periodo;
        }

        private async Task<IEnumerable<CabecalhoPainelMeta>> LerDataReaderCabecalho(SqlDataReader reader)
        {
            Collection<CabecalhoPainelMeta> paineis = null;

            if (reader.HasRows)
            {
                paineis = new Collection<CabecalhoPainelMeta>();
                CabecalhoPainelMeta cabecalho = null;
                while (reader.Read())
                {
                    cabecalho = new CabecalhoPainelMeta();

                    cabecalho = new CabecalhoPainelMeta
                    {
                        Id = Guid.Parse(reader["ID_META"].ToString())
                    };
                    cabecalho.LinhasPainel = await BuscarLinhaPainel(cabecalho);
                    cabecalho.StatusPainel = new StatusPainel()
                    {
                        IdStatusPainel = Guid.Parse(reader["ID_STATUS"].ToString()),
                        Descricao = reader["DESCRICAO_STATUS"].ToString()
                    };
                    cabecalho.GrupoPainel = new GrupoPainel()
                    {
                        IdGrupo = Guid.Parse(reader["ID_GRUPO_PAINEL"].ToString()),
                        Descricao = reader["DESCRICAO_GRUPO"].ToString()
                    };
                    cabecalho.Observacao = reader["OBSERVACAO"].ToString();
                    paineis.Add(cabecalho);

                }
            }
            if (paineis != null)
            {
                foreach (var item in paineis)
                {
                    item.LinhasPainel = item.LinhasPainel.OrderBy(a => a.Indicador).ToList();
                }
            }

            return paineis;
        }

        private IList<LinhaPainelMeta> LerDataReaderLinha(SqlDataReader reader)
        {
            IList<LinhaPainelMeta> linhas = null;

            if (reader.HasRows)
            {
                linhas = new Collection<LinhaPainelMeta>();
                var linha = new LinhaPainelMeta();

                while (reader.Read())
                {
                    linha = new LinhaPainelMeta
                    {
                        IdLinhaPainelMeta = Guid.Parse(reader["ID_LINHA_META"].ToString()),
                        Indicador = reader["INDICADOR"].ToString(),
                        Meta = reader["META"].ToString(),
                        Peso = Convert.ToByte(reader["PESO"]),
                        DadosOrigemPainel = new DadosOrigemPainel
                        {
                            IdOrigem = Guid.Parse(reader["ID_ORIGEM"].ToString()),
                            Descricao = reader["DESCRICAO"].ToString()
                        },
                    };

                    linhas.Add(linha);
                }
            }

            return linhas;
        }

        private CabecalhoRelatorioPainel LerDataReaderCabecalhoRelatorio(SqlDataReader reader)
        {
            var cabecalho = new CabecalhoRelatorioPainel();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cabecalho = new CabecalhoRelatorioPainel
                    {
                        NomeUsuario = reader["NOME"].ToString(),
                        Cargo = reader["CARGO"].ToString(),
                        Filial = reader["FILIAL"].ToString(),
                        DataAdmissao = DateTime.Parse(reader["DATA_ADMISSAO"].ToString()),
                        DataDemissao = DateTime.TryParse(reader["DATA_DEMISSAO"].ToString(), out DateTime dataDemissao) ? dataDemissao : DateTime.MinValue,
                        DataInicioCampanha = DateTime.Parse(reader["DATA_INICIO_CAMPANHA"].ToString()),
                        DataFimCampanha = DateTime.Parse(reader["DATA_FIM_CAMPANHA"].ToString())
                    };
                }
            }

            return cabecalho;
        }

        public int BuscarPorcentagemMinimaPagamento()
        {
            SqlDataReader reader = null;

            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarPorcentagemMinimaPagamento");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();

                    return LerDataReaderPorcentagemMinima(reader);
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

        private int LerDataReaderPorcentagemMinima(SqlDataReader reader)
        {
            int porcentagemMinima = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    porcentagemMinima = int.Parse(reader["PORCENTAGEM_MINIMA_PAGAMENTO"].ToString());
                }
            }

            return porcentagemMinima;
        }

        public int BuscarPorcentagemPagamento(decimal? porcentagemRealizada)
        {
            SqlDataReader reader = null;

            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarPorcentagemPagamento");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@PORCENTAGEM_RESULTADO", porcentagemRealizada);

                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();

                    return LerDataReaderPorcentagemPagamento(reader);
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

        private int LerDataReaderPorcentagemPagamento(SqlDataReader reader)
        {
            int porcentagemPagamento = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    porcentagemPagamento = int.Parse(reader["PORCENTAGEM_PAGAMENTO"].ToString());
                }
            }

            return porcentagemPagamento;
        }
    }
}

