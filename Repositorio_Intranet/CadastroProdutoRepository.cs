using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class CadastroProdutoRepository : ICadastroProdutoRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;
        public CadastroProdutoRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<CadastroProduto> CarregarInformacoesTela()
        {

            var cadastroProduto = new CadastroProduto
            {
                Mercados = await CarregarMercado(),
                Anos = await CarregarAno(),
                Disciplinas = await CarregarDisciplina(),
                Midias = await CarregarMidia(),
                Tipos = await CarregarTipo(),
                Versoes = await CarregarVersao()
            };

            return cadastroProduto;
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

        public async Task SalvarCodigoEbsa(Produto produto)
        {

            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _baseRepository.BuscarArquivoConsulta("SalvarProdutoProvisorio");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@MERCADO", produto.Mercado);
                            command.Parameters.AddWithValue("@ANO", produto.Ano);
                            command.Parameters.AddWithValue("@DISCIPLINA", produto.Disciplina);
                            command.Parameters.AddWithValue("@MIDIA", produto.Midia);
                            command.Parameters.AddWithValue("@TIPO", produto.Tipo);
                            command.Parameters.AddWithValue("@EDICAO", produto.Edicao);
                            command.Parameters.AddWithValue("@CODIGO_EBSA", produto.CodigoEbsa);
                            command.Parameters.AddWithValue("@TITULO", produto.Titulo);
                            command.Parameters.AddWithValue("@VERSAO", produto.Versao);
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

        public async Task<Produtos> Consultar(long id)
        {
            Produtos produto = new Produtos();
            SqlDataReader reader = null;

            string consulta = "SELECT P.*, AEDU.AEDU_DESCRICAO, COLE.COLE_DESCRICAO, CDIS.CDIS_DESCRICAO, FETA.FETA_DESCRICAO, SEGM.SEGM_DESCRICAO, TEMA.TEMA_DESCRICAO, " +
                          "CASE WHEN PFB.NOME_FB IS NOT NULL THEN PFB.CAMINHO_ARQUIVO ELSE '../../../files/' + PE.NOME_EPUB END AS CAMINHOARQUIVO, FT.FOTO, " +
                          "CASE WHEN PFB.NOME_FB IS NOT NULL THEN 'FB' ELSE 'EPUB' END AS TIPOARQUIVO, " +
                          "SUBSTRING(REPLACE(REPLACE(STUFF((SELECT TEMA.TEMA_DESCRICAO FROM PRODUTO_TEMATRANS PTEM  " +
                          "      INNER JOIN TEMA " +
                          "        ON PTEM.PTEM_TEMA = TEMA.TEMA_ID " +
                          "      WHERE PTEM.PTEM_PROD = P.PROD_ID " +
                          "   FOR XML PATH('')), 1, 0, ''), '</TEMA_DESCRICAO>', ''), '<TEMA_DESCRICAO>', ', '), 2, 4000) AS TEMAS_TRANSVERSAIS, " +
                          "SUBSTRING(REPLACE(REPLACE(STUFF((SELECT ASSU.ASSU_DESCRICAO FROM ASSUNTO ASSU " +
                          "      INNER JOIN PRODUTO_ASSUNTO PRODASSU " +
                          "        ON PRODASSU.PASS_ASSU = ASSU.ASSU_ID " +
                          "      WHERE P.PROD_ID = PRODASSU.PASS_PROD " +
                          "   FOR XML PATH('')), 1, 0, ''), '</ASSU_DESCRICAO>', ''), '<ASSU_DESCRICAO>', ', '), 2, 4000) AS ASSU_DESCRICAO " +
                          "  FROM PRODUTO P " +
                          "  LEFT JOIN TB_PRODUTO_EPUB PE " +
                          "    ON P.PROD_ID = PE.PROD_ID " +
                          "  LEFT JOIN TB_PRODUTO_FLIPPINGBOOK PFB " +
                          "    ON P.PROD_ID = PFB.PROD_ID" +
                          "  LEFT JOIN FICHATECNICA FT " +
                          "    ON P.PROD_ID = FT.PROD_ID " +
                          "  LEFT JOIN ANO_EDUCACAO AEDU " +
                          "    ON P.PROD_AEDU = AEDU.AEDU_ID " +
                          "  LEFT JOIN COLECAO COLE " +
                          "    ON P.PROD_COLE = COLE.COLE_ID " +
                          "  LEFT JOIN CONTEUDO_DISCIPLINAR CDIS " +
                          "    ON P.PROD_CDIS = CDIS.CDIS_ID " +
                          "  LEFT JOIN FAIXA_ETARIA FETA " +
                          "    ON P.PROD_FAIXAETARIA = FETA.FETA_ID " +
                          "  LEFT JOIN SEGMENTO SEGM " +
                          "    ON P.PROD_SEGM = SEGM.SEGM_ID " +
                          "  LEFT JOIN TEMA " +
                          "    ON P.PROD_TEMA = TEMA.TEMA_ID " +
                          "WHERE P.PROD_ID = " + id;

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        produto.PROD_ID = Convert.ToInt32(reader["prod_id"]);
                        produto.PROD_CDIS = !DBNull.Value.Equals(reader["prod_cdis"]) ? Convert.ToInt32(reader["prod_cdis"]) : 0;
                        produto.CONTEUDO = !DBNull.Value.Equals(reader["cdis_descricao"].ToString()) ? reader["cdis_descricao"].ToString() : "";
                        produto.PROD_TEMA = !DBNull.Value.Equals(reader["prod_tema"]) ? Convert.ToInt32(reader["prod_tema"]) : 0;
                        produto.TEMAS = !DBNull.Value.Equals(reader["tema_descricao"].ToString()) ? reader["tema_descricao"].ToString() : "";
                        produto.TEMAS_TRANS = !DBNull.Value.Equals(reader["TEMAS_TRANSVERSAIS"].ToString()) ? reader["TEMAS_TRANSVERSAIS"].ToString() : "";
                        produto.PROD_MERCADO = !DBNull.Value.Equals(reader["prod_mercado"]) ? Convert.ToInt32(reader["prod_mercado"]) : 0;
                        produto.PROD_PROGRAMA = !DBNull.Value.Equals(reader["prod_programa"].ToString()) ? reader["prod_programa"].ToString() : "";
                        produto.PROD_TIPO = !DBNull.Value.Equals(reader["prod_tipo"]) ? Convert.ToInt32(reader["prod_tipo"]) : 0;
                        produto.PROD_SELO = !DBNull.Value.Equals(reader["prod_selo"].ToString()) ? reader["prod_selo"].ToString() : "";
                        produto.PROD_SEGM = !DBNull.Value.Equals(reader["prod_segm"]) ? Convert.ToInt32(reader["prod_segm"]) : 0;
                        produto.SEGMENTO = !DBNull.Value.Equals(reader["segm_descricao"].ToString()) ? reader["segm_descricao"].ToString() : "";
                        produto.PROD_AEDU = !DBNull.Value.Equals(reader["prod_aedu"]) ? Convert.ToInt32(reader["prod_aedu"]) : 0;
                        produto.PROD_ANO = !DBNull.Value.Equals(reader["aedu_descricao"].ToString()) ? reader["aedu_descricao"].ToString() : "";
                        produto.PROD_COMP = !DBNull.Value.Equals(reader["prod_comp"]) ? Convert.ToInt32(reader["prod_comp"]) : 0;
                        produto.PROD_FAIXAETARIA = !DBNull.Value.Equals(reader["prod_faixaetaria"]) ? Convert.ToInt32(reader["prod_faixaetaria"]) : 0;
                        produto.FAIXAETARIA = !DBNull.Value.Equals(reader["feta_descricao"].ToString()) ? reader["feta_descricao"].ToString() : "";
                        produto.PROD_ASSU = !DBNull.Value.Equals(reader["prod_assu"]) ? Convert.ToInt32(reader["prod_assu"]) : 0;
                        produto.ASSUNTO = !DBNull.Value.Equals(reader["assu_descricao"].ToString()) ? reader["assu_descricao"].ToString() : "";
                        produto.PROD_GTEX = !DBNull.Value.Equals(reader["prod_gtex"]) ? Convert.ToInt32(reader["prod_gtex"]) : 0;
                        produto.PROD_PREMIACAO = !DBNull.Value.Equals(reader["prod_premiacao"].ToString()) ? reader["prod_premiacao"].ToString() : "";
                        //produto.PROD_VERSAO = (prod_versao)Enum.Parse(typeof(prod_versao), reader["prod_versao"].ToString());
                        produto.PROD_COLE = !DBNull.Value.Equals(reader["prod_cole"]) ? Convert.ToInt32(reader["prod_cole"]) : 0;
                        produto.COLECAO = !DBNull.Value.Equals(reader["cole_descricao"].ToString()) ? reader["cole_descricao"].ToString() : "";
                        produto.PROD_TITULO = !DBNull.Value.Equals(reader["prod_titulo"].ToString()) ? reader["prod_titulo"].ToString() : "";
                        produto.PROD_AUTOR = !DBNull.Value.Equals(reader["prod_nome_capa"].ToString()) ? reader["prod_nome_capa"].ToString() : "";
                        produto.PROD_DISC = !DBNull.Value.Equals(reader["prod_disc"]) ? Convert.ToInt32(reader["prod_disc"]) : 0;
                        produto.PROD_EDICAO = !DBNull.Value.Equals(reader["prod_edicao"]) ? Convert.ToInt32(reader["prod_edicao"]) : 0;
                        produto.PROD_MIDI = !DBNull.Value.Equals(reader["prod_midi"]) ? Convert.ToInt32(reader["prod_midi"]) : 0;
                        produto.PROD_PAGINAS = !DBNull.Value.Equals(reader["prod_paginas"]) ? Convert.ToInt32(reader["prod_paginas"]) : 0;
                        produto.PROD_PLAT = !DBNull.Value.Equals(reader["prod_plat"]) ? Convert.ToInt32(reader["prod_plat"]) : 0;
                        produto.PROD_FORALTURA = !DBNull.Value.Equals(reader["prod_foraltura"]) ? Convert.ToInt32(reader["prod_foraltura"]) : 0;
                        produto.PROD_FORLARGURA = !DBNull.Value.Equals(reader["prod_forlargura"]) ? Convert.ToInt32(reader["prod_forlargura"]) : 0;
                        produto.PROD_PRECO = !DBNull.Value.Equals(reader["prod_preco"]) ? Convert.ToInt32(reader["prod_preco"]) : 0;
                        try
                        {
                            produto.PROD_PUBLICACAO = DateTime.Parse(reader["prod_publicacao"].ToString());
                        }
                        catch (Exception ex)
                        {
                            produto.PROD_PUBLICACAO = null;
                        }
                        produto.PROD_ISBN = !DBNull.Value.Equals(reader["prod_isbn"].ToString()) ? reader["prod_isbn"].ToString() : "";
                        //produto.PROD_STATUS = EnumExtension.ToEnum<prod_status>(reader["prod_status"].ToString());
                        produto.PROD_CODBARRAS = !DBNull.Value.Equals(reader["prod_codbarras"].ToString()) ? reader["prod_codbarras"].ToString() : "";
                        produto.PROD_SINOPSE = !DBNull.Value.Equals(reader["prod_sinopse"].ToString()) ? reader["prod_sinopse"].ToString() : "";
                        produto.PROD_PRECO = !DBNull.Value.Equals(reader["prod_preco"]) ? decimal.Parse(reader["prod_preco"].ToString()) : 0;
                        produto.PROD_EBSA = !DBNull.Value.Equals(reader["prod_ebsa"].ToString()) ? reader["prod_ebsa"].ToString() : "";
                        produto.PROD_UNIDADE = !DBNull.Value.Equals(reader["prod_unidade"].ToString()) ? reader["prod_unidade"].ToString() : "";
                        produto.PROD_PUBLICADO = (!DBNull.Value.Equals(reader["prod_publicado"].ToString()) ? false : Convert.ToBoolean(reader["PROD_PUBLICADO"].ToString()));
                        produto.PROD_VIDEO = !DBNull.Value.Equals(reader["prod_video"].ToString()) ? reader["prod_video"].ToString() : "";
                        produto.CAMINHOARQUIVO = !DBNull.Value.Equals(reader["caminhoarquivo"].ToString()) ? reader["caminhoarquivo"].ToString().Replace("../", "") : "";
                        produto.CAPA = !DBNull.Value.Equals(reader["foto"].ToString()) ? reader["foto"].ToString().Replace("../", "") : "";
                    }

                    return produto;
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

        public async Task<IEnumerable<ProdutosDocumentosAutorias>> Listar()
        {
            List<ProdutosDocumentosAutorias> lstproduto = new List<ProdutosDocumentosAutorias>();
            SqlDataReader reader = null;

            string consulta = "SELECT P.PROD_ID, " +
                                "PE.ID_PRODUTO_EPUB AS DOC_ID, " +
                                "P.PROD_TITULO, " +
                                "P.PROD_NOME_CAPA, " +
                                "P.PROD_FAIXAETARIA, " +
                                "CASE WHEN PFB.NOME_FB IS NOT NULL THEN PFB.CAMINHO_ARQUIVO ELSE '../../../files/' + PE.NOME_EPUB END AS CAMINHOARQUIVO, " +
                                "SUBSTRING(REPLACE(REPLACE(STUFF((SELECT ASSU.ASSU_DESCRICAO FROM ASSUNTO ASSU " +
                                "    INNER JOIN PRODUTO_ASSUNTO PRODASSU " +
                                "        ON PRODASSU.PASS_ASSU = ASSU.ASSU_ID " +
                                "    WHERE P.PROD_ID = PRODASSU.PASS_PROD " +
                                "    FOR XML PATH('')), 1, 0, ''), '</ASSU_DESCRICAO>', ''), '<ASSU_DESCRICAO>', ', '), 2, 4000) AS ASSU_DESCRICAO, " +
                                "CDIS.CDIS_DESCRICAO, " +
                                "FETA.FETA_DESCRICAO, " +
                                "FT.FOTO, " +
                                "CASE WHEN PFB.NOME_FB IS NOT NULL THEN 'FB' ELSE 'EPUB' END AS TIPOARQUIVO " +
                                "FROM PRODUTO P " +
                                "INNER JOIN TB_PRODUTO_EPUB PE " +
                                "ON P.PROD_ID = PE.PROD_ID " +
                                "LEFT JOIN TB_PRODUTO_FLIPPINGBOOK PFB " +
                                "ON P.PROD_ID = PFB.PROD_ID " +
                                "INNER JOIN FICHATECNICA FT " +
                                "ON P.PROD_ID = FT.PROD_ID " +
                                "LEFT JOIN PRODUTO_ASSUNTO PRODASSU " +
                                "ON P.PROD_EBSA = PRODASSU.PASS_PROD " +
                                "LEFT JOIN ASSUNTO ASSU " +
                                "ON PRODASSU.PASS_ASSU = ASSU.ASSU_ID " +
                                "LEFT JOIN CONTEUDO_DISCIPLINAR CDIS " +
                                "ON P.PROD_CDIS = CDIS.CDIS_ID " +
                                "LEFT JOIN FAIXA_ETARIA FETA " +
                                "ON P.PROD_FAIXAETARIA = FETA.FETA_ID " +
                                "WHERE P.PROD_PLAT = '1' " +
                                "AND P.PROD_PUBLICADO = 1 " +
                                "AND P.PROD_STATUS = 1 " +
                                "AND P.PROD_UNIDADE_MEDIDA = 4";

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        ProdutosDocumentosAutorias produto = new ProdutosDocumentosAutorias();

                        produto.PROD_ID = !DBNull.Value.Equals(reader["prod_id"]) ? Convert.ToDecimal(reader["prod_id"]) : 0;
                        //produto.AUTO_ID = !DBNull.Value.Equals(reader["auto_id"]) ? Convert.ToDecimal(reader["auto_id"]) : 0;
                        produto.AUTORES = !DBNull.Value.Equals(reader["prod_nome_capa"].ToString()) ? reader["prod_nome_capa"].ToString() : "";
                        produto.DOC_ID = !DBNull.Value.Equals(reader["doc_id"]) ? reader["doc_id"].ToString() : "";
                        produto.TITULO = !DBNull.Value.Equals(reader["prod_titulo"].ToString()) ? reader["prod_titulo"].ToString() : "";
                        produto.FAIXAETARIA = !DBNull.Value.Equals(reader["prod_faixaetaria"]) ? Convert.ToInt32(reader["prod_faixaetaria"]) : 0;
                        produto.CAMINHOARQUIVO = !DBNull.Value.Equals(reader["caminhoarquivo"].ToString()) ? reader["caminhoarquivo"].ToString().Replace("../", "") : "";
                        produto.CAPA = !DBNull.Value.Equals(reader["FOTO"].ToString()) ? reader["FOTO"].ToString().Replace("../", "") : "";
                        produto.ASSU_DESCRICAO = !DBNull.Value.Equals(reader["assu_descricao"].ToString()) ? reader["assu_descricao"].ToString() : "";
                        produto.CDIS_DESCRICAO = !DBNull.Value.Equals(reader["cdis_descricao"].ToString()) ? reader["cdis_descricao"].ToString() : "";
                        produto.FETA_DESCRICAO = !DBNull.Value.Equals(reader["feta_descricao"].ToString()) ? reader["feta_descricao"].ToString() : "";

                        lstproduto.Add(produto);
                    }

                    return lstproduto;
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

        public async Task<IEnumerable<ProdutosDocumentosAutorias>> ListarIdEscola(int IdEscola)
        {
            List<ProdutosDocumentosAutorias> lstproduto = new List<ProdutosDocumentosAutorias>();
            SqlDataReader reader = null;

            string consulta = "SELECT P.PROD_ID, " +
                                "PE.ID_PRODUTO_EPUB AS DOC_ID, " +
                                "P.PROD_TITULO, " +
                                "P.PROD_NOME_CAPA, " +
                                "P.PROD_FAIXAETARIA, " +
                                "CASE WHEN PFB.NOME_FB IS NOT NULL THEN PFB.CAMINHO_ARQUIVO ELSE '../../../files/' + PE.NOME_EPUB END AS CAMINHOARQUIVO, " +
                                "SUBSTRING(REPLACE(REPLACE(STUFF((SELECT ASSU.ASSU_DESCRICAO FROM ASSUNTO ASSU " +
                                "    INNER JOIN PRODUTO_ASSUNTO PRODASSU " +
                                "        ON PRODASSU.PASS_ASSU = ASSU.ASSU_ID " +
                                "    WHERE P.PROD_ID = PRODASSU.PASS_PROD " +
                                "    FOR XML PATH('')), 1, 0, ''), '</ASSU_DESCRICAO>', ''), '<ASSU_DESCRICAO>', ', '), 2, 4000) AS ASSU_DESCRICAO, " +
                                "CDIS.CDIS_DESCRICAO, " +
                                "FETA.FETA_DESCRICAO, " +
                                "FT.FOTO, " +
                                "CASE WHEN PFB.NOME_FB IS NOT NULL THEN 'FB' ELSE 'EPUB' END AS TIPOARQUIVO " +
                                "FROM PRODUTO P " +
                                "INNER JOIN TB_PRODUTO_EPUB PE " +
                                "ON P.PROD_ID = PE.PROD_ID " +
                                "LEFT JOIN TB_PRODUTO_FLIPPINGBOOK PFB " +
                                "ON P.PROD_ID = PFB.PROD_ID " +
                                "INNER JOIN FICHATECNICA FT " +
                                "ON P.PROD_ID = FT.PROD_ID " +
                                "LEFT JOIN PRODUTO_ASSUNTO PRODASSU " +
                                "ON P.PROD_EBSA = PRODASSU.PASS_PROD " +
                                "LEFT JOIN ASSUNTO ASSU " +
                                "ON PRODASSU.PASS_ASSU = ASSU.ASSU_ID " +
                                "LEFT JOIN CONTEUDO_DISCIPLINAR CDIS " +
                                "ON P.PROD_CDIS = CDIS.CDIS_ID " +
                                "LEFT JOIN FAIXA_ETARIA FETA " +
                                "ON P.PROD_FAIXAETARIA = FETA.FETA_ID " +
                                "WHERE P.PROD_PLAT = '1' " +
                                "AND P.PROD_PUBLICADO = 1 " +
                                "AND P.PROD_STATUS = 1 " +
                                "AND P.PROD_UNIDADE_MEDIDA = 4 " +
                          "AND P.PROD_ID NOT IN(SELECT ID_PRODUTO FROM TB_CONTROLE_CONTEUDO WHERE ID_ESCOLA = " + IdEscola + ")";

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        ProdutosDocumentosAutorias produto = new ProdutosDocumentosAutorias();

                        produto.PROD_ID = !DBNull.Value.Equals(reader["prod_id"]) ? Convert.ToDecimal(reader["prod_id"]) : 0;
                        //produto.AUTO_ID = !DBNull.Value.Equals(reader["auto_id"]) ? Convert.ToDecimal(reader["auto_id"]) : 0;
                        produto.AUTORES = !DBNull.Value.Equals(reader["prod_nome_capa"].ToString()) ? reader["prod_nome_capa"].ToString() : "";
                        produto.DOC_ID = !DBNull.Value.Equals(reader["doc_id"]) ? reader["doc_id"].ToString() : "";
                        produto.TITULO = !DBNull.Value.Equals(reader["prod_titulo"].ToString()) ? reader["prod_titulo"].ToString() : "";
                        produto.FAIXAETARIA = !DBNull.Value.Equals(reader["prod_faixaetaria"]) ? Convert.ToInt32(reader["prod_faixaetaria"]) : 0;
                        produto.CAMINHOARQUIVO = !DBNull.Value.Equals(reader["caminhoarquivo"].ToString()) ? reader["caminhoarquivo"].ToString().Replace("../", "") : "";
                        produto.CAPA = !DBNull.Value.Equals(reader["FOTO"].ToString()) ? reader["FOTO"].ToString().Replace("../", "") : "";
                        produto.ASSU_DESCRICAO = !DBNull.Value.Equals(reader["assu_descricao"].ToString()) ? reader["assu_descricao"].ToString() : "";
                        produto.CDIS_DESCRICAO = !DBNull.Value.Equals(reader["cdis_descricao"].ToString()) ? reader["cdis_descricao"].ToString() : "";
                        produto.FETA_DESCRICAO = !DBNull.Value.Equals(reader["feta_descricao"].ToString()) ? reader["feta_descricao"].ToString() : "";

                        lstproduto.Add(produto);
                    }

                    return lstproduto;
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

        public async Task<IEnumerable<ProdutoControleConteudo>> ListarControleConteudo(int IdEscola)
        {
            List<ProdutoControleConteudo> lstproduto = new List<ProdutoControleConteudo>();
            SqlDataReader reader = null;

            string consulta = "SELECT P.PROD_ID, " +
                               "P.PROD_TITULO,  " +
                               "P.PROD_NOME_CAPA,  " +
                               "CASE WHEN PFB.NOME_FB IS NOT NULL THEN PFB.CAMINHO_ARQUIVO ELSE '../../../files/' + PE.NOME_EPUB END AS CAMINHOARQUIVO, " +
                               "SUBSTRING(PIC.CAMINHO_ARQUIVO, 34, 40) AS CAMINHO_ARQUIVO, " +
                               "P.PROD_SINOPSE, " +
                               "CASE WHEN EXISTS( " +
                               " SELECT * " +
                               " FROM TB_CONTROLE_CONTEUDO CC " +
                               " WHERE CC.ID_PRODUTO = P.PROD_ID " +
                               " AND CC.ID_ESCOLA = " + IdEscola + ") " +
                             "THEN 1 ELSE 0 END AS CONTROLE " +
                          "FROM PRODUTO P " +
                          "INNER JOIN TB_PRODUTO_EPUB PE " +
                            "ON P.PROD_ID = PE.PROD_ID " +
                          "  LEFT JOIN TB_PRODUTO_FLIPPINGBOOK PFB " +
                          "    ON P.PROD_ID = PFB.PROD_ID " +
                          "INNER JOIN TB_PRODUTO_IMAGEM_CAPA PIC " +
                            "ON P.PROD_EBSA = PIC.PROD_EBSA " +
                        "WHERE P.PROD_PLAT = '1' " +
                        "AND P.PROD_PUBLICADO = 1 " +
                        "AND P.PROD_STATUS = 1 " +
                        "AND P.PROD_UNIDADE_MEDIDA = 4";

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        ProdutoControleConteudo produto = new ProdutoControleConteudo();

                        produto.ProdId = Convert.ToInt64(reader["PROD_ID"]);
                        produto.ProdTitulo = reader["PROD_TITULO"].ToString();
                        produto.ProdNomeCapa = reader["PROD_NOME_CAPA"].ToString();
                        produto.CaminhoArquivo = reader["CAMINHOARQUIVO"].ToString();
                        produto.Foto = reader["CAMINHO_ARQUIVO"].ToString();
                        produto.ProdSinopse = reader["PROD_SINOPSE"].ToString();
                        produto.Controle = Convert.ToBoolean(reader["CONTROLE"]);

                        lstproduto.Add(produto);
                    }

                    return lstproduto;
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

        public async Task BloquearEscola(int idEscola, int idProduto)
        {
            string consulta = "IF EXISTS (SELECT * FROM TB_CONTROLE_CONTEUDO WHERE ID_ESCOLA = " + idEscola + " AND ID_PRODUTO = " + idProduto + ") " +
                              "  BEGIN " +
                              "      DELETE FROM TB_CONTROLE_CONTEUDO WHERE ID_ESCOLA = " + idEscola + " AND ID_PRODUTO = " + idProduto + "; " +
                              "          END " +
                              "      ELSE " +
                              "  BEGIN " +
                              "      INSERT INTO TB_CONTROLE_CONTEUDO(ID_ESCOLA, ID_PRODUTO) VALUES(" + idEscola + ", " + idProduto + "); " +
                              "          END";

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

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

        public async Task<IEnumerable<Assuntos>> Assuntos()
        {
            List<Assuntos> lstassunto = new List<Assuntos>();
            SqlDataReader reader = null;

            string consulta = "SELECT ASSU_ID, " +
                          "       ASSU_DESCRICAO " +
                          "  FROM ASSUNTO";

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        Assuntos assunto = new Assuntos();

                        assunto.ASSU_ID = Convert.ToDecimal(reader["assu_id"]);
                        assunto.ASSU_DESCRICAO = reader["assu_descricao"].ToString();

                        lstassunto.Add(assunto);
                    }

                    return lstassunto;
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

        public async Task<IEnumerable<Autores>> Autores()
        {
            List<Autores> lstautor = new List<Autores>();
            SqlDataReader reader = null;

            string consulta = "SELECT DISTINCT PROD_NOME_CAPA " +
                              "FROM PRODUTO " +
                              "WHERE PROD_NOME_CAPA IS NOT NULL " +
                              "AND PROD_PLAT = '1' " +
                              "AND PROD_PUBLICADO = 1";

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        Autores autores = new Autores()
                        {
                            AUTOR_NOME_CONTRATUAL = reader["auto_nomecon"].ToString()
                        };

                        lstautor.Add(autores);
                    }

                    return lstautor;
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

        public async Task<IEnumerable<Conteudos>> Conteudos()
        {
            List<Conteudos> lstconteudo = new List<Conteudos>();
            SqlDataReader reader = null;

            string consulta = "SELECT CDIS_ID, " +
                          "       CDIS_DESCRICAO " +
                          "  FROM CONTEUDO_DISCIPLINAR";

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        Conteudos conteudo = new Conteudos();

                        conteudo.CDIS_ID = Convert.ToDecimal(reader["cdis_id"]);
                        conteudo.CDIS_DESCRICAO = reader["cdis_descricao"].ToString();

                        lstconteudo.Add(conteudo);
                    }

                    return lstconteudo;
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

        private async Task<IList<Dicionario>> CarregarMercado()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarMercado");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderDicionario(reader);
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
        private async Task<IList<Dicionario>> CarregarAno()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarAno");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderDicionario(reader);
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
        private async Task<IList<Dicionario>> CarregarDisciplina()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarDisciplina");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderDicionario(reader);
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
        private async Task<IList<Dicionario>> CarregarMidia()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarMidia");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderDicionario(reader);
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
        private async Task<IList<Dicionario>> CarregarTipo()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarTipo");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderDicionario(reader);
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
        private async Task<IList<Dicionario>> CarregarVersao()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarVersao");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderDicionario(reader);
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

        private IList<Dicionario> LerDataReaderDicionario(SqlDataReader reader)
        {
            IList<Dicionario> linhas = null;

            if (reader.HasRows)
            {
                linhas = new List<Dicionario>();
                Dicionario linha;

                while (reader.Read())
                {
                    linha = new Dicionario
                    {
                        Chave = reader["CHAVE"].ToString(),
                        Valor = reader["VALOR"].ToString()
                    };

                    linhas.Add(linha);
                }
            }

            return linhas;
        }
    }
}
