using System;

namespace CoBRA.Application.ViewModels
{
    public class ComissaoViewModel
    {
        public string CodDivulgador { get; set; }
        public string NomeDivulgador { get; set; }
        public string Cliente { get; set; }
        public string Estado { get; set; }
        public string Filial { get; set; }
        public string Cep { get; set; }
        public string CodPromocional { get; set; }
        public string NF { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime DataPagamento { get; set; }
        public string TipoVenda { get; set; }
        public string Formato { get; set; }
        public decimal ValorNF { get; set; }
        public decimal ValorRecebido { get; set; }
        public decimal ValorReceber { get; set; }
        public decimal PagoComissao { get; set; }
        public decimal AReceberComissao { get; set; }
        public string Rateio { get; set; }
        public string MicroRegiao { get; set; }
        public string Transferencia { get; set; }
        public string Parcela { get; set; }
    }
}
