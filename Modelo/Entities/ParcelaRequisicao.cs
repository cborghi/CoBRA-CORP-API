using System;

namespace CoBRA.Domain.Entities
{
    public class ParcelaRequisicao
    {
        public string ID { get; set; }
        public string RequisicaoID { get; set; }
        public int NumeroParcela { get; set; }
        public decimal Valor { get; set; }
        public DateTime PrazoPagamento { get; set; }
    }
}
