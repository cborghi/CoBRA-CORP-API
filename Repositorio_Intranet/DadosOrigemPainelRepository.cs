using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class DadosOrigemPainelRepository : IDadosOrigemPainelRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public DadosOrigemPainelRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IEnumerable<DadosOrigemPainel>> ListarDadosOrigemPainel()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarDadosOrigemPainel");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderDadosOrigemPainel(reader);
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

        private IEnumerable<DadosOrigemPainel> LerDataReaderDadosOrigemPainel(SqlDataReader reader)
        {
            var origens = new List<DadosOrigemPainel>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DadosOrigemPainel origem = new DadosOrigemPainel
                    {
                        IdOrigem = Guid.Parse(reader["ID_ORIGEM"].ToString()),
                        Descricao = reader["DESCRICAO"].ToString()
                    };
                    origens.Add(origem);
                }
            }

            return origens;
        }
    }
}
