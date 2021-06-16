using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;

using Microsoft.Extensions.Configuration;

using static CoBRA.Domain.Enum.Enum;
using CoBRA.Domain.Interfaces;
using CoBRA.Domain.Entities;


namespace CoBRA.Infra.Protheus {
    public class RelatorioProtheusRepository : BaseRepository, IRelatorioProtheusRepository {

        public RelatorioProtheusRepository(IConfiguration configuration) : base(configuration) {

        }

        public List<Comissao> Comissao(int? qntMes, int idEstado, int idUsuario, int idHierarquia, string codDivulgador, DateTime? dataInicio = null, DateTime? dataFim = null) { 

            List<Comissao> lComissao = new List<Comissao>();

            StringBuilder _query = new StringBuilder(@"
                            SELECT  [dbo].[ZC1010].[ZC1_CODDIV],
                                    [dbo].[SA2010].[A2_EST],
                                    [dbo].[ZC1010].[ZC1_NOMDIV],
                                    SUM([dbo].[ZC1010].[ZC1_TOTNF]) [ZC1_TOTNF],
                                    SUM([dbo].[ZC1010].[ZC1_VLRMVB]) [ZC1_VLRMVB],
                                    SUM([dbo].[ZC1010].[ZC1_VLCODI]) [ZC1_VLCODI]
                            FROM [dbo].[ZC1010]
                            LEFT JOIN (SELECT [ZC0010].[ZC0_NUMCOM],
		                            [ZC0010].[ZC0_DTPROC] DATAZ
		                            FROM [dbo].[ZC0010]
		                            WHERE D_E_L_E_T_ <> '*') AS ZC0 ON [ZC0].[ZC0_NUMCOM] = [ZC1_NUMCOM]
                            LEFT JOIN [SA2010] ON [SA2010].[A2_COD] = [ZC1010].[ZC1_CODDIV]
");

            List<SqlParameter> param = new List<SqlParameter>();

            if (idHierarquia == (int)Hierarquia.Diretoria) {
                _query.Append(@" INNER JOIN [EBSA].[dbo].[USUARIO] ON [EBSA].[dbo].[USUARIO].[COD_USUARIO] COLLATE Latin1_General_CI_AS = [dbo].[ZC1010].[ZC1_CODDIV] COLLATE Latin1_General_CI_AS");
            } 
            
            if (idHierarquia == (int)Hierarquia.GerenteComercial) {
                _query.Append(@" INNER JOIN [EBSA].[dbo].[GERENTE_SUPERVISOR_REGIAO] ON [EBSA].[dbo].[GERENTE_SUPERVISOR_REGIAO].[ID_USUARIO_GERENTE] = @idgerente
                                 INNER JOIN [EBSA].[dbo].[SUPERVISOR_DIVULGADOR] ON [EBSA].[dbo].[SUPERVISOR_DIVULGADOR].[ID_USUARIO_SUPERVISOR] = [EBSA].[dbo].[GERENTE_SUPERVISOR_REGIAO].[ID_USUARIO_SUPERVISOR]
                                 INNER JOIN [EBSA].[dbo].[USUARIO] ON [EBSA].[dbo].[USUARIO].[ID_USUARIO] = [EBSA].[dbo].[SUPERVISOR_DIVULGADOR].[ID_USUARIO_DIVULGADOR] AND [dbo].[ZC1010].[ZC1_CODDIV] COLLATE Latin1_General_CI_AS = [EBSA].[dbo].[USUARIO].[COD_USUARIO] COLLATE Latin1_General_CI_AS");

                param.Add(new SqlParameter() { ParameterName = "@idgerente", Value = idUsuario });
            } else if (idHierarquia == (int)Hierarquia.SupervisorComercial) {

                _query.Append(@" INNER JOIN [EBSA].[dbo].[SUPERVISOR_DIVULGADOR] ON [EBSA].[dbo].[SUPERVISOR_DIVULGADOR].[ID_USUARIO_SUPERVISOR] = @idsupervisor
                                 INNER JOIN [EBSA].[dbo].[USUARIO] ON [EBSA].[dbo].[USUARIO].[ID_USUARIO] = [EBSA].[dbo].[SUPERVISOR_DIVULGADOR].[ID_USUARIO_DIVULGADOR] AND [dbo].[ZC1010].[ZC1_CODDIV] COLLATE Latin1_General_CI_AS = [EBSA].[dbo].[USUARIO].[COD_USUARIO]");

                param.Add(new SqlParameter() { ParameterName = "@idsupervisor", Value = idUsuario });
            }

            if (idEstado > 0) {
                _query.Append(@" INNER JOIN [EBSA].[dbo].[ESTADO] ON UPPER([EBSA].[dbo].[ESTADO].[Sigla]) COLLATE Latin1_General_CI_AS = UPPER([dbo].[ZC1010].[ZC1_EST]) AND [EBSA].[dbo].[ESTADO].[ID_ESTADO] = @estado");
                
                param.Add(new SqlParameter() { ParameterName = "@estado", Value = idEstado });
            }

            if (qntMes != null) {
                _query.Append(@" WHERE CONVERT(DATE, DATAZ, 112) >= Convert(DATE, DATEADD(month, DATEDIFF(month, 0, DATEADD(month, @qntMes, GETDATE())), 0))");


                param.Add(new SqlParameter() { ParameterName = "@qntMes", Value = -qntMes });
            } else {
                _query.Append(@" WHERE CONVERT(DATE, DATAZ, 112) BETWEEN @dataIni AND @dataFim");

                param.Add(new SqlParameter() { ParameterName = "@dataIni", Value = dataInicio });
                param.Add(new SqlParameter() { ParameterName = "@dataFim", Value = dataFim });
            }

            if (!String.IsNullOrEmpty(codDivulgador)) {
                _query.Append(@" AND [dbo].[ZC1010].[ZC1_CODDIV] = @divulgador");

                param.Add(new SqlParameter() { ParameterName = "@divulgador", Value = codDivulgador});
            }

            _query.Append(@" AND [ZC1010].[D_E_L_E_T_] <> '*' AND [ZC1010].[ZC1_STATUS] NOT IN (' ', '1', '5')");

            _query.Append(@" GROUP BY [dbo].[ZC1010].[ZC1_CODDIV], 
                                      [dbo].[ZC1010].[ZC1_NOMDIV], 
                                      [dbo].[SA2010].[A2_EST]
                             ORDER BY [dbo].[ZC1010].[ZC1_CODDIV], 
                                      [dbo].[ZC1010].[ZC1_NOMDIV]");


            using (SqlDataReader reader = GetDataReader(_query.ToString(), param, CommandType.Text)) {

                if (reader != null && reader.HasRows) {

                    while (reader.Read()) {
                        lComissao.Add(new Comissao() {
                            CodDivulgador = reader["ZC1_CODDIV"].ToString(),
                            Estado = reader["A2_EST"].ToString(),
                            NomeDivulgador = reader["ZC1_NOMDIV"].ToString(),
                            ValorNF = Convert.ToDecimal(reader["ZC1_TOTNF"]),
                            ValorRecebido = reader["ZC1_VLRMVB"] != DBNull.Value ? Convert.ToDecimal(reader["ZC1_VLRMVB"]) : 0,
                            AReceberComissao = reader["ZC1_VLCODI"] != DBNull.Value ? Convert.ToDecimal(reader["ZC1_VLCODI"]) : 0
                        });

                    }
                }

            }

            return lComissao;
        }

        public List<Comissao> ComissaoDetalhes(int? qntMes, int idEstado, string divulgador, DateTime? dataInicio = null, DateTime? dataFim = null) {
            
            List<Comissao> lComissao = new List<Comissao>();

            StringBuilder _query = new StringBuilder(@"SELECT [ZC1010].[ZC1_NUMCOM],
                                                            [ZC1010].[ZC1_CODDIV],
		                                                    [ZC1010].[ZC1_NOMDIV], 
		                                                    [ZC1010].[ZC1_NOMCLI], 
		                                                    [ZC1010].[ZC1_EST], 
		                                                    [ZC1010].[ZC1_FILDOC], 
		                                                    [ZC1010].[ZC1_CEP], 
		                                                    [ZC1010].[ZC1_DOC],
		                                                    CONVERT(DATE, [ZC1010].[ZC1_EMITIT], 0) ZC1_EMITIT,
		                                                    CONVERT(DATE, [ZC1010].[ZC1_VENCRE], 0) ZC1_VENCRE, 
		                                                    --CONVERT(DATE , [SE1010].[E1_BAIXA], 0) E1_BAIXA,
		                                                    [ZC1010].[ZC1_TPVEND], 
		                                                    [ZC1010].[ZC1_TOTNF], 
		                                                    [ZC1010].[ZC1_PARCEL], 
		                                                    [ZC1010].[ZC1_VLRMVB], 
		                                                    [ZC1010].[ZC1_VLCODI],
		                                                    [ZC1010].[ZC1_DESCPG],
		                                                    CONVERT(DATE,DATAZ,112) DATAZ,
		                                                    (SELECT [SC5010].[C5_CODPROM] 
			                                                    FROM [dbo].[SC5010] 
			                                                    WHERE [SC5010].[C5_NUM] = [ZC1010].[ZC1_PEDIDO] 
			                                                    AND [SC5010].[C5_FILIAL] = [ZC1010].[ZC1_FILDOC] 
			                                                    AND [SC5010].[C5_NOTA] = [ZC1010].[ZC1_DOC] 
			                                                    AND [SC5010].[C5_SERIE] = [ZC1010].[ZC1_SERIE]
		                                                    ) AS [COD_PROM]
                                                    FROM [dbo].[ZC1010]	
	                                                    LEFT JOIN (SELECT [ZC0010].[ZC0_NUMCOM],
					                                                      [ZC0010].[ZC0_DTPROC] DATAZ
				                                                    FROM [dbo].[ZC0010]
				                                                    WHERE D_E_L_E_T_ <> '*') AS ZC0 ON [ZC0].[ZC0_NUMCOM] = [ZC1_NUMCOM]
                                                        ");

            List<SqlParameter> param = new List<SqlParameter>();

            if (qntMes != null) {
                _query.Append(@" WHERE DATAZ >= Convert(DATE, DATEADD(month, DATEDIFF(month, 0, DATEADD(month, @qntMes, GETDATE())), 0))");

                param.Add(new SqlParameter() { ParameterName = "@qntMes", Value = -qntMes });
            } else {
                _query.Append(@" WHERE DATAZ BETWEEN @dataIni AND @dataFim");

                param.Add(new SqlParameter() { ParameterName = "@dataIni", Value = dataInicio });
                param.Add(new SqlParameter() { ParameterName = "@dataFim", Value = dataFim });
            }

            _query.Append(@" AND [ZC1010].[D_E_L_E_T_] <> '*' AND [ZC1010].[ZC1_STATUS] NOT IN (' ', '1', '5')");

            _query.Append(@" AND [ZC1010].[ZC1_CODDIV] = @divulgador ");

            param.Add(new SqlParameter() { ParameterName = "@divulgador", Value = divulgador });

            try {
                using (SqlDataReader reader = base.GetDataReader(_query.ToString(), param, CommandType.Text)) {

                    if (reader != null && reader.HasRows) {

                        while (reader.Read()) {

                            lComissao.Add(new Comissao {
                                CodDivulgador = reader["ZC1_CODDIV"].ToString(),
                                NomeDivulgador = reader["ZC1_NOMDIV"].ToString(),
                                Cliente = reader["ZC1_NOMCLI"].ToString(),
                                Estado = reader["ZC1_EST"].ToString(),
                                Filial = reader["ZC1_FILDOC"].ToString(),
                                Cep = reader["ZC1_CEP"].ToString(),
                                CodPromocional = reader["COD_PROM"].ToString(),
                                NF = reader["ZC1_DOC"].ToString(),
                                Emissao = Convert.ToDateTime(reader["ZC1_EMITIT"]),
                                DataPagamento = Convert.ToDateTime(reader["DATAZ"]),
                                TipoVenda = reader["ZC1_TPVEND"].ToString(),
                                Formato = reader["ZC1_DESCPG"].ToString(),
                                ValorNF = Convert.ToDecimal(reader["ZC1_TOTNF"]),
                                ValorRecebido = reader["ZC1_VLRMVB"] != DBNull.Value ? Convert.ToDecimal(reader["ZC1_VLRMVB"]) : 0,
                                //ValorReceber = reader[""].ToString(),
                                //PagoComissao = reader["ZC1_VLCODI"] != DBNull.Value ? Convert.ToDecimal(reader["ZC1_VLCODI"]) : 0,
                                AReceberComissao = reader["ZC1_VLCODI"] != DBNull.Value ? Convert.ToDecimal(reader["ZC1_VLCODI"]) : 0,
                                //AReceberComissao = reader[""].ToString(),
                                //Rateio = reader[""].ToString(),
                                Parcela = reader["ZC1_PARCEL"].ToString(),
                                //MicroRegiao = reader[""].ToString(),
                                //Transferencia = reader[""].ToString(),
                            });

                        }

                    }

                }

                return lComissao;

            } catch (Exception ex) {
                
                throw ex;
            
            }

        }

    }
}
