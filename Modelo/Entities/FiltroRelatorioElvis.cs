namespace CoBRA.Domain.Entities
{
    public class FiltroRelatorioElvis
    {
        public string Descricao { get; set; }

        public string Impresso { get; set; }
        public string ProgramaSelecionado { get; set; }
        public string DisciplinaSelecionada { get; set; }
        public string ColecaoSelecionada { get; set; }
        public string AnoSelecionado { get; set; }
        public ParametroPesquisa ParametroPesquisa { get; set; }
    }
}
