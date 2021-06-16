using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoBRA.API.Controllers
{

    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class CadastroProdutoController : Controller
    {
        private readonly ICadastroProdutoService _cadastroProdutoService;
        public readonly INetlitAppService _netlitAppService;

        public CadastroProdutoController(ICadastroProdutoService cadastroProdutoService, INetlitAppService netlitAppService)
        {
            _cadastroProdutoService = cadastroProdutoService;
            _netlitAppService = netlitAppService;
        }

        [HttpGet]
        public IActionResult ValidarToken()
        {
            try
            {
                Claim role = User.Claims.ToArray()[4];
                
                if (role.Value.Contains("produto"))
                {
                    string nomeUsuario = User.Claims.Where(c => c.Type.Equals(ClaimTypes.Name)).First().Value;
                    int idUsuario = Convert.ToInt32(User.Claims.Where(c => c.Type.Equals("Id")).First().Value);

                    return Ok(new { nomeUsuario, idUsuario });
                }

                return StatusCode((int)HttpStatusCode.Unauthorized);
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpPost("GerarCodigoEbsa")]
        public async Task<IActionResult> GerarCodigoEbsa([FromBody] ProdutoViewModel viewModel)
        {
            try
            {
                string codigoEbsa = await _cadastroProdutoService.GerarCodigoEbsa(viewModel);

                return Ok(codigoEbsa);
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpPost("SalvarCodigoEbsa")]
        public async Task<IActionResult> SalvarCodigoEbsa([FromBody] ProdutoViewModel viewModel)
        {
            try
            {
                await _cadastroProdutoService.SalvarCodigoEbsa(viewModel);

                return Ok();
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpGet("CarregarInformacoesTela")]
        public async Task<IActionResult> CarregarInformacoesTela()
        {
            try
            {
                CadastroProdutoViewModel informacoesTela = await _cadastroProdutoService.CarregarInformacoesTela();

                return Ok(informacoesTela);
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpGet("Consultar")]
        public async Task<IActionResult> Consultar(long ID)
        {
            try
            {
                return Ok(await _cadastroProdutoService.Consultar(ID));
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                return Ok(await _cadastroProdutoService.Listar());
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpGet("ListarIdEscola")]
        public async Task<IActionResult> ListarIdEscola(int IdEscola)
        {
            try
            {
                return Ok(await _cadastroProdutoService.ListarIdEscola(IdEscola));
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpGet("ListarControleConteudo")]
        public async Task<IActionResult> ListarControleConteudo(int IdEscola)
        {
            try
            {
                return Ok(await _cadastroProdutoService.ListarControleConteudo(IdEscola));
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpGet("BloquearEscola")]
        public async Task<IActionResult> BloquearEscola(int IdEscola, int IdProduto)
        {
            try
            {
                await _cadastroProdutoService.BloquearEscola(IdEscola, IdProduto);
                return Ok();
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpGet("CarregarConteudoNetLitPP")]
        public async Task<IActionResult> CarregarConteudoNetLitPP(int NumeroPagina, int RegistrosPagina)
        {
            try
            {
                var Conteudo = await _netlitAppService.CarregarConteudoNetLitPP(NumeroPagina, RegistrosPagina, "");
                return Ok(Conteudo);
            }
            catch (Exception ex)
            {
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("Assuntos")]
        public async Task<IActionResult> Assuntos()
        {
            try
            {
                return Ok(await _cadastroProdutoService.Assuntos());
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpGet("Autores")]
        public async Task<IActionResult> Autores()
        {
            try
            {
                return Ok(await _cadastroProdutoService.Autores());
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpGet("Conteudos")]
        public async Task<IActionResult> Conteudos()
        {
            try
            {
                return Ok(await _cadastroProdutoService.Conteudos());
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.ExpectationFailed);
            }
        }
    }
}