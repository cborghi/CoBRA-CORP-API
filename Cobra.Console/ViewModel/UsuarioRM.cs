using System;

namespace Cobra.Console.ViewModel
{
    public class UsuarioRM
    {
        public int CodigoFilial { get; set; }
        public string NomeFilial { get; set; }
        public string Chapa { get; set; }
        public string Nome { get; set; }
        public string CodigoFuncao { get; set; }
        public string Funcao { get; set; }
        public string CodigoSecao { get; set; }
        public string DescricaoSecao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataDemissao { get; set; }
        public string Cpf { get; set; }
    }
}