using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Application.ViewModels
{
    public class RelatorioElvisViewModel
    {

        public Guid? IdRelatorio { get; set; }
        [JsonProperty("tituloDoLivro")]
        public string TituloLivro { get; set; }
        [JsonProperty("parteDoLivro")]
        public string ParteLivro { get; set; }
        [JsonProperty("numeroDeCollections")]
        public int QtdTotal { get; set; }
        [JsonProperty("total")]
        public int QtdTotalMenosCaiu { get; set; }
        public int QtdAprovada { get; set; }
        [JsonProperty("finalizado")]
        public int QtdFinalizado { get; set; }
        [JsonProperty("PorcentagemFinalizadoDecimal")]
        public decimal PorcentagemFinalizado { get; set; }
        [JsonProperty("caiu")]
        public int QtdCaiu { get; set; }
        public int QtdReprovada { get; set; }
        public int QtdAguardandoAprovacao { get; set; }
        public int QtdEmPesquisa { get; set; }
        public int QtdAprovacaoOrcamento { get; set; }
        public int QtdOutrosStatus { get; set; }
        public DateTime DtCadastro { get; set; }
        public int QtdPendente { get; set; }
        public bool Impresso { get; set; }
        public int TotalEmAndamento
        {
            get
            {
                return QtdEmPesquisa + QtdAguardandoAprovacao + QtdAprovada + QtdReprovada + QtdAprovacaoOrcamento + QtdPendente;
            }
        }

        [JsonProperty("porcentagemFinalizado")]
        public string PorcentagemFinalizadoTexto { get {
                return string.Concat(PorcentagemFinalizado, " %");
            } 
        }

        public int TotalItens { get; set; }
    }
}
