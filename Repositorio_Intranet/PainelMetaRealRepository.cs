using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet
{
    public class PainelMetaRealRepository : IPainelMetaRealRepository
    {                                                                                                                                                                                
        private readonly IBaseRepositoryPainelMeta _baseRepository;

        public PainelMetaRealRepository(IBaseRepositoryPainelMeta baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IEnumerable<PainelMetaReal>> ConsultaPainelMetaReal(Guid? MetaRealId, Guid? UsuarioId, Guid? LinhaMetaId)
        {
            string consulta = _baseRepository.BuscarArquivoConsulta("ConsultaPainelMetaReal");

            using (SqlConnection connection = new SqlConnection(_baseRepository.Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                SqlParameter paramIdMetaReal = new SqlParameter("@ID_META_REAL", SqlDbType.UniqueIdentifier);
                SqlParameter paramIdUsuario = new SqlParameter("@ID_USUARIO_RM", SqlDbType.UniqueIdentifier);
                SqlParameter paramIdLinhaMeta = new SqlParameter("@ID_LINHA_META", SqlDbType.UniqueIdentifier);

                paramIdMetaReal.Value = !string.IsNullOrEmpty(MetaRealId.GetValueOrDefault().ToString()) && MetaRealId != null ? MetaRealId : new Guid();
                paramIdUsuario.Value = !string.IsNullOrEmpty(UsuarioId.GetValueOrDefault().ToString()) && UsuarioId != null ? UsuarioId : new Guid();
                paramIdLinhaMeta.Value = !string.IsNullOrEmpty(LinhaMetaId.GetValueOrDefault().ToString()) && LinhaMetaId != null ? LinhaMetaId : new Guid();

                command.Parameters.Add(paramIdMetaReal);
                command.Parameters.Add(paramIdUsuario);
                command.Parameters.Add(paramIdLinhaMeta);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    IEnumerable<PainelMetaReal> item = LerDataReaderMetaReal(reader);

                    return item;
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

        private IEnumerable<PainelMetaReal> LerDataReaderMetaReal(SqlDataReader reader)
        {
            var metas = new List<PainelMetaReal>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    PainelMetaReal meta = new PainelMetaReal
                    {
                        IdMetaReal = !string.IsNullOrEmpty(reader["ID_META_REAL"].ToString()) ? Guid.Parse(reader["ID_META_real"].ToString()) : new Guid(),
                        IdUsuario = Guid.Parse(reader["ID_USUARIO_RM"].ToString()),
                        IdLinhaMeta = Guid.Parse(reader["ID_LINHA_META"].ToString()),
                        Realizado = !string.IsNullOrEmpty(reader["REALIZADO"].ToString()) ? Convert.ToDecimal(reader["REALIZADO"].ToString()) : 0,
                        Ponderado = !string.IsNullOrEmpty(reader["PONDERADO"].ToString()) ? Convert.ToDecimal(reader["PONDERADO"].ToString()) : 0,
                        DataCriacao = !string.IsNullOrEmpty(reader["DATA_CRIACAO"].ToString()) ? DateTime.Parse(reader["DATA_CRIACAO"].ToString()) : DateTime.MinValue
                    };
                    metas.Add(meta);
                }
            }

            return metas;
        }
    }
}
