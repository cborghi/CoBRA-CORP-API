using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace CoBRA.Infra.Intranet
{
    public class LogRepository : BaseRepository, ILogRepository
    {
        public LogRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public void GravarLog(Log log)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Execute("dbo.SP_I_LOG",
                new
                {
                    USUARIO = log.Usuario,
                    TIPO_LOG = log.TipoLog,
                    DATA_LOG = log.Data,
                    IP = log.IpAdress,
                    DESCRICAO = log.Descricao,
                    TIPO_SISTEMA = 1
                },
                commandType: CommandType.StoredProcedure);
            }
        }
    }
}
