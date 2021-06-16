using System;
using System.Collections.Generic;

namespace CoBRA.API.Commands
{
    public class RequisicaoUpdateCommand
    {
        public int UsuarioId { get; set; }
        public string TipoDocumento { get; set; }
        public string Loja { get; set; }
        public string Nota { get; set; }
        public string Fornecedor { get; set; }
        public string FornecedorCodigo { get; set; }
        public string Telefone { get; set; }
        public string Contato { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataEntrega { get; set; }
        public string Moeda { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal Valor { get; set; }
        public string FormaPagamento { get; set; }
        public string ConfirmaPagamento { get; set; }
        public string Cartao { get; set; }
        public string Servico { get; set; }
        public string Observacao { get; set; }
        public string IdElvis { get; set; }
        public string Pessoa { get; set; }
        public List<RequisicaoABDCommand> RequisicaoABD { get; set; }
    }
}
