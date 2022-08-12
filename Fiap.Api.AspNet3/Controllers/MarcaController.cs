using Fiap.Api.AspNet3.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fiap.Api.AspNet3.Controllers
{


   
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public MarcaController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IList<MarcaModel> Get()
        {
            var listaMarcas = _dataContext.Marcas.ToList<MarcaModel>();
            return listaMarcas;

        }

       
        [HttpGet("{id:int}")]
        public MarcaModel Get(int id)
        {
            var marca = _dataContext.Marcas.Find(id);
            return marca;
        }


       





/*
        // POST api/<MarcaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MarcaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MarcaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        */
    }
}
