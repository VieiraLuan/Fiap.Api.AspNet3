using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.AspNet3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AcessoTestController : ControllerBase
    {
        [AllowAnonymous]
        [Route("Anonimo")]
        public string Anonimo()
        {
            return "Anonimo";
        }

        [Route("Autenticado")]
     
        public string Autenticado() 
        {
            return "Autenticado";
        }

        [Route("Junior")]
        [Authorize(Roles = "Junior, Pleno, Senior")]
        public string Junior() 
        {

            return "Junior";
        }


        [Route("Pleno")]
        [Authorize(Roles = "Pleno, Senior")]
        public string Pleno()
        {

            return "Pleno";
        }

        [Route("Senior")]
        [Authorize(Roles = "Senior")]
        public string Senior()
        {

            return "Senior";
        }
    }
}
