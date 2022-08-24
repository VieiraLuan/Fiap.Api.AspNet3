using Fiap.Api.AspNet3.Models;
using Fiap.Api.AspNet3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Fiap.Api.AspNet3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Senior")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] UsuarioModel usuarioModel)
        {
            if (usuarioModel.Senha.Equals("123456"))
            {
                usuarioModel.NomeUsuario = "Luan";
                usuarioModel.Regra = "Senior";

                var token = AuthenticationService.GetToken(usuarioModel);

                return new
                {
                    usuario =  usuarioModel,
                    token = token 
                };
            }
            else
            {
                return NotFound();
            }


        }

        
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
