using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Application.ViewModels
{
    public class FiltroRelatorioElvisViewModel
    {
        public string Descricao { get; set; }

        public string Impresso { get; set; }
        public string ProgramaSelecionado { get; set; }
        public string DisciplinaSelecionada { get; set; }
        public string ColecaoSelecionada { get; set; }
        public string AnoSelecionado { get; set; }
        public ParametroPesquisaViewModel ParametroPesquisa { get; set; }
    }
}
