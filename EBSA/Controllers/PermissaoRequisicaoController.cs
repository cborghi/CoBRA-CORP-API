using CoBRA.API.Commands;
using CoBRA.Application;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CoBRA.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class PermissaoRequisicaoController : ControllerBase
    {
        private readonly IPermissaoRequisicaoService _requisicaoService;

        public PermissaoRequisicaoController(IPermissaoRequisicaoService requisicaoService)
        {
            _requisicaoService = requisicaoService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarPermissaoUsuario()
        {
            try
            {
                int idUsuario = Convert.ToInt32(User.Claims.Where(c => c.Type.Equals("Id")).First().Value);
                IList<PermissaoRequisicaoViewModel> permissoes =  await _requisicaoService.ListarPermissaoUsuario(new UsuarioViewModel 
                { 
                    Id = idUsuario 
                });

                return Ok(permissoes);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.ExpectationFailed, ex);
            }
        }

        [HttpPost("FiltrarPermissaoUsuario")]
        public async Task<IActionResult> FiltrarPermissaoUsuario([FromBody] PermissaoRequisicaoViewModel permissao)
        {
            try
            {
                IList<PermissaoRequisicaoViewModel> permissoes = await _requisicaoService.FiltrarPermissaoUsuario(permissao);

                return Ok(permissoes);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.ExpectationFailed, ex);
            }
        }

        [HttpPost("GravarPermissaoUsuario")]
        public async Task<IActionResult> GravarPermissaoUsuario([FromBody] PermissaoRequisicaoCommand permissao)
        {
            try
            {
                int idUsuario = Convert.ToInt32(User.Claims.Where(c => c.Type.Equals("Id")).First().Value);
                string ipRequisicao = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                await _requisicaoService.GravarPermissaoUsuario(new PermissaoRequisicaoViewModel 
                { 
                    Usuario = new UsuarioViewModel { Id = permissao.idUsuario },
                    AprovaRequisicaoSupervisor = permissao.AprovaRequisicaoSupervisor,
                    ReprovaRequisicaoSupervisor = permissao.ReprovaRequisicaoSupervisor,
                    CancelaRequisicaoSupervisor = permissao.CancelaRequisicaoSupervisor,
                    AprovaRequisicaoGerente = permissao.AprovaRequisicaoGerente,
                    ReprovaRequisicaoGerente = permissao.ReprovaRequisicaoGerente,
                    CancelaRequisicaoGerente = permissao.CancelaRequisicaoGerente,
                },
                idUsuario, ipRequisicao);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.ExpectationFailed, ex);
            }
        }
    }
}
