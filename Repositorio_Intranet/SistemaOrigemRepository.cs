using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class SistemaOrigemRepository : ISistemaOrigemRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public SistemaOrigemRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IEnumerable<SistemaOrigem>> ListaSistemaOrigem()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarSistemaOrigem");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderSistemaOrigem(reader);
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

        private IEnumerable<SistemaOrigem> LerDataReaderSistemaOrigem(SqlDataReader reader)
        {
            var sistemas = new List<SistemaOrigem>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SistemaOrigem sistema = new SistemaOrigem
                    {
                        IdSistema = Guid.Parse(reader["ID_SISTEMA"].ToString()),
                        Descricao = reader["DESCRICAO"].ToString(),
                        NomeBanco = reader["ATIVO"].ToString(),
                        Ativo = Convert.ToBoolean(reader["ATIVO"])
                    };
                    sistemas.Add(sistema);
                }
            }

            return sistemas;
        }
    }
}

