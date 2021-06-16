using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CoBRA.Domain.Interfaces;
using CoBRA.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace CoBRA.Infra.Intranet
{
    public class RegraRepository : BaseRepository, IRegraRepository
    {

        public RegraRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public Regra ObterRegraPorId(int id)
        {
            Regra regra = new Regra();

            List<SqlParameter> param = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@id", Value = id }
            };

            StringBuilder query = new StringBuilder(@"SELECT DESCRICAO FROM REGRA WHERE ID = @id and ATIVO = 1");

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        regra.Descricao = dataReader["DESCRICAO"].ToString();                
                    }
                }
            }

            return regra;
        }
    }
}
