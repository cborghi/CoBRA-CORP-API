using System;
using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class GrupoViewModel
    {
        public GrupoViewModel()
        {
            Menus = new List<MenuViewModel>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string Usuario { get; set; }
        public Guid IdGrupoAD { get; set; }
        public List<MenuViewModel> Menus { get; set; }
        public string Departamento { get; set; }
        public string CentroCusto { get; set; }
    }
}
