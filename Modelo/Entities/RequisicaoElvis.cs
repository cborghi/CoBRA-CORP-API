using System;

namespace CoBRA.Domain.Entities
{
    public class RequisicaoElvis
    {
        public string Id { get; set; }
        public string Obra { get; set; }
        public string Tipo { get; set; }
        public string Imagem { get; set; }
        public int Contador { get; set; }
        public string Projeto { get; set; }
        public string Nota { get; set; }
        public decimal Valor { get; set; }
        public string Titulo { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Vencimento { get; set; }
        public string Fornecedor { get; set; }
    }
}
