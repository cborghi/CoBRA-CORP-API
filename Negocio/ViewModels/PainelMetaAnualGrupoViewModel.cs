using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class PainelMetaAnualGrupoViewModel
    {
        public string Cargo { get; set; }
        public string Filial { get; set; }
        public string RegiaoAtuacao { get; set; }
        public string Periodo { get; set; }
        public List<MetasGrupoViewModel> LstMetasGrupo { get; set; }
        public List<UsuarioGrupoViewModel> LstUsuarioGrupo { get; set; }
    }
}
