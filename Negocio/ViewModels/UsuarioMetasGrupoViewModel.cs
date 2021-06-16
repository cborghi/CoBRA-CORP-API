using System;

namespace CoBRA.Application.ViewModels
{
    public class UsuarioMetasGrupoViewModel
    {


        public Guid idMeta { get; set; }
        public decimal Total { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal ValorMaximo { get; set; }
        public virtual decimal? realizado { get; set; }
        public virtual decimal? ponderado { get; set; }
        public virtual decimal? percentual { get; set; }
        public virtual decimal? realAtingido { get; set; }
        public virtual decimal? valorRecebimento { get; set; }
        public virtual decimal? valorMetaReceitaLiquida { get; set; }
        public virtual decimal? valorMetaReceitaLiquidaAtualizada { get; set; }
        public virtual decimal porcentagemPagamento { get; set; }
        public virtual decimal? ganhoReal { get; set; }
        public virtual decimal? ganhoReceita { get; set; }
        public virtual decimal? antecipado { get; set; }
        public virtual decimal? totalReceberReal { get; set; }
        public virtual decimal? totalReceberReceita { get; set; }
        public virtual decimal valorMaximoPercentual { get; set; }
        public virtual decimal valorMinimoPercentual { get; set; }
        public virtual decimal valorRecebimentoCalculado { get; set; }
        
        public string UnidadeMedida { get; set; }
        public decimal? PesoTotal { get; set; }
    }
}
