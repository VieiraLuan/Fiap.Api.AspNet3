using Fiap.Api.AspNet3.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.AspNet3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            var client = new CursoClient();

            var cursos =  client.GetAll().Result;

            return "Olá - GET";
        }


        [HttpPost]
        [Authorize(Roles = "Pleno,Senior")]
        public void Post()
        {
          
        }


        [HttpPut]
        [Authorize(Roles = "Pleno,Senior")]
        public void  Put()
        {
           
        }


        [HttpDelete]
        [Authorize(Roles = "Pleno,Senior")]
        public bool Delete()
        {
          return true;
        }


    }
}
