using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;

namespace CoBRA.Application
{
    public class InicializacaoAppService : IInicializacaoAppService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IInicializacaoRepository _inicializacaoRepository;

        private readonly IUsuarioCorporeRepository _usuarioCorporeRepository;

        public InicializacaoAppService(IMapper mapper, IUsuarioRepository usuarioRepository, IUsuarioCorporeRepository usuarioCorporeRepository, IMenuRepository menuRepository, IInicializacaoRepository inicializacaoRepository)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _menuRepository = menuRepository;
            _inicializacaoRepository = inicializacaoRepository;
            _usuarioCorporeRepository = usuarioCorporeRepository;
        }

        public List<EstadoViewModel> ObterEstados()
        {
            return _mapper.Map<List<EstadoViewModel>>(_inicializacaoRepository.ObterEstados());
        }

        public List<MenuViewModel> ObterMenu()
        {
            return _mapper.Map<List<MenuViewModel>>(_inicializacaoRepository.ObterMenu());
        }

        public List<PeriodoViewModel> ObterPeriodos()
        {
            return _mapper.Map<List<PeriodoViewModel>>(_inicializacaoRepository.ObterPeriodos());
        }

        public string ObterPrevisaoDoTempo(string latitude, string longetude)
        {
            return _inicializacaoRepository.ObterPrevisaoDoTempo(latitude, longetude).ToString();
        }

        public List<UsuarioViewModel>ObterAniversariantes()
        {
            return _mapper.Map<List<UsuarioViewModel>>(_usuarioCorporeRepository.ObterAniversariantes());
        }
        public List<UsuarioViewModel> ObterAdmissoes()
        {
            return _mapper.Map<List<UsuarioViewModel>>(_usuarioCorporeRepository.ObterAdmissoes());
        }
        public List<UsuarioViewModel> ObterRamais()
        {
            return _mapper.Map<List<UsuarioViewModel>>(_inicializacaoRepository.ObterRamais());
        }

        public object ObterUsuario(int id)
        {
            var usuario = _usuarioRepository.Obter(id);
            var gruposUsuario = _usuarioRepository.ObterGruposUsuario(usuario.Id);

            var obj = new
            {
                usuario = new
                {
                    id = usuario.Id,
                    nome = usuario.Nome,
                    email = usuario.Email,
                    grupo = new
                    {
                        id = usuario.Grupo.Id,
                        descricao = usuario.Grupo.Descricao,
                        departamento = usuario.Grupo.Departamento,
                        centroCusto = usuario.Grupo.CentroCusto
                    },

                },
                grupos = gruposUsuario,
                Menu = MontarListaMenu(gruposUsuario),
            };

            return obj;
        }

        private HashSet<Menu> MontarListaMenu(IList<Grupo> grupos) 
        {
            var menus = new HashSet<Menu>();

            if (grupos is null)
                return menus;

            foreach (var grupo in grupos)
            {
                var menusGrupo = _menuRepository.ObterMenusPorGrupoId(grupo.Id);
                foreach (var menu in menusGrupo)
                {
                    if(!menus.Any(m => m.Id == menu.Id))
                    menus.Add(menu);
                }
            }

            return menus;
        }
    }
}
