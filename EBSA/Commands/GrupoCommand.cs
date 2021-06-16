using System;
using System.Collections.Generic;

namespace CoBRA.API.Commands
{
    public class GrupoCommand
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string Usuario { get; set; }
        public Guid IdGrupoAD { get; set; }
        public List<MenuCommand> Menus { get; set; }
        public string Departamento { get; set; }
    }
}
