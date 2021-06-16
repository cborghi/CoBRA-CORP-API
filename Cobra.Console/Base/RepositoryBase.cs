using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Cobra.Console.Base
{
    public class RepositoryBase
    {
        
        private Dictionary<string, string> _arquivosSql;
        private string _conexaoEbsa;
        private string _conexaoCorpore;
        private string _conexaoProtheus;
        private string _nomeBancoProtheus;

        public RepositoryBase(string conexaoEbsa, string conexaoCorpore, string conexaoProtheus, string nomeBancoProtheus)
        {
            _arquivosSql = new Dictionary<string, string>();
            _conexaoCorpore = conexaoCorpore;
            _conexaoEbsa = conexaoEbsa;
            _conexaoProtheus = conexaoProtheus;
            _nomeBancoProtheus = nomeBancoProtheus;
            CarregarArquivosConsulta();
        }

        public string NomeBancoProtheus
        {
            get { return _nomeBancoProtheus; }
        }

        public string ConexaoProtheus
        {
            get { return _conexaoProtheus; }
        }

        public string ConexaoEBSA
        {
            get { return _conexaoEbsa; }
        }

        public string ConexaoCorpore
        {
            get { return _conexaoCorpore; }
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
