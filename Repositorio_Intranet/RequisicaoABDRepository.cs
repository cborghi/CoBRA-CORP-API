using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using CoBRA.Domain.Interfaces;
using CoBRA.Domain.Entities;
using Microsoft.Extensions.Configuration;
using CoBRA.Infra.Intranet.Base;

namespace CoBRA.Infra.Intranet
{
    public class RequisicaoABDRepository : BaseRepositoryRequisicaoElvis, IRequisicaoABDRepository
    {
        public RequisicaoABDRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<RequisicaoElvis> ObterRequisicoes(string tipo)
        {
            List<RequisicaoElvis> requisicoes = new List<RequisicaoElvis>();

            List<SqlParameter> param = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@tipo", Value = tipo }
            };

            StringBuilder query = new StringBuilder(@"SELECT EOE.ID_ELVIS,EOE.NF,EOE.TIPO,EOE.VALOR,L.OBRA AS TITULO_OBRA FROM ENVIO_OBRA_ELVIS EOE
                                LEFT JOIN LIVRO L ON EOE.OBRA = L.CODIGO
                                WHERE (EOE.REQUISICAO IS NULL OR EOE.REQUISICAO = 0) AND EOE.TIPO LIKE @tipo + '%'");

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        requisicoes.Add(new RequisicaoElvis()
                        {
                            Id = dataReader["ID_ELVIS"].ToString(),
                            Nota = dataReader["NF"].ToString(),
                            Tipo = dataReader["TIPO"].ToString(),
                            Valor = decimal.Parse(dataReader["Valor"].ToString()),
                            Titulo = dataReader["TITULO_OBRA"].ToString()
                        });
                    }
                }
            }

            return requisicoes;
        }

        public List<RequisicaoElvis> ObterRequisicoesPorID(int ID)
        {
            List<RequisicaoElvis> requisicoes = new List<RequisicaoElvis>();

            List<SqlParameter> param = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@ID", Value = ID }
            };

            StringBuilder query = new StringBuilder(@"SELECT DISTINCT eoe.ID_ELVIS
			                                                          , eoe.NF
			                                                          , eoe.TIPO
			                                                          , eoe.VALOR
                                                                      , eoe.NOME_ARQUIVO
                                                        FROM ENVIO_OBRA_ELVIS eoe
                                                        LEFT JOIN REQUISICAO_OBRA ro
	                                                        ON eoe.OBRA = ro.OBRA
                                                        LEFT JOIN REQUISICAO r
	                                                        ON ro.ID_REQ = R.ID
                                                        LEFT JOIN REQUISICAO_SERVICO rs
	                                                        ON r.ID = rs.ID_REQ
                                                        LEFT JOIN FASE_SERVICO fs
	                                                        ON rs.SERVICO = fs.TIPO_SERVICO
                                                        INNER JOIN GRUPO G 
	                                                        ON G.ID_GRUPO = @ID
                                                        INNER JOIN Protheus_Dev.dbo.CTT010 AS CT 
	                                                        ON CT.CTT_DESC01 COLLATE Latin1_General_CI_AS = G.DEPARTAMENTO COLLATE Latin1_General_CI_AS
                                                        WHERE UPPER(LTRIM(RTRIM(eoe.TIPO))) LIKE UPPER(LTRIM(RTRIM(G.DEPARTAMENTO))) + '%'
                                                            AND (eoe.REQUISICAO IS NULL OR eoe.REQUISICAO = 0)
                                                        ORDER BY NF");

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        requisicoes.Add(new RequisicaoElvis()
                        {
                            Id = dataReader["ID_ELVIS"].ToString(),
                            Nota = dataReader["NF"].ToString(),
                            Tipo = dataReader["TIPO"].ToString(),
                            Valor = decimal.Parse(dataReader["VALOR"].ToString()),
                            Fornecedor = dataReader["NOME_ARQUIVO"].ToString().Trim(),
                        });
                    }
                }
            }

            return requisicoes;
        }

        public List<RequisicaoElvis> ObterRequisicaoPorNota(string nota)
        {
            List<RequisicaoElvis> requisicoes = new List<RequisicaoElvis>();

            StringBuilder query = new StringBuilder(@"SELECT OB.ID_ELVIS, OB.ENV_CONTADOR, OB.NF, OB.VENCIMENTO, OB.TEXTO, OB.TIPO, OB.OBRA AS CODIGO, OB.VALOR, L.OBRA, L.PROJETO FROM ENVIO_OBRA_ELVIS as OB
                                LEFT JOIN LIVRO as L on L.CODIGO = OB.OBRA
                                WHERE (REQUISICAO IS NULL OR REQUISICAO = 0) and NF = @nf");

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
                        requisicoes.Add(new RequisicaoElvis()
                        {
                            Id = dataReader["ID_ELVIS"].ToString(),
                            Nota = dataReader["NF"].ToString(),
                            Tipo = dataReader["TIPO"].ToString(),
                            Obra = dataReader["CODIGO"].ToString(),
                            Imagem = dataReader["TEXTO"].ToString(),
                            Contador = int.Parse(dataReader["ENV_CONTADOR"].ToString()),
                            Titulo = dataReader["OBRA"].ToString(),
                            Valor = decimal.Parse(dataReader["VALOR"].ToString()),
                            Vencimento = DateTime.Parse(dataReader["VENCIMENTO"].ToString()),
                            Projeto = dataReader["PROJETO"].ToString()
                        });
                    }
                }
            }

            return requisicoes;
        }

        public List<RequisicaoElvis> ObterRequisicaoGeradaPorNota(string nota)
        {
            List<RequisicaoElvis> requisicoes = new List<RequisicaoElvis>();

            StringBuilder query = new StringBuilder(@"SELECT OB.ID_ELVIS, OB.ENV_CONTADOR, OB.NF, OB.VENCIMENTO, OB.TEXTO, OB.TIPO, OB.OBRA AS CODIGO, OB.VALOR, L.PROJETO, L.OBRA FROM ENVIO_OBRA_ELVIS as OB
                                LEFT JOIN LIVRO as L on L.CODIGO = OB.OBRA
                                WHERE OB.NF = @nf AND NF <> 'termolaerte'");

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
                        requisicoes.Add(new RequisicaoElvis()
                        {
                            Id = dataReader["ID_ELVIS"].ToString(),
                            Nota = dataReader["NF"].ToString(),
                            Tipo = dataReader["TIPO"].ToString(),
                            Obra = dataReader["CODIGO"].ToString(),
                            Projeto = dataReader["PROJETO"].ToString(),
                            Imagem = dataReader["TEXTO"].ToString(),
                            Contador = int.Parse(dataReader["ENV_CONTADOR"].ToString()),
                            Titulo = dataReader["OBRA"].ToString(),
                            Valor = decimal.Parse(dataReader["VALOR"].ToString()),
                            Vencimento = DateTime.Parse(dataReader["VENCIMENTO"].ToString())
                        });
                    }
                }
            }

            return requisicoes;
        }

        public int InsertRequisicaoServico(int requisicao, string servico)
        {
            return InserirRequisicaoServico(requisicao, servico);
        }

        public int UpdateRequisicaoObraElvis(int contador, int requisicao, int usuarioId)
        {
            string updateSql = @"UPDATE ENVIO_OBRA_ELVIS SET REQ_GERADA = 'S', REQ_DATA = @emissao,  REQ_INCLUIDOR = @usuario, REQUISICAO = @requisicao WHERE
                                ENV_CONTADOR = @contador";

            List<DbParameter> param = new List<DbParameter>()
            {
                  new SqlParameter() { ParameterName = "@emissao", Value = DateTime.Now },
                  new SqlParameter() { ParameterName = "@usuario", Value = usuarioId},
                  new SqlParameter() { ParameterName = "@requisicao", Value = requisicao},
                  new SqlParameter() { ParameterName = "@contador", Value = contador},
            };

            return ExecuteNonQuery(updateSql, param, CommandType.Text);
        }

        public int InsertRequisicao(RequisicaoGerada requisicao, bool elvis)
        {
            return InserirRequisicao(requisicao, elvis);
        }

        public int InsertRequisicaoObra(int requisicao, string obra, decimal valor)
        {
            return InserirRequisicaoObra(requisicao, obra, valor);
        }

        public int UpdateRequisicaoObra(int requisicao, string obra, decimal valor)
        {
            return AlterarRequisicaoObra(requisicao, obra, valor);
        }

        public void ExcluirRequisicaoObra(int requisicao)
        {
            ExcluiRequisicaoObra(requisicao);
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
    }
}
