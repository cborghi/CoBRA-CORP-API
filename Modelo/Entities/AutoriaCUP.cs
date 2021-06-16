using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class AutoriaCUP
    {
        public long IdAutoria { get; set; }
        public string NomeContratual { get; set; }
        public List<FuncoesCUP> lstFuncoes { get; set; }
        public DateTime? DataLiberacao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public string RenovacaoAuto { get; set; }
        public int? QtdeMeses { get; set; }
        public long? DireitosAutorais { get; set; }
        public DateTime? DataLimite { get; set; }
        public long IdProduto { get; set; }
        public string Nacionalidade { get; set; }
        public string Naturalidade { get; set; }
        public int ReparteImpressao { get; set; }
        public int ReparteReimpressao { get; set; }
        public string CapaCodAutor { get; set; }
    }
}
