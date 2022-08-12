using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.AspNet3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "Olá - GET";
        }


        [HttpPost]
        public void Post()
        {
          
        }


        [HttpPut]
        public void  Put()
        {
           
        }


        [HttpDelete]
        public bool Delete()
        {
          return true;
        }


    }
}
