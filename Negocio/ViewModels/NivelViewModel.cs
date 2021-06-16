using System;

namespace CoBRA.Application.ViewModels
{
    public class NivelViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
