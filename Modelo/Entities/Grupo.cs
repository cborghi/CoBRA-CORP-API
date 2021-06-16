using System;
using System.Collections.Generic;

namespace CoBRA.Domain.Entities
{
    public class Grupo
    {
        public Grupo()
        {
            Menus = new List<Menu>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string Usuario { get; set; }
        public Guid IdGrupoAD { get; set; }
        public List<Menu> Menus { get; set; }
        public string Departamento { get; set; }
        public string CentroCusto { get; set; }
    }
}
