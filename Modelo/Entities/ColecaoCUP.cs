using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class ColecaoCUP
    {
        public int Id_Colecao { get; set; }
        public string Descricao_Colecao { get; set; }
        public int Tipo_Colecao { get; set; }
        public int Segmento_Colecao { get; set; }
    }
}
