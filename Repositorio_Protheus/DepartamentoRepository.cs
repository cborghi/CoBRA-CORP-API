using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace CoBRA.Infra.Protheus
{
    public class DepartamentoRepository : BaseRepository, IDepartamentoRepository
    {
        public DepartamentoRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<Departamento> ObterDepartamentos()
        {
            try
            {
                List<Departamento> departamentos = new List<Departamento>();

                string query = @"SELECT DISTINCT CTT_CUSTO, CTT_DESC01 FROM CTT010";

                using (DbDataReader dataReader = base.GetDataReader(query.ToString(), null, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            departamentos.Add(new Departamento()
                            {
                                CentroDeCusto = dataReader["CTT_CUSTO"].ToString().Trim(),
                                Descricao = dataReader["CTT_DESC01"].ToString().Trim()
                            });
                        }
                    }
                }
                return departamentos;
            }
            catch
            {
                throw;
            }
        }        
    }
}
