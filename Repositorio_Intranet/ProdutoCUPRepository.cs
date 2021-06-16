using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class ProdutoCUPRepository : BaseRepository, IProdutoCUPRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public ProdutoCUPRepository(IBaseRepositoryPainelMeta baseRepository, IConfiguration configuration) : base(configuration)
        {
            _baseRepository = baseRepository;
        }

        #region Produto

        public ProdutoPaginadoCUP CarregarProdutosCUP(int NumeroPagina, int RegistrosPagina, bool ebook)
        {
            string consulta = "";
            if (ebook)
            {
                consulta = _baseRepository.BuscarArquivoConsulta("CarregarProdutosEbookCUP");
            }
            else
            {
                consulta = _baseRepository.BuscarArquivoConsulta("CarregarProdutosCUP");
            }

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@NumeroPagina", NumeroPagina);
                command.Parameters.AddWithValue("@RegistrosPagina", RegistrosPagina);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderProdutosPaginadosCUP(reader);
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

        public ProdutoPaginadoCUP CarregarProdutosFiltroCUP(int NumeroPagina, int RegistrosPagina, string Filtro, bool ebook)
        {
            string consulta = "";
            if (ebook)
            {
                consulta = _baseRepository.BuscarArquivoConsulta("CarregarProdutosFiltroEbookCUP");
            }
            else
            {
                consulta = _baseRepository.BuscarArquivoConsulta("CarregarProdutosFiltroCUP");
            }

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@NumeroPagina", NumeroPagina);
                command.Parameters.AddWithValue("@RegistrosPagina", RegistrosPagina);
                command.Parameters.AddWithValue("@Filtro", Filtro);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderProdutosPaginadosCUP(reader);
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

        public ProdutoCUP CarregarProdutosIdCUP(long IdProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarProdutosIdCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@IdProduto", IdProduto);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderProdutosCUP(reader);
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

        public List<ProdutoCUP> CarregarProdutosRelatorioCabecalhoCUP(string Titulo)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarProdutosTituloCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@Titulo", Titulo);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderProdutosListCUP(reader);
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

        public List<AssuntoCUP> CarregarAssuntoIdProdutoCUP(long IdProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarAssuntoIdProdutoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@IdProduto", IdProduto);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderAssuntoProdutoCUP(reader);
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

        public List<TemaCUP> CarregarTemaIdProdutoCUP(long IdProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarTemaIdProdutoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@IdProduto", IdProduto);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderTemaProdutoCUP(reader);
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

        public List<DataEspecialCUP> CarregarDatasEspeciaisIdProdutoCUP(long IdProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarDatasIdProdutoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@IdProduto", IdProduto);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderDatasEspeciaisProdutoCUP(reader);
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

        public MioloCUP CarregarMioloIdProdutoCUP(long IdProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarMioloIdProdutoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@IdProduto", IdProduto);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderMioloProdutoCUP(reader);
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

        public CapaCUP CarregarCapaIdProdutoCUP(long IdProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarCapaIdProdutoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@IdProduto", IdProduto);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderCapaProdutoCUP(reader);
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

        public List<CadernoCUP> CarregarCadernoIdProdutoCUP(long IdProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarCadernoIdProdutoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@IdProduto", IdProduto);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderCadernoProdutoCUP(reader);
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

        public List<EncarteCUP> CarregarEncarteIdProdutoCUP(long IdProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarEncarteIdProdutoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@IdProduto", IdProduto);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderEncarteProdutoCUP(reader);
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

        public MidiaFichaCUP CarregarMidiaIdProdutoCUP(long IdProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarMidiaIdProdutoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@IdProduto", IdProduto);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderMidiaProdutoCUP(reader);
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

        public List<ControleImpressaoCUP> CarregarImpressaoIdProdutoCUP(long IdProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarImpressaoIdProdutoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@IdProduto", IdProduto);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderImpressaoProdutoCUP(reader);
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

        public async Task<long> SalvarProdutoCUP(ProdutoCUP produto)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("SalvarProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@PROD_CDIS", produto.ConteudoDisciplinar != null ? produto.ConteudoDisciplinar : 0);
                command.Parameters.AddWithValue("@PROD_TEMA", produto.TemaTransversal != null ? produto.TemaTransversal : 0);
                command.Parameters.AddWithValue("@PROD_DISC", produto.Disciplina != null ? produto.Disciplina : 0);
                command.Parameters.AddWithValue("@PROD_MERCADO", produto.Mercado);
                command.Parameters.AddWithValue("@PROD_PROGRAMA", produto.Programa);
                command.Parameters.AddWithValue("@PROD_TIPO", produto.Tipo);
                command.Parameters.AddWithValue("@PROD_SELO", produto.Selo);
                command.Parameters.AddWithValue("@PROD_SEGM", produto.Segmento != null ? produto.Segmento : 0);
                command.Parameters.AddWithValue("@PROD_AEDU", produto.Ano != null ? produto.Ano : 0);
                command.Parameters.AddWithValue("@PROD_COMP", produto.Composicao != null ? produto.Composicao : 0);
                command.Parameters.AddWithValue("@PROD_FAIXAETARIA", produto.FaixaEtaria != null ? produto.FaixaEtaria : 0);
                command.Parameters.AddWithValue("@PROD_GTEX", produto.GeneroTextual != null ? produto.GeneroTextual : 0);
                command.Parameters.AddWithValue("@PROD_PREMIACAO", produto.Premiacao);
                command.Parameters.AddWithValue("@PROD_VERSAO", produto.Versao);
                command.Parameters.AddWithValue("@PROD_COLE", produto.Colecao);
                command.Parameters.AddWithValue("@PROD_TITULO", produto.Titulo);
                command.Parameters.AddWithValue("@PROD_EDICAO", produto.Edicao);
                command.Parameters.AddWithValue("@PROD_MIDI", produto.Midia != null ? produto.Midia : 0);
                command.Parameters.AddWithValue("@PROD_PLAT", produto.Plataforma);
                command.Parameters.AddWithValue("@PROD_PUBLICACAO", produto.DataPublicacao == null ? DateTime.Now : produto.DataPublicacao);
                command.Parameters.AddWithValue("@PROD_ISBN", produto.ISBN);
                command.Parameters.AddWithValue("@PROD_STATUS", produto.Status);
                command.Parameters.AddWithValue("@PROD_CODBARRAS", produto.CodigoBarras);
                command.Parameters.AddWithValue("@PROD_SINOPSE", produto.Sinopse);
                command.Parameters.AddWithValue("@PROD_EBSA", produto.EBSA);
                command.Parameters.AddWithValue("@PROD_ORIGEM", produto.Origem);
                command.Parameters.AddWithValue("@EXCLUIDO", false);
                command.Parameters.AddWithValue("@PROD_TIPO_PRODUTO", produto.TipoProduto);
                command.Parameters.AddWithValue("@PROD_UNIDADE_MEDIDA", produto.UnidadeMedida);
                command.Parameters.AddWithValue("@ID_SEGMENTO", produto.SegmentoProtheus);
                command.Parameters.AddWithValue("@PROD_NOME_CAPA", produto.NomeCapa);
                command.Parameters.AddWithValue("@ANO_PROGRAMA", produto.AnoPrograma);
                command.Parameters.AddWithValue("@PROD_INTEGRADO", "naoIntegrado");
                command.Parameters.AddWithValue("@REFORMULADO", produto.Reformulado);
                try
                {
                    connection.Open();
                    long idProduto = Convert.ToInt64(await command.ExecuteScalarAsync());
                    return idProduto;
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

        public async Task AtualizarProdutoCUP(ProdutoCUP produto)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("AtualizarProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@PROD_ID", produto.IdProduto);
                command.Parameters.AddWithValue("@PROD_CDIS", produto.ConteudoDisciplinar != null ? produto.ConteudoDisciplinar : 0);
                command.Parameters.AddWithValue("@PROD_TEMA", produto.TemaTransversal != null ? produto.TemaTransversal : 0);
                command.Parameters.AddWithValue("@PROD_DISC", produto.Disciplina != null ? produto.Disciplina : 0);
                command.Parameters.AddWithValue("@PROD_MERCADO", produto.Mercado);
                command.Parameters.AddWithValue("@PROD_PROGRAMA", produto.Programa);
                command.Parameters.AddWithValue("@PROD_TIPO", produto.Tipo);
                command.Parameters.AddWithValue("@PROD_SELO", produto.Selo);
                command.Parameters.AddWithValue("@PROD_SEGM", produto.Segmento != null ? produto.Segmento : 0);
                command.Parameters.AddWithValue("@PROD_AEDU", produto.Ano != null ? produto.Ano : 0);
                command.Parameters.AddWithValue("@PROD_COMP", produto.Composicao != null ? produto.Composicao : 0);
                command.Parameters.AddWithValue("@PROD_FAIXAETARIA", produto.FaixaEtaria != null ? produto.FaixaEtaria : 0);
                command.Parameters.AddWithValue("@PROD_GTEX", produto.GeneroTextual != null ? produto.GeneroTextual : 0);
                command.Parameters.AddWithValue("@PROD_PREMIACAO", produto.Premiacao);
                command.Parameters.AddWithValue("@PROD_VERSAO", produto.Versao);
                command.Parameters.AddWithValue("@PROD_COLE", produto.Colecao);
                command.Parameters.AddWithValue("@PROD_TITULO", produto.Titulo);
                command.Parameters.AddWithValue("@PROD_EDICAO", produto.Edicao);
                command.Parameters.AddWithValue("@PROD_MIDI", produto.Midia != null ? produto.Midia : 0);
                command.Parameters.AddWithValue("@PROD_PLAT", produto.Plataforma);
                command.Parameters.AddWithValue("@PROD_PUBLICACAO", produto.DataPublicacao == null ? DateTime.Now : produto.DataPublicacao);
                command.Parameters.AddWithValue("@PROD_ISBN", produto.ISBN);
                command.Parameters.AddWithValue("@PROD_STATUS", produto.Status);
                command.Parameters.AddWithValue("@PROD_CODBARRAS", produto.CodigoBarras);
                command.Parameters.AddWithValue("@PROD_SINOPSE", produto.Sinopse);
                command.Parameters.AddWithValue("@PROD_EBSA", produto.EBSA);
                command.Parameters.AddWithValue("@PROD_ORIGEM", produto.Origem);
                command.Parameters.AddWithValue("@EXCLUIDO", false);
                command.Parameters.AddWithValue("@PROD_TIPO_PRODUTO", produto.TipoProduto);
                command.Parameters.AddWithValue("@PROD_UNIDADE_MEDIDA", produto.UnidadeMedida);
                command.Parameters.AddWithValue("@ID_SEGMENTO", produto.SegmentoProtheus);
                command.Parameters.AddWithValue("@PROD_NOME_CAPA", produto.NomeCapa);
                command.Parameters.AddWithValue("@ANO_PROGRAMA", produto.AnoPrograma);
                command.Parameters.AddWithValue("@REFORMULADO", produto.Reformulado);
                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task<bool> VerificaExisteEscpeCUP(long idProdutop)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("VerificaExisteEspecCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@PROD_ID", idProdutop);

                try
                {
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
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

        public async Task SalvarAssuntoProdutoCUP(int idAssunto, long idProduto)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("SalvarAssuntoProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@PASS_PROD", idProduto);
                command.Parameters.AddWithValue("@PASS_ASSU", idAssunto);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task ExcluirAssuntoProdutoCUP(long idProduto)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("ExcluirAssuntoProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@PASS_PROD", idProduto);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task SalvarTemaNorteadorProdutoCUP(int idTema, long idProduto)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("SalvarTemaNorteadorProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@PTEM_PROD", idProduto);
                command.Parameters.AddWithValue("@PTEM_TEMA", idTema);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task ExcluirTemaNorteadorProdutoCUP(long idProduto)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("ExcluirTemaProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@PTEM_PROD", idProduto);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task SalvarDataEspecialProdutoCUP(int idDataEspecial, long idProduto)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("SalvarDataEspecialProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@PDES_PROD", idProduto);
                command.Parameters.AddWithValue("@PDES_DAES", idDataEspecial);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task ExcluirDataEspecialProdutoCUP(long idProduto)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("ExcluirDataEspecialProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@PDES_PROD", idProduto);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task SalvarMioloProdutoCUP(MioloCUP miolo)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("SalvarMioloProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", miolo.IdProduto);
                command.Parameters.AddWithValue("@MIOLO_PAGINAS", miolo.mioloPaginas);
                command.Parameters.AddWithValue("@MIOLO_FORM_ALT", miolo.mioloFormatoAltura);
                command.Parameters.AddWithValue("@MIOLO_FORM_LARG", miolo.mioloFormatoLargura);
                command.Parameters.AddWithValue("@MIOLO_FORM_PESO", miolo.mioloPeso);
                command.Parameters.AddWithValue("@MIOLO_CORES", miolo.mioloCores);
                command.Parameters.AddWithValue("@MIOLO_TIPO_PAPEL", miolo.mioloTipoPapel);
                command.Parameters.AddWithValue("@MIOLO_GRAMATURA", miolo.mioloGramatura);
                command.Parameters.AddWithValue("@MIOLO_OBS", miolo.mioloObservacoes);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task AtualizarMioloProdutoCUP(MioloCUP miolo)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("AtualizarMioloProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", miolo.IdProduto);
                command.Parameters.AddWithValue("@MIOLO_PAGINAS", miolo.mioloPaginas);
                command.Parameters.AddWithValue("@MIOLO_FORM_ALT", miolo.mioloFormatoAltura);
                command.Parameters.AddWithValue("@MIOLO_FORM_LARG", miolo.mioloFormatoLargura);
                command.Parameters.AddWithValue("@MIOLO_FORM_PESO", miolo.mioloPeso);
                command.Parameters.AddWithValue("@MIOLO_CORES", miolo.mioloCores);
                command.Parameters.AddWithValue("@MIOLO_TIPO_PAPEL", miolo.mioloTipoPapel);
                command.Parameters.AddWithValue("@MIOLO_GRAMATURA", miolo.mioloGramatura);
                command.Parameters.AddWithValue("@MIOLO_OBS", miolo.mioloObservacoes);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task SalvarCapaProdutoCUP(CapaCUP capa)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("SalvarCapaProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", capa.IdProduto);
                command.Parameters.AddWithValue("@CAPA_CORES", capa.CapaCores);
                command.Parameters.AddWithValue("@CAPA_TIPO_PAPEL", capa.CapaTipoPapel);
                command.Parameters.AddWithValue("@CAPA_GRAMATURA", capa.CapaGramatura);
                command.Parameters.AddWithValue("@CAPA_ORELHA", capa.CapaOrelha);
                command.Parameters.AddWithValue("@CAPA_ACABAMENTO", capa.CapaAcabamento);
                command.Parameters.AddWithValue("@CAPA_OBS", capa.CapaObservacoes);
                command.Parameters.AddWithValue("@CAPA_ACABAMENTO_LOMBADA", capa.CapaAcabamentoLombada);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task AtualizarCapaProdutoCUP(CapaCUP capa)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("AtualizarCapaProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", capa.IdProduto);
                command.Parameters.AddWithValue("@CAPA_CORES", capa.CapaCores);
                command.Parameters.AddWithValue("@CAPA_TIPO_PAPEL", capa.CapaTipoPapel);
                command.Parameters.AddWithValue("@CAPA_GRAMATURA", capa.CapaGramatura);
                command.Parameters.AddWithValue("@CAPA_ORELHA", capa.CapaOrelha);
                command.Parameters.AddWithValue("@CAPA_ACABAMENTO", capa.CapaAcabamento);
                command.Parameters.AddWithValue("@CAPA_OBS", capa.CapaObservacoes);
                command.Parameters.AddWithValue("@CAPA_ACABAMENTO_LOMBADA", capa.CapaAcabamentoLombada);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task SalvarCadernoProdutoCUP(CadernoCUP caderno)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("SalvarCadernoProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", caderno.IdProduto);
                command.Parameters.AddWithValue("@CADERNO_PAGINAS", caderno.CadernoPaginas);
                command.Parameters.AddWithValue("@CADERNO_ALT", caderno.CadernoAltura);
                command.Parameters.AddWithValue("@CADERNO_LARG", caderno.CadernoLargura);
                command.Parameters.AddWithValue("@CADERNO_TIPO", caderno.CadernoTipo);
                command.Parameters.AddWithValue("@CADERNO_TIPO_OUTROS", caderno.CadernoTipoOutros);
                command.Parameters.AddWithValue("@CADERNO_PESO", caderno.CadernoPeso);
                command.Parameters.AddWithValue("@CADERNO_MIOLO_CORES", caderno.CadernoMioloCores);
                command.Parameters.AddWithValue("@CADERNO_MIOLO_TP_PAPEL", caderno.CadernoMioloTipoPapel);
                command.Parameters.AddWithValue("@CADERNO_MIOLO_GRAMATURA", caderno.CadernoMioloGramatura);
                command.Parameters.AddWithValue("@CADERNO_MIOLO_OBS", caderno.CadernoMioloObservacoes);
                command.Parameters.AddWithValue("@CADERNO_CAPA_CORES", caderno.CadernoCapaCores);
                command.Parameters.AddWithValue("@CADERNO_CAPA_TP_PAPEL", caderno.CadernoCapaTipoPapel);
                command.Parameters.AddWithValue("@CADERNO_CAPA_GRAMATURA", caderno.CadernoCapaGramatura);
                command.Parameters.AddWithValue("@CADERNO_CAPA_ORELHA", caderno.CadernoCapaOrelha);
                command.Parameters.AddWithValue("@CADERNO_CAPA_ACABAMENTO", caderno.CadernoCapaAcabamento);
                command.Parameters.AddWithValue("@CADERNO_CAPA_OBS", caderno.CadernoCapaObservacoes);
                command.Parameters.AddWithValue("@CADERNO_CAPA_ACAB_LOMBAD", caderno.CadernoCapaAcabamentoLombada);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task ExcluirCadernoProdutoCUP(long IdProduto)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("ExcluirCadernoProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", IdProduto);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task SalvarEncarteProdutoCUP(EncarteCUP encarte)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("SalvarEncarteProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", encarte.IdProduto);
                command.Parameters.AddWithValue("@ENCARTE_TIPO", encarte.EncarteTipo);
                command.Parameters.AddWithValue("@ENCARTE_ACABAMENTO", encarte.EncarteAcabamento);
                command.Parameters.AddWithValue("@ENCARTE_PAPEL", encarte.EncartePapel);
                command.Parameters.AddWithValue("@ENCARTE_GRAMATURA", encarte.EncarteGramatura);
                command.Parameters.AddWithValue("@ENCARTE_FORMATO", encarte.EncarteFormato);
                command.Parameters.AddWithValue("@ENCARTE_COR", encarte.EncarteCor);
                command.Parameters.AddWithValue("@ENCARTE_OUTROS", encarte.EncarteOutros);
                command.Parameters.AddWithValue("@ENCARTE_PAGINAS", encarte.EncartePaginas);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task ExcluirEncarteProdutoCUP(long IdProduto)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("ExcluirEncarteProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", IdProduto);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task SalvarMidiaProdutoCUP(long idProduto, string midiaTipo, string midiaOutros)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("SalvarMidiaProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", idProduto);
                command.Parameters.AddWithValue("@MIDIA_TIPO", midiaTipo);
                command.Parameters.AddWithValue("@MIDIA_OUTROS", midiaOutros);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task AtualizarMidiaProdutoCUP(long idProduto, string midiaTipo, string midiaOutros)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("SalvarMidiaProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", idProduto);
                command.Parameters.AddWithValue("@MIDIA_TIPO", midiaTipo);
                command.Parameters.AddWithValue("@MIDIA_OUTROS", midiaOutros);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task SalvarControleImpressaoProdutoCUP(ControleImpressaoCUP controle)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("SalvarControleImpressaoProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", controle.IdProduto);
                command.Parameters.AddWithValue("@CONT_IMP_EDICAO", controle.ContImpEdicao);
                command.Parameters.AddWithValue("@CONT_IMP_GRAFICA", controle.ContImpGrafica);
                command.Parameters.AddWithValue("@CONT_IMP_IMPRESSAO", controle.ContImpImpressao);
                command.Parameters.AddWithValue("@CONT_IMP_DATA", controle.ContImpData == null ? DateTime.Now : controle.ContImpData);
                command.Parameters.AddWithValue("@CONT_IMP_TIRAGEM", controle.ContImpTiragem);
                command.Parameters.AddWithValue("@CONT_IMP_OBS", controle.ContImpObservacoesGerais);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task ExcluirControleImpressaoProdutoCUP(long IdProduto)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("ExcluirControleImpressaoProdutosCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", IdProduto);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task ExcluirProdutoCUP(long IdProduto)
        {
            string deleteProduto = _baseRepository.BuscarArquivoConsulta("ExcluirProdutoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(deleteProduto, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", IdProduto);
                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task PublicarProdutoCUP(long IdProduto)
        {
            string publicaProduto = _baseRepository.BuscarArquivoConsulta("PublicarProdutoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(publicaProduto, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", IdProduto);
                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task SalvarProdutoLinkCUP(long IdProduto, string Url, decimal? Preco)
        {
            string publicaProduto = _baseRepository.BuscarArquivoConsulta("SalvarProdutoLinkCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(publicaProduto, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", IdProduto);
                command.Parameters.AddWithValue("@PROD_VIDEO", Url);
                command.Parameters.AddWithValue("@PROD_PRECO", Preco);
                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task<int> BuscarDigitoSequencialProduto(Produto produto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarDigitoSequencialProduto");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@PROD_MERCADO", ((Object)produto.Mercado) ?? DBNull.Value);
                command.Parameters.AddWithValue("@PROD_AEDU", ((Object)produto.Ano) ?? DBNull.Value);
                command.Parameters.AddWithValue("@PROD_DISC", ((Object)produto.Disciplina) ?? DBNull.Value);
                command.Parameters.AddWithValue("@PROD_MIDI", ((Object)produto.Midia) ?? DBNull.Value);
                command.Parameters.AddWithValue("@PROD_TIPO", ((Object)produto.Tipo) ?? DBNull.Value);
                command.Parameters.AddWithValue("@PROD_EDICAO", ((Object)produto.Edicao) ?? DBNull.Value);
                command.Parameters.AddWithValue("@PROD_VERSAO", ((Object)produto.Versao) ?? DBNull.Value);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderDigitoSequencial(reader);
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

        public async Task<bool> CodigoEbsaExiste(string codigoEbsa)
        {
            using (SqlConnection con = new SqlConnection(_baseRepository.Conexao))
            {
                string sqlQuery = "SELECT 1 FROM PRODUTO WHERE PROD_EBSA = @CODIGO_EBSA";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    await con.OpenAsync().ConfigureAwait(false);
                    cmd.Parameters.AddWithValue("@CODIGO_EBSA", ((Object)codigoEbsa) ?? DBNull.Value);

                    SqlDataReader rdr = await cmd.ExecuteReaderAsync().ConfigureAwait(false);

                    if (rdr.HasRows)
                        return true;
                }

            }
            return false;
        }

        private ProdutoPaginadoCUP LerDataReaderProdutosPaginadosCUP(SqlDataReader reader)
        {
            var retorno = new ProdutoPaginadoCUP();
            retorno.LstProdutos = new List<ProdutoBaseCUP>();
            var cont = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ProdutoBaseCUP produto = new ProdutoBaseCUP
                    {
                        IdProduto = Convert.ToInt32(reader["prod_id"]),
                        Titulo = reader["prod_titulo"].ToString(),
                        ISBN = reader["prod_isbn"].ToString(),
                        EBSA = reader["prod_ebsa"].ToString(),
                        Publicado = reader["prod_publicado"] == DBNull.Value ? false : Convert.ToBoolean(reader["prod_publicado"]),
                        Integrado = reader["prod_integrado"] == DBNull.Value ? "naoIntegrado" : reader["prod_integrado"].ToString(),
                        Url = reader["prod_video"].ToString(),
                        Preco = reader["prod_preco"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["prod_preco"]),
                        AnoEducacao = reader["AEDU_DESCRICAO"].ToString()
                    };
                    cont = Convert.ToInt32(reader["CONTAGEM"]);
                    retorno.LstProdutos.Add(produto);
                }
            }
            retorno.Contagem = cont;

            return retorno;
        }

        private ProdutoCUP LerDataReaderProdutosCUP(SqlDataReader reader)
        {
            var retorno = new ProdutoCUP();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    retorno = new ProdutoCUP
                    {
                        IdProduto = Convert.ToInt32(reader["PROD_ID"]),
                        ConteudoDisciplinar = reader["PROD_CDIS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_CDIS"]),
                        TemaTransversal = reader["PROD_TEMA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_TEMA"]),
                        Disciplina = reader["PROD_DISC"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_DISC"]),
                        Mercado = reader["PROD_MERCADO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_MERCADO"]),
                        Programa = reader["PROD_PROGRAMA"].ToString(),
                        Tipo = reader["PROD_TIPO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_TIPO"]),
                        Selo = reader["PROD_SELO"].ToString(),
                        Segmento = reader["PROD_SEGM"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_SEGM"]),
                        Ano = reader["PROD_AEDU"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_AEDU"]),
                        Composicao = reader["PROD_COMP"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_COMP"]),
                        FaixaEtaria = reader["PROD_FAIXAETARIA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_FAIXAETARIA"]),
                        GeneroTextual = reader["PROD_GTEX"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_GTEX"]),
                        Premiacao = reader["PROD_PREMIACAO"].ToString(),
                        Versao = reader["PROD_VERSAO"].ToString(),
                        Colecao = reader["PROD_COLE"].ToString(),
                        Titulo = reader["PROD_TITULO"].ToString(),
                        Edicao = reader["PROD_EDICAO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_EDICAO"]),
                        Midia = reader["PROD_MIDI"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_MIDI"]),
                        Plataforma = reader["PROD_PLAT"].ToString(),
                        DataPublicacao = reader["PROD_PUBLICACAO"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["PROD_PUBLICACAO"]),
                        ISBN = reader["PROD_ISBN"].ToString(),
                        Status = reader["PROD_STATUS"].ToString(),
                        CodigoBarras = reader["PROD_CODBARRAS"].ToString(),
                        Sinopse = reader["PROD_SINOPSE"].ToString(),
                        EBSA = reader["PROD_EBSA"].ToString(),
                        Origem = reader["PROD_ORIGEM"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_ORIGEM"]),
                        TipoProduto = reader["PROD_TIPO_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_TIPO_PRODUTO"]),
                        UnidadeMedida = reader["PROD_UNIDADE_MEDIDA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_UNIDADE_MEDIDA"]),
                        SegmentoProtheus = reader["ID_SEGMENTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_SEGMENTO"]),
                        DataPublicacaoProtheus = reader["PROD_DATAINTEGRACAO"].ToString(),
                        NomeCapa = reader["PROD_NOME_CAPA"].ToString(),
                        AnoPrograma = reader["ANO_PROGRAMA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ANO_PROGRAMA"]),
                        Reformulado = reader["REFORMULADO"] == DBNull.Value ? false : Convert.ToBoolean(reader["REFORMULADO"])
                    };
                }
            }

            return retorno;
        }

        private List<ProdutoCUP> LerDataReaderProdutosListCUP(SqlDataReader reader)
        {
            var retorno = new List<ProdutoCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var prod = new ProdutoCUP();
                    prod.IdProduto = Convert.ToInt32(reader["PROD_ID"]);
                    prod.ConteudoDisciplinar = reader["PROD_CDIS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_CDIS"]);
                    prod.TemaTransversal = reader["PROD_TEMA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_TEMA"]);
                    prod.Disciplina = reader["PROD_DISC"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_DISC"]);
                    prod.Mercado = reader["PROD_MERCADO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_MERCADO"]);
                    prod.Programa = reader["PROD_PROGRAMA"].ToString();
                    prod.Tipo = reader["PROD_TIPO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_TIPO"]);
                    prod.Selo = reader["PROD_SELO"].ToString();
                    prod.Segmento = reader["PROD_SEGM"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_SEGM"]);
                    prod.Ano = reader["PROD_AEDU"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_AEDU"]);
                    prod.Composicao = reader["PROD_COMP"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_COMP"]);
                    prod.FaixaEtaria = reader["PROD_FAIXAETARIA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_FAIXAETARIA"]);
                    prod.GeneroTextual = reader["PROD_GTEX"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_GTEX"]);
                    prod.Premiacao = reader["PROD_PREMIACAO"].ToString();
                    prod.Versao = reader["PROD_VERSAO"].ToString();
                    prod.Colecao = reader["PROD_COLE"].ToString();
                    prod.Titulo = reader["PROD_TITULO"].ToString();
                    prod.Edicao = reader["PROD_EDICAO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_EDICAO"]);
                    prod.Midia = reader["PROD_MIDI"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_MIDI"]);
                    prod.Plataforma = reader["PROD_PLAT"].ToString();
                    prod.DataPublicacao = reader["PROD_PUBLICACAO"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["PROD_PUBLICACAO"]);
                    prod.ISBN = reader["PROD_ISBN"].ToString();
                    prod.Status = reader["PROD_STATUS"].ToString();
                    prod.CodigoBarras = reader["PROD_CODBARRAS"].ToString();
                    prod.Sinopse = reader["PROD_SINOPSE"].ToString();
                    prod.EBSA = reader["PROD_EBSA"].ToString();
                    prod.Origem = reader["PROD_ORIGEM"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_ORIGEM"]);
                    prod.TipoProduto = reader["PROD_TIPO_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_TIPO_PRODUTO"]);
                    prod.UnidadeMedida = reader["PROD_UNIDADE_MEDIDA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_UNIDADE_MEDIDA"]);
                    prod.SegmentoProtheus = reader["ID_SEGMENTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_SEGMENTO"]);
                    prod.DataPublicacaoProtheus = reader["PROD_DATAINTEGRACAO"].ToString();
                    prod.NomeCapa = reader["PROD_NOME_CAPA"].ToString();
                    prod.AnoPrograma = reader["ANO_PROGRAMA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ANO_PROGRAMA"]);
                    retorno.Add(prod);
                }
            }

            return retorno;
        }

        private List<AssuntoCUP> LerDataReaderAssuntoProdutoCUP(SqlDataReader reader)
        {
            var retorno = new List<AssuntoCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    AssuntoCUP assunto = new AssuntoCUP
                    {
                        Id_Assunto = Convert.ToInt32(reader["PASS_ASSU"]),
                        Descricao_Assunto = reader["ASSU_DESCRICAO"].ToString()
                    };
                    retorno.Add(assunto);
                }
            }
            return retorno;
        }

        private List<TemaCUP> LerDataReaderTemaProdutoCUP(SqlDataReader reader)
        {
            var retorno = new List<TemaCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TemaCUP tema = new TemaCUP
                    {
                        Id_Tema = Convert.ToInt32(reader["PTEM_TEMA"]),
                        Descricao_Tema = reader["TEMA_DESCRICAO"].ToString(),
                        Tipo_Tema = reader["TEMA_TIPO"].ToString()
                    };
                    retorno.Add(tema);
                }
            }
            return retorno;
        }

        private List<DataEspecialCUP> LerDataReaderDatasEspeciaisProdutoCUP(SqlDataReader reader)
        {
            var retorno = new List<DataEspecialCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataEspecialCUP tema = new DataEspecialCUP
                    {
                        Id_DataEspecial = Convert.ToInt32(reader["PDES_DAES"]),
                        Descricao_DataEspecial = reader["DAES_DESCRICAO"].ToString(),
                        Dia_DataEspecial = reader["DAES_DIA"].ToString()
                    };
                    retorno.Add(tema);
                }
            }
            return retorno;
        }

        private MioloCUP LerDataReaderMioloProdutoCUP(SqlDataReader reader)
        {
            var retorno = new MioloCUP();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    retorno = new MioloCUP
                    {
                        IdMiolo = Convert.ToInt64(reader["ID_MIOLO"]),
                        IdProduto = Convert.ToInt64(reader["ID_PRODUTO"]),
                        mioloPaginas = reader["MIOLO_PAGINAS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MIOLO_PAGINAS"]),
                        mioloFormatoAltura = reader["MIOLO_FORM_ALT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["MIOLO_FORM_ALT"]),
                        mioloFormatoLargura = reader["MIOLO_FORM_LARG"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["MIOLO_FORM_LARG"]),
                        mioloPeso = reader["MIOLO_FORM_PESO"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["MIOLO_FORM_PESO"]),
                        mioloCores = reader["MIOLO_CORES"].ToString(),
                        mioloTipoPapel = reader["MIOLO_TIPO_PAPEL"].ToString(),
                        mioloGramatura = reader["MIOLO_GRAMATURA"].ToString(),
                        mioloObservacoes = reader["MIOLO_OBS"].ToString()
                    };
                }
            }
            return retorno;
        }

        private CapaCUP LerDataReaderCapaProdutoCUP(SqlDataReader reader)
        {
            var retorno = new CapaCUP();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    retorno = new CapaCUP
                    {
                        IdCapa = Convert.ToInt64(reader["ID_CAPA"]),
                        IdProduto = Convert.ToInt64(reader["ID_PRODUTO"]),
                        CapaCores = reader["CAPA_CORES"].ToString(),
                        CapaTipoPapel = reader["CAPA_TIPO_PAPEL"].ToString(),
                        CapaGramatura = reader["CAPA_GRAMATURA"].ToString(),
                        CapaOrelha = reader["CAPA_ORELHA"].ToString(),
                        CapaObservacoes = reader["CAPA_ACABAMENTO"].ToString(),
                        CapaAcabamento = reader["CAPA_OBS"].ToString(),
                        CapaAcabamentoLombada = reader["CAPA_ACABAMENTO_LOMBADA"].ToString(),
                    };
                }
            }
            return retorno;
        }

        private List<CadernoCUP> LerDataReaderCadernoProdutoCUP(SqlDataReader reader)
        {
            var retorno = new List<CadernoCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CadernoCUP cad = new CadernoCUP
                    {
                        IdCaderno = Convert.ToInt64(reader["ID_CADERNO"]),
                        IdProduto = Convert.ToInt64(reader["ID_PRODUTO"]),
                        CadernoTipo = reader["CADERNO_TIPO"].ToString(),
                        CadernoTipoOutros = reader["CADERNO_TIPO_OUTROS"].ToString(),
                        CadernoPaginas = reader["CADERNO_PAGINAS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CADERNO_PAGINAS"]),
                        CadernoAltura = reader["CADERNO_ALT"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["CADERNO_ALT"]),
                        CadernoLargura = reader["CADERNO_LARG"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["CADERNO_LARG"]),
                        CadernoPeso = reader["CADERNO_PESO"].ToString(),
                        CadernoMioloCores = reader["CADERNO_MIOLO_CORES"].ToString(),
                        CadernoMioloTipoPapel = reader["CADERNO_MIOLO_TP_PAPEL"].ToString(),
                        CadernoMioloGramatura = reader["CADERNO_MIOLO_GRAMATURA"].ToString(),
                        CadernoMioloObservacoes = reader["CADERNO_MIOLO_OBS"].ToString(),
                        CadernoCapaCores = reader["CADERNO_CAPA_CORES"].ToString(),
                        CadernoCapaTipoPapel = reader["CADERNO_CAPA_TP_PAPEL"].ToString(),
                        CadernoCapaGramatura = reader["CADERNO_CAPA_GRAMATURA"].ToString(),
                        CadernoCapaOrelha = reader["CADERNO_CAPA_ORELHA"].ToString(),
                        CadernoCapaAcabamento = reader["CADERNO_CAPA_ACABAMENTO"].ToString(),
                        CadernoCapaObservacoes = reader["CADERNO_CAPA_OBS"].ToString(),
                        CadernoCapaAcabamentoLombada = reader["CADERNO_CAPA_ACAB_LOMBADA"].ToString(),
                    };
                    retorno.Add(cad);
                }
            }
            return retorno;
        }

        private List<EncarteCUP> LerDataReaderEncarteProdutoCUP(SqlDataReader reader)
        {
            var retorno = new List<EncarteCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EncarteCUP enc = new EncarteCUP
                    {
                        IdEncarte = Convert.ToInt64(reader["ID_ENCARTE"]),
                        IdProduto = Convert.ToInt64(reader["ID_PRODUTO"]),
                        EncarteTipo = reader["ENCARTE_TIPO"].ToString(),
                        EncarteAcabamento = reader["ENCARTE_ACABAMENTO"].ToString(),
                        EncartePapel = reader["ENCARTE_PAPEL"].ToString(),
                        EncarteGramatura = reader["ENCARTE_GRAMATURA"].ToString(),
                        EncarteFormato = reader["ENCARTE_FORMATO"].ToString(),
                        EncarteCor = reader["ENCARTE_COR"].ToString(),
                        EncarteOutros = reader["ENCARTE_OUTROS"].ToString(),
                        EncartePaginas = reader["ENCARTE_PAGINAS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ENCARTE_PAGINAS"]),
                    };
                    retorno.Add(enc);
                }
            }
            return retorno;
        }

        private MidiaFichaCUP LerDataReaderMidiaProdutoCUP(SqlDataReader reader)
        {
            var retorno = new MidiaFichaCUP();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    retorno = new MidiaFichaCUP
                    {
                        IdMidia = Convert.ToInt64(reader["ID_MIDIA"]),
                        IdProduto = Convert.ToInt64(reader["ID_PRODUTO"]),
                        MidiaTipo = reader["MIDIA_TIPO"].ToString(),
                        MidiaOutros = reader["MIDIA_OUTROS"].ToString(),
                    };
                }
            }
            return retorno;
        }

        private List<ControleImpressaoCUP> LerDataReaderImpressaoProdutoCUP(SqlDataReader reader)
        {
            var retorno = new List<ControleImpressaoCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ControleImpressaoCUP imp = new ControleImpressaoCUP
                    {
                        IdControleImpressao = Convert.ToInt64(reader["ID_CONTROLE_IMPRESSAO"]),
                        IdProduto = Convert.ToInt64(reader["ID_PRODUTO"]),
                        ContImpEdicao = reader["CONT_IMP_EDICAO"].ToString(),
                        ContImpImpressao = reader["CONT_IMP_GRAFICA"].ToString(),
                        ContImpGrafica = reader["CONT_IMP_IMPRESSAO"].ToString(),
                        ContImpData = reader["CONT_IMP_DATA"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CONT_IMP_DATA"]),
                        ContImpTiragem = reader["CONT_IMP_TIRAGEM"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CONT_IMP_TIRAGEM"]),
                        ContImpObservacoesGerais = reader["CONT_IMP_OBS"].ToString(),

                    };
                    retorno.Add(imp);
                }
            }
            return retorno;
        }

        private int LerDataReaderDigitoSequencial(SqlDataReader reader)
        {
            int digitoSequencial = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    digitoSequencial = Convert.ToInt32(reader["DIGITO_SEQUENCIAL"]);
                }
            }

            return digitoSequencial;
        }

        public void SalvarPreferenciasRelatorioCUP(string viewmodel, int idUsuario)
        {
            string publicaProduto = _baseRepository.BuscarArquivoConsulta("SalvarPreferenciasRelatorioCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(publicaProduto, connection);
                command.Parameters.AddWithValue("@ID_USUARIO", idUsuario);
                command.Parameters.AddWithValue("@PREFERENCIA", viewmodel);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
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

        public void AtualizarPreferenciasRelatorioCUP(string viewmodel, int idUsuario)
        {
            string publicaProduto = _baseRepository.BuscarArquivoConsulta("AtualizarPreferenciasRelatorioCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(publicaProduto, connection);
                command.Parameters.AddWithValue("@ID_USUARIO", idUsuario);
                command.Parameters.AddWithValue("@PREFERENCIA", viewmodel);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
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

        public string CarregarPreferenciasRelatorioCUP(int idUsuario)
        {
            string publicaProduto = _baseRepository.BuscarArquivoConsulta("CarregarPreferenciasRelatorioCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(publicaProduto, connection);
                command.Parameters.AddWithValue("@ID_USUARIO", idUsuario);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderPreferenciasRelatorioCUP(reader);
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

        private string LerDataReaderPreferenciasRelatorioCUP(SqlDataReader reader)
        {
            string pref = "";

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    pref = reader["PREFERENCIA"].ToString();
                }
            }

            return pref;
        }

        #endregion

        #region Arquivo

        public async Task SalvarArquivoCUP(ArquivoProdutoCUP arquivo)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("SalvarArquivoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@NOME_CAPA", arquivo.NomeArquivo);
                command.Parameters.AddWithValue("@CAMINHO_ARQUIVO", arquivo.CaminhoArquivo);
                command.Parameters.AddWithValue("@PROD_ID", arquivo.IdProduto);
                command.Parameters.AddWithValue("@PROD_EBSA", arquivo.ProdutoEBSA);
                command.Parameters.AddWithValue("@DT_CADASTRO", arquivo.DataCadastro);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
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

        public async Task SalvarEpubCUP(ArquivoProdutoCUP arquivo)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("SalvarEpubCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@NOME_EPUB", arquivo.NomeArquivo);
                command.Parameters.AddWithValue("@CAMINHO_ARQUIVO", arquivo.CaminhoArquivo);
                command.Parameters.AddWithValue("@PROD_ID", arquivo.IdProduto);
                command.Parameters.AddWithValue("@PROD_EBSA", arquivo.ProdutoEBSA);
                command.Parameters.AddWithValue("@DT_CADASTRO", arquivo.DataCadastro);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
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

        public async Task<int> SalvarArquivoAutoriaCUP(ArquivoProdutoCUP arquivo)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("SalvarArquivoAutoriaCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@NOME", arquivo.NomeArquivo);
                command.Parameters.AddWithValue("@CAMINHOARQUIVO", arquivo.CaminhoArquivo);
                command.Parameters.AddWithValue("@AUTO_ID", arquivo.IdProduto);
                command.Parameters.AddWithValue("@CAMINHOLOCAL", arquivo.CaminhoArquivo);

                try
                {
                    await connection.OpenAsync();
                    return Convert.ToInt32(await command.ExecuteScalarAsync());
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

        public async Task ExcluirArquivoCUP(long IdProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ExcluirArquivoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@PROD_ID", IdProduto);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
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

        public async Task ExcluirEpubCUP(long IdProduto, string nomeArquivo)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ExcluirEpubCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@PROD_ID", IdProduto);
                command.Parameters.AddWithValue("@NOME_EPUB", nomeArquivo);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
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

        public async Task ExcluirArquivoAutoriaCUP(long IdDoc)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ExcluirArquivoAutoriaCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@DOC_ID", IdDoc);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
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

        public string CarregarCaminhoImagemCUP(long IdProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarCaminhoImagemCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@PROD_ID", IdProduto);

                try
                {
                    connection.OpenAsync();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataCaminhoImagemCUP(reader);
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

        public List<ArquivoProdutoCUP> CarregarCaminhoEpubCUP(long IdProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarCaminhoEpubCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@PROD_ID", IdProduto);

                try
                {
                    connection.OpenAsync();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataCaminhoEpubCUP(reader);
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

        public List<ArquivoAutoriaCUP> BuscarArquivosAutoria(long IdAutoria)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarArquivosAutoriaCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@AUTO_ID", IdAutoria);

                try
                {
                    connection.OpenAsync();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataArquivoAutoriaCUP(reader);
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

        private string LerDataCaminhoImagemCUP(SqlDataReader reader)
        {
            var retorno = "";
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    retorno = reader["CAMINHO_ARQUIVO"].ToString();
                }
            }
            return retorno;
        }

        private List<ArquivoProdutoCUP> LerDataCaminhoEpubCUP(SqlDataReader reader)
        {
            var retorno = new List<ArquivoProdutoCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ArquivoProdutoCUP arq = new ArquivoProdutoCUP();
                    arq.IdProdutoEPUB = reader["ID_PRODUTO_EPUB"].ToString();
                    arq.IdProduto = Convert.ToInt64(reader["PROD_ID"]);
                    arq.ProdutoEBSA = reader["PROD_EBSA"].ToString();
                    arq.NomeArquivo = reader["NOME_EPUB"].ToString();
                    arq.CaminhoArquivo = reader["CAMINHO_ARQUIVO"].ToString();
                    arq.DataCadastro = Convert.ToDateTime(reader["DT_CADASTRO"]);
                    retorno.Add(arq);
                }
            }
            return retorno;
        }

        private List<ArquivoAutoriaCUP> LerDataArquivoAutoriaCUP(SqlDataReader reader)
        {
            var lst = new List<ArquivoAutoriaCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ArquivoAutoriaCUP arq = new ArquivoAutoriaCUP();
                    arq.IdAutoria = Convert.ToInt64(reader["AUTO_ID"]);
                    arq.IdDoc = Convert.ToInt32(reader["DOC_ID"]);
                    arq.Caminho = reader["CAMINHOARQUIVO"].ToString();
                    arq.Nome = reader["NOME"].ToString();
                    lst.Add(arq);
                }
            }
            return lst;
        }

        #endregion

        #region Autoria

        public List<AutoriaCUP> CarregarAutoriaPorIdProdutoCUP(long IdProduto)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarAutoriasIdProdutoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_PRODUTO", IdProduto);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderAutoriaCUP(reader);
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

        private List<AutoriaCUP> LerDataReaderAutoriaCUP(SqlDataReader reader)
        {
            var lstAutoria = new List<AutoriaCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    AutoriaCUP autoria = new AutoriaCUP
                    {
                        IdAutoria = Convert.ToInt64(reader["AUTO_ID"]),
                        NomeContratual = reader["AUTO_NOMECON"].ToString(),
                        DataLiberacao = reader["AUTO_DATALIBERA"] != DBNull.Value ? Convert.ToDateTime(reader["AUTO_DATALIBERA"]) : new DateTime(),
                        DataVencimento = reader["AUTO_DATAVCTO"] != DBNull.Value ? Convert.ToDateTime(reader["AUTO_DATAVCTO"]) : new DateTime(),
                        RenovacaoAuto = reader["AUTO_RENOVAUTO"].ToString(),
                        QtdeMeses = Convert.ToInt32(reader["AUTO_QTDEMESES"]),
                        DireitosAutorais = Convert.ToInt32(reader["AUTO_PCRECEBDA"]),
                        DataLimite = reader["AUTO_DATALIMITE"] != DBNull.Value ? Convert.ToDateTime(reader["AUTO_DATALIMITE"]) : new DateTime(),
                        IdProduto = Convert.ToInt64(reader["AUTO_PROD"]),
                        Nacionalidade = reader["AUTO_NACIONALIDADE"].ToString(),
                        Naturalidade = reader["AUTO_NATURALIDADE"].ToString(),
                        ReparteImpressao = Convert.ToInt32(reader["AUTO_REPARTEIMP"]),
                        ReparteReimpressao = Convert.ToInt32(reader["AUTO_REPARTEREIMP"]),
                        CapaCodAutor = reader["AUTO_COD_AUTOR"].ToString()
                    };

                    lstAutoria.Add(autoria);
                }
            }
            return lstAutoria;
        }

        public List<AutoresCUP> CarregarAutoresCUP(string NomeContratual)
        {

            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarAutoresCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@NOME", string.IsNullOrEmpty(NomeContratual) ? "" : NomeContratual);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderAutorCUP(reader);
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

        public List<AutoresCUP> CarregarAutoresProdIdCUP(long prodId)
        {

            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarAutoresProdId");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@PROD_ID", prodId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderAutorCUP(reader);
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

        private List<AutoresCUP> LerDataReaderAutorCUP(SqlDataReader reader)
        {
            var lstAutores = new List<AutoresCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    AutoresCUP mercado = new AutoresCUP
                    {
                        IdAutor = Convert.ToInt32(reader["AUTOR_ID"]),
                        NomeContratual = reader["AUTOR_NOME_CONTRATUAL"].ToString(),
                        CapaCodAutor = reader["AUTOR_CAPA_COD_AUTOR"].ToString()
                    };

                    lstAutores.Add(mercado);
                }
            }
            return lstAutores;
        }

        public List<FuncoesCUP> CarregarFuncoesCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarFuncoesCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderFuncaoCUP(reader);
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

        public List<FuncoesCUP> CarregarFuncaoAutoriaCUP(long IdAutoria)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarFuncaoAutoriaCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_AUTORIA", IdAutoria);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderFuncaoCUP(reader);
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

        private List<FuncoesCUP> LerDataReaderFuncaoCUP(SqlDataReader reader)
        {
            var lstFuncoes = new List<FuncoesCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    FuncoesCUP funcao = new FuncoesCUP
                    {
                        IdFuncao = Convert.ToInt32(reader["FCAO_ID"]),
                        DescricaoFuncao = reader["FCAO_DESCRICAO"].ToString(),
                    };

                    lstFuncoes.Add(funcao);
                }
            }
            return lstFuncoes;
        }

        public async Task<long> SalvarAutoriaCUP(AutoriaCUP autoria)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("SalvarAutoriaCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@AUTO_NOMECON", autoria.NomeContratual);
                command.Parameters.AddWithValue("@AUTO_DATALIBERA", autoria.DataLiberacao);
                command.Parameters.AddWithValue("@AUTO_DATAVCTO", autoria.DataVencimento);
                command.Parameters.AddWithValue("@AUTO_RENOVAUTO", autoria.RenovacaoAuto);
                command.Parameters.AddWithValue("@AUTO_QTDEMESES", autoria.QtdeMeses);
                command.Parameters.AddWithValue("@AUTO_PCRECEBDA", autoria.DireitosAutorais);
                command.Parameters.AddWithValue("@AUTO_DATALIMITE", autoria.DataLimite);
                command.Parameters.AddWithValue("@AUTO_PROD", autoria.IdProduto);
                command.Parameters.AddWithValue("@AUTO_NACIONALIDADE", autoria.Nacionalidade);
                command.Parameters.AddWithValue("@AUTO_NATURALIDADE", autoria.Naturalidade);
                command.Parameters.AddWithValue("@AUTO_REPARTEIMP", autoria.ReparteImpressao);
                command.Parameters.AddWithValue("@AUTO_REPARTEREIMP", autoria.ReparteReimpressao);
                command.Parameters.AddWithValue("@AUTO_COD_AUTOR", autoria.CapaCodAutor);

                try
                {
                    connection.Open();
                    long idAutoria = Convert.ToInt64(await command.ExecuteScalarAsync());
                    return idAutoria;
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

        public async Task AtualizarAutoriaCUP(AutoriaCUP autoria)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("AtualizarAutoriaCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@AUTO_ID", autoria.IdAutoria);
                command.Parameters.AddWithValue("@AUTO_NOMECON", autoria.NomeContratual);
                command.Parameters.AddWithValue("@AUTO_DATALIBERA", autoria.DataLiberacao);
                command.Parameters.AddWithValue("@AUTO_DATAVCTO", autoria.DataVencimento);
                command.Parameters.AddWithValue("@AUTO_RENOVAUTO", autoria.RenovacaoAuto);
                command.Parameters.AddWithValue("@AUTO_QTDEMESES", autoria.QtdeMeses);
                command.Parameters.AddWithValue("@AUTO_PCRECEBDA", autoria.DireitosAutorais);
                command.Parameters.AddWithValue("@AUTO_DATALIMITE", autoria.DataLimite);
                command.Parameters.AddWithValue("@AUTO_PROD", autoria.IdProduto);
                command.Parameters.AddWithValue("@AUTO_NACIONALIDADE", autoria.Nacionalidade);
                command.Parameters.AddWithValue("@AUTO_NATURALIDADE", autoria.Naturalidade);
                command.Parameters.AddWithValue("@AUTO_REPARTEIMP", autoria.ReparteImpressao);
                command.Parameters.AddWithValue("@AUTO_REPARTEREIMP", autoria.ReparteReimpressao);
                command.Parameters.AddWithValue("@AUTO_COD_AUTOR", autoria.CapaCodAutor);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task ExcluirAutoriaCUP(long IdAutoria)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("ExcluirAutoriaCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@AUTO_ID", IdAutoria);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task SalvarAutoriaFuncaoCUP(int IdFuncao, long IdAutoria)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("SalvarAutoriaFuncaoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@PFUN_FCAO", IdFuncao);
                command.Parameters.AddWithValue("@PFUN_AUTO", IdAutoria);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        public async Task ExcluirAutoriaFuncaoCUP(long IdAutoria)
        {
            string insertProduto = _baseRepository.BuscarArquivoConsulta("ExcluirAutoriaFuncaoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(insertProduto, connection);
                command.Parameters.AddWithValue("@PFUN_AUTO", IdAutoria);

                try
                {
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
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

        #endregion

        #region Pemissoes Aba

        public List<AbaCUP> CarregarPermissoesAbaUsuarioCUP(int idUsuario)
        {
            SqlDataReader reader = null;

            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarAuthAbaCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                command.Parameters.AddWithValue("@ID_USUARIO", ((Object)idUsuario) ?? DBNull.Value);

                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();

                    return LerDataReaderAba(reader);
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

        public List<AbaCUP> LerDataReaderAba(SqlDataReader reader)
        {
            List<AbaCUP> abas = new List<AbaCUP>();
            AbaCUP aba = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aba = new AbaCUP();
                    aba.AcessoModificacao = Convert.ToBoolean(reader["ACESSO_MODIFICACAO"]);
                    aba.AcessoVisualizacao = Convert.ToBoolean(reader["ACESSO_VISUALIZACAO"]);
                    aba.NomeAba = reader["NOME_ABA"].ToString();

                    abas.Add(aba);
                }
            }

            return abas;
        }

        #endregion

        #region Mercado

        public List<MercadoCUP> CarregarMercadoCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarMercadoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderMercadoCUP(reader);
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

        private List<MercadoCUP> LerDataReaderMercadoCUP(SqlDataReader reader)
        {
            var lstMercado = new List<MercadoCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    MercadoCUP mercado = new MercadoCUP
                    {
                        Codigo_Mercado = reader["CODIGO"].ToString(),
                        Descricao_Mercado = reader["DESCRICAO"].ToString()
                    };

                    lstMercado.Add(mercado);
                }
            }
            return lstMercado;
        }

        #endregion

        #region Selos

        public List<SeloCUP> CarregarSeloCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarSeloCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderSeloCUP(reader);
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

        private List<SeloCUP> LerDataReaderSeloCUP(SqlDataReader reader)
        {
            var lstSelo = new List<SeloCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SeloCUP selo = new SeloCUP
                    {
                        Id_Selo = Convert.ToInt32(reader["ID_SELO"]),
                        Descricao_Selo = reader["DESCRICAO"].ToString(),
                        Chave_Grupo_Selo = reader["CHAVE_GRUPO"].ToString()
                    };

                    lstSelo.Add(selo);
                }
            }
            return lstSelo;
        }

        #endregion

        #region Tipo

        public List<TipoCUP> CarregarTipoCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarTipoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderTipoCUP(reader);
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

        private List<TipoCUP> LerDataReaderTipoCUP(SqlDataReader reader)
        {
            var lstTipo = new List<TipoCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TipoCUP tipo = new TipoCUP
                    {
                        Id_Tipo = Convert.ToInt32(reader["TIPO_ID"]),
                        Descricao_Tipo = reader["TIPO_DESCRICAO"].ToString()
                    };

                    lstTipo.Add(tipo);
                }
            }
            return lstTipo;
        }

        public List<TipoEspecCUP> CarregarTipoEspecCUP(string TipoEspec)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarTipoEspecCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ESPEC_TIPO", TipoEspec);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderTipoEspecCUP(reader);
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

        private List<TipoEspecCUP> LerDataReaderTipoEspecCUP(SqlDataReader reader)
        {
            var lstTipo = new List<TipoEspecCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TipoEspecCUP tipo = new TipoEspecCUP
                    {
                        IdTipoEspec = Convert.ToInt32(reader["ID_TIPO"]),
                        Descricao = reader["DESCRICAO"].ToString(),
                        TipoEspec = reader["ESPEC_TIPO"].ToString(),
                    };

                    lstTipo.Add(tipo);
                }
            }
            return lstTipo;
        }

        #endregion

        #region Segmento

        public List<SegmentoCUP> CarregarSegmentoCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarSegmentoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderSegmentoCUP(reader);
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

        private List<SegmentoCUP> LerDataReaderSegmentoCUP(SqlDataReader reader)
        {
            var lstSegmento = new List<SegmentoCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SegmentoCUP segmento = new SegmentoCUP
                    {
                        Id_Segmento = Convert.ToInt32(reader["SEGM_ID"]),
                        Descricao_Segmento = reader["SEGM_DESCRICAO"].ToString(),
                        Abreviacao_Segmento = reader["ABREVIACAO"].ToString()
                    };

                    lstSegmento.Add(segmento);
                }
            }
            return lstSegmento;
        }

        #endregion

        #region Composição

        public List<ComposicaoCUP> CarregarComposicaoCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarComposicaoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderComposicaoCUP(reader);
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

        private List<ComposicaoCUP> LerDataReaderComposicaoCUP(SqlDataReader reader)
        {
            var lstComposicao = new List<ComposicaoCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ComposicaoCUP composicao = new ComposicaoCUP
                    {
                        Id_Composicao = Convert.ToInt32(reader["COMP_ID"]),
                        Descricao_Composicao = reader["COMP_DESCRICAO"].ToString()
                    };

                    lstComposicao.Add(composicao);
                }
            }
            return lstComposicao;
        }

        #endregion

        #region Ano

        public List<AnoCUP> CarregarAnoCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarAnoEducacaoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderAnoCUP(reader);
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

        public List<AnoCUP> CarregarAnoPorIdSegmentoCUP(int IdSegmento)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarAnoEducacaoPorIdSegmentoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_SEGMENTO", IdSegmento);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderAnoCUP(reader);
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

        private List<AnoCUP> LerDataReaderAnoCUP(SqlDataReader reader)
        {
            var lstAno = new List<AnoCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    AnoCUP ano = new AnoCUP
                    {
                        Id_Ano = Convert.ToInt32(reader["AEDU_ID"]),
                        Descricao_Ano = reader["AEDU_DESCRICAO"].ToString(),
                        Segmento_Ano = Convert.ToInt32(reader["AEDU_SEGM"]),
                        Abreviacao_Ano = reader["ABREVIACAO"].ToString()
                    };

                    lstAno.Add(ano);
                }
            }
            return lstAno;
        }

        #endregion

        #region Faixa Etaria

        public List<FaixaEtariaCUP> CarregarFaixaEtariaCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarFaixaEtariaEducacaoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderFaixaEtariaCUP(reader);
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

        public List<FaixaEtariaCUP> CarregarFaixaEtariaPorIdAnoCUP(int IdAno)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarFaixaEtariaPorIdAnoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_ANO", IdAno);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderFaixaEtariaCUP(reader);
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

        private List<FaixaEtariaCUP> LerDataReaderFaixaEtariaCUP(SqlDataReader reader)
        {
            var lstFaixaEtaria = new List<FaixaEtariaCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    FaixaEtariaCUP faixaEtaria = new FaixaEtariaCUP
                    {
                        Id_FaixaEtaria = Convert.ToInt32(reader["FETA_ID"]),
                        Descricao_FaixaEtaria = reader["FETA_DESCRICAO"].ToString(),
                        Id_Ano = Convert.ToInt32(reader["FETA_AEDU"])
                    };

                    lstFaixaEtaria.Add(faixaEtaria);
                }
            }
            return lstFaixaEtaria;
        }

        #endregion

        #region Disciplina

        public List<DisciplinaCUP> CarregarDisciplinaCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarDisciplinaCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderDisciplinaCUP(reader);
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

        private List<DisciplinaCUP> LerDataReaderDisciplinaCUP(SqlDataReader reader)
        {
            var lstDisciplina = new List<DisciplinaCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DisciplinaCUP disciplina = new DisciplinaCUP
                    {
                        Id_Disciplina = Convert.ToInt32(reader["DISC_ID"]),
                        Descricao_Disciplina = reader["DISC_NOME"].ToString()
                    };

                    lstDisciplina.Add(disciplina);
                }
            }
            return lstDisciplina;
        }

        #endregion

        #region Conteudo Disciplinar

        public List<ConteudoDisciplinarCUP> CarregarConteudoDisciplinarCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarConteudoDisciplinarCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderConteudoDisciplinarCUP(reader);
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

        private List<ConteudoDisciplinarCUP> LerDataReaderConteudoDisciplinarCUP(SqlDataReader reader)
        {
            var lstConteudoDisciplinar = new List<ConteudoDisciplinarCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ConteudoDisciplinarCUP conteudoDisciplinar = new ConteudoDisciplinarCUP
                    {
                        Id_ConteudoDisciplinar = Convert.ToInt32(reader["CDIS_ID"]),
                        Descricao_ConteudoDisciplinar = reader["CDIS_DESCRICAO"].ToString()
                    };

                    lstConteudoDisciplinar.Add(conteudoDisciplinar);
                }
            }
            return lstConteudoDisciplinar;
        }

        #endregion

        #region Tema

        public List<TemaCUP> CarregarTemaTransversalCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarTemaTransversalCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderTemaCUP(reader);
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

        public List<TemaCUP> CarregarTemaNorteadorCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarTemaNorteadorCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderTemaCUP(reader);
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

        private List<TemaCUP> LerDataReaderTemaCUP(SqlDataReader reader)
        {
            var lstTemaTransversal = new List<TemaCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TemaCUP tema = new TemaCUP
                    {
                        Id_Tema = Convert.ToInt32(reader["TEMA_ID"]),
                        Descricao_Tema = reader["TEMA_DESCRICAO"].ToString(),
                        Tipo_Tema = reader["TEMA_TIPO"].ToString()
                    };

                    lstTemaTransversal.Add(tema);
                }
            }
            return lstTemaTransversal;
        }

        #endregion

        #region Data Especial

        public List<DataEspecialCUP> CarregarDataEspecialCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarDataEspecialCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderDataEspecialCUP(reader);
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

        private List<DataEspecialCUP> LerDataReaderDataEspecialCUP(SqlDataReader reader)
        {
            var lstDataEspecial = new List<DataEspecialCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataEspecialCUP dataEspecial = new DataEspecialCUP
                    {
                        Id_DataEspecial = Convert.ToInt32(reader["DAES_ID"]),
                        Descricao_DataEspecial = reader["DAES_DESCRICAO"].ToString(),
                        Dia_DataEspecial = reader["DAES_DIA"].ToString()
                    };

                    lstDataEspecial.Add(dataEspecial);
                }
            }
            return lstDataEspecial;
        }

        #endregion

        #region Assunto

        public List<AssuntoCUP> CarregarAssuntoCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarAssuntoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderAssuntoCUP(reader);
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

        private List<AssuntoCUP> LerDataReaderAssuntoCUP(SqlDataReader reader)
        {
            var lstAssunto = new List<AssuntoCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    AssuntoCUP assunto = new AssuntoCUP
                    {
                        Id_Assunto = Convert.ToInt32(reader["ASSU_ID"]),
                        Descricao_Assunto = reader["ASSU_DESCRICAO"].ToString()
                    };

                    lstAssunto.Add(assunto);
                }
            }
            return lstAssunto;
        }

        #endregion

        #region Assunto

        public List<GeneroTextualCUP> CarregarGeneroTextualCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarGeneroTextualCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderGeneroTextualCUP(reader);
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

        private List<GeneroTextualCUP> LerDataReaderGeneroTextualCUP(SqlDataReader reader)
        {
            var lstGeneroTextual = new List<GeneroTextualCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    GeneroTextualCUP genero = new GeneroTextualCUP
                    {
                        Id_GeneroTextual = Convert.ToInt32(reader["GTEX_ID"]),
                        Descricao_GeneroTextual = reader["GTEX_DESCRICAO"].ToString()
                    };

                    lstGeneroTextual.Add(genero);
                }
            }
            return lstGeneroTextual;
        }

        #endregion

        #region Versao

        public List<VersaoCUP> CarregarVersaoCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarVersaoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderVersaoCUP(reader);
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

        private List<VersaoCUP> LerDataReaderVersaoCUP(SqlDataReader reader)
        {
            var lstVersaoTextual = new List<VersaoCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    VersaoCUP versao = new VersaoCUP
                    {
                        Id_Versao = Convert.ToInt32(reader["CODIGO"]),
                        Descricao_Versao = reader["DESCRICAO"].ToString(),
                        Abreviacao_Versao = reader["ABREVIACAO"].ToString()
                    };

                    lstVersaoTextual.Add(versao);
                }
            }
            return lstVersaoTextual;
        }

        #endregion

        #region Colecao

        public List<ColecaoCUP> CarregarColecaoCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarColecaoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderColecaoCUP(reader);
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

        private List<ColecaoCUP> LerDataReaderColecaoCUP(SqlDataReader reader)
        {
            var lstColecao = new List<ColecaoCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ColecaoCUP colecao = new ColecaoCUP
                    {
                        Id_Colecao = Convert.ToInt32(reader["COLE_ID"]),
                        Descricao_Colecao = reader["COLE_DESCRICAO"].ToString(),
                        Tipo_Colecao = Convert.ToInt32(reader["COLE_TIPO"]),
                        Segmento_Colecao = Convert.ToInt32(reader["COLE_SEGM"])
                    };

                    lstColecao.Add(colecao);
                }
            }
            return lstColecao;
        }

        #endregion

        #region Midia

        public List<MidiaCUP> CarregarMidiaCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarMidiaCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderMidiaCUP(reader);
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

        private List<MidiaCUP> LerDataReaderMidiaCUP(SqlDataReader reader)
        {
            var lstMidia = new List<MidiaCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    MidiaCUP midia = new MidiaCUP
                    {
                        Id_Midia = Convert.ToInt32(reader["MIDI_ID"]),
                        Descricao_Midia = reader["MIDI_DESCRICAO"].ToString(),
                        UnidadeVenda_Midia = reader["MIDI_UNIDVENDA"].ToString(),
                        Plataforma_Midia = Convert.ToInt32(reader["MIDI_PLAT"])
                    };

                    lstMidia.Add(midia);
                }
            }
            return lstMidia;
        }

        #endregion

        #region Unidade de Medida

        public List<UnidadeMedidaCUP> CarregarUnidadeMedidaCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarUnidadeMedidaCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderUnidadeMedidaCUP(reader);
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

        private List<UnidadeMedidaCUP> LerDataReaderUnidadeMedidaCUP(SqlDataReader reader)
        {
            var lstUnidadeMedida = new List<UnidadeMedidaCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    UnidadeMedidaCUP unidade = new UnidadeMedidaCUP
                    {
                        Id_UnidadeMedida = Convert.ToInt32(reader["ID_UNIDADE_MEDIDA"]),
                        Descricao_UnidadeMedida = reader["DESCRICAO"].ToString(),
                        Sigla_UnidadeMedida = reader["SIGLA"].ToString()
                    };

                    lstUnidadeMedida.Add(unidade);
                }
            }
            return lstUnidadeMedida;
        }

        #endregion

        #region Plataforma

        public List<PlataformaCUP> CarregarPlataformaCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarPlataformaCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderPlataformaCUP(reader);
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

        public List<PlataformaCUP> CarregarPlataformaPorIdMidiaCUP(int IdMidia)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarPlataformaPorIdMidiaCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_MIDIA", IdMidia);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderPlataformaCUP(reader);
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

        private List<PlataformaCUP> LerDataReaderPlataformaCUP(SqlDataReader reader)
        {
            var lstPlataforma = new List<PlataformaCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    PlataformaCUP plataforma = new PlataformaCUP
                    {
                        Id_Plataforma = Convert.ToInt32(reader["PLAT_ID"]),
                        Descricao_Plataforma = reader["PLAT_DESCRICAO"].ToString()
                    };

                    lstPlataforma.Add(plataforma);
                }
            }
            return lstPlataforma;
        }

        #endregion

        #region Status

        public List<StatusCUP> CarregarStatusCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarStatusCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderStatusCUP(reader);
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

        private List<StatusCUP> LerDataReaderStatusCUP(SqlDataReader reader)
        {
            var lstStatus = new List<StatusCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    StatusCUP status = new StatusCUP
                    {
                        Id_Status = Convert.ToInt32(reader["Id_Produto_Status"]),
                        Descricao_Status = reader["Descricao_Produto_Status"].ToString(),
                        Cododigo_Status = reader["Cod_Produto_Status"].ToString()
                    };

                    lstStatus.Add(status);
                }
            }
            return lstStatus;
        }

        #endregion

        #region Origem

        public List<OrigemCUP> CarregarOrigemCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarOrigemCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderOrigemCUP(reader);
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

        private List<OrigemCUP> LerDataReaderOrigemCUP(SqlDataReader reader)
        {
            var lstOrigem = new List<OrigemCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    OrigemCUP origem = new OrigemCUP
                    {
                        Id_Origem = Convert.ToInt32(reader["ID_ORIGEM"]),
                        Descricao_Origem = reader["DESCRICAO"].ToString(),
                    };

                    lstOrigem.Add(origem);
                }
            }
            return lstOrigem;
        }

        #endregion

        #region Tipo Produto

        public List<TipoProdutoCUP> CarregarTipoProdutoCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarTipoProdutoCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderTipoProdutoCUP(reader);
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

        private List<TipoProdutoCUP> LerDataReaderTipoProdutoCUP(SqlDataReader reader)
        {
            var lstTipoProduto = new List<TipoProdutoCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TipoProdutoCUP tipoProduto = new TipoProdutoCUP
                    {
                        Id_TipoProduto = Convert.ToInt32(reader["ID_TIPO_PRODUTO"]),
                        Descricao_TipoProduto = reader["DESCRICAO"].ToString(),
                        Sigla_TipoProduto = reader["SIGLA"].ToString(),
                    };

                    lstTipoProduto.Add(tipoProduto);
                }
            }
            return lstTipoProduto;
        }

        #endregion

        #region Segmento Protheus

        public List<SegmentoProtheusCUP> CarregarSegmentoProtheusCUP()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("CarregarSegmentoProtheusCUP");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return LerDataReaderSegmentoProtheusCUP(reader);
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

        private List<SegmentoProtheusCUP> LerDataReaderSegmentoProtheusCUP(SqlDataReader reader)
        {
            var lstSegmentoProtheus = new List<SegmentoProtheusCUP>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SegmentoProtheusCUP segmentoProtheus = new SegmentoProtheusCUP
                    {
                        Id_SegmentoProtheus = Convert.ToInt32(reader["ID_SEGMENTO"]),
                        Descricao_SegmentoProtheus = reader["DESCRICAO_SEGMENTO"].ToString(),
                        Codigo_SegmentoProtheus = reader["CODIGO_SEGMENTO"].ToString(),
                    };

                    lstSegmentoProtheus.Add(segmentoProtheus);
                }
            }
            return lstSegmentoProtheus;
        }

        #endregion
    }
}
