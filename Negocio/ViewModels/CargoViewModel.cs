using System;

namespace CoBRA.Application.ViewModels
{
    public class CargoViewModel
    {
        public Guid IdCargo { get; set; }
        public string Descricao { get; set; }
        public Guid IdHierarquia { get; set; }
        public Guid IdGrupo { get; set; }
        public DateTime DtCadastro { get; set; }
    }
}
