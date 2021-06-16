using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class RegiaoConsultorRepository : IRegiaoConsultorRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public RegiaoConsultorRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;
        }


        public async Task<IEnumerable<RegiaoConsultor>> ListarRegiaoConsultor()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarRegiaoConsultor");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderRegiaoConsultor(reader);
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

        public async Task<IEnumerable<RegiaoConsultor>> BuscarRegiaoConsultor(Guid idUsuarioRM)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarRegiaoConsultor");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_USUARIO_RM", idUsuarioRM);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderRegiaoConsultor(reader);
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

        private IEnumerable<RegiaoConsultor> LerDataReaderRegiaoConsultor(SqlDataReader reader)
        {
            IList<RegiaoConsultor> regioes = null;

            if (reader.HasRows)
            {
                regioes = new List<RegiaoConsultor>();
                while (reader.Read())
                {
                    RegiaoConsultor regiao = new RegiaoConsultor
                    {
                        IdRegiao = Guid.Parse(reader["ID_REGIAO"].ToString()),
                        Descricao = reader["DESCRICAO"].ToString(),
                        Uf = reader["UF"].ToString()
                    };
                    regioes.Add(regiao);
                }
            }

            return regioes;
        }
    }
}
