using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class ResultadoPagamentoMetaRepository : IResultadoPagamentoMetaRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public ResultadoPagamentoMetaRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ResultadoPagamentoMeta> ConsultarResultadoPagamentoMeta(int Percentual)
        {
            string consulta = "SELECT [ID_RESULTADO_PAGAMENTO] ,[PORCENTAGEM_RESULTADO] ,[PORCENTAGEM_PAGAMENTO] ,[DT_CADASTRO] FROM [TB_RESULTADO_PAGAMENTO_META] WHERE [PORCENTAGEM_RESULTADO] = @PORCENTAGEM_RESULTADO"; //_baseRepository.BuscarArquivoConsulta("ConsultaResultadoPagamento");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                SqlParameter paramPercentual = new SqlParameter("@PORCENTAGEM_RESULTADO", SqlDbType.Int)
                {
                    Value = Percentual
                };

                command.Parameters.Add(paramPercentual);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderResultadoPagamentoMeta(reader).FirstOrDefault();
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

        private IEnumerable<ResultadoPagamentoMeta> LerDataReaderResultadoPagamentoMeta(SqlDataReader reader)
        {
            var resultadoPagamentosMeta = new List<ResultadoPagamentoMeta>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ResultadoPagamentoMeta resultadoPagamentoMeta = new ResultadoPagamentoMeta
                    {
                        IdResultadoPagamento = Guid.Parse(reader["ID_RESULTADO_PAGAMENTO"].ToString()),
                        PorcentagemResultado = Convert.ToInt32(reader["PORCENTAGEM_RESULTADO"].ToString()),
                        PorcentagemPagamento = Convert.ToInt32(reader["PORCENTAGEM_PAGAMENTO"].ToString()),
                        DataCadastro = Convert.ToDateTime(reader["DT_CADASTRO"].ToString())
                    };
                    resultadoPagamentosMeta.Add(resultadoPagamentoMeta);
                }
            }

            return resultadoPagamentosMeta;
        }
    }
}
