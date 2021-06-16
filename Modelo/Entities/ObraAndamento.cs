using System;

namespace CoBRA.Domain.Entities
{
    public class ObraAndamento
    {
        public string CodigoObra { get; set; }
        public string Titulo { get; set; }
        public int QuantidadeCollection { get; set; }
        public decimal ValorGasto { get; set; }
        public decimal ValorOrcado { get; set; }
        public decimal PorcentagemGasta { get; set; }
        public DateTime DtCadastro { get; set; }
        public int TotalItens { get; set; }
        public string Programa { get; set; }
    }
}
