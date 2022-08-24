using Fiap.Api.AspNet3.Models;
using Fiap.Api.AspNet3.Repository.Interface;

namespace Fiap.Api.AspNet3.Repository
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly DataContext _context;
        public MarcaRepository(DataContext context)
        {
            _context = context;
        }
        public int Count()
        {
            return _context.Marcas.Count();
        }
        public IList<MarcaModel> FindAll(int pagina, int tamanho)
        {
            IList<MarcaModel> listaMarcas = _context.Marcas
                                .Skip(tamanho * pagina)
                                .Take(tamanho)
                                .ToList();
            return listaMarcas;
        }
    }
}
