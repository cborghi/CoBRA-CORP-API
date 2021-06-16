namespace CoBRA.Domain.Entities
{
    public class AnoEducacaoCUP
    {
        public int aedu_id { get; set; }
        public string aedu_descricao { get; set; }
        public int aedu_segm { get; set; }
        public string Abreviacao { get; set; }
        public string full => $"{aedu_id.ToString().PadLeft(2, '0')} {aedu_descricao}";
        public string aedu_id_formatado => aedu_id.ToString().PadLeft(2, '0');
    }
}
