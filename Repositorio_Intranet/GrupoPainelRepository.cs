using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class GrupoPainelRepository : IGrupoPainelRepository
    {
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public GrupoPainelRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IEnumerable<GrupoPainel>> ListaGrupoPainel(Guid? StatusId)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ListarGrupoPainel");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                SqlParameter paramIdStatus = new SqlParameter("@ID_STATUS", SqlDbType.UniqueIdentifier)
                {
                    Value = !string.IsNullOrEmpty(StatusId.GetValueOrDefault().ToString()) && StatusId != null ? StatusId : new Guid()
                };
                command.Parameters.Add(paramIdStatus);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderGrupoPainel(reader);
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

        private IEnumerable<GrupoPainel> LerDataReaderGrupoPainel(SqlDataReader reader)
        {
            var grupos = new List<GrupoPainel>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    GrupoPainel grupo = new GrupoPainel
                    {
                        IdGrupo = Guid.Parse(reader["ID_GRUPO_PAINEL"].ToString()),
                        Descricao = reader["DESCRICAO"].ToString(),
                        Dt_Cadastro = Convert.ToDateTime(reader["DT_CADASTRO"].ToString()),
                        Ativo = Convert.ToBoolean(reader["ATIVO"])
                    };
                    grupos.Add(grupo);
                }
            }
            return grupos;
        }
    }
}
