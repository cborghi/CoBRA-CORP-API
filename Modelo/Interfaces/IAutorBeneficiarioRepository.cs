using CoBRA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IAutorBeneficiarioRepository
    {
        Task<int> SalvarAutor(AutorDA autorBeneficiario);
        Task AtualizarAutor(AutorDA autorBeneficiario);
        AutorDA BuscarAutorPorId(int id);
        Task<int> SalvarBeneficiario(BeneficiarioDA autorBeneficiario);
        Task AtualizarBeneficiario(BeneficiarioDA autorBeneficiario);
        List<TipoContaBancaria> ListarContaBancaria();
        List<EstadoCivil> ListarEstadoCivil();
        List<AutorDA> ListarAutores(string filtro);
        Task SalvarAutoresBeneficiario(int idAutor, int idBeneficiario);
        Task SalvarLogAutorBeneficiario(int idAutorBeneficiario, int? idUsuarioInclusao, string msg);
        Task ExcluirAutoresBeneficiario(int idBeneficiario);
        List<Estado> ListarEstados();
        AutoresBeneficiariosPaginado ListarAutoresBeneficiarios(int? idTipoCadastro, string tipoPessoa, int? idEstado, bool? ativo, int numeroPagina, int registrosPagina);
        BeneficiarioDA BuscarBeneficiarioPorId(int id);
        List<AutorDA> BuscarAutoresBeneficiarioPorId(int id);
        Task SalvarNomeCapa(string nomeCapaDescricao, int idAutorBeneficiario);
        Task SalvarArquivoAutorBeneficiario(ArquivoAutorBeneficiario arq);
        Task SalvarEmail(string destinatario, int idAutorBeneficiario);
        void ExcluirArquivoAutorBeneficiario(int idAutorBeneficiario, string nomeArquivo);
        List<LogAutorBeneficiario> BuscarLogAutorBeneficiarioPorId(int id);
        List<NomeCapa> ListarNomeCapaPorAutor(int idAutorBeneficiario);
        List<NomeCapa> ListarNomeCapaPorAutorDet(int idAutorBeneficiario);
        List<ArquivoAutorBeneficiario> ListarArquivoAutorBeneficiario(int idAutorBeneficiario);
        List<CorrespondenciaDA> ListarCorrespondenciaAutorBeneficiario(int idAutorBeneficiario);
        Task<int> SalvarCorrespondenciaAutorBeneficiario(CorrespondenciaDA json);
        Task AtualizarCorrespondenciaAutorBeneficiario(CorrespondenciaDA json);
        Task SalvarLogCorrespAutorBeneficiario(long idCorrespondencia, int idUsuario, string v);
        List<LogCorrespAutorBeneficiario> BuscarLogCorrespAutorBeneficiarioPorId(long idCorrespondencia);
        Task ExcluirEmail(int idAutorBeneficiario);
        List<EmailAutorBeneficiario> ListarEmailPorAutor(int idAutorBeneficiario);
        List<AutorDA> ListarAutoresPorNome(string nome);
        Task AtualizarNomeCapaPorAutor(NomeCapa nomeCapa);
        Task ExcluirCorrespondenciaAutorBeneficiario(int idCorrespondencia);
        CorrespondenciaDA ListarCorrespondenciaAutorBeneficiarioId(int idCorrespondencia);
        void ExcluirNomeCapaPorAutor(int idNomeCapa);
        Task<int> IncluirNomeCapaPorAutor(NomeCapa entrada);
        List<NomeCapa> ListarNomeCapaPorId(int? idNomeCapa);
    }
}
