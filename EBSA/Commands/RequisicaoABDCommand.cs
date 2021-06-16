using System;

namespace CoBRA.API.Commands
{
    public class RequisicaoABDCommand
    {
        public string Id { get; set; }
        public string Obra { get; set; }
        public string Tipo { get; set; }
        public string Imagem { get; set; }
        public int Contador { get; set; }
        public string Nota { get; set; }
        public decimal Valor { get; set; }
        public string Titulo { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Vencimento { get; set; }
    }
}
