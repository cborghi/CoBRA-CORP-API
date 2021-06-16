using System;

namespace CoBRA.Application.ViewModels
{
    public class GrupoPainelViewModel
    {
        public Guid? IdGrupo { get; set; }
        public string Descricao { get; set; }
        public DateTime Dt_Cadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
