using System;
using System.Threading.Tasks;
using CoBRA.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoBRA.API.Controllers
{
    // [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class AcompanhamentoMetaLookupController : BaseController
    {
        private IAcompanhamentoMetaLookupAppService _acompanhamentoMetaLookup;

        public AcompanhamentoMetaLookupController(
            IAcompanhamentoMetaLookupAppService acompanhamentoMetaLookup,
            ILogAppService logAppService) : base(logAppService)
        {
            _acompanhamentoMetaLookup = acompanhamentoMetaLookup;
        }

        [HttpGet("ListarLookupCargo")]
        public async Task<IActionResult> ListarLookupCargo()
        {
            try
            {
                var result = await _acompanhamentoMetaLookup.ListarLookupCargo();
                Guid idUsuarioRM = new Guid();

                return Ok(new { idUsuarioRM, cargos = result });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ObterLookupCargo/{idUsuario}")]
        public async Task<IActionResult> ObterLookupCargo(int idUsuario)
        {
            try
            {
                var result = await _acompanhamentoMetaLookup.ObterLookupCargo(idUsuario);
                Guid idUsuarioRM = await _acompanhamentoMetaLookup.ObterIdUsuarioRM(idUsuario);

                return Ok(new {  idUsuarioRM,  cargos = result });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ObterLookupUf")]
        public async Task<IActionResult> ObterLookupUf()
        {
            try
            {
                var result = await _acompanhamentoMetaLookup.ObterLookupUf();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ObterLookupNome")]
        public async Task<IActionResult> ObterLookupNome(string idCargo, string idRegiao, string idUsuario)
        {
            try
            {
                var result = await _acompanhamentoMetaLookup.ObterLookupNome(idCargo, idRegiao, idUsuario);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ObterLookupNomeGeral")]
        public async Task<IActionResult> ObterLookupNome(string idRegiao, string idCargo)
        {
            try
            {
                var result = await _acompanhamentoMetaLookup.ObterLookupNomeGeral(idRegiao, idCargo);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

