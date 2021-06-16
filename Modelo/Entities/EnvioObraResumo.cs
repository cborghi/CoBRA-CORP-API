using System;

namespace CoBRA.Domain.Entities
{
    public class EnvioObraResumo
    {
        public string OBRA { get; set; }
        public string STATUS { get; set; }
        public string REVISAO_PROVA { get; set; }
        public DateTime? REVISAO_PROVA_DATA { get; set; }
        public string IMAGEM_ALTA { get; set; }
        public DateTime? IMAGEM_ALTA_DATA { get; set; }
        public string PAGTO { get; set; }
        public DateTime? PAGTO_DATA { get; set; }
        public string FATURAMENTO { get; set; }
        public string FATURAMENTO_OBS { get; set; }
        public string USO_IMAGEM { get; set; }
        public string USO_IMAGEM_OBS { get; set; }
        public string LICENCIAMENTO { get; set; }
        public string LICENCIAMENTO_OBS { get; set; }
        public string ICONOGRAFO { get; set; }
        public string RESPONSAVEL { get; set; }
        public string COORDENADOR { get; set; }
        public string E_STATUS { get; set; }
        public string E_REVISAO_PROVA { get; set; }
        public DateTime? E_REVISAO_PROVA_DATA { get; set; }
        public string E_IMAGEM_ALTA { get; set; }
        public DateTime? E_IMAGEM_ALTA_DATA { get; set; }
        public string E_PAGTO { get; set; }
        public DateTime? E_PAGTO_DATA { get; set; }
        public string E_FATURAMENTO { get; set; }
        public string E_FATURAMENTO_OBS { get; set; }
        public string E_USO_IMAGEM { get; set; }
        public string E_USO_IMAGEM_OBS { get; set; }
        public string E_LICENCIAMENTO { get; set; }
        public string E_LICENCIAMENTO_OBS { get; set; }
        public string E_ILUSTRADOR { get; set; }
        public string E_RESPONSAVEL { get; set; }
        public string E_COORDENADOR { get; set; }
        public string COD_AUTOR { get; set; }
        public string COD_EDITOR { get; set; }
        public DateTime? FECHAMENTO { get; set; }
        public DateTime? E_FECHAMENTO { get; set; }
        public string COD_ASSISTENTE { get; set; }
        public string COD_ASSISTENTE2 { get; set; }
        public DateTime? G_FECHAMENTO { get; set; }
        public string G_OBS_FECHAMENTO { get; set; }
        public string COD_AUXILIAR { get; set; }
        public string VISUALIZAR { get; set; }
        public string COD_G_GI { get; set; }
        public string OBRA_ANTIGA { get; set; }
        public decimal? ID_ENVIO_OBRA_RESUMO { get; set; }
    }
}