using CoBRA.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IAutorBeneficiarioAppService
    {
        Task<int> SalvarAutor(AutorDAViewModel json);
        Task AtualizarAutor(AutorDAViewModel autorBeneficiario);
        AutorDAViewModel BuscarAutorPorId(int id);
        Task<int> SalvarBeneficiario(BeneficiarioDAViewModel json);
        Task AtualizarBeneficiario(BeneficiarioDAViewModel autorBeneficiario);
        List<TipoContaBancariaViewModel> ListarContaBancaria();
        List<EstadoCivilViewModel> ListarEstadoCivil();
        List<AutorDAViewModel> ListarAutores(string filtro);
        List<EstadoViewModel> ListarEstados();
        AutoresBeneficiariosPaginadoViewModel ListarAutoresBeneficiarios(int? idTipoCadastro, string tipoPessoa, int? idEstado, bool? ativo, int numeroPagina, int registrosPagina);
        BeneficiarioDAViewModel BuscarBeneficiarioPorId(int id);
        Task SalvarArquivoAutorBeneficiario(ArquivoAutorBeneficiarioViewModel arq);
        void ExcluirArquivoAutorBeneficiario(int idAutorBeneficiario, string nomeArquivo);
        List<ArquivoAutorBeneficiarioViewModel> ListarArquivoAutorBeneficiario(int idAutorBeneficiario);
        List<CorrespondenciaDAViewModel> ListarCorrespondenciaAutorBeneficiario(int idAutorBeneficiario);
        Task<int> SalvarCorrespondenciaAutorBeneficiario(CorrespondenciaDAViewModel json, int IdUsuario);
        Task AtualizarCorrespondenciaAutorBeneficiario(CorrespondenciaDAViewModel json, int IdUsuario);
        List<AutorDAViewModel> ListarAutoresPorNome(string nome);
        Task AtualizarNomeCapaPorAutor(NomeCapaViewModel nomeCapa);
        Task ExcluirCorrespondenciaAutorBeneficiario(int idCorrespondencia);
        CorrespondenciaDAViewModel ListarCorrespondenciaAutorBeneficiarioId(int idCorrespondencia);
        void ExcluirNomeCapaPorAutor(int idAutorBeneficiario);
        Task<int> IncluirNomeCapaPorAutor(NomeCapaViewModel json);
        NomeCapaViewModel ListarNomeCapaPorId(int? idNomeCapa);
    }
}
