using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CoBRA.Domain.Interfaces;
using CoBRA.Domain.Entities;
using static CoBRA.Domain.Enum.Enum;
using Microsoft.Extensions.Configuration;

namespace CoBRA.Infra.Intranet
{
    public class RelatorioRepository : BaseRepository, IRelatorioRepository
    {
        public RelatorioRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<Comissao> Comissao(int? qntMes, int idEstado, int idUsuario, int idHierarquia, string codDivulgador, DateTime? dataInicio = null, DateTime? dataFim = null)
        {

            StringBuilder query = new StringBuilder(@"SELECT		
			 DIVULGADOR
			,ESTADO
			,NOME_DIVULGADOR
            ,(SELECT SUM(VL_NF) from(
                SELECT 
	            DIVULGADOR, VL_NF FROM REL_COMISSAO WHERE DIVULGADOR = rl.divulgador
                GROUP BY DIVULGADOR, VL_NF) VENDA
			 ) VL_NF
            ,SUM(VL_RECEBIDO) as 'VL_RECEBIDO' 
			,SUM(VL_A_RECEBER) as 'VL_A_RECEBER' 
			,SUM(PAGO_COMISSAO) as 'PAGO_COMISSAO'
            ,SUM(A_RECEBER_COMISSAO) as 'A_RECEBER_COMISSAO'
			--,rl.MICRO_REGIAO 
			
			FROM REL_COMISSAO rl");

            List<SqlParameter> listaParametros = new List<SqlParameter>();
            if (idHierarquia == (int)Hierarquia.Diretoria)
            {
                query.Append(@" INNER JOIN EBSA..USUARIO us ON us.COD_USUARIO COLLATE Latin1_General_CI_AS = rl.DIVULGADOR COLLATE Latin1_General_CI_AS");
            }
            if (idHierarquia == (int)Hierarquia.GerenteComercial)
            {
                query.Append(@" INNER JOIN EBSA..GERENTE_SUPERVISOR_REGIAO gs ON gs.ID_USUARIO_GERENTE = @idgerente
                             INNER JOIN EBSA..SUPERVISOR_DIVULGADOR sd ON sd.ID_USUARIO_SUPERVISOR = gs.ID_USUARIO_SUPERVISOR
                           INNER JOIN EBSA..USUARIO us ON us.ID_USUARIO = sd.ID_USUARIO_DIVULGADOR AND rl.DIVULGADOR COLLATE Latin1_General_CI_AS = us.COD_USUARIO COLLATE Latin1_General_CI_AS");

                listaParametros.Add(new SqlParameter() { ParameterName = "@idgerente", Value = idUsuario });
            }
            else if (idHierarquia == (int)Hierarquia.SupervisorComercial)
            {
                query.Append(@" INNER JOIN EBSA..SUPERVISOR_DIVULGADOR sd ON sd.ID_USUARIO_SUPERVISOR = @idsupervisor
                       INNER JOIN EBSA..USUARIO us ON us.ID_USUARIO = sd.ID_USUARIO_DIVULGADOR AND rl.DIVULGADOR COLLATE Latin1_General_CI_AS = us.COD_USUARIO COLLATE Latin1_General_CI_AS");

                listaParametros.Add(new SqlParameter() { ParameterName = "@idsupervisor", Value = idUsuario });
            }

            if (idEstado > 0)
            {
                listaParametros.Add(new SqlParameter() { ParameterName = "@estado", Value = idEstado });
                query.Append(" INNER JOIN EBSA..ESTADO es ON UPPER(es.Sigla) COLLATE Latin1_General_CI_AS = UPPER(rl.ESTADO) AND es.ID_ESTADO = @estado");
            }

            if (qntMes != null)
            {
                query.Append(" WHERE DATA_PG_RECEBIDO >= Convert(DATE, DATEADD(month, DATEDIFF(month, 0, DATEADD(month, @qntMes, GETDATE())), 0))");
                listaParametros.Add(new SqlParameter() { ParameterName = "@qntMes", Value = -qntMes });
            }
            else
            {
                query.Append("  WHERE Convert(DATE, DATA_PG_RECEBIDO, 108) BETWEEN @dataini and @dataFim");
                listaParametros.Add(new SqlParameter() { ParameterName = "@dataini", Value = dataInicio });
                listaParametros.Add(new SqlParameter() { ParameterName = "@dataFim", Value = dataFim });
            }

            if (!String.IsNullOrEmpty(codDivulgador))
            {
                listaParametros.Add(new SqlParameter() { ParameterName = "@divulgador", Value = codDivulgador });
                query.Append(" AND DIVULGADOR = @divulgador");
            }

            query.Append(" GROUP BY DIVULGADOR, NOME_DIVULGADOR, ESTADO ORDER BY DIVULGADOR, NOME_DIVULGADOR");

            List<Comissao> listaComissao = new List<Comissao>();

            using (SqlDataReader dataReader = base.GetDataReader(query.ToString(), listaParametros, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        listaComissao.Add(new Comissao()
                        {
                            CodDivulgador = dataReader["DIVULGADOR"].ToString(),
                            NomeDivulgador = dataReader["NOME_DIVULGADOR"].ToString(),
                            Estado = dataReader["ESTADO"].ToString(),
                            ValorNF = Convert.ToDecimal(dataReader["VL_NF"]),
                            ValorRecebido = dataReader["VL_RECEBIDO"] != DBNull.Value ? Convert.ToDecimal(dataReader["VL_RECEBIDO"]) : 0,
                            ValorReceber = dataReader["VL_A_RECEBER"] != DBNull.Value ? Convert.ToDecimal(dataReader["VL_A_RECEBER"]) : 0,
                            PagoComissao = dataReader["PAGO_COMISSAO"] != DBNull.Value ? Convert.ToDecimal(dataReader["PAGO_COMISSAO"]) : 0,
                            AReceberComissao = dataReader["A_RECEBER_COMISSAO"] != DBNull.Value ? Convert.ToDecimal(dataReader["A_RECEBER_COMISSAO"]) : 0,
                        });
                    }
                }
            }
            return listaComissao;
        }

        public List<Comissao> ComissaoDetalhes(int? qntMes, int idEstado, string divulgador, DateTime? dataInicio = null, DateTime? dataFim = null)
        {
            StringBuilder query = new StringBuilder(@" SELECT DIVULGADOR,NOME_DIVULGADOR, CLIENTE, ESTADO, FILIAL, CEP_CLIENTE 
                                                   ,COD_PROMOCIONAL ,NF ,EMISSAO ,DATA_PG_RECEBIDO ,TIPO_VENDA ,FORMTT ,VL_NF, PARCELA, (RATEIO * 100) as RT, TRANSFERE, VL_RECEBIDO ,VL_A_RECEBER ,PAGO_COMISSAO 
                                                   ,A_RECEBER_COMISSAO ,RATEIO, MICRO_REGIAO FROM REL_COMISSAO");
            
            List<SqlParameter> listaParametros = new List<SqlParameter>() {
                    new SqlParameter() { ParameterName = "@divulgador", Value = divulgador }
            };

            if (qntMes != null)
            {
                query.Append(" WHERE DATA_PG_RECEBIDO >= Convert(DATE, DATEADD(month, DATEDIFF(month, 0, DATEADD(month, @qntMes, GETDATE())), 0))");
                listaParametros.Add(new SqlParameter() { ParameterName = "@qntMes", Value = -qntMes });
            }
            else
            {
                query.Append("  WHERE Convert(DATE,DATA_PG_RECEBIDO,108) BETWEEN @dataIni and @dataFim");
                listaParametros.Add(new SqlParameter() { ParameterName = "@dataIni", Value = dataInicio });
                listaParametros.Add(new SqlParameter() { ParameterName = "@dataFim", Value = dataFim });
            }

            query.Append(" AND DIVULGADOR = @divulgador ORDER BY EMISSAO");

            List<Comissao> listaComissao = new List<Comissao>();
            try
            {
                using (SqlDataReader dataReader = base.GetDataReader(query.ToString(), listaParametros, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            listaComissao.Add(new Comissao()
                            {
                                CodDivulgador = dataReader["DIVULGADOR"].ToString(),
                                NomeDivulgador = dataReader["NOME_DIVULGADOR"].ToString(),
                                Cliente = dataReader["CLIENTE"].ToString(),
                                Estado = dataReader["ESTADO"].ToString(),
                                Filial = dataReader["FILIAL"].ToString(),
                                Cep = dataReader["CEP_CLIENTE"].ToString(),
                                CodPromocional = dataReader["COD_PROMOCIONAL"].ToString(),
                                NF = dataReader["NF"].ToString(),
                                Emissao = Convert.ToDateTime(dataReader["EMISSAO"]),
                                DataPagamento = Convert.ToDateTime(dataReader["DATA_PG_RECEBIDO"]),
                                TipoVenda = dataReader["TIPO_VENDA"].ToString(),
                                Formato = dataReader["FORMTT"].ToString(),
                                ValorNF = Convert.ToDecimal(dataReader["VL_NF"]),
                                ValorRecebido = dataReader["VL_RECEBIDO"] != DBNull.Value ? Convert.ToDecimal(dataReader["VL_RECEBIDO"]) : 0,
                                ValorReceber = dataReader["VL_A_RECEBER"] != DBNull.Value ? Convert.ToDecimal(dataReader["VL_A_RECEBER"]) : 0,
                                PagoComissao = dataReader["PAGO_COMISSAO"] != DBNull.Value ? Convert.ToDecimal(dataReader["PAGO_COMISSAO"]) : 0,
                                AReceberComissao = dataReader["A_RECEBER_COMISSAO"] != DBNull.Value ? Convert.ToDecimal(dataReader["A_RECEBER_COMISSAO"]) : 0,
                                Rateio = dataReader["RT"].ToString(),
                                Parcela = dataReader["PARCELA"].ToString(),
                                MicroRegiao = dataReader["MICRO_REGIAO"].ToString(),
                                Transferencia = dataReader["TRANSFERE"].ToString()

                            });
                        }
                    }
                }
                return listaComissao;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
