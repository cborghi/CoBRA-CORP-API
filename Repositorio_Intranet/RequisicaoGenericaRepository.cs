using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using CoBRA.Domain.Interfaces;
using CoBRA.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace CoBRA.Infra.Intranet
{
    public class RequisicaoGenericaRepository : BaseRepository, IRequisicaoGenericaRepository
    {
        public RequisicaoGenericaRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<RequisicaoGenerica> ObterRequisicoes()
        {
            List<RequisicaoGenerica> requisicoes = new List<RequisicaoGenerica>();

            StringBuilder query = new StringBuilder(@"SELECT
                                                        REC.ID,
                                                        REC.TIPODOC,
                                                        REC.DOC,
                                                        REC.FORNECEDOR,
                                                        REC.FORMA_PAGTO,
                                                        REC.DESC_PAG,
                                                        REC.PRAZO_PAGTO,
                                                        REC.OBS, REC.VALOR,
                                                        REC.INCLUIDOR,
                                                        REC.INCLUIDO
                                                      FROM REQUISICAO REC
                                                      INNER JOIN
                                                           REQUISICAO_GENERICA RG
                                                      ON REC.ID = RG.REQUISICAO");

            using (DbDataReader dataReader = GetDataReader(query.ToString(), null, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        requisicoes.Add(new RequisicaoGenerica()
                        {
                            Id = int.Parse(dataReader["ID"].ToString()),
                            TipoDoc = dataReader["TIPODOC"].ToString(),
                            Doc = dataReader["DOC"].ToString(),
                            Fornecedor = dataReader["FORNECEDOR"].ToString(),
                            FormaPagto = dataReader["FORMA_PAGTO"].ToString(),
                            DescPagto = dataReader["DESC_PAG"].ToString(),
                            PrazoPagto = DateTime.Parse(dataReader["PRAZO_PAGTO"].ToString()),
                            Obs = dataReader["OBS"].ToString(),
                            Valor = decimal.Parse(dataReader["VALOR"].ToString()),
                            Incluidor = dataReader["INCLUIDOR"].ToString(),
                            Incluido = DateTime.Parse(dataReader["INCLUIDO"].ToString())
                        });
                    }
                }
            }

            return requisicoes;
        }

        public int InserirRequisicao(RequisicaoGenerica requisicao)
        {
            string updateSql = @"";

            List<DbParameter> param = new List<DbParameter>()
            {
                  new SqlParameter() { ParameterName = "@requisicao", Value = requisicao }
            };

            return ExecuteNonQuery(updateSql, param, CommandType.Text);
        }
    }
}
