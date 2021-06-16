using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class RelatorioElvisRepository : BaseRepository, IRelatorioElvisRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public RelatorioElvisRepository(IBaseRepositoryPainelMeta baseRepository, IConfiguration configuration): base(configuration)
        {
            _baseRepository = baseRepository;
        }


        public async Task<IList<RelatorioElvis>> FiltrarItensRelatorio(FiltroRelatorioElvis filtro)
        {
            SqlDataReader reader = null;
            
            string consulta = _baseRepository.BuscarArquivoConsulta("FiltrarItensRelatorio");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@IMPRESSO", filtro.Impresso);
                command.Parameters.AddWithValue("@PROGRAMA", ((Object)ConverterSelecaoGeral(filtro.ProgramaSelecionado)) ?? DBNull.Value);
                command.Parameters.AddWithValue("@DISCIPLINA", ((Object)ConverterSelecaoGeral(filtro.DisciplinaSelecionada)) ?? DBNull.Value);
                command.Parameters.AddWithValue("@COLECAO", ((Object)ConverterSelecaoGeral(filtro.ColecaoSelecionada)) ?? DBNull.Value);
                command.Parameters.AddWithValue("@ANO", ((Object)ConverterSelecaoGeral(filtro.AnoSelecionado)) ?? DBNull.Value);
                command.Parameters.AddWithValue("@QUANTIDADE_MAX_REGISTROS", ((Object)filtro.ParametroPesquisa.QuantidadeMaximaItensPesquisa) ?? DBNull.Value);
                command.Parameters.AddWithValue("@NUMERO_PAGINA", ((Object)filtro.ParametroPesquisa.NumeroPagina) ?? DBNull.Value);
                command.Parameters.AddWithValue("@ORDENACAO", ((Object)filtro.ParametroPesquisa.Ordenacao) ?? DBNull.Value);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    return LerDataReaderRelatorio(reader);
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

        public async Task<IList<RelatorioElvis>> ListarItensRelatorio()
        {
            SqlDataReader reader = null;
            
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarRelatorioElvis");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    return LerDataReaderRelatorio(reader);
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

        private IList<RelatorioElvis> LerDataReaderRelatorio(SqlDataReader reader)
        {
            var relatorios = new List<RelatorioElvis>();

            if (reader.HasRows)
            {
                relatorios = new List<RelatorioElvis>();
                RelatorioElvis item;
                while (reader.Read())
                {
                    item = new RelatorioElvis
                    {
                        IdRelatorio = Guid.Parse(reader["ID_RELATORIO"].ToString()),
                        TituloLivro = reader["TITULO"].ToString(),
                        ParteLivro = reader["PARTE_LIVRO"].ToString(),
                        QtdTotal = int.TryParse(reader["QTD_TOTAL"].ToString(), out int qtdTotal) ? qtdTotal : 0,
                        QtdTotalMenosCaiu = int.TryParse(reader["QTD_TOTAL_MENOS_CAIU"].ToString(), out int qtdTotalMenosCaiu) ? qtdTotalMenosCaiu : 0,
                        QtdAprovada = int.TryParse(reader["QTD_APROVADA"].ToString(), out int qtdAprovada) ? qtdAprovada : 0,
                        QtdFinalizado = int.TryParse(reader["QTD_FINALIZADA"].ToString(), out int qtdFinalizado) ? qtdFinalizado : 0,
                        QtdCaiu = int.TryParse(reader["QTD_CAIU"].ToString(), out int qtdCaiu) ? qtdCaiu : 0,
                        QtdReprovada = int.TryParse(reader["QTD_REPROVADA"].ToString(), out int qtdReprovada) ? qtdReprovada : 0,
                        QtdAguardandoAprovacao = int.TryParse(reader["QTD_AGUARDANDO_APROVACAO"].ToString(), out int qtdAguardandoAprovacao) ? qtdAguardandoAprovacao : 0,
                        QtdEmPesquisa = int.TryParse(reader["QTD_EM_PESQUISA"].ToString(), out int qtdEmPesquisa) ? qtdEmPesquisa : 0,
                        QtdAprovacaoOrcamento = int.TryParse(reader["QTD_APROVACAO_ORCAMENTO"].ToString(), out int qtdAprovacaoOrcamento) ? qtdAprovacaoOrcamento : 0,
                        QtdOutrosStatus = int.TryParse(reader["QTD_OUTROS_STATUS"].ToString(), out int qtdOutrosStatus) ? qtdOutrosStatus : 0,
                        PorcentagemFinalizado = Decimal.TryParse(reader["PORCENTAGEM_FINALIZADA"].ToString(), out decimal porcentagemFinalizado) ? porcentagemFinalizado : 0,
                        QtdPendente = int.TryParse(reader["QTD_PENDENTE"].ToString(), out int qtdPendente) ? qtdPendente : 0,
                        DtCadastro = DateTime.Parse(reader["DT_CADASTRO"].ToString()),
                        TotalItens = Convert.ToInt32(reader["QTD_TOTAL_REGISTRO"]),
                        
                };

                    relatorios.Add(item);
                }
            }

            return relatorios;
        }


        public async Task<IList<ObraAndamento>> FiltrarRelatorioObrasAndamento(FiltroObraAndamento filtro)
        {
            SqlDataReader reader = null;

            string consulta = _baseRepository.BuscarArquivoConsulta("FiltrarRelatorioObrasAndamento");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@DESCRICAO", ((Object) filtro?.Descricao) ?? DBNull.Value);
                command.Parameters.AddWithValue("@PROGRAMA", ((Object)filtro?.ProgramaSelecionado) ?? DBNull.Value);
                command.Parameters.AddWithValue("@QUANTIDADE_MAX_REGISTROS", ((Object)filtro.ParametroPesquisa.QuantidadeMaximaItensPesquisa) ?? DBNull.Value);
                command.Parameters.AddWithValue("@NUMERO_PAGINA", ((Object)filtro.ParametroPesquisa.NumeroPagina) ?? DBNull.Value);
                command.Parameters.AddWithValue("@ORDENACAO", ((Object)filtro.ParametroPesquisa.Ordenacao) ?? DBNull.Value);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    return LerDataReaderObraAndamento(reader);
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

        private IList<ObraAndamento> LerDataReaderObraAndamento(SqlDataReader reader)
        {
            var relatorios = new List<ObraAndamento>();

            if (reader.HasRows)
            {
                relatorios = new List<ObraAndamento>();
                ObraAndamento item;
                while (reader.Read())
                {
                    item = new ObraAndamento
                    {
                        CodigoObra = reader["CODIGO_OBRA"].ToString(),
                        Titulo = reader["TITULO"].ToString(),
                        QuantidadeCollection = int.TryParse(reader["QTD_COLLECTION"].ToString(), out int quantidadeCollection) ? quantidadeCollection : 0,
                        ValorOrcado = decimal.TryParse(reader["VALOR_ORCADO"].ToString(), out decimal valorOrcado) ? valorOrcado : 0,
                        ValorGasto = decimal.TryParse(reader["VALOR_GASTO"].ToString(), out decimal valorGasto) ? valorGasto : 0,
                        PorcentagemGasta = decimal.TryParse(reader["PORCENTAGEM_GASTA"].ToString(), out decimal porcentagemGasta) ? porcentagemGasta : 0,
                        DtCadastro = DateTime.Parse(reader["DT_CADASTRO"].ToString()),
                        TotalItens = Convert.ToInt32(reader["QTD_TOTAL_REGISTRO"]),
                        Programa = reader["PROGRAMA"].ToString()
                    };

                    relatorios.Add(item);
                }
            }

            return relatorios;
        }


        public async Task<IList<FiltroRelatorioElvis>> CarregarPrograma()
        {
            SqlDataReader reader = null;

            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarProgramaElvis");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    return LerDataReaderFiltro(reader);
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

        public async Task<IList<FiltroRelatorioElvis>> CarregarProgramaObraAndamento()
        {
            SqlDataReader reader = null;

            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarProgramaObraAndamento");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    return LerDataReaderFiltro(reader);
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

        public async Task<IList<FiltroRelatorioElvis>> CarregarDisciplina()
        {
            SqlDataReader reader = null;

            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarDisciplinaElvis");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    return LerDataReaderFiltro(reader);
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

        public async Task<IList<FiltroRelatorioElvis>> CarregarColecao()
        {
            SqlDataReader reader = null;

            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarColecaoElvis");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    return LerDataReaderFiltro(reader);
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

        public async Task<IList<FiltroRelatorioElvis>> CarregarAnoEscolar()
        {
            SqlDataReader reader = null;

            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarAnoEscolarElvis");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    return LerDataReaderFiltro(reader);
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

        private IList<FiltroRelatorioElvis> LerDataReaderFiltro(SqlDataReader reader)
        {
            var itensFiltro = new List<FiltroRelatorioElvis>();

            if (reader.HasRows)
            {
                itensFiltro = new List<FiltroRelatorioElvis>();
                FiltroRelatorioElvis item;
                while (reader.Read())
                {
                    item = new FiltroRelatorioElvis
                    {
                        
                        Descricao= reader["DESCRICAO"].ToString(),
                    };

                    if (string.IsNullOrEmpty(item.Descricao))
                        item.Descricao = "Não preenchido";

                    itensFiltro.Add(item);
                }
            }

            return itensFiltro;
        }

        private string ConverterSelecaoGeral(string descricao)
        {
            if (descricao.Equals("Não preenchido"))
                return string.Empty;

            return descricao;
        }
    }
}
