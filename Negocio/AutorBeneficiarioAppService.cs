using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class AutorBeneficiarioAppService : IAutorBeneficiarioAppService
    {
        private readonly IMapper _mapper;
        private readonly IAutorBeneficiarioRepository _autorBeneficiarioRepository;

        public AutorBeneficiarioAppService(IAutorBeneficiarioRepository autorBeneficiarioRepository, IMapper mapper)
        {
            _autorBeneficiarioRepository = autorBeneficiarioRepository;
            _mapper = mapper;
        }

        public async Task<int> SalvarAutor(AutorDAViewModel autorBeneficiario)
        {
            var a = _mapper.Map<AutorDA>(autorBeneficiario);
            var idAutorBeneficiario = await _autorBeneficiarioRepository.SalvarAutor(a);

            await _autorBeneficiarioRepository.SalvarLogAutorBeneficiario(idAutorBeneficiario, a.IdUsuarioInclusao, "Inclusão do Autor " + a.Nome);
            foreach (var item in a.LstNomeCapa)
            {
                await _autorBeneficiarioRepository.SalvarNomeCapa(item.NomeCapaDescricao, idAutorBeneficiario);
            }
            foreach (var item2 in a.LstEmail)
            {
                await _autorBeneficiarioRepository.SalvarEmail(item2.Destinatario, idAutorBeneficiario);
            }

            return idAutorBeneficiario;
        }

        public async Task AtualizarAutor(AutorDAViewModel autorBeneficiario)
        {
            var a = _mapper.Map<AutorDA>(autorBeneficiario);
            await _autorBeneficiarioRepository.AtualizarAutor(a);

            await _autorBeneficiarioRepository.SalvarLogAutorBeneficiario(a.IdAutorBeneficiario, a.IdUsuarioInclusao, "Alteração do Autor " + a.Nome);

            foreach (var item in a.LstNomeCapa)
            {
                await _autorBeneficiarioRepository.SalvarNomeCapa(item.NomeCapaDescricao, autorBeneficiario.IdAutorBeneficiario);
            }

            await _autorBeneficiarioRepository.ExcluirEmail(autorBeneficiario.IdAutorBeneficiario);
            foreach (var item2 in a.LstEmail)
            {
                await _autorBeneficiarioRepository.SalvarEmail(item2.Destinatario, a.IdAutorBeneficiario);
            }
        }

        public AutorDAViewModel BuscarAutorPorId(int id)
        {
            AutorDAViewModel retorno = _mapper.Map<AutorDAViewModel>(_autorBeneficiarioRepository.BuscarAutorPorId(id));
            retorno.LstLogAutorBeneficiario = _mapper.Map<List<LogAutorBeneficiarioViewModel>>(_autorBeneficiarioRepository.BuscarLogAutorBeneficiarioPorId(id));
            retorno.LstNomeCapa = _mapper.Map<List<NomeCapaViewModel>>(_autorBeneficiarioRepository.ListarNomeCapaPorAutor(id));
            retorno.LstEmail = _mapper.Map<List<EmailAutorBeneficiarioViewModel>>(_autorBeneficiarioRepository.ListarEmailPorAutor(id));
            return retorno;
        }

        public BeneficiarioDAViewModel BuscarBeneficiarioPorId(int id)
        {
            BeneficiarioDAViewModel retorno = _mapper.Map<BeneficiarioDAViewModel>(_autorBeneficiarioRepository.BuscarBeneficiarioPorId(id));
            retorno.LstAutores = _mapper.Map<List<AutorDAViewModel>>(_autorBeneficiarioRepository.BuscarAutoresBeneficiarioPorId(id));
            retorno.LstLogAutorBeneficiario = _mapper.Map<List<LogAutorBeneficiarioViewModel>>(_autorBeneficiarioRepository.BuscarLogAutorBeneficiarioPorId(id));
            retorno.LstNomeCapa = _mapper.Map<List<NomeCapaViewModel>>(_autorBeneficiarioRepository.ListarNomeCapaPorAutor(id));
            retorno.LstEmail = _mapper.Map<List<EmailAutorBeneficiarioViewModel>>(_autorBeneficiarioRepository.ListarEmailPorAutor(id));
            return retorno;
        }

        public async Task<int> SalvarBeneficiario(BeneficiarioDAViewModel autorBeneficiario)
        {
            var a = _mapper.Map<BeneficiarioDA>(autorBeneficiario);
            var idAutorBeneficiario = await _autorBeneficiarioRepository.SalvarBeneficiario(a);

            await _autorBeneficiarioRepository.SalvarLogAutorBeneficiario(idAutorBeneficiario, a.IdUsuarioInclusao, "Inclusão do Beneficiário " + a.Nome);

            if (a.IdTipoCadastro == 2)
            {
                foreach (var item in a.LstAutores)
                {
                    await _autorBeneficiarioRepository.SalvarAutoresBeneficiario(item.IdAutorBeneficiario, idAutorBeneficiario);
                }
            }

            foreach (var item in a.LstNomeCapa)
            {
                await _autorBeneficiarioRepository.SalvarNomeCapa(item.NomeCapaDescricao, idAutorBeneficiario);
            }

            foreach (var item2 in a.LstEmail)
            {
                await _autorBeneficiarioRepository.SalvarEmail(item2.Destinatario, a.IdAutorBeneficiario);
            }

            return idAutorBeneficiario;
        }

        public async Task AtualizarBeneficiario(BeneficiarioDAViewModel autorBeneficiario)
        {
            var a = _mapper.Map<BeneficiarioDA>(autorBeneficiario);
            await _autorBeneficiarioRepository.AtualizarBeneficiario(a);

            await _autorBeneficiarioRepository.SalvarLogAutorBeneficiario(a.IdAutorBeneficiario, a.IdUsuarioInclusao, "Alteração do Beneficiário " + a.Nome);

            if (a.IdTipoCadastro == 2)
            {
                await _autorBeneficiarioRepository.ExcluirAutoresBeneficiario(a.IdAutorBeneficiario);
                foreach (var item in a.LstAutores)
                {
                    await _autorBeneficiarioRepository.SalvarAutoresBeneficiario(item.IdAutorBeneficiario, a.IdAutorBeneficiario);
                }
            }

            foreach (var item in a.LstNomeCapa)
            {
                await _autorBeneficiarioRepository.SalvarNomeCapa(item.NomeCapaDescricao, autorBeneficiario.IdAutorBeneficiario);
            }

            await _autorBeneficiarioRepository.ExcluirEmail(autorBeneficiario.IdAutorBeneficiario);
            foreach (var item2 in a.LstEmail)
            {
                await _autorBeneficiarioRepository.SalvarEmail(item2.Destinatario, a.IdAutorBeneficiario);
            }
        }

        public List<TipoContaBancariaViewModel> ListarContaBancaria()
        {
            List<TipoContaBancaria> lstConta = _autorBeneficiarioRepository.ListarContaBancaria();

            List<TipoContaBancariaViewModel> retorno = new List<TipoContaBancariaViewModel>();
            foreach (var item in lstConta)
            {
                TipoContaBancariaViewModel r = new TipoContaBancariaViewModel();
                r.IdTipoConta = item.IdTipoConta;
                r.Descricao = item.Descricao;
                retorno.Add(r);
            }

            return retorno;
        }

        public List<EstadoCivilViewModel> ListarEstadoCivil()
        {
            List<EstadoCivil> lstEstado = _autorBeneficiarioRepository.ListarEstadoCivil();

            List<EstadoCivilViewModel> retorno = new List<EstadoCivilViewModel>();
            foreach (var item in lstEstado)
            {
                EstadoCivilViewModel r = new EstadoCivilViewModel();
                r.IdEstadoCivil = item.IdEstadoCivil;
                r.Descricao = item.Descricao;
                retorno.Add(r);
            }

            return retorno;
        }

        public List<AutorDAViewModel> ListarAutores(string filtro)
        {
            List<AutorDAViewModel> retorno = _mapper.Map<List<AutorDAViewModel>>(_autorBeneficiarioRepository.ListarAutores(filtro));
            return retorno;
        }

        public List<EstadoViewModel> ListarEstados()
        {
            List<EstadoViewModel> retorno = _mapper.Map<List<EstadoViewModel>>(_autorBeneficiarioRepository.ListarEstados());
            return retorno;
        }

        public AutoresBeneficiariosPaginadoViewModel ListarAutoresBeneficiarios(int? idTipoCadastro, string tipoPessoa, int? idEstado, bool? ativo, int numeroPagina, int registrosPagina)
        {
            AutoresBeneficiariosPaginadoViewModel retorno = _mapper.Map<AutoresBeneficiariosPaginadoViewModel>(_autorBeneficiarioRepository.ListarAutoresBeneficiarios(idTipoCadastro, tipoPessoa, idEstado, ativo, numeroPagina, registrosPagina));
            retorno.RegistrosPagina = registrosPagina;
            retorno.NumeroPagina = numeroPagina;

            int resultado = System.Math.DivRem(retorno.Contagem, registrosPagina, out int resto);

            retorno.QtdePaginas = resto != 0 ? resultado + 1 : resultado;

            return retorno;
        }

        public async Task SalvarArquivoAutorBeneficiario(ArquivoAutorBeneficiarioViewModel arquivo)
        {
            ArquivoAutorBeneficiario arq = new ArquivoAutorBeneficiario
            {
                IdArquivo = arquivo.IdArquivo,
                IdAutorBeneficiario = arquivo.IdAutorBeneficiario,
                Nome = arquivo.Nome,
                CaminhoArquivo = arquivo.CaminhoArquivo,
                DataCadastro = arquivo.DataCadastro
            };
            await _autorBeneficiarioRepository.SalvarArquivoAutorBeneficiario(arq);
        }

        public void ExcluirArquivoAutorBeneficiario(int idAutorBeneficiario, string nomeArquivo)
        {
            _autorBeneficiarioRepository.ExcluirArquivoAutorBeneficiario(idAutorBeneficiario, nomeArquivo);
        }

        public List<ArquivoAutorBeneficiarioViewModel> ListarArquivoAutorBeneficiario(int idAutorBeneficiario)
        {
            List<ArquivoAutorBeneficiarioViewModel> retorno = _mapper.Map<List<ArquivoAutorBeneficiarioViewModel>>(_autorBeneficiarioRepository.ListarArquivoAutorBeneficiario(idAutorBeneficiario));
            foreach (var item in retorno)
            {
                item.Nome = item.Nome.Substring(36);
            }
            return retorno;
        }

        public List<CorrespondenciaDAViewModel> ListarCorrespondenciaAutorBeneficiario(int idAutorBeneficiario)
        {
            List<CorrespondenciaDAViewModel> retorno = _mapper.Map<List<CorrespondenciaDAViewModel>>(_autorBeneficiarioRepository.ListarCorrespondenciaAutorBeneficiario(idAutorBeneficiario));
            foreach (var item in retorno)
            {
                item.LstLog = _mapper.Map<List<LogCorrespAutorBeneficiarioViewModel>>(_autorBeneficiarioRepository.BuscarLogCorrespAutorBeneficiarioPorId(item.IdCorrespondencia));
            }
            return retorno;
        }

        public async Task<int> SalvarCorrespondenciaAutorBeneficiario(CorrespondenciaDAViewModel json, int idUsuario)
        {
            var a = _mapper.Map<CorrespondenciaDA>(json);
            var IdCorrespondencia = await _autorBeneficiarioRepository.SalvarCorrespondenciaAutorBeneficiario(a);

            await _autorBeneficiarioRepository.SalvarLogCorrespAutorBeneficiario(IdCorrespondencia, idUsuario, "Inclusão de Correspondencia " + IdCorrespondencia);
            return IdCorrespondencia;
        }

        public async Task AtualizarCorrespondenciaAutorBeneficiario(CorrespondenciaDAViewModel json, int IdUsuario)
        {
            var a = _mapper.Map<CorrespondenciaDA>(json);

            await _autorBeneficiarioRepository.SalvarLogCorrespAutorBeneficiario(a.IdCorrespondencia, IdUsuario, "Alteração de Correspondencia " + a.IdCorrespondencia);
            await _autorBeneficiarioRepository.AtualizarCorrespondenciaAutorBeneficiario(a);
        }

        public List<AutorDAViewModel> ListarAutoresPorNome(string nome)
        {
            List<AutorDAViewModel> retorno = _mapper.Map<List<AutorDAViewModel>>(_autorBeneficiarioRepository.ListarAutoresPorNome(nome));
            foreach (var item in retorno)
            {
                item.LstNomeCapa = _mapper.Map<List<NomeCapaViewModel>>(_autorBeneficiarioRepository.ListarNomeCapaPorAutorDet(item.IdAutorBeneficiario));
            }
            return retorno;
        }
        
        public async Task AtualizarNomeCapaPorAutor(NomeCapaViewModel nomeCapa)
        {
            NomeCapa entrada = _mapper.Map<NomeCapa>(nomeCapa);
            await _autorBeneficiarioRepository.AtualizarNomeCapaPorAutor(entrada);
        }

        public void ExcluirNomeCapaPorAutor(int idNomeCapa)
        {
            _autorBeneficiarioRepository.ExcluirNomeCapaPorAutor(idNomeCapa);
        }

        public async Task ExcluirCorrespondenciaAutorBeneficiario(int idCorrespondencia)
        {
            await _autorBeneficiarioRepository.ExcluirCorrespondenciaAutorBeneficiario(idCorrespondencia);
        }

        public CorrespondenciaDAViewModel ListarCorrespondenciaAutorBeneficiarioId(int idCorrespondencia)
        {
            CorrespondenciaDAViewModel retorno = _mapper.Map<CorrespondenciaDAViewModel>(_autorBeneficiarioRepository.ListarCorrespondenciaAutorBeneficiarioId(idCorrespondencia));
            retorno.LstLog = _mapper.Map<List<LogCorrespAutorBeneficiarioViewModel>>(_autorBeneficiarioRepository.BuscarLogCorrespAutorBeneficiarioPorId(idCorrespondencia));
            
            return retorno;
        }

        public async Task<int> IncluirNomeCapaPorAutor(NomeCapaViewModel json)
        {
            NomeCapa entrada = _mapper.Map<NomeCapa>(json);
            return await _autorBeneficiarioRepository.IncluirNomeCapaPorAutor(entrada);
        }

        public NomeCapaViewModel ListarNomeCapaPorId(int? idNomeCapa)
        {
            return _mapper.Map<NomeCapaViewModel>(_autorBeneficiarioRepository.ListarNomeCapaPorId(idNomeCapa)[0]);
        }
    }
}
