using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class TabelaOrigemRepository : ITabelaOrigemRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public TabelaOrigemRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IEnumerable<TabelaOrigem>> BuscarTabelaOrigem(SistemaOrigem sistema)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarTabelaOrigem");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_SISTEMA", ((Object)sistema.IdSistema) ?? DBNull.Value);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderTabelaOrigem(reader);
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

        public async Task<IEnumerable<TabelaOrigem>> ListarTabelaOrigem()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarTabelaOrigem");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderTabelaOrigem(reader);
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

        private IEnumerable<TabelaOrigem> LerDataReaderTabelaOrigem(SqlDataReader reader)
        {
            var tabelas = new List<TabelaOrigem>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TabelaOrigem tabela = new TabelaOrigem
                    {
                        IdTabela = Guid.Parse(reader["ID_TABELA"].ToString()),
                        Descricao = reader["DESCRICAO"].ToString(),
                        NomeTabela = reader["NOME_TABELA"].ToString(),
                        Ativo = Convert.ToBoolean(reader["ATIVO"])
                    };
                    tabelas.Add(tabela);
                }
            }

            return tabelas;
        }

    }
}
