using System;

namespace CoBRA.Domain.Entities
{
    public class RequisicaoGenerica
    {
        public int Id { get; set; }
        public string TipoDoc { get; set; }
        public string Doc { get; set; }
        public string Fornecedor { get; set; }
        public string FormaPagto { get; set; }
        public string DescPagto { get; set; }
        public DateTime PrazoPagto { get; set; }
        public string Obs { get; set; }
        public decimal Valor { get; set; }
        public string Incluidor { get; set; }
        public DateTime Incluido { get; set; }
    }
}
