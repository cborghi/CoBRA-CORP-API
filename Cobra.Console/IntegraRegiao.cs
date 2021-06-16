using Cobra.Console.Base;
using Cobra.Console.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Cobra.Console
{
    public class IntegraRegiao
    {
        private readonly RepositoryBase _repositoryBase;

        public IntegraRegiao(RepositoryBase repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task IntegrarRegiao()
        {
            var regioes = await BuscarRegiaoProtheus();

            foreach (Regiao regiao in regioes)
            {
                await GravarRegiao(regiao);
            }
        }

        private async Task<IList<Regiao>> BuscarRegiaoProtheus() 
        {
            SqlDataReader reader = null;

            string consulta = _repositoryBase.BuscarArquivoConsulta("BuscarRegiaoProtheus");

            using (SqlConnection connection = new SqlConnection(_repositoryBase.ConexaoProtheus))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                try
                {
                    await connection.OpenAsync();
                    reader = await command.ExecuteReaderAsync();

                    return LerDataReaderRegiao(reader);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        await connection.CloseAsync();

                    await command.DisposeAsync();
                }
            }

        }
        private IList<Regiao> LerDataReaderRegiao(SqlDataReader reader) {

            var regioes = new List<Regiao>();
            Regiao regiao = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    regiao = new Regiao();
                    regiao.Descricao = reader["DESCRICAO"].ToString();
                    regiao.Chave = reader["CHAVE"].ToString();
                    regioes.Add(regiao);
                }
            }
            return regioes;

        }
        private async Task GravarRegiao(Regiao regiao) {

            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_repositoryBase.ConexaoEBSA))
                {
                    await connection.OpenAsync();

                    using (transaction = connection.BeginTransaction())
                    {
                        string consulta = _repositoryBase.BuscarArquivoConsulta("GravarRegiao");

                        using (SqlCommand command = new SqlCommand(consulta, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@DESCRICAO", regiao.Descricao);
                            command.Parameters.AddWithValue("@CHAVE", regiao.Chave);
                            await command.ExecuteNonQueryAsync();
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

        }
    }
}
