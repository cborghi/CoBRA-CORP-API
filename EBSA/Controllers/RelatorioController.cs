using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CoBRA.Application.Interfaces;
using static CoBRA.Domain.Enum.Enum;
using CoBRA.Application.ViewModels;

namespace CoBRA.API.Controllers
{
    [Authorize("Bearer"), Authorize(Policy = "MenuAcesso")]
    [Route("api/[controller]")]
    public class RelatorioController : BaseController {
        public readonly IRelatorioAppService _relatorioAppService;

        public RelatorioController(IRelatorioAppService relatorioAppService, ILogAppService logAppService) : base(logAppService) {
            _relatorioAppService = relatorioAppService;
        }

        [HttpGet("Comissao")]
        public IActionResult Index(int? qntMes, int idEstado = 0, string codDivulgador = "", string dataInicio = "", string dataFim = "") {
            try {
                DateTime dataIni = new DateTime();
                DateTime dataF = new DateTime();

                if (qntMes is null) {
                    if (String.IsNullOrEmpty(dataInicio) || String.IsNullOrEmpty(dataFim)) {
                        return StatusCode(400, "Data inicio e data fim devem ser enviadas.");
                    } else {
                        if (!DateTime.TryParse(dataInicio, out dataIni) || !DateTime.TryParse(dataFim, out dataF))
                            return StatusCode(400, "Formato de data inválido.");
                    }
                }

                int idHierarquia = ObterHierarquia();

                int idUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                string idDivulgador = ObterClaim(x => x.Type == "Id").Value;
                return Ok(_relatorioAppService.ObterRelatorioComissao(qntMes, idEstado, idUsuario, idHierarquia, codDivulgador, dataIni, dataF, idDivulgador));
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ComissaoDetalhes")]
        public IActionResult ComissaoDetalhes(int? qntMes, int idEstado = 0, string codDivulgador = "", string datainicio = "", string datafim = "") {
            try {
                DateTime dataIni = new DateTime();
                DateTime dataF = new DateTime();

                if (qntMes is null) {
                    if (String.IsNullOrEmpty(datainicio) || String.IsNullOrEmpty(datafim)) {
                        return StatusCode(400, "Data inicio e data fim devem ser enviadas.");
                    } else {
                        if (!DateTime.TryParse(datainicio, out dataIni) || !DateTime.TryParse(datafim, out dataF))
                            return StatusCode(400, "Formado de data inválido.");
                    }
                }

                if (String.IsNullOrEmpty(codDivulgador))
                    return StatusCode(400, "O divulgador deve ser informado.");

                int idHierarquia = ObterHierarquia();
                int idDivulgador = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                return Ok(_relatorioAppService.ObterRelatorioComissaoDetalhes(qntMes, idEstado, idHierarquia, codDivulgador, dataIni, dataF, idDivulgador));
            } catch (Exception ex) {
                return StatusCode(500);
            }
        }

        [HttpGet("GerarRelatorio")]
        public IActionResult GerarRelatorio(int? qntMes, int idEstado = 0, string codDivulgador = "", string datainicio = "", string datafim = "") {
            try {
                DateTime dataIni = new DateTime();
                DateTime dataF = new DateTime();

                if (qntMes is null) {
                    if (String.IsNullOrEmpty(datainicio) || String.IsNullOrEmpty(datafim)) {
                        return StatusCode(400, "Data inicio e data fim devem ser enviadas.");
                    } else {
                        if (!DateTime.TryParse(datainicio, out dataIni) || !DateTime.TryParse(datafim, out dataF))
                            return StatusCode(400, "Formado de data inválido.");
                    }
                }

                int idHierarquia = ObterHierarquia();
                int idDivulgador = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                byte[] arquivo = _relatorioAppService.GerarExcelRelatorioComissao(qntMes, idEstado, idHierarquia, codDivulgador, dataIni, dataF, idDivulgador);
                return File(arquivo, "application/octet-stream", "Relatorio.csv");
            } catch (Exception ex) {
                return StatusCode(500);
            }
        }
               
        [HttpGet("ComissaoProtheus")]
        public IActionResult ComissaoProtheus(int? qntMes, int idEstado = 0, string codDivulgador = "", string dataInicio = "", string dataFim = "") {
            try {
                DateTime dataIni = new DateTime();
                DateTime dataF = new DateTime();

                if (qntMes is null) {
                    if (String.IsNullOrEmpty(dataInicio) || String.IsNullOrEmpty(dataFim)) {
                        return StatusCode(400, "Data inicio e data fim devem ser enviadas.");
                    } else {
                        if (!DateTime.TryParse(dataInicio, out dataIni) || !DateTime.TryParse(dataFim, out dataF))
                            return StatusCode(400, "Formato de data inválido.");
                    }
                }

                int idHierarquia = ObterHierarquia();

                int idUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                string idDivulgador = ObterClaim(x => x.Type == "Id").Value;

                return Ok(_relatorioAppService.ObterRelatorioComissaoProtheus(qntMes, idEstado, idUsuario, idHierarquia, codDivulgador, dataIni, dataF, idDivulgador));

            } catch (Exception ex) {
                
                var comissaoProtheusLog = new LogViewModel() {
                    Data = DateTime.Now,
                    TipoLog = (int)TipoLog.Erro,
                    IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Usuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value),
                    Descricao = $"ComissaoProtheus. Erro: {ex.Message} StackTrace:{ex.StackTrace}"
                };

                _logAppService.GravarLog(comissaoProtheusLog);
                
                return StatusCode(500, ex.Message);

            }

        }
        
        [HttpGet("ComissaoDetalhesProtheus")]
        public IActionResult ComissaoDetalhesProtheus(int? qntMes, int idEstado = 0, string codDivulgador = "", string datainicio = "", string datafim = "") {
            try {
                DateTime dataIni = new DateTime();
                DateTime dataF = new DateTime();

                if (qntMes is null) {
                    if (String.IsNullOrEmpty(datainicio) || String.IsNullOrEmpty(datafim)) {
                        return StatusCode(400, "Data inicio e data fim devem ser enviadas.");
                    } else {
                        if (!DateTime.TryParse(datainicio, out dataIni) || !DateTime.TryParse(datafim, out dataF))
                            return StatusCode(400, "Formado de data inválido.");
                    }
                }

                if (String.IsNullOrEmpty(codDivulgador))
                    return StatusCode(400, "O divulgador deve ser informado.");

                int idHierarquia = ObterHierarquia();

                int idDivulgador = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);

                return Ok(_relatorioAppService.ObterRelatorioComissaoDetalhesProtheus(qntMes, idEstado, idHierarquia, codDivulgador, dataIni, dataF, idDivulgador));
                
            } catch (Exception ex) {

                return StatusCode(500);

            }
        }
        
        [HttpGet("GerarRelatorioProtheus")]
        public IActionResult GerarRelatorioProtheus(int? qntMes, int idEstado = 0, string codDivulgador = "", string datainicio = "", string datafim = "") {

            try {

                DateTime dataIni = new DateTime();
                DateTime dataF = new DateTime();

                if (qntMes is null) {
                    if (String.IsNullOrEmpty(datainicio) || String.IsNullOrEmpty(datafim)) {

                        return StatusCode(400, "Data inicio e data fim devem ser enviadas.");

                    } else {
                        if (!DateTime.TryParse(datainicio, out dataIni) || !DateTime.TryParse(datafim, out dataF))

                            return StatusCode(400, "Formado de data inválido.");

                    }
                }

                int idHierarquia = ObterHierarquia();
                int idDivulgador = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);

                byte[] arquivo = _relatorioAppService.GerarExecelRelatorioComissaoProtheus(qntMes, idEstado, idHierarquia, codDivulgador, dataIni, dataF, idDivulgador);

                return File(arquivo, "application/octet-stream", "Relatorio.csv");

            } catch (Exception ex) {

                return StatusCode(500);

            }
        }
    }
}
