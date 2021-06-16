using System;

namespace CoBRA.Domain.Entities
{
    public class Log
    {
        public int Usuario { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public int TipoLog { get; set; }
        public string IpAdress { get; set; }
    }
}
