using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Infra.CrossCutting.EmailService.ViewModels
{
    public class EmailViewModel
    {
        public string Destinatario { get; set; }
        public string Assunto { get; set; }
        public string CorpoEmail { get; set; }
        public string Mensagem { get; set; }
    }
}
