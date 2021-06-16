using System;

namespace CoBRA.Domain.Entities
{
    public class RelatorioElvis
    {
        public Guid? IdRelatorio { get; set; }
        public string TituloLivro { get; set; }
        public string ParteLivro { get; set; }
        public int QtdTotal { get; set; }
        public int QtdTotalMenosCaiu { get; set; }
        public int QtdAprovada { get; set; }
        public int QtdFinalizado { get; set; }
        public decimal PorcentagemFinalizado { get; set; }
        public int QtdCaiu { get; set; }
        public int QtdReprovada { get; set; }
        public int QtdAguardandoAprovacao { get; set; }
        public int QtdEmPesquisa { get; set; }
        public int QtdAprovacaoOrcamento { get; set; }
        public int QtdOutrosStatus { get; set; }
        public DateTime DtCadastro { get; set; }
        public int QtdPendente { get; set; }
        public bool Impresso { get; set; }
        public int TotalItens { get; set; }
    }
}
