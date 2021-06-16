using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Application.ViewModels
{
    public class LogAmarracaoViewModel
    {
        public long IdLogAmarracao { get; set; }
        public int IdAmarracao { get; set; }
        public DateTime DataLog { get; set; }
        public string DescricaoLog { get; set; }
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
    }
}
