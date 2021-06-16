using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace CoBRA.API.Authorize
{
    public class AccessPerfilHandler : AuthorizationHandler<PerfilRequirement>
    {
        private readonly IHttpContextAccessor contextAccessor;

        public AccessPerfilHandler(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PerfilRequirement requirement)
        {
            HttpContext httpcontext = contextAccessor.HttpContext;

            var path = httpcontext.Request.Path.ToString().Split('/')[2].ToLower();

            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                return Task.CompletedTask;
            }

            IEnumerable<Claim> roles = context.User.Claims.Where(c => c.Type == ClaimTypes.Role);

            var identity = httpcontext.User.Identity as ClaimsIdentity;

            if (identity != null && roles.FirstOrDefault(x => x.Value.ToLower().Contains(path)) != null)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}