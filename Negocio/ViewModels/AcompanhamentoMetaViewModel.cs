namespace CoBRA.Application.ViewModels
{
    public class AcompanhamentoMetaViewModel
     {
          public string Id { get; set; }
          public string Uf { get; set; }
          public string Cargo { get; set; }
          public string Nome { get; set; }
          public string Nota { get; set; }
          public decimal? PesoTotal { get; set; }
          public decimal? RealAtingido { get; set; }
          public ValorPotencialGanhoViewModel ValorPotencialGanho { get; set; }
     }
}

