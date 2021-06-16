using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class MidiaCUP
    {
        public int Id_Midia { get; set; }
        public string Descricao_Midia { get; set; }
        public string UnidadeVenda_Midia { get; set; }
        public int Plataforma_Midia { get; set; }
    }
}
