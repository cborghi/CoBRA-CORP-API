using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Application.ViewModels
{
    public class NomeCapaViewModel
    {
        public int? IdNomeCapa { get; set; }
        public int IdAutorBeneficiario { get; set; }
        public string NomeCapaDescricao { get; set; }
        public int? IdSegmento { get; set; }
        public string DescricaoSegmento { get; set; }
        public int? IdDisciplina { get; set; }
        public string DescricaoDisciplina { get; set; }
        public int? IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime? DataInclusao { get; set; }
        public bool Ativo { get; set; }
    }
}
