using System;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static CoBRA.Domain.Enum.Enum;

namespace CoBRA.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class UsuarioController : BaseController
    {
        public readonly IUsuarioAppService _usuarioAppService;
        public readonly ILogAppService _logAppService;

        public UsuarioController(IUsuarioAppService usuarioAppService, ILogAppService logAppService) : base(logAppService) 
        {
            _usuarioAppService = usuarioAppService;
            _logAppService = logAppService;
        }

        [HttpGet("colaboradores")]
        public IActionResult Colaboradores(int idEstado = 0)
        {
            try
            {
                int idHierarquia = ObterHierarquia();
                int idUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                
                return Ok(_usuarioAppService.Colaboradores(idHierarquia,idUsuario,idEstado));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet, Authorize(Policy = "MenuAcesso")]
        public IActionResult GetUsuarios()
        {
            try
            {
                return Ok(_usuarioAppService.ObterTodos());
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuario(int id)
        {
            try
            {
                return Ok(_usuarioAppService.Obter(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost, Authorize(Policy = "MenuAcesso")]
        public IActionResult AdicionarUsuario([FromBody] UsuarioViewModel usuario)
        {
            try
            {
                var retorno = _usuarioAppService.Adicionar(usuario);

                if (retorno != "Usuário cadastrado com sucesso!")
                    return StatusCode(400, retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut, Authorize(Policy = "MenuAcesso")]
        public IActionResult EditarUsuario([FromBody] UsuarioViewModel usuario)
        {
            try
            {
                var retorno = _usuarioAppService.Editar(usuario);

                if (retorno != "Usuário editado com sucesso!")
                    return StatusCode(400, retorno);

                return Ok(JsonConvert.SerializeObject(retorno));
            }
            catch (Exception ex)
            {
                var log = new LogViewModel()
                {
                    Data = DateTime.Now,
                    TipoLog = (int)TipoLog.Erro,
                    IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Descricao = "Erro no método: EditarUsuario. Classe: UsuarioController",
                    Erro = ex
                };

                _logAppService.GravarLog(log);

                return StatusCode(500);
            }
        }

        [HttpPut("ativar"), Authorize(Policy = "MenuAcesso")]
        public IActionResult Ativar([FromBody] UsuarioViewModel usuario)
        {
            try
            {
                var retorno = _usuarioAppService.AtivarUsuario(usuario);

                if (retorno != "Alterado com sucesso!")
                    return StatusCode(400, retorno);

                return Ok(JsonConvert.SerializeObject(retorno));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("filiais")]
        public IActionResult GetFiliais()
        {
            try
            {
                return Ok(_usuarioAppService.ObterFiliais());
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("filtrar"), Authorize(Policy = "MenuAcesso")]
        public IActionResult GetUsuario(string nome, string email, int grupo = 0)
        {
            try
            {
                return Ok(_usuarioAppService.ObterFiltrados(nome, email, grupo));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("adicionarFavoritado")]
        public IActionResult FavoritarUsuario([FromBody] UsuarioRamalViewModel usuario)
        {
            try
            {
                var retorno = _usuarioAppService.FavoritarUsuario(usuario);

                if (retorno != "Usuário favoritado com sucesso!")
                    return StatusCode(400, retorno);

                return Ok(JsonConvert.SerializeObject(retorno));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        
        [HttpGet("excluirFavoritado")]
        public IActionResult ExcluirUsuarioFavoritado(int id)
        {
            try
            {
                var retorno = _usuarioAppService.ExcluirUsuarioFavoritado(id);

                if (retorno != "Usuário favoritado deletado com sucesso!")
                    return StatusCode(400, retorno);

                return Ok(JsonConvert.SerializeObject(retorno));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("listarFavoritados")]
        public IActionResult ListarUsuariosFavoritados(int id)
        {
            try
            {
                return Ok(_usuarioAppService.ListarUsuariosFavoritados(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}