using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System.Collections.Generic;

namespace CoBRA.Application
{
    public class MenuAppService : IMenuAppService
    {
        private readonly IMapper _mapper;
        private readonly IMenuRepository _menuRepository;

        public MenuAppService(IMapper mapper, IMenuRepository menuRepository)
        {
            _mapper = mapper;
            _menuRepository = menuRepository;
        }

        public List<MenuViewModel> ObterMenusPorGrupo(int grupoId)
        {
            return _mapper.Map<List<MenuViewModel>>(_menuRepository.ObterMenusPorGrupoId(grupoId));
        }

        public List<MenuViewModel> ObterTodos(bool submenus = true)
        {
            return _mapper.Map<List<MenuViewModel>>(_menuRepository.ObterMenus(submenus));
        }

        public List<MenuViewModel> RecuperarIdsMenus(List<MenuViewModel> listaMenus)
        {
            var lista = _mapper.Map<List<Menu>>(listaMenus);

            return _mapper.Map<List<MenuViewModel>>(_menuRepository.ObterIdsMenus(lista));
        }
    }
}
