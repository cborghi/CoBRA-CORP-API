using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using CoBRA.Infra.Intranet.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace CoBRA.Infra.Intranet
{
    public class RequisicaoCompraRepository : BaseRepositoryRequisicaoElvis, IRequisicaoCompraRepository
    {
        public RequisicaoCompraRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<RequisicaoCompras> ObterRequisicoes(Usuario usuario)
        {
            List<RequisicaoCompras> requisicoes = new List<RequisicaoCompras>();

            StringBuilder query = new StringBuilder(@"SELECT rq.ID, us.NOME, rq.EMISSAO, rq.VALOR, rq.DOC, rq.FORMA_PAGTO, rq.PESSOA, rq.DATA_ENVIO_FINANCEIRO, rq.PRAZO_PAGTO, pt.A2_NOME as FORNECEDOR, pt.A2_CGC as CGC,
                                                      CASE WHEN rq.APROVADO_GERENTE = 'S' THEN 'Aprovado' WHEN rq.APROVADO_GERENTE = 'R' THEN 'Reprovado' ELSE 'Aguardando análise' END AS APROVADO_GERENTE,
                                                      CASE WHEN rq.APROVADO_SUPERVISOR = 'S' THEN 'Aprovado' WHEN rq.APROVADO_SUPERVISOR = 'R' THEN 'Reprovado' ELSE 'Aguardando análise' END AS APROVADO_SUPERVISOR
                                                      , RQ.INTEGRADO_PROTHEUS, RQ.CANCELADA, RQ.ELVIS_ORIGEM, L.OBRA AS TITULO_OBRA, ISNULL(RQ.NOTA_FISCAL_COMPARTILHADA,'') as NOTA_FISCAL_COMPARTILHADA
                                                      FROM REQUISICAO as RQ
                                                      LEFT JOIN REQUISICAO_OBRA RO ON RQ.ID = RO.ID_REQ
                                                      LEFT JOIN LIVRO L ON RO.OBRA = L.CODIGO
                                                      INNER JOIN USUARIO us ON us.ID_USUARIO = rq.INCLUIDOR
                                                      JOIN ");
            query.Append(ProdEnvironment() ? "[Protheus]" : "[Protheus_Dev]");
            query.Append(".dbo.SA2010 pt on pt.A2_COD COLLATE Latin1_General_CI_AS = rq.FORNECEDOR COLLATE Latin1_General_CI_AS and rq.LOJA COLLATE Latin1_General_CI_AS = pt.A2_LOJA COLLATE Latin1_General_CI_AS" +
                " WHERE rq.ID_REQUISICAO_DEPTO = " + usuario.Grupo.Id
                );

            List<SqlParameter> param = new List<SqlParameter>()
            {
                    new SqlParameter() { ParameterName = "@CentroCusto", Value = usuario.Grupo.CentroCusto },
                    new SqlParameter() { ParameterName = "@Grupo", Value = usuario.Grupo.Descricao },
            };

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        requisicoes.Add(new RequisicaoCompras()
                        {
                            Id = int.Parse(dataReader["ID"].ToString()),
                            Solicitante = dataReader["NOME"].ToString(),
                            Emissao = DateTime.Parse(dataReader["EMISSAO"].ToString()),
                            Nota = dataReader["DOC"].ToString(),
                            PrazoPagamento = DateTime.Parse(dataReader["PRAZO_PAGTO"].ToString()),
                            FormaPagamento = dataReader["FORMA_PAGTO"].ToString(),
                            Cgc = dataReader["CGC"].ToString(),
                            Valor = decimal.Parse(dataReader["VALOR"].ToString()),
                            Pessoa = dataReader["PESSOA"].ToString(),
                            AnaliseGerente = dataReader["APROVADO_GERENTE"].ToString(),
                            AnaliseSupervisor = dataReader["APROVADO_SUPERVISOR"].ToString(),
                            Fornecedor = new Fornecedor() { Nome = dataReader["FORNECEDOR"].ToString() },
                            EnviadoFinanceiro = dataReader["DATA_ENVIO_FINANCEIRO"] != DBNull.Value ? Convert.ToDateTime(dataReader["DATA_ENVIO_FINANCEIRO"]) : (DateTime?)null,
                            IntegradoProtheus = dataReader["INTEGRADO_PROTHEUS"].ToString(),
                            Cancelada = Convert.ToBoolean(dataReader["CANCELADA"]),
                            ElvisOrigem = Convert.ToBoolean(dataReader["ELVIS_ORIGEM"]),
                            NotaFiscalCompartilhada = Convert.ToBoolean(dataReader["NOTA_FISCAL_COMPARTILHADA"]),
                            TituloObra = dataReader["TITULO_OBRA"].ToString()
                        });
                    }
                }
            }
            return requisicoes;
        }

        public RequisicaoCompras ObterRequisicaoPorId(int id)
        {
            RequisicaoCompras requisicoes = new RequisicaoCompras();

            StringBuilder query = new StringBuilder(@"SELECT rq.ID, rq.DEPTO, us.NOME, rq.INCLUIDOR, rq.EMISSAO, rs.SERVICO, rq.VALOR, rq.TIPODOC, rq.CONTATO, rq.CONFIRMAR_PAGTO, rq.OBS, rq.TELEFONE, rq.INCLUIDO, rq.SERIE
                                   , rq.DOC, rq.DOC_LINK, rq.FORMA_PAGTO, rq.PESSOA, rq.DATA_ENVIO_FINANCEIRO, rq.PRAZO_PAGTO, rq.PRAZO_ENTREGA, pt.A2_NOME as FORNECEDOR, pt.A2_SISBCO, pt.A2_SISAGEN, pt.A2_SISCONT, pt.A2_CGC as CGC, gru.DEPARTAMENTO,
                                   CASE WHEN rq.APROVADO_GERENTE = 'S' THEN 'Aprovado' WHEN rq.APROVADO_GERENTE = 'R' THEN 'Reprovado' ELSE 'Aguardando análise' END AS APROVADO_GERENTE,
                                   CASE WHEN rq.APROVADO_SUPERVISOR = 'S' THEN 'Aprovado' WHEN rq.APROVADO_SUPERVISOR = 'R' THEN 'Reprovado' ELSE 'Aguardando análise' END AS APROVADO_SUPERVISOR,
                                   (SELECT NOME FROM USUARIO WHERE ID_USUARIO = rq.GERENTE) AS GERENTE,
                                   (SELECT NOME FROM USUARIO WHERE ID_USUARIO = rq.SUPERVISOR) AS SUPERVISOR
                                    , rq.INTEGRADO_PROTHEUS
                                    , rq.CANCELADA
                                    , rq.ELVIS_ORIGEM
                                   FROM REQUISICAO as RQ
                                   INNER JOIN GRUPO gru ON rq.ID_REQUISICAO_DEPTO = gru.ID_GRUPO
                                   INNER JOIN USUARIO us ON us.ID_USUARIO = rq.INCLUIDOR
                                   LEFT JOIN REQUISICAO_SERVICO rs on rs.ID_REQ = rq.ID
                                   JOIN ");
            query.Append(ProdEnvironment() ? "[Protheus]" : "[Protheus_Dev]");
            query.Append(".dbo.SA2010 pt on pt.A2_COD COLLATE Latin1_General_CI_AS = rq.FORNECEDOR COLLATE Latin1_General_CI_AS" +
                " and rq.LOJA COLLATE Latin1_General_CI_AS = pt.A2_LOJA COLLATE Latin1_General_CI_AS" +
                " WHERE rq.id = @id AND rq.EXCLUIDO like 'N%'");

            List<SqlParameter> param = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@id", Value = id }
            };

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        requisicoes = (new RequisicaoCompras()
                        {
                            Id = int.Parse(dataReader["ID"].ToString()),
                            Solicitante = dataReader["NOME"].ToString(),
                            SerieNotaFiscal = dataReader["NOME"].ToString(),
                            IdSolicitante = int.Parse(dataReader["INCLUIDOR"].ToString()),
                            CentroCusto = dataReader["DEPTO"].ToString(),
                            Emissao = DateTime.Parse(dataReader["EMISSAO"].ToString()),
                            Nota = dataReader["DOC"].ToString(),
                            PrazoPagamento = DateTime.Parse(dataReader["PRAZO_PAGTO"].ToString()),
                            PrazoEntrega = DateTime.Parse(dataReader["PRAZO_ENTREGA"].ToString()),
                            Fornecedor = new Fornecedor()
                            {
                                Nome = dataReader["FORNECEDOR"].ToString(),
                                Sisagen = dataReader["A2_SISAGEN"].ToString(),
                                Sisbco = dataReader["A2_SISBCO"].ToString(),
                                Siscont = dataReader["A2_SISCONT"].ToString(),
                            },
                            FormaPagamento = dataReader["FORMA_PAGTO"].ToString(),
                            Cgc = dataReader["CGC"].ToString(),
                            Valor = decimal.Parse(dataReader["VALOR"].ToString()),
                            Pessoa = dataReader["PESSOA"].ToString(),
                            EnviadoFinanceiro = dataReader["DATA_ENVIO_FINANCEIRO"] != DBNull.Value ? Convert.ToDateTime(dataReader["DATA_ENVIO_FINANCEIRO"]) : (DateTime?)null,
                            Observacao = dataReader["OBS"].ToString(),
                            Contato = dataReader["CONTATO"].ToString(),
                            Telefone = dataReader["TELEFONE"].ToString(),
                            Incluido = DateTime.Parse(dataReader["INCLUIDO"].ToString()),
                            Deposito = dataReader["CONFIRMAR_PAGTO"].ToString(),
                            Servico = new Servico() { TipoServico = dataReader["SERVICO"].ToString() },
                            TipoDocumento = dataReader["TIPODOC"].ToString(),
                            AprovadorGerente = dataReader["GERENTE"].ToString(),
                            AprovadorSupervisor = dataReader["SUPERVISOR"].ToString(),
                            AnaliseGerente = dataReader["APROVADO_GERENTE"].ToString(),
                            AnaliseSupervisor = dataReader["APROVADO_SUPERVISOR"].ToString(),
                            DocLink = dataReader["DOC_LINK"].ToString(),
                            Departamento = dataReader["DEPARTAMENTO"].ToString(),
                            IntegradoProtheus = dataReader["INTEGRADO_PROTHEUS"].ToString(),
                            ElvisOrigem = Convert.ToBoolean(dataReader["ELVIS_ORIGEM"]),
                            Cancelada = Convert.ToBoolean(dataReader["CANCELADA"])
                        });
                    }
                }
            }

            return requisicoes;
        }

        public Requisicao BuscarRequisicao(int idRequisicao)
        {
            var req = new Requisicao();
            var query = @"SELECT DEPTO 
                        , TIPODOC
                        , EMISSAO
                        , FORNECEDOR
                        , LOJA
                        , VALOR
                        , PRAZO_ENTREGA
                        , PRAZO_PAGTO
                        , PESSOA
                        , OPSIMP
                        , FORMA_PAGTO
                        , CONFIRMAR_PAGTO
                        , DOC
                        , TELEFONE
                        , CONTATO
                        , OUTROS
                        , OBS
                        , APROVADO_SUPERVISOR
                        , APROVADO_GERENTE
                        , OUTROS_SERVICOS
                        , DESC_PAG
                        , ID_REQUISICAO_DEPTO
                        , MOEDA
                        , ID_ELVIS
                        , DOC_LINK
                        , INTEGRADO_PROTHEUS
                        , CANCELADA
                        , ELVIS_ORIGEM
                        , NOTA_FISCAL_COMPARTILHADA
                        , SERIE
                    FROM REQUISICAO
                    WHERE ID = @IdRequisicao";
            List<SqlParameter> param = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@IdRequisicao", Value = idRequisicao }
            };

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        req.IdRequisicao = idRequisicao;
                        req.TipoDocumento = dataReader["TIPODOC"].ToString();
                        req.Nota = dataReader["DOC"].ToString();
                        req.Telefone = dataReader["TELEFONE"].ToString();
                        req.Contato = dataReader["CONTATO"].ToString();
                        req.DataPagamento = Convert.ToDateTime(dataReader["PRAZO_PAGTO"].ToString());
                        req.DataEntrega = Convert.ToDateTime(dataReader["PRAZO_ENTREGA"].ToString());
                        req.Moeda = dataReader["MOEDA"].ToString();
                        req.Valor = Convert.ToDecimal(dataReader["VALOR"].ToString());
                        req.FormaPagamento = dataReader["FORMA_PAGTO"].ToString();
                        req.ConfirmaPagamento = dataReader["CONFIRMAR_PAGTO"].ToString();
                        req.Observacao = dataReader["OBS"].ToString();
                        req.IdElvis = dataReader["ID_ELVIS"].ToString();
                        req.Fornecedor = new Fornecedor
                        {
                            Codigo = dataReader["FORNECEDOR"].ToString()
                        };
                        req.Link = dataReader["DOC_LINK"].ToString();
                        req.IntegradoProtheus = dataReader["INTEGRADO_PROTHEUS"].ToString();
                        req.Cancelada = Convert.ToBoolean(dataReader["CANCELADA"]);
                        req.ElvisOrigem = Convert.ToBoolean(dataReader["ELVIS_ORIGEM"]);
                        req.SerieNotaFiscal = dataReader["SERIE"].ToString();
                        req.NotaFiscalCompartilhada = bool.TryParse(dataReader["NOTA_FISCAL_COMPARTILHADA"].ToString(), out bool notaCompartilhada) ? notaCompartilhada : false;
                    }
                }
            }
            return req;
        }

        public string BuscarServicoRequisicao(int idRequisicao)
        {
            var serv = "";
            var query = @"SELECT SERVICO
                        FROM REQUISICAO_SERVICO
                        WHERE ID_REQ = @IdRequisicao";
            List<SqlParameter> param = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@IdRequisicao", Value = idRequisicao }
            };

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        serv = dataReader["SERVICO"].ToString();
                    }
                }
            }
            return serv;
        }

        public List<Obra> BuscarObraRequisicao(int idRequisicao)
        {
            List<Obra> obras = new List<Obra>();

            StringBuilder query = new StringBuilder(@"SELECT DISTINCT
                                                            [L].[CODIGO] AS OBRA,
                                                            [L].[OBRA] AS DESCRICAO,
                                                            [L].[PROJETO],
                                                            [RO].[RATEIO]
                                                          FROM
                                                            [EBSA].[dbo].[LIVRO] [L]
                                                          INNER JOIN
                                                            [EBSA].[dbo].[ENVIO_OBRA_ELVIS] [EOE]
                                                          ON
                                                            [L].[CODIGO] = [EOE].[OBRA]
                                                          INNER JOIN
                                                            [EBSA].[dbo].[REQUISICAO_OBRA] [RO]
                                                          ON
                                                            [EOE].[OBRA] = [RO].[OBRA]
                                                          WHERE
                                                            [RO].ID_REQ = " + idRequisicao);

            using (DbDataReader dataReader = GetDataReader(query.ToString(), null, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        obras.Add(new Obra()
                        {
                            Codigo = dataReader["OBRA"].ToString(),
                            Descricao = dataReader["DESCRICAO"].ToString().Trim(),
                            Projeto = dataReader["PROJETO"].ToString().Trim(),
                            Rateio = float.Parse(dataReader["RATEIO"].ToString().Trim())
                        });
                    }
                }
            }
            return obras;

        }

        public List<RequisicaoObra> ObterRequisicaoObra(int requisicao, string obra)
        {
            List<RequisicaoObra> requisicoes = new List<RequisicaoObra>();
            StringBuilder query = new StringBuilder(@"SELECT ID_REQ FROM REQUISICAO_OBRA WHERE ID_REQ = @requisicao AND OBRA = @obra");

            List<SqlParameter> param = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@requisicao", Value = requisicao },
                  new SqlParameter() { ParameterName = "@obra", Value = obra},
            };

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        requisicoes.Add(new RequisicaoObra()
                        {
                            RequisicaoId = int.Parse(dataReader["ID_REQ"].ToString()),
                        });
                    }
                }
            }

            return requisicoes;
        }

        public List<RequisicaoObra> ObterRequisicaoGeradaPorNota(string nota)
        {
            List<RequisicaoObra> requisicoes = new List<RequisicaoObra>();

            StringBuilder query = new StringBuilder(@"SELECT RO.*, L.PROJETO, L.OBRA AS DESCRICAO FROM REQUISICAO_OBRA RO INNER JOIN LIVRO L ON RO.OBRA = L.CODIGO WHERE RO.ID_REQ = @nf");

            List<SqlParameter> param = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@nf", Value = nota }
            };

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        requisicoes.Add(new RequisicaoObra()
                        {
                            RequisicaoId = int.Parse(dataReader["ID_REQ"].ToString()),
                            Obra = dataReader["OBRA"].ToString(),
                            Total = float.Parse(dataReader["RATEIO"].ToString()),
                            Projeto = dataReader["PROJETO"].ToString(),
                            Titulo = dataReader["DESCRICAO"].ToString()
                        });
                    }
                }
            }

            return requisicoes;
        }

        public bool VerificarExistenciaNotaFornecedor(string nota, string documento)
        {
            StringBuilder query = new StringBuilder(@"SELECT RO.ID FROM REQUISICAO RQ JOIN ");

            query.Append(ProdEnvironment() ? "[Protheus]" : "[Protheus_Dev]");
            query.Append(".dbo.SA2010 pt on pt.A2_COD COLLATE Latin1_General_CI_AS = rq.FORNECEDOR COLLATE Latin1_General_CI_AS and rq.LOJA COLLATE Latin1_General_CI_AS = pt.A2_LOJA COLLATE Latin1_General_CI_AS WHERE pt.A2_CGC = @documento AND RQ.ID = @nf");

            List<SqlParameter> param = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@nf", Value = nota.Trim() },
                  new SqlParameter() { ParameterName = "@doc", Value = documento.Trim() }
            };

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int AprovarSupervisorRequisicao(int id, string supervisor, int idAprovador)
        {
            string updateSql = @"UPDATE REQUISICAO SET APROVADO_SUPERVISOR = 'S', SUPERVISOR = @idAprovador, DATA_APROV_SUPERVISOR = @dataAprovacao
                                WHERE id = @id AND EXCLUIDO = 'N'";

            List<DbParameter> param = new List<DbParameter>()
            {
                  new SqlParameter() { ParameterName = "@id", Value = id },
                  new SqlParameter() { ParameterName = "@supervisor", Value = supervisor },
                  new SqlParameter() { ParameterName = "@dataAprovacao", Value = DateTime.Now },
                  new SqlParameter() { ParameterName = "@idAprovador", Value = idAprovador }
            };

            return ExecuteNonQuery(updateSql, param, CommandType.Text);
        }

        public int AprovarGerenteRequisicao(int id, string gerente, int idAprovador)
        {
            string updateSql = @"UPDATE REQUISICAO SET APROVADO_GERENTE = 'S', GERENTE = @idAprovador, DATA_APROV_GERENTE = @dataAprovacao
                                WHERE id = @id AND EXCLUIDO = 'N'";

            List<DbParameter> param = new List<DbParameter>()
            {
                  new SqlParameter() { ParameterName = "@id", Value = id },
                  new SqlParameter() { ParameterName = "@gerente", Value = gerente },
                  new SqlParameter() { ParameterName = "@dataAprovacao", Value = DateTime.Now },
                  new SqlParameter() { ParameterName = "@idAprovador", Value = idAprovador }
            };

            return ExecuteNonQuery(updateSql, param, CommandType.Text);
        }

        public int ReprovarSupervisorRequisicao(int id, string supervisor, int idAprovador)
        {
            string updateSql = @"UPDATE REQUISICAO SET APROVADO_SUPERVISOR = 'R', SUPERVISOR = @idAprovador, DATA_APROV_SUPERVISOR = @dataAprovacao
                                WHERE id = @id AND EXCLUIDO = 'N'";

            List<DbParameter> param = new List<DbParameter>()
            {
                  new SqlParameter() { ParameterName = "@id", Value = id },
                  new SqlParameter() { ParameterName = "@supervisor", Value = supervisor },
                  new SqlParameter() { ParameterName = "@dataAprovacao", Value = DateTime.Now },
                  new SqlParameter() { ParameterName = "@idAprovador", Value = idAprovador }
            };

            return ExecuteNonQuery(updateSql, param, CommandType.Text);
        }

        public int ReprovarGerenteRequisicao(int id, string gerente, int idAprovador)
        {
            string updateSql = @"UPDATE REQUISICAO SET APROVADO_GERENTE = 'R', GERENTE = @idAprovador, DATA_APROV_GERENTE = @dataAprovacao
                                WHERE id = @id AND EXCLUIDO = 'N'";

            List<DbParameter> param = new List<DbParameter>()
            {
                  new SqlParameter() { ParameterName = "@id", Value = id },
                  new SqlParameter() { ParameterName = "@gerente", Value = gerente },
                  new SqlParameter() { ParameterName = "@dataAprovacao", Value = DateTime.Now },
                  new SqlParameter() { ParameterName = "@idAprovador", Value = idAprovador }
            };

            return ExecuteNonQuery(updateSql, param, CommandType.Text);
        }

        public int InsertRequisicao(RequisicaoGerada requisicao, bool elvis)
        {
            return InserirRequisicao(requisicao, elvis);
        }

        public void UpdateRequisicao(RequisicaoAtualizada requisicao)
        {
            AtualizarRequisicao(requisicao);
        }

        public void DeleteParcelasRequisicao(string requisicaoID)
        {
            ExcluirParcelasRequisicao(requisicaoID);
        }

        public int InsertRequisicaoObra(int requisicao, string obra, decimal valor)
        {
            return InserirRequisicaoObra(requisicao, obra, valor);
        }

        public int InsertRequisicaoServico(int requisicao, string servico)
        {
            return InserirRequisicaoServico(requisicao, servico);
        }

        public List<ParcelaRequisicao> ObterParcelasRequisicao(string requisicaoId)
        {
            return ListarParcelasRequisicao(requisicaoId);
        }

        public int InsertParcelaRequisicao(string requisicaoID, ParcelaRequisicao parcela)
        {
            return InserirParcelaRequisicao(requisicaoID, parcela);
        }

        public int AtualizarRequisicao(int id, string link)
        {
            string updateSql = @"UPDATE REQUISICAO SET DOC_LINK = @link WHERE id = @id AND EXCLUIDO = 'N'";

            List<DbParameter> param = new List<DbParameter>()
            {
                  new SqlParameter() { ParameterName = "@id", Value = id },
                  new SqlParameter() { ParameterName = "@link", Value = link }
            };

            return ExecuteNonQuery(updateSql, param, CommandType.Text);
        }

        public int ExcluirRequisicao(int id, string nomeUsuario, string descricao)
        {
            string updateSql = @"UPDATE REQUISICAO SET EXCLUIDOR = @nomeUsuario, EXCLUIDO = 'S', DATA_EXCLUSAO = @dataExclusao, OBSD = @descricao
                                WHERE id = @id AND EXCLUIDO = 'N'";
            List<DbParameter> param = new List<DbParameter>()
            {
                  new SqlParameter() { ParameterName = "@id", Value = id },
                  new SqlParameter() { ParameterName = "@nomeUsuario", Value = nomeUsuario },
                  new SqlParameter() { ParameterName = "@dataExclusao", Value = DateTime.Now },
                  new SqlParameter() { ParameterName = "@descricao", Value = descricao }
            };

            return ExecuteNonQuery(updateSql, param, CommandType.Text);
        }

        public List<CentroCusto> ObterCentrosDeCusto()
        {
            string protheusAmbiente = ProdEnvironment() ? "Protheus" : "Protheus_Dev";
            List<CentroCusto> centrosCusto = new List<CentroCusto>();

            StringBuilder query = new StringBuilder(@"SELECT CT.CTT_CUSTO, CT.CTT_DESC01 FROM " + protheusAmbiente + ".dbo.CTT010 CT WHERE CT.CTT_BLOQ <> 1 AND CT.D_E_L_E_T_ =  ' '");

            using (DbDataReader dataReader = GetDataReader(query.ToString(), null, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        centrosCusto.Add(new CentroCusto()
                        {
                            Id = int.Parse(dataReader["CTT_CUSTO"].ToString()),
                            Nome = dataReader["CTT_DESC01"].ToString().Trim()
                        });
                    }
                }
            }

            return centrosCusto;
        }

        public List<Obra> ObterObras(string Nome)
        {
            List<Obra> obras = new List<Obra>();

            if (Nome.Length > 1)
            {
                StringBuilder query = new StringBuilder(@"SELECT DISTINCT
                                                            [L].[CODIGO] AS OBRA,
                                                            [L].[OBRA] AS DESCRICAO,
                                                            [L].[PROJETO],
                                                            ISNULL([RO].[RATEIO], 0) AS RATEIO
                                                          FROM
                                                            [EBSA].[dbo].[LIVRO] [L]
                                                          INNER JOIN
                                                            [EBSA].[dbo].[ENVIO_OBRA_ELVIS] [EOE]
                                                          ON
                                                            [L].[CODIGO] = [EOE].[OBRA]
                                                          LEFT JOIN
                                                            [EBSA].[dbo].[REQUISICAO_OBRA] [RO]
                                                          ON
                                                            [EOE].[OBRA] = [RO].[OBRA]
                                                          WHERE
                                                            [L].[CODIGO] LIKE '%" + Nome + "%' OR [L].[OBRA] LIKE '%" + Nome + "%'");

                using (DbDataReader dataReader = GetDataReader(query.ToString(), null, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            obras.Add(new Obra()
                            {
                                Codigo = dataReader["OBRA"].ToString(),
                                Descricao = dataReader["DESCRICAO"].ToString().Trim(),
                                Projeto = dataReader["PROJETO"].ToString().Trim(),
                                Rateio = float.Parse(dataReader["RATEIO"].ToString().Trim())
                            });
                        }
                    }
                }
            }
            return obras;
        }

        public void CancelarRequisicao(int idRequisicao, int idUsuario)
        {
            string updateSql = @"UPDATE REQUISICAO 
                                SET CANCELADA = 1,
                                DATA_CANCELAMENTO = GETDATE(),
                                USER_CANCELAMENTO = @idUsuario
                                WHERE id = @idRequisicao";

            List<DbParameter> param = new List<DbParameter>()
            {
                  new SqlParameter() { ParameterName = "@idUsuario", Value = idUsuario },
                  new SqlParameter() { ParameterName = "@idRequisicao", Value = idRequisicao },
            };

            ExecuteNonQuery(updateSql, param, CommandType.Text);
        }
    }
}
