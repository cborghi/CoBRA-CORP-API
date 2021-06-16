﻿namespace CoBRA.Application.ViewModels
{
    public class PermissaoRequisicaoViewModel
    {
        public UsuarioViewModel Usuario { get; set; }
        public bool? AprovaRequisicaoSupervisor { get; set; }
        public bool? ReprovaRequisicaoSupervisor { get; set; }
        public bool? CancelaRequisicaoSupervisor { get; set; }
        public bool? AprovaRequisicaoGerente { get; set; }
        public bool? ReprovaRequisicaoGerente { get; set; }
        public bool? CancelaRequisicaoGerente { get; set; }
    }
}
