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
    public class ElvisRepository : BaseRepository, IElvisRepository
    {
        public ElvisRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<RequisicaoArquivos> GetFiles(long ID)
        {
            List<RequisicaoArquivos> arquivos = new List<RequisicaoArquivos>();

            List<SqlParameter> param = new List<SqlParameter>()
            {
                    new SqlParameter() { ParameterName = "@ID", Value = ID }
            };

            StringBuilder query = new StringBuilder(@"SELECT ID, REQUISICAO, CAMINHO, NOME FROM REQUISICAO_ARQUIVOS WHERE REQUISICAO = @ID");

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        arquivos.Add(new RequisicaoArquivos()
                        {
                            Id = long.Parse(dataReader["ID"].ToString()),
                            Requisicao = long.Parse(dataReader["REQUISICAO"].ToString()),
                            Caminho = dataReader["CAMINHO"].ToString(),
                            Nome = dataReader["NOME"].ToString()
                        });
                    }
                }
            }

            return arquivos;
        }

        public RequisicaoArquivos GetFile(long ID)
        {
            RequisicaoArquivos arquivo = new RequisicaoArquivos();

            List<SqlParameter> param = new List<SqlParameter>()
            {
                    new SqlParameter() { ParameterName = "@ID", Value = ID }
            };

            StringBuilder query = new StringBuilder(@"SELECT ID, REQUISICAO, CAMINHO, NOME FROM REQUISICAO_ARQUIVOS WHERE ID = @ID");

            using (DbDataReader dataReader = GetDataReader(query.ToString(), param, CommandType.Text))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        arquivo.Id = long.Parse(dataReader["ID"].ToString());
                        arquivo.Requisicao = long.Parse(dataReader["REQUISICAO"].ToString());
                        arquivo.Caminho = dataReader["CAMINHO"].ToString();
                        arquivo.Nome = dataReader["NOME"].ToString();
                    }
                }
            }

            return arquivo;
        }

        public long SetFile(RequisicaoArquivos arquivo)
        {
            string query = @"INSERT INTO REQUISICAO_ARQUIVOS(REQUISICAO, CAMINHO, NOME) OUTPUT INSERTED.ID VALUES (@REQUISICAO, @CAMINHO, @NOME)";

            List<SqlParameter> listaParam = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@ID", Value = 0, Direction = ParameterDirection.Output },
                new SqlParameter() { ParameterName = "@REQUISICAO", Value = arquivo.Requisicao },
                new SqlParameter() { ParameterName = "@CAMINHO", Value = arquivo.Caminho },
                new SqlParameter() { ParameterName = "@NOME", Value = arquivo.Nome }
            };

            return long.Parse(ExecuteScalar(query, listaParam, CommandType.Text).ToString());
        }

        public RequisicaoArquivos DeleteFile(long ID)
        {
            RequisicaoArquivos arquivo = GetFile(ID);

            string query = @"DELETE FROM REQUISICAO_ARQUIVOS WHERE ID = @ID";

            List<SqlParameter> listaParam = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@ID", Value = ID }
            };

            ExecuteScalar(query, listaParam, CommandType.Text);

            return arquivo;
        }

        public void DeleteFilesRequisicao(int idRequisicao)
        {
            string query = @"DELETE FROM REQUISICAO_ARQUIVOS WHERE REQUISICAO = @ID";
            List<DbParameter> listaParam = new List<DbParameter>()
            {
                new SqlParameter() { ParameterName = "@ID", Value = idRequisicao }
            };

            ExecuteNonQuery(query, listaParam, CommandType.Text);
        }
    }
}
