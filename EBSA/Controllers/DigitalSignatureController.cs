using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoBRA.API.Controllers {
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class DigitalSignatureController : Controller {

        [HttpGet("token/validar")]
        public IActionResult ValidarToken() {
            try {
                Claim role = User.Claims.ToArray()[4]; //assinatura-digital

                if (role.Value.Contains("assinatura-digital")) {
                    string nomeUsuario = User.Claims.Where(c => c.Type.Equals(ClaimTypes.Name)).First().Value;
                    int idUsuario = Convert.ToInt32(User.Claims.Where(c => c.Type.Equals("Id")).First().Value);

                    return StatusCode(202, new { nomeUsuario, idUsuario });
                }

                return StatusCode(401, new { Error = "Role not valid" });

            } catch {
                
                return StatusCode(417);
            }
        }
    }
}
