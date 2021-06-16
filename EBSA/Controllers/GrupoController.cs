using CoBRA.API.Commands;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace CoBRA.API.Controllers
{
    [Authorize("Bearer"), Authorize(Policy = "MenuAcesso")]
    [Route("api/[controller]")]
    public class GrupoController : BaseController
    {
        private readonly IGrupoAppService _grupoAppService;
        private readonly IMenuAppService _menuAppService;
        private readonly IDepartamentoAppService _departamentoAppService;
        private readonly IUsuarioAppService _usuarioAppService;

        public GrupoController(IGrupoAppService grupoAppService, IMenuAppService menuAppService, IDepartamentoAppService departamentoAppService, ILogAppService logAppService, IUsuarioAppService usuarioAppService) : base(logAppService)
        {
            _grupoAppService = grupoAppService;
            _menuAppService = menuAppService;
            _departamentoAppService = departamentoAppService;
            _usuarioAppService = usuarioAppService;
        }

        [HttpGet]
        public IActionResult ListarGrupos()
        {
            try
            {
                var listaGrupos = _grupoAppService.ObterTodos();

                return Ok(listaGrupos);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        
        [HttpGet("ad")]
        public IActionResult ObterGruposAD()
        {
            try
            {
                List<GrupoCommand> grupos = new List<GrupoCommand>();

                using (DirectoryEntry entry = new DirectoryEntry(@"LDAP://192.168.1.13", "ebsa.api", "E93bs@125"))
                {
                    DirectorySearcher dSearch = new DirectorySearcher(entry);
                    dSearch.Filter = "(&(objectClass=group))";
                    dSearch.SearchScope = SearchScope.Subtree;

                    SearchResultCollection results = dSearch.FindAll();


                    for (int i = 0; i < results.Count; i++)
                    {
                        DirectoryEntry de = results[i].GetDirectoryEntry();
                        grupos.Add(new GrupoCommand()
                        {
                            IdGrupoAD = de.Guid,
                            Descricao = de.Name
                        });
                    }

                }

                return Ok(grupos);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }

        [HttpGet("menus")]
        public IActionResult ListarTodosMenus()
        {
            try
            {
                var listaMenus = _menuAppService.ObterTodos();

                return Ok(listaMenus);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("menusGrupo/{id}")]
        public IActionResult ListarMenuPorGrupo(int id)
        {
            try
            {
                
                var listaMenus = _menuAppService.ObterMenusPorGrupo(id);

                return Ok(listaMenus);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("gruposUsuario/{idUsuario}")]
        public IActionResult GruposUsuario(int idUsuario)
        {
            try
            {
                const int ID_NOVO_USUARIO = 0;

                if (idUsuario == ID_NOVO_USUARIO)
                    return Ok(new int[]{ });

                var idGruposUsuario = _usuarioAppService.ObterGruposUsuario(idUsuario);

                return Ok(idGruposUsuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("departamento")]
        public IActionResult ListarDepartamentos()
        {
            try
            {
                var listaDepts = _departamentoAppService.ObterTodos();

                return Ok(listaDepts);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody]GrupoViewModel grupo)
        {
            try
            {
                //recuperar id dos menus selecionados no frontend
                grupo.Menus = _menuAppService.RecuperarIdsMenus(grupo.Menus);

                //adiciona o grupo novo e adiciona os menus permitidos ao perfil criado na tabela MENU_PERFIL
                var retorno = _grupoAppService.Adicionar(grupo);

                if (retorno == "Grupo já cadastrado!" || retorno == "Erro ao obter id do grupo!")
                    return StatusCode(400, retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public IActionResult Editar([FromBody] GrupoViewModel grupo)
        {
            try
            {
                //recuperar id dos menus selecionados no frontend
                grupo.Menus = _menuAppService.RecuperarIdsMenus(grupo.Menus);

                //edita o grupo novo e atualiza os menus permitidos ao grupo editado na tabela MENU_GRUPO
                var retorno = _grupoAppService.Editar(grupo);

                if (retorno == "Grupo não cadastrado!")
                    return StatusCode(400, retorno);

                return Ok(JsonConvert.SerializeObject(retorno));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("ativar")]
        public IActionResult Ativar([FromBody] GrupoViewModel grupo)
        {
            try
            {
                _grupoAppService.AtivarGrupo(grupo);

                return Ok(JsonConvert.SerializeObject("Alterado com sucesso!"));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}