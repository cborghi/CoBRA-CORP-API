using System;

namespace CoBRA.Application.ViewModels
{
    public class ParcelaRequisicaoViewModel
    {
        public string ID { get; set; }
        public string ID_REQ { get; set; }
        public int NumeroParcela { get; set; }
        public decimal Valor { get; set; }
        public DateTime PrazoPagamento { get; set; }
    }
}
