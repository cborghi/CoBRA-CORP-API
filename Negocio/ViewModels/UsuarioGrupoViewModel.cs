using System;
using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class UsuarioGrupoViewModel
    {
        public string Nome { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public DateTime? DataDemissao { get; set; }
        public string Proporcao { get; set; }
        public List<UsuarioMetasGrupoViewModel> LstUsuarioMetasGrupo { get; set; }
        public string Regiao { get; internal set; }
    }
}
