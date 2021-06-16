 using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class CampoOrigemRepository : ICampoOrigemRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public CampoOrigemRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IEnumerable<CampoOrigem>> BuscarTabelaOrigem(TabelaOrigem tabela)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("BuscarCampoOrigem");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_TABELA", ((Object)tabela.IdTabela) ?? DBNull.Value);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderCampoOrigem(reader);
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

        public async Task<IEnumerable<CampoOrigem>> ListarCampoOrigem()
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarCampoOrigem");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderCampoOrigem(reader);
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

        private IEnumerable<CampoOrigem> LerDataReaderCampoOrigem(SqlDataReader reader)
        {
            var campos = new List<CampoOrigem>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CampoOrigem campo = new CampoOrigem
                    {
                        IdCampo = Guid.Parse(reader["ID_CAMPO"].ToString()),
                        Descricao = reader["DESCRICAO"].ToString(),
                        NomeCampo = reader["NOME_CAMPO"].ToString(),
                        Ativo = Convert.ToBoolean(reader["ATIVO"])
                    };
                    campos.Add(campo);
                }
            }

            return campos;
        }
    }
}
