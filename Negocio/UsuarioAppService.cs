using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using static CoBRA.Domain.Enum.Enum;

namespace CoBRA.Application
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly ILoginAppService _loginAppService;
        private readonly IPermissaoRequisicaoService _permissaoRequisicaoService;

        public UsuarioAppService(IMapper mapper, IUsuarioRepository usuarioRepository, IMenuRepository menuRepository, ILoginAppService loginAppService, IPermissaoRequisicaoService permissaoService)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _menuRepository = menuRepository;
            _loginAppService = loginAppService;
            _permissaoRequisicaoService = permissaoService;
        }

        public string Adicionar(UsuarioViewModel usuario)
        {
            var user = _mapper.Map<Usuario>(usuario);
            
            foreach (var idGrupo in usuario.GruposAcesso)
            {
                user.Grupos.Add(new Grupo { Id = idGrupo });
            }

            return _usuarioRepository.Adicionar(user);
        }

        public string AtivarUsuario(UsuarioViewModel usuario)
        {
            var user = _mapper.Map<Usuario>(usuario);

            return _usuarioRepository.Ativar(user);
        }

        public string Editar(UsuarioViewModel usuario)
        {
            var user = _mapper.Map<Usuario>(usuario);

            foreach (var idGrupo in usuario.GruposAcesso)
            {
                user.Grupos.Add(new Grupo { Id = idGrupo });
            }
            
            return _usuarioRepository.Editar(user);
        }

        public async Task<UsuarioViewModel> Login(string conta)
        {
            var usuario = _mapper.Map<UsuarioViewModel>(_usuarioRepository.Login(conta, _loginAppService.ValidaEmail(conta)));
            
            if (usuario != null)
            {
                await CarregarPermissaoUsuario(usuario);
                usuario.Grupo.Menus = _mapper.Map<List<MenuViewModel>>(_menuRepository.ObterMenusPorGrupoId(usuario.Grupo.Id));

                for (int i = 0; i < usuario.Grupos.Count; i++)
                {
                    usuario.Grupos[i].Menus = _mapper.Map<List<MenuViewModel>>(_menuRepository.ObterMenusPorGrupoId(usuario.Grupos[i].Id));
                }
            }

            return usuario;
        }

        private async Task CarregarPermissaoUsuario(UsuarioViewModel usuario)
        {
            var permissaoRequisicao = await _permissaoRequisicaoService.FiltrarPermissaoUsuario(new PermissaoRequisicaoViewModel
            {
                Usuario = usuario
            });

            usuario.Permissoes = new PermissoesUsuarioViewModel
            {
                AprovaRequisicaoSupervisor = permissaoRequisicao.First().AprovaRequisicaoSupervisor,
                ReprovaRequisicaoSupervisor = permissaoRequisicao.First().ReprovaRequisicaoSupervisor,
                CancelaRequisicaoSupervisor = permissaoRequisicao.First().CancelaRequisicaoSupervisor,
                AprovaRequisicaoGerente = permissaoRequisicao.First().AprovaRequisicaoGerente,
                ReprovaRequisicaoGerente = permissaoRequisicao.First().ReprovaRequisicaoGerente,
                CancelaRequisicaoGerente = permissaoRequisicao.First().CancelaRequisicaoGerente,
            };
        }

        public UsuarioViewModel Obter(int id)
        {
            return _mapper.Map<UsuarioViewModel>(_usuarioRepository.Obter(id));
        }

        public List<UsuarioViewModel> Colaboradores(int idHierarquia, int idUsuario, int idEstado = 0)
        {
            List<UsuarioViewModel> listaUsuarios = new List<UsuarioViewModel>();

            if (idHierarquia == (int)Hierarquia.GerenteComercial)
                listaUsuarios = ObterColaboradoresGerente(idUsuario, idEstado);
            else if (idHierarquia == (int)Hierarquia.Diretoria)
                listaUsuarios = ObterDivulgadoresDiretoria(idEstado);
            else
                listaUsuarios = ObterColaboradores(idUsuario, idEstado);

            return listaUsuarios;
        }

        public List<UsuarioViewModel> ObterColaboradores(int idUsuario, int idEstado = 0)
        {
            return _mapper.Map<List<UsuarioViewModel>>(_usuarioRepository.ObterColaboradores(idUsuario, idEstado));
        }

        public List<UsuarioViewModel> ObterColaboradoresGerente(int idUsuario, int idEstado = 0)
        {
            return _mapper.Map<List<UsuarioViewModel>>(_usuarioRepository.ObterColaboradoresGerente(idUsuario, idEstado));
        }

        public List<UsuarioViewModel> ObterDivulgadoresDiretoria(int idEstado = 0)
        {
            return _mapper.Map<List<UsuarioViewModel>>(_usuarioRepository.ObterDivulgadoresDiretoria(idEstado));
        }

        public List<FilialViewModel> ObterFiliais()
        {
            return _mapper.Map<List<FilialViewModel>>(_usuarioRepository.ObterFiliais());
        }

        public List<UsuarioViewModel> ObterFiltrados(string nome, string email, int grupoId)
        {
            return _mapper.Map<List<UsuarioViewModel>>(_usuarioRepository.ObterFiltrados(nome,email, grupoId));
        }

        public List<UsuarioViewModel> ObterTodos()
        {
            return _mapper.Map<List<UsuarioViewModel>>(_usuarioRepository.ObterTodos());
        }

        public string FavoritarUsuario(UsuarioRamalViewModel usuario)
        {
            var user = _mapper.Map<UsuarioRamal>(usuario);

            return _usuarioRepository.FavoritarUsuario(user);
        }

        public string ExcluirUsuarioFavoritado(int id)
        {
            return _usuarioRepository.ExcluirUsuarioFavoritado(id);
        }

        public List<UsuarioRamalViewModel> ListarUsuariosFavoritados(int idUsuario)
        {
            return _mapper.Map<List<UsuarioRamalViewModel>>(_usuarioRepository.ListarUsuariosFavoritados(idUsuario));
        }

        public IList<int> ObterGruposUsuario(int idUsuario) 
        {
            var grupos = _usuarioRepository.ObterGruposUsuario(idUsuario);
            var idGrupos = new List<int>();

            foreach (var grupo in grupos)
            {
                idGrupos.Add(grupo.Id);
            }

            return idGrupos;
        }
    }
}
