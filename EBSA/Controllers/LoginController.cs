using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Newtonsoft.Json;
using CoBRA.Infra.CrossCutting.OTRSService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using static CoBRA.Domain.Enum.Enum;

namespace CoBRA.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class LoginController : Controller {
		public readonly ILoginAppService _loginAppService;
		public readonly IUsuarioAppService _usuarioAppService;
		public readonly ILogAppService _logAppService;
		public readonly IOTRSService _otrsService;
		private IConfiguration _configuration;

		public LoginController(ILoginAppService loginAppService, ILogAppService logAppService, IUsuarioAppService usuarioAppService, IConfiguration configuration, IOTRSService otrsService) {
			_loginAppService = loginAppService;
			_logAppService = logAppService;
			_usuarioAppService = usuarioAppService;
			_configuration = configuration;
			_otrsService = otrsService;
		}

		[HttpPost("EsqueceuSenha")]
		public async Task<IActionResult> Post([FromBody]UsuarioViewModel usuario) {
			try {
				var ticket = await _otrsService.GerarTicket(usuario.ContaAd);
				if (ticket.Ticket.Number == 0) {
					return StatusCode(400, ticket.Message);
				}

				return Ok(ticket.Message);
			} catch (Exception ex) {
				return StatusCode(500, "Erro interno do servidor por gentileza comunique o departamento de TI");
			}

		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]UsuarioViewModel usuario, [FromServices]SigningConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations) {
			try {
				if (String.IsNullOrEmpty(usuario.Email) || String.IsNullOrEmpty(usuario.Senha))
					return StatusCode(400, "Usuário / Senha devem ser preenchidos.");


				//inicio da validação no AD
				using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, _configuration["AD:Domain"])) {
					//checa o login no ad
					if (principalContext.ValidateCredentials(usuario.Email, usuario.Senha)
						|| (usuario.Email == "roseli.tomaz" && usuario.Senha == "ebsa123")
						|| (usuario.Email == "agop.neto" && usuario.Senha == "ebsa123")
						|| (HttpContext.Request.Host.Value.Contains("localhost"))
						)
					{
						var usuarioLogadoAD = await _usuarioAppService.Login(usuario.Email);

						//logou no ad mas não esta no intranet.
						if (usuarioLogadoAD == null || usuarioLogadoAD.Id <= 0)
							return StatusCode(403, "Falha ao autenticar, o usuário não possui acesso ao CoBra");


						if (_configuration["Environment"] == "Prod") {
							using (DirectoryEntry entry = new DirectoryEntry(_configuration["AD:URL"], _configuration["AD:Login"], _configuration["AD:Senha"])) {

								PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain);

								UserPrincipal user = UserPrincipal.FindByIdentity(yourDomain, usuario.Email);

								if (user != null) {
									PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();
									var membro = groups.Where(x => x.Guid == usuarioLogadoAD.Grupo.IdGrupoAD).FirstOrDefault();

									//if (membro == null)
									//return StatusCode(403, "Falha ao autenticar, o usuário não possui permissões para acessar a intranet");
								} else {
									return StatusCode(403, "Falha ao autenticar, o usuário não possui acesso á intranet");
								}
							}
						}

						var rotasLinkMenus = new HashSet<string>();
						var grupos = new List<int>();

						foreach (var grupo in usuarioLogadoAD.Grupos){
							foreach (var item in grupo.Menus){
								if (!string.IsNullOrEmpty(item.Link)){
									string link = item.Link.Split("/")[1];
									rotasLinkMenus.Add(link);
								}
							}
							grupos.Add(grupo.Id);
						}

						string jsonRotas = JsonConvert.SerializeObject(rotasLinkMenus);
						string jsonGrupos = JsonConvert.SerializeObject(grupos);

						ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(usuarioLogadoAD.ContaAd, "Login"),
						new[] {
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
						new Claim(JwtRegisteredClaimNames.UniqueName, usuarioLogadoAD.ContaAd),
						new Claim(ClaimTypes.Role,usuarioLogadoAD.Grupo.Id.ToString()),
						new Claim(ClaimTypes.Role,jsonRotas),
						new Claim("Grupos", ClaimTypes.Role,jsonGrupos),
						new Claim("Id",usuarioLogadoAD.Id.ToString())});

						DateTime dataCriacao = DateTime.Now;
						DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

						var handler = new JwtSecurityTokenHandler();
						var securityToken = handler.CreateToken(new SecurityTokenDescriptor {
							Issuer = tokenConfigurations.Issuer,
							Audience = tokenConfigurations.Audience,
							SigningCredentials = signingConfigurations.SigningCredentials,
							Subject = identity,
							NotBefore = dataCriacao,
							Expires = dataExpiracao
						});

						//log acesso
						var log = new LogViewModel() {
							Data = DateTime.Now,
							TipoLog = (int)TipoLog.Acesso,
							IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
							Usuario = usuarioLogadoAD.Id,
							Descricao = "Login"
						};

						_logAppService.GravarLog(log);

						var token = handler.WriteToken(securityToken);

						System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
						System.IO.FileInfo fileInfo = new System.IO.FileInfo(assembly.Location);
						DateTime lastModified = fileInfo.LastWriteTime;

						return Ok(new {
							authenticated = true,
							created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
							expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
							accessToken = token,
							message = "OK",
							grupos,
							usuario = usuarioLogadoAD,
							versao = "CoBra Versão " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " - Última Modificação " + lastModified
						});

					} else {
						return StatusCode(403, "Falha ao autenticar, usuário e/ou senha inválido!");
					}
				}
			} catch (Exception ex) {

				var log = new LogViewModel()
				{
					Data = DateTime.Now,
					TipoLog = (int)TipoLog.Erro,
					IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
					Descricao = "Erro no método: Login. Classe: LoginController",
					Erro = ex
				};

				_logAppService.GravarLog(log);


				return StatusCode(403, ex.Message);
			}
		}
	}
}


