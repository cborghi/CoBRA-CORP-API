using System;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CoBRA.Domain.Enum.Enum;

namespace CoBRA.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class InicializacaoController : BaseController
    {
        private readonly IInicializacaoAppService _inicializacaoAppService;
        public readonly ILogAppService _logAppService;

        public InicializacaoController(IInicializacaoAppService inicializacaoAppService, ILogAppService logAppService) : base(logAppService)
        {
            _inicializacaoAppService = inicializacaoAppService;
            _logAppService = logAppService;
        }

        [HttpGet]
        public IActionResult Inicializacao()
        {
            try
            {
                var usuario = _inicializacaoAppService.ObterUsuario(Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value));

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                var log = new LogViewModel()
                {
                    Data = DateTime.Now,
                    TipoLog = (int)TipoLog.Erro,
                    IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Descricao = "Erro no método: Inicializacao. Classe: InicializacaoController",
                    Erro = ex
                };

                _logAppService.GravarLog(log);

                return StatusCode(500);
            }
        }

        //[Authorize(Policy = "MenuAcesso")]
        [HttpGet("Estados")]
        public IActionResult ObterEstados()
        {            
            try
            {
                var listaEstados = _inicializacaoAppService.ObterEstados();

                return Ok(listaEstados);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        //[Authorize(Policy = "MenuAcesso")]
        [HttpGet("Periodos")]
        public IActionResult ObterPeriodos()
        {
            try
            {
                var listaPeriodos = _inicializacaoAppService.ObterPeriodos();

                return Ok(listaPeriodos);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("Aniversariantes")]
        public IActionResult ObterAniversariantes()
        {
            try
            {
                var aniversariantes = _inicializacaoAppService.ObterAniversariantes();

                return Ok(aniversariantes);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("Admissoes")]
        public IActionResult ObterAdmissoes()
        {
            try
            {
                var admissoes = _inicializacaoAppService.ObterAdmissoes();

                return Ok(admissoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("PrevisaoTempo")]
        public IActionResult ObterPrevisaoDoTempo(string latitude, string longetude)
        {
            try
            {
                var jsonPrevisao = _inicializacaoAppService.ObterPrevisaoDoTempo(latitude, longetude);

                return Ok(jsonPrevisao);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("Ramais")]
        public IActionResult ObterRamais()
        {
            try
            {
                var listaRamais = _inicializacaoAppService.ObterRamais();

                return Ok(listaRamais);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

    }
}

