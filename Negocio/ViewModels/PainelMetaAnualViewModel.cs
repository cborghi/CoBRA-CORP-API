using System;

namespace CoBRA.Application.ViewModels
{
    public class PainelMetaAnualViewModel
    {
        public Guid IdMetaAnual { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdLinhaMeta { get; set; }
        public Guid IdUnidadeMedida { get; set; }
        public Guid IdCargo { get; set; }
        public Guid IdStatus { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Grupo { get; set; }
        public string Situacao { get; set; }
        public string Meta { get; set; }
        public Guid IdMeta { get; set; }
        public string Indicador { get; set; }
        public int Peso { get; set; }
        public string UnidadeMedida { get; set; }
        public string Observacao { get; set; }
        public string Editar { get; set; }
        public decimal Total { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal ValorMaximo { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string Filial { get; set; }
        public string RegiaoAtuacao { get; set; }
        public string Periodo { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public DateTime? DataDemissao { get; set; }
        public string Proporcao { get; set; }
        public virtual int pesoTotal { get; set; }
        public virtual decimal valorRecebimento { get; set; }
        public virtual decimal valorRecebimentoCalculado { get; set; }
        public virtual decimal valorMetaReceitaLiquida { get; set; }
        public virtual decimal valorMetaReceitaLiquidaAtualizada { get; set; }
        public virtual decimal valorMinimoPercentual { get; set; }
        public virtual decimal valorMaximoPercentual { get; set; }
        public virtual decimal? realizado { get; set; }
        public virtual decimal? realizadoPercentual { get; set; }
        public virtual decimal? ponderado { get; set; }
        public virtual decimal? percentual { get; set; }
        public virtual decimal? realAtingido { get; set; }
        public virtual decimal porcentagemPagamento { get; set; }
        public virtual decimal? ganhoReal { get; set; }
        public virtual decimal ganhoReceita { get; set; }
        public virtual decimal antecipado { get; set; }
        public virtual decimal? totalReceberReal { get; set; }
        public virtual decimal totalReceberReceita { get; set; }
    }
}
