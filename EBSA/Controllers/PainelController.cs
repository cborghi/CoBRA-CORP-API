using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoBRA.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class PainelController : BaseController
    {
        public readonly IPainelAppService _painelAppService;

        public PainelController(IPainelAppService painelAppService, ILogAppService logAppService) : base(logAppService)
        {
            _painelAppService = painelAppService;
        }

        [HttpPost("InserirPainel")]
        public async Task<IActionResult> InserirPainel([FromBody] CabecalhoPainelMetaViewModel painel)
        {
            try
            {
                await _painelAppService.InserirPainel(painel);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("BuscarPainel")]
        public async Task<IActionResult> BuscarPainel([FromBody] CabecalhoPainelMetaViewModel painel, Guid? PeriodoId)
        {
            try
            {
                IEnumerable<CabecalhoPainelMetaViewModel> cabecalho =
                    await _painelAppService.BuscarPainel(painel, PeriodoId);
                
                if (cabecalho != null)
                {
                    foreach (var item in cabecalho)
                    {
                        if (item.GrupoPainel.IdGrupo == new Guid("C4DF435E-B292-4017-A991-7E9CEA57BA2D"))
                            item.IdCargo = new Guid("00000000-0000-0000-0000-000000000000");
                        else if (item.GrupoPainel.IdGrupo == new Guid("E5A35298-057A-4E90-99B3-6CEF0CEDA6F2"))
                            item.IdCargo = new Guid("76ED75B2-437D-48E3-BFAF-34EE309BB399");
                        else if (item.GrupoPainel.IdGrupo == new Guid("15C33E35-145D-4554-B21C-F1953E43CC57"))
                            item.IdCargo = new Guid("B32F70B8-60E4-4BD4-BC51-2C66D403496E");
                    }
                }
                //cabecalho
                return Ok(cabecalho);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("BuscarPeriodo")]
        public async Task<IActionResult> BuscarPeriodo()
        {
            try
            {
                IEnumerable<PeriodoCampanhaViewModel> cabecalho =
                    await _painelAppService.BuscarPeriodo();
                return Ok(cabecalho);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("FiltrarPainel")]
        public async Task<IActionResult> FiltrarPainel([FromBody] CabecalhoPainelMetaViewModel painel)
        {
            try
            {
                IEnumerable<CabecalhoPainelMetaViewModel> cabecalho =
                    await _painelAppService.FiltrarPainel(painel);
                return Ok(cabecalho);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("BuscarLinha")]
        public async Task<IActionResult> BuscarLinha([FromBody] LinhaPainelMetaViewModel painel)
        {
            try
            {
                IEnumerable<LinhaPainelMetaViewModel> linhas =
                    await _painelAppService.BuscarLinhaPainel(painel);
                return Ok(linhas);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("AtualizarLinha")]
        public async Task<IActionResult> AtualizarLinha([FromBody] LinhaPainelMetaViewModel linha)
        {
            try
            {
                await _painelAppService.AtualizarLinhaPainel(linha);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ExcluirLinhaPainel")]
        public async Task<IActionResult> ExcluirLinhaPainel([FromBody] LinhaPainelMetaViewModel linha)
        {
            try
            {
                await _painelAppService.ExcluirLinhaPainel(linha);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("EnviarPainelAprovacao")]
        public async Task<IActionResult> EnviarPainelAprovacao([FromBody] CabecalhoPainelMetaViewModel cabecalho)
        {
            try
            {
                await _painelAppService.EnviarPainelAprovacao(cabecalho);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ExcluirCabecalhoPainel")]
        public async Task<IActionResult> ExcluirCabecalhoPainel([FromBody] CabecalhoPainelMetaViewModel cabecalho)
        {
            try
            {
                await _painelAppService.ExcluirCabecalhoPainel(cabecalho);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ObterPesoTotalMeta")]
        public async Task<IActionResult> ObterPesoTotalMeta([FromBody] CabecalhoPainelMetaViewModel cabecalho)
        {
            try
            {
                byte pesoTotal = await _painelAppService.ObterPesoTotalMeta(cabecalho);
                return Ok(pesoTotal);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("VerificarMetaCadastrada")]
        public async Task<IActionResult> VerificarMetaCadastrada([FromBody] CabecalhoPainelMetaViewModel cabecalho)
        {
            try
            {
                bool metaCadastrada = await _painelAppService.VerificarMetaCadastrada(cabecalho);
                return Ok(metaCadastrada);

            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}

