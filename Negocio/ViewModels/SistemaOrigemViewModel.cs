using System;

namespace CoBRA.Application.ViewModels
{
    public class SistemaOrigemViewModel
    {
        public Guid IdSistema { get; set; }
        public string Descricao { get; set; }
        public IEquatable<TabelaOrigemViewModel> Tabelas { get; set; }
    }
}
