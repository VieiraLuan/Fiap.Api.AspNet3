using Fiap.Api.AspNet3.Client;
using Fiap.Api.AspNet3.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fiap.Api.AspNet3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly CursoClient _cursoClient;
        public CursoController(CursoClient cursoClient)
        {
            _cursoClient = cursoClient;
        }

        [HttpGet]
        public async Task<ActionResult<IList<CursoModel>>> Get()
        {

            var cursos = await _cursoClient.GetAll();

            if (cursos != null)
            {
                return Ok(cursos);
            }
            else
            {
                return NoContent();
            }

        }


       [HttpGet("{id}")]
        public async Task<ActionResult<CursoModel>> Get([FromBody] CursoModel cursoModel, [FromRoute] int id)
        {
            if (cursoModel.Id == null && id == null)
            {
                return BadRequest("Informe um ID ou um cursoModel com ID válido");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Informe um cursoModel válido");
            }

            if (id != cursoModel.Id)
            {
                return BadRequest("Os IDs informados não são iguais");
            }
            var curso = await _cursoClient.Get(id);

            if (curso != null)
            {
                return Ok(curso);
            }
            else
            {
                return NoContent();
            }

        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CursoModel cursoModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("O cursoModel não é válido");
            }

            if (cursoModel == null)
            {
                return BadRequest("O cursoModel está vazio");
            }

            await _cursoClient.Insert(cursoModel);

            var location = new Uri(Request.GetEncodedUrl() + "/" + cursoModel.Id);

            return Created(location, cursoModel);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CursoModel cursoModel)
        {

            if (id != cursoModel.Id)
            {
                return BadRequest("Informe IDs válidos");
            }

            if (cursoModel == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("O cursoModel não é válido");
            }

            var curso = await _cursoClient.Get(id);

            if (curso == null)
            {
                return NotFound();
            }
            else
            {
                await _cursoClient.Update(curso);

                return Ok();

            }



        }

  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromBody] MarcaModel marcaModel)
        {
            if (id == null)
            {
                return BadRequest("Informe um ID");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("O cursoModel não está válido");
            }

            if (await _cursoClient.Get(id) == null)
            {
                return NoContent();
            }
            else
            {
                await _cursoClient.Delete(id);
                return Ok();
            }

        }

    }







 
}

