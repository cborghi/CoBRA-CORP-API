using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class ConteudoPP
    {
        public int IdConteudo { get; set; }
        public string Nome { get; set; }
        public string Caminho { get; set; }
        public string NomeGuid { get; set; }
        public int Usuario { get; set; }
        public DateTime DtCadastro { get; set; }
        public bool Ativo { get; set; }
        public string PrimeiraPagina { get; set; }
    }
}
