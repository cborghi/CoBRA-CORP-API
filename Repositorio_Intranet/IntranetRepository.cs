using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class IntranetRepository : IIntranetRepository
    {
        private readonly IBaseRepositoryIntranet _baseRepository;
        public IntranetRepository(IBaseRepositoryIntranet baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task CriarLivro(Livro item)
        {
            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _baseRepository.BuscarArquivoConsulta("SalvarLivro");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@CODIGO", !string.IsNullOrEmpty(item.CODIGO) ? item.CODIGO : "");
                            command.Parameters.AddWithValue("@OBRA", !string.IsNullOrEmpty(item.OBRA) ? item.OBRA : "");
                            command.Parameters.AddWithValue("@DATA", !string.IsNullOrEmpty(item.DATA.ToString()) ? item.DATA : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@SERIE", !string.IsNullOrEmpty(item.SERIE) ? item.SERIE : "");
                            command.Parameters.AddWithValue("@ENCADERNACAO", !string.IsNullOrEmpty(item.ENCADERNACAO) ? item.ENCADERNACAO : "");
                            command.Parameters.AddWithValue("@FORMATO", !string.IsNullOrEmpty(item.FORMATO) ? item.FORMATO : "");
                            command.Parameters.AddWithValue("@CORES", !string.IsNullOrEmpty(item.CORES.ToString()) ? item.CORES : 0);
                            command.Parameters.AddWithValue("@PAGINAS", !string.IsNullOrEmpty(item.PAGINAS.ToString()) ? item.PAGINAS : 0);
                            command.Parameters.AddWithValue("@TIPO", !string.IsNullOrEmpty(item.TIPO) ? item.TIPO : "");
                            command.Parameters.AddWithValue("@STATUS", !string.IsNullOrEmpty(item.STATUS) ? item.STATUS : "");
                            command.Parameters.AddWithValue("@DSTATUS", !string.IsNullOrEmpty(item.DSTATUS) ? item.DSTATUS : "");
                            command.Parameters.AddWithValue("@GRAVAR", !string.IsNullOrEmpty(item.GRAVAR) ? item.GRAVAR : "");
                            command.Parameters.AddWithValue("@INICIO", !string.IsNullOrEmpty(item.INICIO.ToString()) ? item.INICIO : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@PREVISAO", !string.IsNullOrEmpty(item.PREVISAO.ToString()) ? item.PREVISAO : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@TERMINO", !string.IsNullOrEmpty(item.TERMINO.ToString()) ? item.TERMINO : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@OBS", !string.IsNullOrEmpty(item.OBS) ? item.OBS : "");
                            command.Parameters.AddWithValue("@PROJETO", !string.IsNullOrEmpty(item.PROJETO) ? item.PROJETO : "");
                            command.Parameters.AddWithValue("@VOLUME", !string.IsNullOrEmpty(item.VOLUME) ? item.VOLUME : "");
                            command.Parameters.AddWithValue("@OPERADOR", !string.IsNullOrEmpty(item.OPERADOR) ? item.OPERADOR : "");
                            command.Parameters.AddWithValue("@DESCRICAO", !string.IsNullOrEmpty(item.DESCRICAO) ? item.DESCRICAO : "");
                            command.Parameters.AddWithValue("@COLECAO", !string.IsNullOrEmpty(item.COLECAO) ? item.COLECAO : "");
                            command.Parameters.AddWithValue("@CONDICAO", !string.IsNullOrEmpty(item.CONDICAO) ? item.CONDICAO : "");
                            command.Parameters.AddWithValue("@SITUACAO", !string.IsNullOrEmpty(item.SITUACAO.ToString()) ? item.SITUACAO : 0);
                            command.Parameters.AddWithValue("@COD_IMAGEM", !string.IsNullOrEmpty(item.COD_IMAGEM) ? item.COD_IMAGEM : "");
                            command.Parameters.AddWithValue("@MATERIA", !string.IsNullOrEmpty(item.MATERIA) ? item.MATERIA : "");
                            command.Parameters.AddWithValue("@AREA", !string.IsNullOrEmpty(item.AREA) ? item.AREA : "");
                            command.Parameters.AddWithValue("@PAG_MP", !string.IsNullOrEmpty(item.PAG_MP.ToString()) ? item.PAG_MP : 0);
                            command.Parameters.AddWithValue("@DT_INC", !string.IsNullOrEmpty(item.DT_INC.ToString()) ? item.DT_INC : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@OPERADOR_ALT", !string.IsNullOrEmpty(item.OPERADOR_ALT) ? item.OPERADOR_ALT : "");
                            command.Parameters.AddWithValue("@DT_ALT", !string.IsNullOrEmpty(item.DT_ALT.ToString()) ? item.DT_ALT : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@DATAD", !string.IsNullOrEmpty(item.DATAD.ToString()) ? item.DATAD : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@DATAE", !string.IsNullOrEmpty(item.DATAE.ToString()) ? item.DATAE : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@INFGERAL", !string.IsNullOrEmpty(item.INFGERAL) ? item.INFGERAL : "");
                            command.Parameters.AddWithValue("@DVD", !string.IsNullOrEmpty(item.DVD) ? item.DVD : "");
                            command.Parameters.AddWithValue("@OBRA_ANTIGA", !string.IsNullOrEmpty(item.OBRA_ANTIGA) ? item.OBRA_ANTIGA : "");
                            command.Parameters.AddWithValue("@PSEUDONIMO", !string.IsNullOrEmpty(item.PSEUDONIMO) ? item.PSEUDONIMO : "");
                            command.Parameters.AddWithValue("@NM_EDITORA", !string.IsNullOrEmpty(item.NM_EDITORA) ? item.NM_EDITORA : "");
                            command.Parameters.AddWithValue("@TITULO", !string.IsNullOrEmpty(item.TITULO) ? item.TITULO : "");
                            command.Parameters.AddWithValue("@TITULO_ORIGINAL", !string.IsNullOrEmpty(item.TITULO_ORIGINAL) ? item.TITULO_ORIGINAL : "");
                            command.Parameters.AddWithValue("@ANO_PUB", !string.IsNullOrEmpty(item.ANO_PUB.ToString()) ? item.ANO_PUB : 0);
                            command.Parameters.AddWithValue("@COLABORADOR", !string.IsNullOrEmpty(item.COLABORADOR) ? item.COLABORADOR : "");
                            command.Parameters.AddWithValue("@TEXT_COLAB", !string.IsNullOrEmpty(item.TEXT_COLAB) ? item.TEXT_COLAB : "");
                            command.Parameters.AddWithValue("@EDICAO", !string.IsNullOrEmpty(item.EDICAO.ToString()) ? item.EDICAO : 0);
                            command.Parameters.AddWithValue("@REIMP", !string.IsNullOrEmpty(item.REIMP) ? item.REIMP : "");
                            command.Parameters.AddWithValue("@CO_EDICAO", !string.IsNullOrEmpty(item.CO_EDICAO) ? item.CO_EDICAO : "");
                            command.Parameters.AddWithValue("@TIT_SC", !string.IsNullOrEmpty(item.TIT_SC) ? item.TIT_SC : "");
                            command.Parameters.AddWithValue("@NUM_PUB", !string.IsNullOrEmpty(item.NUM_PUB.ToString()) ? item.NUM_PUB : 0);
                            command.Parameters.AddWithValue("@P_PRECEDE", !string.IsNullOrEmpty(item.P_PRECEDE) ? item.P_PRECEDE : "");
                            command.Parameters.AddWithValue("@INC_BIB", !string.IsNullOrEmpty(item.INC_BIB) ? item.INC_BIB : "");
                            command.Parameters.AddWithValue("@INC_APEN", !string.IsNullOrEmpty(item.INC_APEN) ? item.INC_APEN : "");
                            command.Parameters.AddWithValue("@TIT_ANT", !string.IsNullOrEmpty(item.TIT_ANT) ? item.TIT_ANT : "");
                            command.Parameters.AddWithValue("@TIT_CON", !string.IsNullOrEmpty(item.TIT_CON) ? item.TIT_CON : "");
                            command.Parameters.AddWithValue("@TIT_SUP", !string.IsNullOrEmpty(item.TIT_SUP) ? item.TIT_SUP : "");
                            command.Parameters.AddWithValue("@TIT_OUT", !string.IsNullOrEmpty(item.TIT_OUT) ? item.TIT_OUT : "");
                            command.Parameters.AddWithValue("@SUPLEM", !string.IsNullOrEmpty(item.SUPLEM) ? item.SUPLEM : "");
                            command.Parameters.AddWithValue("@ISBN", !string.IsNullOrEmpty(item.ISBN) ? item.ISBN : "");
                            command.Parameters.AddWithValue("@P_CHAVE", !string.IsNullOrEmpty(item.P_CHAVE) ? item.P_CHAVE : "");
                            command.Parameters.AddWithValue("@CENTRO_CUSTO", !string.IsNullOrEmpty(item.CENTRO_CUSTO) ? item.CENTRO_CUSTO : "");
                            command.Parameters.AddWithValue("@USER_TROCA_CODIGO", !string.IsNullOrEmpty(item.USER_TROCA_CODIGO) ? item.USER_TROCA_CODIGO : "");
                            command.Parameters.AddWithValue("@DATA_TROCA_CODIGO", !string.IsNullOrEmpty(item.DATA_TROCA_CODIGO.ToString()) ? item.DATA_TROCA_CODIGO : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@PRE_TRIAGEM", !string.IsNullOrEmpty(item.PRE_TRIAGEM) ? item.PRE_TRIAGEM : "");
                            command.Parameters.AddWithValue("@TRIAGEM", !string.IsNullOrEmpty(item.TRIAGEM) ? item.TRIAGEM : "");
                            command.Parameters.AddWithValue("@PROVAS", !string.IsNullOrEmpty(item.PROVAS) ? item.PROVAS : "");
                            command.Parameters.AddWithValue("@NOVA_REFORMULADA", !string.IsNullOrEmpty(item.NOVA_REFORMULADA) ? item.NOVA_REFORMULADA : "");
                            command.Parameters.AddWithValue("@REFORMULADA", !string.IsNullOrEmpty(item.REFORMULADA) ? item.REFORMULADA : "");
                            command.Parameters.AddWithValue("@ANO_PROD", !string.IsNullOrEmpty(item.ANO_PROD) ? item.ANO_PROD : "");
                            command.Parameters.AddWithValue("@INICIO2", !string.IsNullOrEmpty(item.INICIO2.ToString()) ? item.INICIO2 : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@PREVISAO2", !string.IsNullOrEmpty(item.PREVISAO2.ToString()) ? item.PREVISAO2 : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@TERMINO2", !string.IsNullOrEmpty(item.TERMINO2.ToString()) ? item.TERMINO2 : DateTime.MinValue.AddYears(1753));

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

        public async Task CriarEnvioObra(EnvioObra item)
        {
            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _baseRepository.BuscarArquivoConsulta("SalvarEnvioObra");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@OBRA", !string.IsNullOrEmpty(item.OBRA) ? item.OBRA : "");
                            command.Parameters.AddWithValue("@STATUS", !string.IsNullOrEmpty(item.STATUS) ? item.STATUS : "");
                            command.Parameters.AddWithValue("@TIPO", !string.IsNullOrEmpty(item.TIPO) ? item.TIPO : "");
                            command.Parameters.AddWithValue("@EMISSAO", !string.IsNullOrEmpty(item.EMISSAO.ToString()) ? item.EMISSAO : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@DIG", !string.IsNullOrEmpty(item.DIG) ? item.DIG : "");
                            command.Parameters.AddWithValue("@NOME_ARQUIVO", !string.IsNullOrEmpty(item.NOME_ARQUIVO) ? item.NOME_ARQUIVO : "");
                            command.Parameters.AddWithValue("@FORNECEDOR", !string.IsNullOrEmpty(item.FORNECEDOR) ? item.FORNECEDOR : "");
                            command.Parameters.AddWithValue("@LOJA", !string.IsNullOrEmpty(item.LOJA) ? item.LOJA : "");
                            command.Parameters.AddWithValue("@CODIGO", !string.IsNullOrEmpty(item.CODIGO.ToString()) ? item.CODIGO : 0);
                            command.Parameters.AddWithValue("@DIGA", !string.IsNullOrEmpty(item.DIGA) ? item.DIGA : "");
                            command.Parameters.AddWithValue("@SITUACAO", !string.IsNullOrEmpty(item.SITUACAO) ? item.SITUACAO : "");
                            command.Parameters.AddWithValue("@OPERADOR", !string.IsNullOrEmpty(item.OPERADOR) ? item.OPERADOR : "");
                            command.Parameters.AddWithValue("@TAMANHO", !string.IsNullOrEmpty(item.TAMANHO) ? item.TAMANHO : "");
                            command.Parameters.AddWithValue("@VALOR", !string.IsNullOrEmpty(item.VALOR.ToString()) ? item.VALOR : 0);
                            command.Parameters.AddWithValue("@AUTORIZACAO", !string.IsNullOrEmpty(item.AUTORIZACAO) ? item.AUTORIZACAO : "");
                            command.Parameters.AddWithValue("@TEXTO", !string.IsNullOrEmpty(item.TEXTO) ? item.TEXTO : "");
                            //command.Parameters.AddWithValue("@IMAGEM", !string.IsNullOrEmpty(item.IMAGEM) ? item.IMAGEM : "");
                            command.Parameters.AddWithValue("@TEXTO_A", !string.IsNullOrEmpty(item.TEXTO_A) ? item.TEXTO_A : "");
                            command.Parameters.AddWithValue("@USUARIO", !string.IsNullOrEmpty(item.USUARIO) ? item.USUARIO : "");
                            command.Parameters.AddWithValue("@MOVIMENTADO", !string.IsNullOrEmpty(item.MOVIMENTADO.ToString()) ? item.MOVIMENTADO : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@ORIGEM", !string.IsNullOrEmpty(item.ORIGEM) ? item.ORIGEM : "");
                            command.Parameters.AddWithValue("@LEG_FIM", !string.IsNullOrEmpty(item.LEG_FIM) ? item.LEG_FIM : "");
                            command.Parameters.AddWithValue("@REFERENCIA", !string.IsNullOrEmpty(item.REFERENCIA) ? item.REFERENCIA : "");
                            command.Parameters.AddWithValue("@VENCIMENTO", !string.IsNullOrEmpty(item.VENCIMENTO.ToString()) ? item.VENCIMENTO : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@REQUER_AUTOR", !string.IsNullOrEmpty(item.REQUER_AUTOR) ? item.REQUER_AUTOR : "");
                            command.Parameters.AddWithValue("@OBRA_ANTIGA", !string.IsNullOrEmpty(item.OBRA_ANTIGA) ? item.OBRA_ANTIGA : "");
                            command.Parameters.AddWithValue("@LEGENDA_OLD", !string.IsNullOrEmpty(item.LEGENDA_OLD) ? item.LEGENDA_OLD : "");
                            command.Parameters.AddWithValue("@DESCRICAO", !string.IsNullOrEmpty(item.DESCRICAO) ? item.DESCRICAO : "");
                            command.Parameters.AddWithValue("@DATA_ALTERACAO_DIGA", !string.IsNullOrEmpty(item.DATA_ALTERACAO_DIGA.ToString()) ? item.DATA_ALTERACAO_DIGA : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@OBS", !string.IsNullOrEmpty(item.OBS) ? item.OBS : "");
                            command.Parameters.AddWithValue("@LEGENDA_AUTOR_OLD", !string.IsNullOrEmpty(item.LEGENDA_AUTOR_OLD) ? item.LEGENDA_AUTOR_OLD : "");
                            command.Parameters.AddWithValue("@LICENC", !string.IsNullOrEmpty(item.LICENC) ? item.LICENC : "");
                            command.Parameters.AddWithValue("@CREDITO", !string.IsNullOrEmpty(item.CREDITO) ? item.CREDITO : "");
                            command.Parameters.AddWithValue("@REQ_GERADA", !string.IsNullOrEmpty(item.REQ_GERADA) ? item.REQ_GERADA : "");
                            command.Parameters.AddWithValue("@REQ_DATA", !string.IsNullOrEmpty(item.REQ_DATA.ToString()) ? item.REQ_DATA : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@REQ_INCLUIDOR", !string.IsNullOrEmpty(item.REQ_INCLUIDOR) ? item.REQ_INCLUIDOR : "");
                            command.Parameters.AddWithValue("@REQUISICAO", !string.IsNullOrEmpty(item.REQUISICAO.ToString()) ? item.REQUISICAO : 0);
                            command.Parameters.AddWithValue("@DATA_FINALIZADA", !string.IsNullOrEmpty(item.DATA_FINALIZADA.ToString()) ? item.DATA_FINALIZADA : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@QUEM_FINALIZOU", !string.IsNullOrEmpty(item.QUEM_FINALIZOU) ? item.QUEM_FINALIZOU : "");
                            command.Parameters.AddWithValue("@NF", !string.IsNullOrEmpty(item.NF) ? item.NF : "");
                            command.Parameters.AddWithValue("@COD_INTERNO", !string.IsNullOrEmpty(item.COD_INTERNO) ? item.COD_INTERNO : "");
                            command.Parameters.AddWithValue("@USERALT", !string.IsNullOrEmpty(item.USERALT) ? item.USERALT : "");
                            command.Parameters.AddWithValue("@DISCIPLINA", !string.IsNullOrEmpty(item.DISCIPLINA) ? item.DISCIPLINA : "");
                            command.Parameters.AddWithValue("@ANO", !string.IsNullOrEmpty(item.ANO) ? item.ANO : "");
                            command.Parameters.AddWithValue("@ENTRA_GALERIA_IMAGEM", !string.IsNullOrEmpty(item.ENTRA_GALERIA_IMAGEM) ? item.ENTRA_GALERIA_IMAGEM : "");
                            command.Parameters.AddWithValue("@PORTAL", !string.IsNullOrEmpty(item.PORTAL) ? item.PORTAL : "");
                            command.Parameters.AddWithValue("@UNIDADE", !string.IsNullOrEmpty(item.UNIDADE) ? item.UNIDADE : "");
                            command.Parameters.AddWithValue("@PAGINA", !string.IsNullOrEmpty(item.PAGINA) ? item.PAGINA : "");
                            command.Parameters.AddWithValue("@LEGENDA", !string.IsNullOrEmpty(item.LEGENDA) ? item.LEGENDA : "");
                            command.Parameters.AddWithValue("@OBS_ICONOGRAFIA", !string.IsNullOrEmpty(item.OBS_ICONOGRAFIA) ? item.OBS_ICONOGRAFIA : "");
                            command.Parameters.AddWithValue("@CONTRATO_GERADO", !string.IsNullOrEmpty(item.CONTRATO_GERADO) ? item.CONTRATO_GERADO : "");
                            command.Parameters.AddWithValue("@CONTRATO_USUARIO", !string.IsNullOrEmpty(item.CONTRATO_USUARIO) ? item.CONTRATO_USUARIO : "");
                            command.Parameters.AddWithValue("@CONTRATO_INCLUIDO", !string.IsNullOrEmpty(item.CONTRATO_INCLUIDO.ToString()) ? item.CONTRATO_INCLUIDO : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@CONTRATO", !string.IsNullOrEmpty(item.CONTRATO.ToString()) ? item.CONTRATO : 0);
                            command.Parameters.AddWithValue("@LEGENDA_AUTOR_OLD2", !string.IsNullOrEmpty(item.LEGENDA_AUTOR_OLD2) ? item.LEGENDA_AUTOR_OLD2 : "");
                            command.Parameters.AddWithValue("@LEGENDA_AUTOR", !string.IsNullOrEmpty(item.LEGENDA_AUTOR) ? item.LEGENDA_AUTOR : "");
                            command.Parameters.AddWithValue("@INFO_DIAGRAMACAO", !string.IsNullOrEmpty(item.INFO_DIAGRAMACAO) ? item.INFO_DIAGRAMACAO : "");
                            command.Parameters.AddWithValue("@ID_ELVIS", !string.IsNullOrEmpty(item.ID_ELVIS) ? item.ID_ELVIS : "");

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

        public async Task CriarEnvioObraResumo(EnvioObraResumo item)
        {
            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _baseRepository.BuscarArquivoConsulta("SalvarEnvioObraResumo");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@OBRA", !string.IsNullOrEmpty(item.OBRA) ? item.OBRA : "");
                            command.Parameters.AddWithValue("@STATUS", !string.IsNullOrEmpty(item.STATUS) ? item.STATUS : "");
                            command.Parameters.AddWithValue("@REVISAO_PROVA", !string.IsNullOrEmpty(item.REVISAO_PROVA) ? item.REVISAO_PROVA : "");
                            command.Parameters.AddWithValue("@REVISAO_PROVA_DATA", !string.IsNullOrEmpty(item.REVISAO_PROVA_DATA.ToString()) ? item.REVISAO_PROVA_DATA : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@IMAGEM_ALTA", !string.IsNullOrEmpty(item.IMAGEM_ALTA) ? item.IMAGEM_ALTA : "");
                            command.Parameters.AddWithValue("@IMAGEM_ALTA_DATA", !string.IsNullOrEmpty(item.IMAGEM_ALTA_DATA.ToString()) ? item.IMAGEM_ALTA_DATA : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@PAGTO", !string.IsNullOrEmpty(item.PAGTO) ? item.PAGTO : "");
                            command.Parameters.AddWithValue("@PAGTO_DATA", !string.IsNullOrEmpty(item.PAGTO_DATA.ToString()) ? item.PAGTO_DATA : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@FATURAMENTO", !string.IsNullOrEmpty(item.FATURAMENTO) ? item.FATURAMENTO : "");
                            command.Parameters.AddWithValue("@FATURAMENTO_OBS", !string.IsNullOrEmpty(item.FATURAMENTO_OBS) ? item.FATURAMENTO_OBS : "");
                            command.Parameters.AddWithValue("@USO_IMAGEM", !string.IsNullOrEmpty(item.USO_IMAGEM) ? item.USO_IMAGEM : "");
                            command.Parameters.AddWithValue("@USO_IMAGEM_OBS", !string.IsNullOrEmpty(item.USO_IMAGEM_OBS) ? item.USO_IMAGEM_OBS : "");
                            command.Parameters.AddWithValue("@LICENCIAMENTO", !string.IsNullOrEmpty(item.LICENCIAMENTO) ? item.LICENCIAMENTO : "");
                            command.Parameters.AddWithValue("@LICENCIAMENTO_OBS", !string.IsNullOrEmpty(item.LICENCIAMENTO_OBS) ? item.LICENCIAMENTO_OBS : "");
                            command.Parameters.AddWithValue("@ICONOGRAFO", !string.IsNullOrEmpty(item.ICONOGRAFO) ? item.ICONOGRAFO : "");
                            command.Parameters.AddWithValue("@RESPONSAVEL", !string.IsNullOrEmpty(item.RESPONSAVEL) ? item.RESPONSAVEL : "");
                            command.Parameters.AddWithValue("@COORDENADOR", !string.IsNullOrEmpty(item.COORDENADOR) ? item.COORDENADOR : "");
                            command.Parameters.AddWithValue("@E_STATUS", !string.IsNullOrEmpty(item.E_STATUS) ? item.E_STATUS : "");
                            command.Parameters.AddWithValue("@E_REVISAO_PROVA", !string.IsNullOrEmpty(item.E_REVISAO_PROVA) ? item.E_REVISAO_PROVA : "");
                            command.Parameters.AddWithValue("@E_REVISAO_PROVA_DATA", !string.IsNullOrEmpty(item.E_REVISAO_PROVA_DATA.ToString()) ? item.E_REVISAO_PROVA_DATA : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@E_IMAGEM_ALTA", !string.IsNullOrEmpty(item.E_IMAGEM_ALTA) ? item.E_IMAGEM_ALTA : "");
                            command.Parameters.AddWithValue("@E_IMAGEM_ALTA_DATA", !string.IsNullOrEmpty(item.E_IMAGEM_ALTA_DATA.ToString()) ? item.E_IMAGEM_ALTA_DATA : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@E_PAGTO", !string.IsNullOrEmpty(item.E_PAGTO) ? item.E_PAGTO : "");
                            command.Parameters.AddWithValue("@E_PAGTO_DATA", !string.IsNullOrEmpty(item.E_PAGTO_DATA.ToString()) ? item.E_PAGTO_DATA : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@E_FATURAMENTO", !string.IsNullOrEmpty(item.E_FATURAMENTO) ? item.E_FATURAMENTO : "");
                            command.Parameters.AddWithValue("@E_FATURAMENTO_OBS", !string.IsNullOrEmpty(item.E_FATURAMENTO_OBS) ? item.E_FATURAMENTO_OBS : "");
                            command.Parameters.AddWithValue("@E_USO_IMAGEM", !string.IsNullOrEmpty(item.E_USO_IMAGEM) ? item.E_USO_IMAGEM : "");
                            command.Parameters.AddWithValue("@E_USO_IMAGEM_OBS", !string.IsNullOrEmpty(item.E_USO_IMAGEM_OBS) ? item.E_USO_IMAGEM_OBS : "");
                            command.Parameters.AddWithValue("@E_LICENCIAMENTO", !string.IsNullOrEmpty(item.E_LICENCIAMENTO) ? item.E_LICENCIAMENTO : "");
                            command.Parameters.AddWithValue("@E_LICENCIAMENTO_OBS", !string.IsNullOrEmpty(item.E_LICENCIAMENTO_OBS) ? item.E_LICENCIAMENTO_OBS : "");
                            command.Parameters.AddWithValue("@E_ILUSTRADOR", !string.IsNullOrEmpty(item.E_ILUSTRADOR) ? item.E_ILUSTRADOR : "");
                            command.Parameters.AddWithValue("@E_RESPONSAVEL", !string.IsNullOrEmpty(item.E_RESPONSAVEL) ? item.E_RESPONSAVEL : "");
                            command.Parameters.AddWithValue("@E_COORDENADOR", !string.IsNullOrEmpty(item.E_COORDENADOR) ? item.E_COORDENADOR : "");
                            command.Parameters.AddWithValue("@COD_AUTOR", !string.IsNullOrEmpty(item.COD_AUTOR) ? item.COD_AUTOR : "");
                            command.Parameters.AddWithValue("@COD_EDITOR", !string.IsNullOrEmpty(item.COD_EDITOR) ? item.COD_EDITOR : "");
                            command.Parameters.AddWithValue("@FECHAMENTO", !string.IsNullOrEmpty(item.FECHAMENTO.ToString()) ? item.FECHAMENTO : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@E_FECHAMENTO", !string.IsNullOrEmpty(item.E_FECHAMENTO.ToString()) ? item.E_FECHAMENTO : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@COD_ASSISTENTE", !string.IsNullOrEmpty(item.COD_ASSISTENTE) ? item.COD_ASSISTENTE : "");
                            command.Parameters.AddWithValue("@COD_ASSISTENTE2", !string.IsNullOrEmpty(item.COD_ASSISTENTE2) ? item.COD_ASSISTENTE2 : "");
                            command.Parameters.AddWithValue("@G_FECHAMENTO", !string.IsNullOrEmpty(item.G_FECHAMENTO.ToString()) ? item.G_FECHAMENTO : DateTime.MinValue.AddYears(1753));
                            command.Parameters.AddWithValue("@G_OBS_FECHAMENTO", !string.IsNullOrEmpty(item.G_OBS_FECHAMENTO) ? item.G_OBS_FECHAMENTO : "");
                            command.Parameters.AddWithValue("@COD_AUXILIAR", !string.IsNullOrEmpty(item.COD_AUXILIAR) ? item.COD_AUXILIAR : "");
                            command.Parameters.AddWithValue("@VISUALIZAR", !string.IsNullOrEmpty(item.VISUALIZAR) ? item.VISUALIZAR : "");
                            command.Parameters.AddWithValue("@COD_G_GI", !string.IsNullOrEmpty(item.COD_G_GI) ? item.COD_G_GI : "");
                            command.Parameters.AddWithValue("@OBRA_ANTIGA", !string.IsNullOrEmpty(item.OBRA_ANTIGA) ? item.OBRA_ANTIGA : "");

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
