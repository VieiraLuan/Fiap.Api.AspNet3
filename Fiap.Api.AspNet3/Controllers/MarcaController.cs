using Fiap.Api.AspNet3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.AspNet3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MarcaController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public MarcaController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IList<MarcaModel>>> Get()
        {
            var listaMarcas = await _dataContext.Marcas.ToListAsync<MarcaModel>();

            if (listaMarcas == null || listaMarcas.Count == 0)
            {
                return Ok(listaMarcas); // Deu tudo certo !
            }
            else
            {
                return NoContent(); // Vazio, mas deu sucesso!
            }

        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<MarcaModel>> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest(new { messagem = $"Não foi possivel encontrar a marca de id:{id}," +
                    $" por favor forneça um id maior que 0" }); //Requisição errada !
            }

            var marca = await _dataContext.Marcas.FindAsync(id);

            if (marca == null)
            {
                return NotFound(new {messagem = $"Não foi possivel encontrar nenhuma marca com o id:{id}"}); // Vazio, mas deu sucesso !
            }
            else
            {
                return Ok(marca); // Deu tudo certo !
            }

        }


        [HttpPost]
        [Authorize(Roles = "Pleno,Senior")]
        public async Task<ActionResult<MarcaModel>> Post([FromBody] MarcaModel marcaModel)
        {
            if ( marcaModel == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dataContext.Marcas.Add(marcaModel);
            await _dataContext.SaveChangesAsync();

            var location = new Uri(Request.GetEncodedUrl() + "/" + marcaModel.MarcaId);

            return Created(location, marcaModel);
        }



        [HttpPut("{id}")]
        [Authorize(Roles = "Pleno,Senior")]
        public async Task<ActionResult<MarcaModel>> Put([FromRoute] int id, [FromBody] MarcaModel marcaModel)
        {
            if (id != marcaModel.MarcaId || (id == 0) || marcaModel == null || !ModelState.IsValid)
            {
                return BadRequest(new { messagem = $"Não foi possivel alterar a marca de id:{id}" });
            }


            var marca = await _dataContext.Marcas.FindAsync(id);

            if (marca == null)
            {
                return NotFound(); // Vazio, mas deu sucesso !
            }
            else
            {
                _dataContext.Marcas.Update(marca);
                await _dataContext.SaveChangesAsync();
                return NoContent();
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Pleno,Senior")]
        public async Task<ActionResult<MarcaModel>> Delete([FromRoute] int id)
        {
            var marca = await _dataContext.Marcas.FindAsync(id);

            if (marca == null)
            {
                return NotFound(); // Vazio, mas deu sucesso !
            }
            else
            {
                _dataContext.Marcas.Remove(marca);
                await _dataContext.SaveChangesAsync();
                return NoContent();
            }
        }


    }
}
