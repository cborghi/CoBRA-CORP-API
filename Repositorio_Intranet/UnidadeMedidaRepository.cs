using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class UnidadeMedidaRepository : IUnidadeMedidaRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public UnidadeMedidaRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IEnumerable<UnidadeMedida>> ListaUnidadeMedida()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarUnidadeMedida");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderUnidadeMedida(reader);
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

        private IEnumerable<UnidadeMedida> LerDataReaderUnidadeMedida(SqlDataReader reader)
        {
            var unidades = new List<UnidadeMedida>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    UnidadeMedida unidade = new UnidadeMedida
                    {
                        IdUnidadeMedida = Guid.Parse(reader["ID_UNIDADE_MEDIDA_PAINEL"].ToString()),
                        Sigla = reader["SIGLA"].ToString(),
                        Descricao = reader["DESCRICAO"].ToString()
                    };
                    unidades.Add(unidade);
                }
            }

            return unidades;
        }
    }
}

