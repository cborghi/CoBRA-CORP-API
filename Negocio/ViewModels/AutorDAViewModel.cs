using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Application.ViewModels
{
    public class AutorDAViewModel
    {
        public int IdAutorBeneficiario { get; set; }
        public short IdTipoCadastro { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public string Loja { get; set; }
        public List<NomeCapaViewModel> LstNomeCapa { get; set; }
        public string CpfCnpj { get; set; }
        public string RG { get; set; }
        public string Sexo { get; set; }
        public string Nacionalidade { get; set; }
        public string Naturalidade { get; set; }
        public DateTime? DataNascimento { get; set; }
        public int? IdEstadoCivil { get; set; }
        public string Profissao { get; set; }
        public string NitPis { get; set; }
        public List<EmailAutorBeneficiarioViewModel> LstEmail { get; set; }
        public string Situacao { get; set; }
        public string TelResidencial { get; set; }
        public string Celular { get; set; }
        public string TelComercial { get; set; }
        public string Contato { get; set; }
        public string CEP { get; set; }
        public int IdEstado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Correntista { get; set; }
        public string CorrentistaCpfCnpj { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public short? IdTipoConta { get; set; }
        public string NumeroConta { get; set; }
        public DateTime? DataAutorizacao { get; set; }
        public bool? EnviarDemonstrativo { get; set; }
        public decimal? PagamentoMinimo { get; set; }
        public bool? EmiteRecibo { get; set; }
        public string EmailContador { get; set; }
        public string Observacao { get; set; }
        public int? IdUsuarioInclusao { get; set; }
        public DateTime? DataInclusao { get; set; }
        public string IncluidoPor { get; set; }
        public string TipoPessoa { get; set; }
        public bool Ativo { get; set; }
        public List<LogAutorBeneficiarioViewModel> LstLogAutorBeneficiario { get; set; }
    }
}
