using System;
using CoBRA.API.Commands;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoBRA.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class RequisicaoElvisController : BaseController
    {
        public readonly IRequisicaoElvisAppService _requisicaoElvisAppService;

        public RequisicaoElvisController(IRequisicaoElvisAppService requisicaoElvisAppService, ILogAppService logAppService) : base(logAppService)
        {
            _requisicaoElvisAppService = requisicaoElvisAppService;
        }

        [HttpGet("Requisicao/{tipo}")]
        public IActionResult RequisicaoElvis(string tipo)
        {
            try
            {
                return Ok(_requisicaoElvisAppService.ObterRequisicoes(tipo));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("Requisicao")]
        public IActionResult RequisicaoElvis(int ID)
        {
            try
            {
                return Ok(_requisicaoElvisAppService.ObterRequisicoesPorID(ID));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{nota}")]
        public IActionResult RequisicaoElvisPorNota(string nota)
        {
            try
            {
                var usuarioId = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                return Ok(_requisicaoElvisAppService.ObterRequisicaoPorNota(nota, usuarioId));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("RequisicaoGerada/{nota}")]
        public IActionResult RequisicaoAbdGeradaPorNota(string nota)
        {
            try
            {

                var usuarioId = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                return Ok(_requisicaoElvisAppService.ObterRequisicaoGeradaPorNota(nota, usuarioId));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("Fornecedores/{nome}")]
        public IActionResult FornecedoresABD(string nome)
        {
            try
            {
                return Ok(_requisicaoElvisAppService.ObterFornecedores(nome));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        [HttpPost("GerarRequisicao")]
        public ActionResult<RequisicaoGeradaCommand> GerarRequisicao([FromBody]RequisicaoGeradaViewModel requisicao)
        {
            try
            {
                return Ok(_requisicaoElvisAppService.GerarRequisicao(requisicao, true));
            }catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
    }

}
