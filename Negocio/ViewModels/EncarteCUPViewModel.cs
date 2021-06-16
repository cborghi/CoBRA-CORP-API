namespace CoBRA.Application.ViewModels
{
    public class EncarteCUPViewModel
    {
        public long IdEncarte { get; set; }
        public long IdProduto { get; set; }
        public string EncarteTipo { get; set; }
        public string EncarteAcabamento { get; set; }
        public string EncartePapel { get; set; }
        public string EncarteGramatura { get; set; }
        public string EncarteFormato { get; set; }
        public string EncarteCor { get; set; }
        public string EncarteOutros { get; set; }
        public int? EncartePaginas { get; set; }
    }
}
