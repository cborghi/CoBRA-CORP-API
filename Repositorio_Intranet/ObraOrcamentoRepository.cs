using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class ObraOrcamentoRepository : IObraOrcamentoRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public ObraOrcamentoRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IEnumerable<ObraOrcamento>> ListarObraOrcamento()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarObraOrcamento");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderObraOrcamento(reader);
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

        private IEnumerable<ObraOrcamento> LerDataReaderObraOrcamento(SqlDataReader reader)
        {
            List<ObraOrcamento> obras = null;

            if (reader.HasRows)
            {
                obras = new List<ObraOrcamento>();
                while (reader.Read())
                {
                    var obraOrcamento = new ObraOrcamento
                    {
                        CodigoProduto = int.Parse(reader["PROD_ID"].ToString()),
                        CodigoEbsa = reader["PROD_EBSA"].ToString(),
                        Isbn = reader["PROD_ISBN"].ToString(),
                        Titulo = reader["PROD_TITULO"].ToString(),
                        Valor = decimal.TryParse(reader["VALOR"].ToString(), out decimal valor) ? valor : 0,
                    };

                    obras.Add(obraOrcamento);
                }
            }

            return obras;
        }

        public async Task GravarOrcamentoLivro(ObraOrcamento obra)
        {
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _baseRepository.BuscarArquivoConsulta("GravarOrcamentoLivro");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ID_PRODUTO", obra.CodigoProduto);
                            command.Parameters.AddWithValue("@VALOR", obra.Valor);
                            command.Parameters.AddWithValue("@ID_USUARIO", obra.IdUsuario);
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
