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
    public class BaseRepositoryIntranet : IBaseRepositoryIntranet
    {
        private string _conexao;
        private Dictionary<string, string> _arquivosSql;

        public BaseRepositoryIntranet(string conexao)
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
    }
}
