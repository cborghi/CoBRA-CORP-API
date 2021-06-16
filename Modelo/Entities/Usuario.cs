using System;
using System.Collections.Generic;

namespace CoBRA.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Grupo Grupo { get; set; }
        public Cargos Cargo { get; set; }
        public Nivel Nivel { get; set; }
        public IList<Grupo> Grupos { get; set; }
        public string Ramal { get; set; }
        public string Email { get; set; }
        public string ContaAd { get; set; }
        public string Telefone { get; set; }
        public string CodUsuario { get; set; }
        public Filial Filial { get; set; }
        public string Secao { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataAdmissao { get; set; }
        public bool Ativo { get; set; }

        public Usuario()
        {
            Grupos = new List<Grupo>();
            Cargo = new Cargos();
            Nivel = new Nivel();
        }
    }
}
