using System;

namespace CoBRA.Application.ViewModels
{
    public class MetasGrupoViewModel
    {
        public Guid IdMeta { get; set; }
        public string Indicador { get; set; }
        public string Meta { get; set; }
        public int Peso { get; set; }
        public int PesoTotal { get; set; }
    }
}
