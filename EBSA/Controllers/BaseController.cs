using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace CoBRA.API.Controllers
{
    public class BaseController : Controller
    {
        public readonly ILogAppService _logAppService;


        public BaseController(ILogAppService logAppService) {
            _logAppService = logAppService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public Claim ObterClaim(Predicate<Claim> condicao)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                return identity.FindFirst(condicao);
            }
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public int ObterHierarquia()
        {
            Claim claim = this.ObterClaim(x => x.Type == ClaimTypes.Role);

            if (claim is null) return 0;

            return Convert.ToInt32(claim.Value);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void GravarLog(Exception ex)
        {
            var log = new LogViewModel();
            log.Data = DateTime.Now;
            log.Descricao = $"Erro: { ex.Message}. Local: { ex.StackTrace}";
            log.Erro = ex;
            _logAppService.GravarLog(log);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void GravarLog(LogViewModel log)
        {
            _logAppService.GravarLog(log);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected void CarregarPermissoesAbaUsuario()
        {
            //var permissoesUsuario = HttpContext.Session.GetObject<PermissaoUsuario>("Permissao");

            //IList<Aba> abas = permissoesUsuario.Abas;

            //CarregarPermissaoAbaProduto(abas);

            //CarregarPermissaoAbaAutoria(abas);

            //CarregarPermissaoAbaEspecificacaoTecnica(abas);

            //CarregarPermissaoAbaCondicaoesComerciais(abas);

            //CarregarPermissaoAbaHistorico(abas);
        }
    }
}
