using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CoBRA.API.Commands;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoBRA.API.Controllers
{
    [Authorize("Bearer"), Authorize(Policy = "MenuAcesso")]
    [Route("api/[controller]")]
    [ApiController]
    public class RequisicaoComprasController : BaseController
    {
        public readonly IRequisicaoComprasAppService _requisicaoComprasAppService;
        public readonly IElvisAppService _elvisAppService;

        public RequisicaoComprasController(IRequisicaoComprasAppService requisicaoComprasAppService, ILogAppService logAppService, IElvisAppService elvisAppService) : base(logAppService)
        {
            _requisicaoComprasAppService = requisicaoComprasAppService;
            _elvisAppService = elvisAppService;
        }

        [HttpGet]
        public IActionResult RequisicaoCompras()
        {
            try
            {
                var usuarioId = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                var requisicoesCompras = _requisicaoComprasAppService.ObterRequisicoes(usuarioId);

                return Ok(requisicoesCompras);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, "Erro ao obter as requisições de compras.");
            }
        }

        [HttpPost("UpdateRequisicao")]
        public async System.Threading.Tasks.Task<ActionResult<RequisicaoUpdateCommand>> UpdateRequisicaoAsync([FromForm] string requisicao, [FromForm] IFormFile file)
        {
            try
            {
                var json = JsonConvert.DeserializeObject<RequisicaoAtualizada>(requisicao);
                _requisicaoComprasAppService.UpdateRequisicao(json);

                _requisicaoComprasAppService.UpdateParcelasRequisicao(json.IdRequisicao.ToString(), json.Parcelas);

                List<RequisicaoArquivosViewModel> lstArquivo = _elvisAppService.GetFiles(json.IdRequisicao);
                foreach (var item in lstArquivo)
                {
                    if (System.IO.File.Exists(item.Caminho + item.Nome))
                    {
                        System.IO.File.Delete(item.Caminho + item.Nome);
                    }
                }

                _elvisAppService.DeleteFileRequisicao(json.IdRequisicao);

                List<RequisicaoArquivosViewModel> arquivos = new List<RequisicaoArquivosViewModel>();
                long size = file.Length;

                
                    if (file.Length > 0)
                    {
                        string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("bin\\Debug\\netcoreapp2.2", "") + "Files\\Upload\\";
                        Guid guid = Guid.NewGuid();

                        using (var stream = System.IO.File.Create(filePath + guid + "_" + file.FileName))
                        {
                            await file.CopyToAsync(stream);
                        }

                        RequisicaoArquivosViewModel arq = new RequisicaoArquivosViewModel()
                        {
                            Requisicao = json.IdRequisicao,
                            Caminho = filePath,
                            Nome = guid + "_" + file.FileName
                        };

                        arq.Id = _elvisAppService.SetFile(arq);

                        arquivos.Add(arq);
                    }

                return Ok(json.IdRequisicao);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CancelarRequisicao")]
        public ActionResult<RequisicaoUpdateCommand> CancelarRequisicao(int idRequisicao, bool testeCancelamento)
        {
            try
            {
                if (testeCancelamento)
                {
                    var idUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                    _requisicaoComprasAppService.CancelaRequisicao(idRequisicao, idUsuario);
                    var ret = JsonConvert.SerializeObject("Requisição cancelada com sucesso");
                    return Ok(ret);
                }
                else
                {
                    var ret = JsonConvert.SerializeObject("Não foi possível cancelar a requisição. Favor entrar em contato com contabil financeiro");
                    return Ok(ret);
                }

            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GerarRequisicao")]
        public async System.Threading.Tasks.Task<ActionResult<RequisicaoGeradaCommand>> GerarRequisicaoAsync([FromForm] string requisicao, [FromForm] IFormFile file)
        {
            try
            {
                var json = JsonConvert.DeserializeObject<RequisicaoGerada>(requisicao);
                int idReq = _requisicaoComprasAppService.GerarRequisicao(json);

                List<RequisicaoArquivosViewModel> arquivos = new List<RequisicaoArquivosViewModel>();
                long size = file!= null ? file.Length : 0;


                if (file!= null && file.Length > 0)
                {
#if DEBUG
                    string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("bin\\Debug\\netcoreapp2.2", "") + "Files\\Upload\\";
#endif
#if (!DEBUG)
                        string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Files\\Upload\\";
#endif
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    Guid guid = Guid.NewGuid();

                    using (var stream = System.IO.File.Create(filePath + guid + "_" + file.FileName))
                    {
                        await file.CopyToAsync(stream);
                    }

                    RequisicaoArquivosViewModel arquivo = new RequisicaoArquivosViewModel()
                    {
                        Requisicao = idReq,
                        Caminho = filePath,
                        Nome = guid + "_" + file.FileName
                    };

                    arquivo.Id = _elvisAppService.SetFile(arquivo);

                    arquivos.Add(arquivo);
                }

                return Ok(idReq);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("BuscarRequisicao")]
        public IActionResult BuscarRequisicao(int idRequisicao)
        {
            try
            {
                var requisicaoCompras = _requisicaoComprasAppService.BuscarRequisicao(idRequisicao);
                requisicaoCompras.Arquivos = _elvisAppService.GetFiles(idRequisicao);
                requisicaoCompras.Parcelas = _requisicaoComprasAppService.ObterParcelasRequisicao(idRequisicao.ToString());

                return Ok(requisicaoCompras);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, "Erro ao obter as requisições de compras.");
            }
        }

        [HttpGet("ObterRequisicaoPorId")]
        public IActionResult ObterRequisicaoPorId(int id)
        {
            try
            {
                var requisicaoCompras = _requisicaoComprasAppService.ObterRequisicaoPorId(id);

                return Ok(requisicaoCompras);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, "Erro ao obter as requisições de compras.");
            }
        }

        [HttpGet("Servicos")]
        public IActionResult ObterServicos()
        {
            try
            {
                return Ok(_requisicaoComprasAppService.ObterServicos());
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ServicosCentroCusto")]
        public IActionResult ObterServicosCentroCusto([FromServices] IRequisicaoElvisAppService requisicaoElvis)
        {
            try
            {
                var usuarioId = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                return Ok(requisicaoElvis.ObterServicosPorCentroCusto(usuarioId));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Obras/{nome}")]
        public IActionResult ObterObras(string nome)
        {
            try
            {
                return Ok(_requisicaoComprasAppService.ObterObras(nome));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("RequisicaoGerada/{nota}")]
        public IActionResult RequisicaoGeradaPorNota(string nota)
        {
            try
            {
                var usuarioId = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                return Ok(_requisicaoComprasAppService.ObterRequisicaoGeradaPorNota(nota, usuarioId));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500);
            }
        }

        [HttpPost("AprovarRequisicao")]
        public ActionResult AprovarRequisicao([FromBody] RequisicaoAprovadaViewModel requisicao)
        {
            try
            {
                return Ok(_requisicaoComprasAppService.AprovarRequisicao(requisicao));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ReprovarRequisicao")]
        public ActionResult ReprovarRequisicao([FromBody] RequisicaoAprovadaViewModel requisicao)
        {
            try
            {
                return Ok(_requisicaoComprasAppService.ReprovarRequisicao(requisicao));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("AtualizarRequisicao")]
        public ActionResult AtualizarRequisicao([FromBody] RequisicaoAtualizadaViewModel requisicao)
        {
            try
            {
                return Ok(_requisicaoComprasAppService.AtualizarRequisicao(requisicao.id, requisicao.link));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ExcluirRequisicao")]
        public ActionResult ExcluirRequisicao([FromBody] RequisicaoExcluidaViewModel requisicao)
        {
            try
            {
                return Ok(_requisicaoComprasAppService.ExcluirRequisicao(requisicao));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("CentrosCusto")]
        public IActionResult ObterCentrosDeCusto()
        {
            try
            {
                return Ok(_requisicaoComprasAppService.ObterCentrosDeCusto());
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}