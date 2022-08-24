using Fiap.Api.AspNet3.Models;

namespace Fiap.Api.AspNet3.Repository.Interface
{
    public interface IMarcaRepository
    {

        public int Count();
        public IList<MarcaModel> FindAll(int pagina, int tamanho);
    }
}
