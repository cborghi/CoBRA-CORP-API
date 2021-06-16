using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Application.ViewModels
{
    public class AmarracaoViewModel
    {
        public int IdAmarracao { get; set; }
        public int IdProduto { get; set; }
        public string Obra { get; set; }
        public string Titulo { get; set; }
        public string Colecao { get; set; }
        public string Projeto { get; set; }
        public string CodigoFND { get; set; }
        public List<AmarracaoAutorViewModel> LstAmarracao { get; set; }
        public string Pseudonimo { get; set; }
        public int IdBeneficiario { get; set; }
        public string TermoCessao { get; set; }
        public long? NumeroContrato { get; set; }
        public DateTime? DtInicioVigencia { get; set; }
        public DateTime? DtFimVigencia { get; set; }
        public string Iniciou { get; set; }
        public bool? RenovacaoAutomatica { get; set; }
        public int IdRegraPagamento { get; set; }
        public DateTime? DtPrazoPagamento { get; set; }
        public string FaixaInicial { get; set; }
        public string FaixaFinal { get; set; }
        public DateTime? DtContrato { get; set; }
        public decimal? Percentual { get; set; }
        public int AnosVigencia { get; set; }
        public int IdPrazoValidade { get; set; }
        public DateTime? DtAdiantamento { get; set; }
        public DateTime? DtDistrato { get; set; }
        public string ObsDistrato { get; set; }
        public int? IdBloqueioPgto { get; set; }
        public DateTime? DtLimiteDenuncia { get; set; }
        public int IdTipoContrato { get; set; }
        public int IdTipoParticipacao { get; set; }
        public long? QtdeExemplarPublicacao { get; set; }
        public long? QtdeExemplarReimpressao { get; set; }
        public string Intencao { get; set; }
        public string Obs { get; set; }
        public List<TermoAditivoViewModel> LstTermoAditivo { get; set; }
        public List<LogAmarracaoViewModel> LstLogAmarracao { get; set; }
    }
}
