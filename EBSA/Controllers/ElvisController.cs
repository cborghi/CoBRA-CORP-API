using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CoBRA.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ElvisController : BaseController
    {
        public readonly IElvisAppService _elvisAppService;

        public ElvisController(IElvisAppService elvisAppService, ILogAppService logAppService) : base(logAppService)
        {
            _elvisAppService = elvisAppService;
        }

        [HttpGet("ArquivosRequisicao")]
        public IActionResult GetFiles(long ID)
        {
            try
            {
                List<RequisicaoArquivosViewModel> arquivos = _elvisAppService.GetFiles(ID);

                return Ok(arquivos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao obter os arquivos da requisição.");
            }
        }

        [HttpPost("IncluirArquivoRequisicao/{ID}")]
        public async Task<IActionResult> PostFile(long ID, List<IFormFile> files)
        {
            try
            {
                List<RequisicaoArquivosViewModel> arquivos = new List<RequisicaoArquivosViewModel>();
                long size = files.Sum(f => f.Length);

                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("bin\\Debug\\netcoreapp2.2", "") + "Files\\Upload\\";
                        Guid guid = Guid.NewGuid();

                        using (var stream = System.IO.File.Create(filePath + guid + "_" + formFile.FileName))
                        {
                            await formFile.CopyToAsync(stream);
                        }

                        RequisicaoArquivosViewModel arquivo = new RequisicaoArquivosViewModel()
                        {
                            Requisicao = ID,
                            Caminho = filePath,
                            Nome = guid + "_" + formFile.FileName
                        };

                        arquivo.Id = _elvisAppService.SetFile(arquivo);

                        arquivos.Add(arquivo);
                    }
                }

                return Ok(arquivos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao carregar o arquivo para upload.");
            }
        }

        [HttpGet("ExcluirArquivoRequisicao")]
        public IActionResult RemoveFile(long ID)    
        {
            try
            {
                RequisicaoArquivosViewModel arquivo = _elvisAppService.DeleteFile(ID);

                if (System.IO.File.Exists(arquivo.Caminho + arquivo.Nome))
                {
                    System.IO.File.Delete(arquivo.Caminho + arquivo.Nome);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao excluir o arquivo da requisição.");
            }
        }

        public class Arquivo
        {
            public string caminho { get; set; }
        }

        [HttpPost("DownloadArquivoRequisicao")]
        public IActionResult DownloadArquivo([FromBody] Arquivo arquivo)
        {
            try
            {
                FileStream file = null;

                if (System.IO.File.Exists(arquivo.caminho))
                {
                    file = System.IO.File.OpenRead(arquivo.caminho);
                }

                byte[] result = new byte[file.Length];

                file.Read(result, 0, result.Length);

                if (Path.GetExtension(arquivo.caminho).Equals(".pdf"))
                {
                    return Ok(File(fileContents: result,
                                contentType: "application/pdf",
                                fileDownloadName: Path.GetFileName(arquivo.caminho)
                                ));
                }
                else
                {
                    return Ok(File(fileContents: result,
                               contentType: "image/" + Path.GetExtension(arquivo.caminho).Replace(".", ""),
                               fileDownloadName: Path.GetFileName(arquivo.caminho)
                               ));
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}