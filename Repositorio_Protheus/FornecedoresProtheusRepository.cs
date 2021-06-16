using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CoBRA.Infra.Protheus
{
    public class FornecedoresProtheusRepository : BaseRepository, IFornecedoresProtheusRepository
    {
        private double result = 0;
        public FornecedoresProtheusRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<Fornecedor> ObterFornecedores(string nome)
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();

            StringBuilder query = new StringBuilder(@"SELECT A2_COD, A2_NOME, A2_TIPO, A2_XPIS, A2_CGC, A2_LOJA, A2_SISAGEN, A2_SISBCO, A2_SISCONT, A2_NREDUZ, A2_REPRES, 
                                                    A2_CPF, A2_DTNASC, A2_INSCR, A2_END, A2_BAIRRO, A2_MUN, A2_EST, A2_CEP, A2_TEL, A2_EMAIL, A2_CONTATO 
                                                    FROM SA2010 WHERE ");

            if (double.TryParse(nome, out result))
                {query.Append("A2_CGC LIKE @nome + '%' AND A2_MSBLQL <>  '1'");}
            else
                {query.Append("A2_NOME LIKE @nome + '%' AND A2_MSBLQL <>  '1'");}


            List<SqlParameter> param = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@nome", Value = nome }
            };

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        fornecedores.Add(new Fornecedor()
                        {
                            Codigo = dataReader["A2_COD"].ToString(),
                            Nome = dataReader["A2_NOME"].ToString().TrimEnd(),
                            CGC = dataReader["A2_CGC"].ToString(),
                            Tipo = dataReader["A2_TIPO"].ToString(),
                            Loja = dataReader["A2_LOJA"].ToString(),
                            Sisagen = dataReader["A2_SISAGEN"].ToString(),
                            Sisbco = dataReader["A2_SISBCO"].ToString(),
                            Siscont = dataReader["A2_SISCONT"].ToString(),
                            Nreduz = dataReader["A2_NREDUZ"].ToString(),
                            Repres = dataReader["A2_REPRES"].ToString(),
                            Cpf = dataReader["A2_CPF"].ToString(),
                            Dtnasc = dataReader["A2_DTNASC"].ToString(),
                            Inscr = dataReader["A2_INSCR"].ToString(),
                            End = dataReader["A2_END"].ToString(),
                            Bairro = dataReader["A2_BAIRRO"].ToString(),
                            Mun = dataReader["A2_MUN"].ToString(),
                            Est = dataReader["A2_EST"].ToString(),
                            Cep = dataReader["A2_CEP"].ToString(),
                            Tel = dataReader["A2_TEL"].ToString(),
                            Email = dataReader["A2_EMAIL"].ToString(),
                            Contato = dataReader["A2_CPF"].ToString(),
                            Pis = dataReader["A2_XPIS"].ToString()
                        });
                    }
                }
            }

            return fornecedores;
        }

        public Fornecedor ObterFornecedorCodigo(string cod)
        {
            Fornecedor fornecedor = new Fornecedor();

            StringBuilder query = new StringBuilder(@"SELECT A2_COD, A2_NOME, A2_TIPO, A2_CGC, A2_LOJA, A2_SISAGEN, A2_SISBCO, A2_SISCONT, A2_NREDUZ, A2_REPRES, 
                                                    A2_CPF, A2_DTNASC, A2_INSCR, A2_END, A2_BAIRRO, A2_MUN, A2_EST, A2_CEP, A2_TEL, A2_EMAIL, A2_CONTATO 
                                                    FROM SA2010 WHERE A2_COD = @cod");


            List<SqlParameter> param = new List<SqlParameter>()
            {
                  new SqlParameter() { ParameterName = "@cod", Value = cod }
            };

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        fornecedor = new Fornecedor()
                        {
                            Codigo = dataReader["A2_COD"].ToString(),
                            Nome = dataReader["A2_NOME"].ToString().TrimEnd(),
                            CGC = dataReader["A2_CGC"].ToString(),
                            Tipo = dataReader["A2_TIPO"].ToString(),
                            Loja = dataReader["A2_LOJA"].ToString(),
                            Sisagen = dataReader["A2_SISAGEN"].ToString(),
                            Sisbco = dataReader["A2_SISBCO"].ToString(),
                            Siscont = dataReader["A2_SISCONT"].ToString(),
                            Nreduz = dataReader["A2_NREDUZ"].ToString(),
                            Repres = dataReader["A2_REPRES"].ToString(),
                            Cpf = dataReader["A2_CPF"].ToString(),
                            Dtnasc = dataReader["A2_DTNASC"].ToString(),
                            Inscr = dataReader["A2_INSCR"].ToString(),
                            End = dataReader["A2_END"].ToString(),
                            Bairro = dataReader["A2_BAIRRO"].ToString(),
                            Mun = dataReader["A2_MUN"].ToString(),
                            Est = dataReader["A2_EST"].ToString(),
                            Cep = dataReader["A2_CEP"].ToString(),
                            Tel = dataReader["A2_TEL"].ToString(),
                            Email = dataReader["A2_EMAIL"].ToString(),
                            Contato = dataReader["A2_CPF"].ToString()
                        };
                    }
                }
            }

            return fornecedor;
        }
    }
}
