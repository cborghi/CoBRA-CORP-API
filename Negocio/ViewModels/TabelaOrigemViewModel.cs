using System;
using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class TabelaOrigemViewModel
    {
        public Guid IdTabela { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<CampoOrigemViewModel> Campos { get; set; }
    }
}
