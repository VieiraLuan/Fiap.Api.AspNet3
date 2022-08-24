using Fiap.Api.AspNet3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.AspNet3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProdutoController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public ProdutoController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IList<ProdutoModel>>> Get()
        {
            var listaProdutos = _dataContext.Produtos.ToList();

            if (listaProdutos == null || listaProdutos.Count == 0)
            {
                return Ok(listaProdutos); // Deu tudo certo !
            }
            else
            {
                return NoContent(); // Vazio, mas deu sucesso!
            }

        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProdutoModel>> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest(new
                {
                    messagem = $"Não foi possivel encontrar o produto de id:{id}," +
                    $" por favor forneça um id maior que 0"
                }); //Requisição errada !
            }

            var produtos = await _dataContext.Produtos.FindAsync(id);

            if (produtos == null)
            {
                return NotFound(new { messagem = $"Não foi possivel encontrar nenhuma produto com o id:{id}" }); // Vazio, mas deu sucesso !
            }
            else
            {
                return Ok(produtos); // Deu tudo certo !
            }

        }


        [HttpPost]
        [Authorize(Roles = "Pleno,Senior")]
        public async Task<ActionResult<ProdutoModel>> Post([FromBody] ProdutoModel produtoModel)
        {
            if (produtoModel == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dataContext.Produtos.Add(produtoModel);
            await _dataContext.SaveChangesAsync();

            var location = new Uri(Request.GetEncodedUrl() + "/" + produtoModel.ProdutoId);

            return Created(location, produtoModel);
        }



        [HttpPut("{id}")]
        [Authorize(Roles = "Pleno,Senior")]
        public async Task<ActionResult<ProdutoModel>> Put([FromRoute] int id, [FromBody] ProdutoModel produtoModel)
        {
            if (id != produtoModel.ProdutoId || (id == 0) || produtoModel == null || !ModelState.IsValid)
            {
                return BadRequest(new { messagem = $"Não foi possivel alterar a marca de id:{id}" });
            }


            var produto = await _dataContext.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound(); // Vazio, mas deu sucesso !
            }
            else
            {
                _dataContext.Produtos.Update(produto);
                await _dataContext.SaveChangesAsync();
                return NoContent();
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Pleno,Senior")]
        public async Task<ActionResult<ProdutoModel>> Delete([FromRoute] int id)
        {
            var produto = await _dataContext.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound(); // Vazio, mas deu sucesso !
            }
            else
            {
                _dataContext.Produtos.Remove(produto);
                await _dataContext.SaveChangesAsync();
                return NoContent();
            }
        }



    }
}
