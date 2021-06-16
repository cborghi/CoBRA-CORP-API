using System;

namespace CoBRA.Application.ViewModels
{
    public class LogViewModel
    {
        public int Usuario { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public int TipoLog { get; set; }
        public string IpAdress { get; set; }
        public Exception Erro { get; set; }
    }
}
