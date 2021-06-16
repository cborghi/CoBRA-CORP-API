using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace CoBRA.Infra.Intranet
{
    public class ServicoRepository : BaseRepository, IServicoRepository
    {
        public ServicoRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<Servico> ObterServico()
        {
            List<Servico> servicos = new List<Servico>();
            StringBuilder query = new StringBuilder(@"SELECT TIPO_SERVICO, SERVICO_DESCRICAO, CCUSTOS_REQUISICAO FROM FASE_SERVICO ");


            using (DbDataReader dataReader = GetDataReader(query.ToString(), null, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        servicos.Add(new Servico()
                        {
                            TipoServico = dataReader["TIPO_SERVICO"].ToString(),
                            ServicoDescricao = dataReader["SERVICO_DESCRICAO"].ToString().Trim(),
                            CentroCusto = dataReader["CCUSTOS_REQUISICAO"].ToString()
                        });
                    }
                }
            }

            return servicos;
        }

        public List<Servico> ObterServicoPorCentroCusto(string centroCusto)
        {
            List<Servico> servicos = new List<Servico>();
            StringBuilder query = new StringBuilder(@"SELECT TIPO_SERVICO, SERVICO_DESCRICAO, CCUSTOS_REQUISICAO FROM FASE_SERVICO WHERE CCUSTOS_REQUISICAO LIKE @CENTRO_CUSTO");
            List<SqlParameter> param = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@CENTRO_CUSTO", Value = centroCusto.Trim() }
            };

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        servicos.Add(new Servico()
                        {
                            TipoServico = dataReader["TIPO_SERVICO"].ToString(),
                            ServicoDescricao = dataReader["SERVICO_DESCRICAO"].ToString(),
                            CentroCusto = dataReader["CCUSTOS_REQUISICAO"].ToString()
                        });
                    }
                }
            }

            return servicos;
        }

        public Servico ObterServicoPorTipo(string tipo)
        {
            Servico servicos = new Servico();
            StringBuilder query = new StringBuilder(@"SELECT TIPO_SERVICO, SERVICO_DESCRICAO, CCUSTOS_REQUISICAO FROM FASE_SERVICO WHERE TIPO_SERVICO = @tipo OR SERVICO_DESCRICAO = @tipo");

            List<SqlParameter> param = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@tipo", Value = tipo }
            };

            using (DbDataReader dataReader = base.GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        servicos = (new Servico()
                        {
                            TipoServico = dataReader["TIPO_SERVICO"].ToString(),
                            ServicoDescricao = dataReader["SERVICO_DESCRICAO"].ToString(),
                            CentroCusto = dataReader["CCUSTOS_REQUISICAO"].ToString()
                        });
                    }
                }
            }

            return servicos;
        }
    }
}
