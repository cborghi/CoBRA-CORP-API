using System;
using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public GrupoViewModel Grupo { get; set; }
        public CargosViewModel Cargo { get; set; }
        public NivelViewModel Nivel { get; set; }
        public IList<GrupoViewModel> Grupos { get; set; }
        public IList<int> GruposAcesso { get; set; }
        public string Ramal { get; set; }
        public string Email { get; set; }
        public string ContaAd { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public string ConfirmSenha { get; set; }
        public string CodUsuario { get; set; }
        public FilialViewModel Filial { get; set; }
        public bool Ativo { get; set; }
        public string Secao { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataAdmissao { get; set; }
        public string Guid { get; set; }
        public PermissoesUsuarioViewModel Permissoes { get; set; }

        public UsuarioViewModel()
        {
            Grupos = new List<GrupoViewModel>();
            GruposAcesso = new List<int>();
            Nivel = new NivelViewModel();
            Cargo = new CargosViewModel();
        }
    }
}
