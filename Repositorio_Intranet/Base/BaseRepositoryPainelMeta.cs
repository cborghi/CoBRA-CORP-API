using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace CoBRA.Infra.Intranet.Base
{
    public class BaseRepositoryPainelMeta : IBaseRepositoryPainelMeta
    {
        private string _conexao;
        private Dictionary<string, string> _arquivosSql;

        public BaseRepositoryPainelMeta(string conexao)
        {
            _arquivosSql = new Dictionary<string, string>();
            _conexao = conexao;

            CarregarArquivosConsulta();
        }

        public string Conexao
        {
            get { return _conexao; }
        }

        public string BuscarArquivoConsulta(string nomeArquivo)
        {
            return _arquivosSql.GetValueOrDefault($"{nomeArquivo}.sql");
        }

        private void CarregarArquivosConsulta()
        {
            var diretorioBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var diretorioScript = new DirectoryInfo(diretorioBase + @"\SQL");

            foreach (var arquivo in diretorioScript.EnumerateFiles("*.sql"))
            {
                _arquivosSql.Add(arquivo.Name, File.ReadAllText(arquivo.FullName));
            }
        }

        public async Task<string> BuscarRegiaoUsuario(PainelMetaAnual meta)
        {
            string consulta = BuscarArquivoConsulta("BuscarRegiaoUsuario");

            using (SqlConnection connection = new SqlConnection(Conexao))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@ID_USUARIO", ((Object)meta.IdUsuario) ?? DBNull.Value);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return LerDataReaderRegiaoUsuario(reader);
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

        private string LerDataReaderRegiaoUsuario(SqlDataReader reader)
        {
            string regiaoUsuario = string.Empty;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    regiaoUsuario = reader["REGIAO"].ToString();
                }
            }

            return regiaoUsuario;

        }
    }
}
