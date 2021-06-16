using System;

namespace CoBRA.Application.ViewModels
{
    public class ColaboradorViewModel
    {
        public Guid IdUsuario { get; set; }
        public Guid IdCargo { get; set; }
        public Guid IdFilial { get; set; }
        public Guid IdRegiao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public Guid IdSecao { get; set; }
        public int CodigoFilial { get; set; }
        public string Nome { get; set; }
        public string NumeroChapa { get; set; }
    }
}