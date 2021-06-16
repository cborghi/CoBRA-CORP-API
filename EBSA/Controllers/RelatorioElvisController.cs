using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CoBRA.Domain.Enum.Enum;

namespace CoBRA.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize("Bearer")]
    public class RelatorioElvisController : BaseController
    {
        private readonly IRelatorioElvisAppService _relatorioElvisAppService;
        
        public RelatorioElvisController(IRelatorioElvisAppService relatorioElvisAppService, ILogAppService logAppService): base(logAppService)
        {
            _relatorioElvisAppService = relatorioElvisAppService;
        }


        [HttpGet("ListarTodos")]
        public async Task<IActionResult> ListarRelatorio()
        {
            try
            {
                IList<RelatorioElvisViewModel> relatorio = await 
                    _relatorioElvisAppService.ListarRelatorio();

                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                var log = new LogViewModel()
                {
                    Data = DateTime.Now,
                    TipoLog = (int)TipoLog.Erro,
                    IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Descricao = "Erro no método: ListarRelatorio. Classe: RelatorioElvisController",
                    Erro = ex
                };

                //_logAppService.GravarLog(log);
                base._logAppService.GravarLog(log);

                return StatusCode(500);
            }
        }

        [HttpPost("FiltrarRelatorio")]
        public async Task<IActionResult> FiltrarRelatorio([FromBody] FiltroRelatorioElvisViewModel filtro)
        {
            try
            {
                IList<RelatorioElvisViewModel> relatorioRetorno = await
                    _relatorioElvisAppService.FiltrarRelatorio(filtro);

                return Ok(relatorioRetorno);
            }
            catch (Exception ex)
            {
                var log = new LogViewModel()
                {
                    Data = DateTime.Now,
                    TipoLog = (int)TipoLog.Erro,
                    IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Descricao = "Erro no método: FiltrarRelatorio. Classe: RelatorioElvisController",
                    Erro = ex
                };

                _logAppService.GravarLog(log);
                return StatusCode(500);
            }
        }

        [HttpPost("ListarObraAndamento")]
        public async Task<IActionResult> ListarObraAndamento([FromBody] FiltroObraAndamentoViewModel filtro)
        {
            try
            {
                IList<ObraAndamentoViewModel> relatorio = await
                    _relatorioElvisAppService.FiltrarRelatorioObrasAndamento(filtro);

                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                var log = new LogViewModel()
                {
                    Data = DateTime.Now,
                    TipoLog = (int)TipoLog.Erro,
                    IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Descricao = "Erro no método: ListarObraAndamento. Classe: RelatorioElvisController",
                    Erro = ex
                };

                _logAppService.GravarLog(log);

                return StatusCode(500);
            }
        }

        [HttpGet("CarregarPrograma")]
        public async Task<IActionResult> CarregarPrograma()
        {
            try
            {
                IList<FiltroRelatorioElvisViewModel> filtro = await
                    _relatorioElvisAppService.CarregarPrograma();

                return Ok(filtro);
            }
            catch (Exception ex)
            {
                var log = new LogViewModel()
                {
                    Data = DateTime.Now,
                    TipoLog = (int)TipoLog.Erro,
                    IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Descricao = "Erro no método: CarregarPrograma. Classe: RelatorioElvisController",
                    Erro = ex
                };

                _logAppService.GravarLog(log);

                return StatusCode(500);
            }
        }

        [HttpGet("CarregarProgramaObraAndamento")]
        public async Task<IActionResult> CarregarProgramaObraAndamento()
        {
            try
            {
                IList<FiltroRelatorioElvisViewModel> filtro = await
                    _relatorioElvisAppService.CarregarProgramaObraAndamento();

                return Ok(filtro);
            }
            catch (Exception ex)
            {
                var log = new LogViewModel()
                {
                    Data = DateTime.Now,
                    TipoLog = (int)TipoLog.Erro,
                    IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Descricao = "Erro no método: CarregarProgramaObraAndamento. Classe: RelatorioElvisController",
                    Erro = ex
                };

                _logAppService.GravarLog(log);

                return StatusCode(500);
            }
        }

        [HttpGet("CarregarDisciplina")]
        public async Task<IActionResult> CarregarDisciplina()
        {
            try
            {
                IList<FiltroRelatorioElvisViewModel> filtro = await
                    _relatorioElvisAppService.CarregarDisciplina();

                return Ok(filtro);
            }
            catch (Exception ex)
            {
                var log = new LogViewModel()
                {
                    Data = DateTime.Now,
                    TipoLog = (int)TipoLog.Erro,
                    IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Descricao = "Erro no método: CarregarDisciplina. Classe: RelatorioElvisController",
                    Erro = ex
                };

                _logAppService.GravarLog(log);

                return StatusCode(500);
            }
        }

        [HttpGet("CarregarColecao")]
        public async Task<IActionResult> CarregarColecao()
        {
            try
            {
                IList<FiltroRelatorioElvisViewModel> filtro = await
                    _relatorioElvisAppService.CarregarColecao();

                return Ok(filtro);
            }
            catch (Exception ex)
            {
                var log = new LogViewModel()
                {
                    Data = DateTime.Now,
                    TipoLog = (int)TipoLog.Erro,
                    IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Descricao = "Erro no método: CarregarColecao. Classe: RelatorioElvisController",
                    Erro = ex
                };

                _logAppService.GravarLog(log);

                return StatusCode(500);
            }
        }

        [HttpGet("CarregarAnoEscolar")]
        public async Task<IActionResult> CarregarAnoEscolar()
        {
            try
            {
                IList<FiltroRelatorioElvisViewModel> filtro = await
                    _relatorioElvisAppService.CarregarAnoEscolar();

                return Ok(filtro);
            }
            catch (Exception ex)
            {
                var log = new LogViewModel()
                {
                    Data = DateTime.Now,
                    TipoLog = (int)TipoLog.Erro,
                    IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Descricao = "Erro no método: CarregarAnoEscolar. Classe: RelatorioElvisController",
                    Erro = ex
                };

                _logAppService.GravarLog(log);

                return StatusCode(500);
            }
        }
    }
}
