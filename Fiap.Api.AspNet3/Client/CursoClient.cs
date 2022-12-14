using Fiap.Api.AspNet3.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Fiap.Api.AspNet3.Client
{
    public class CursoClient
    {
        private readonly string endpoint = "https://5cb544bd07f233001424ceb8.mockapi.io/fiap/curso";

        private readonly HttpClient httpClient;

        public CursoClient()
        {
            httpClient = new HttpClient();   
        }

       

        public async Task<IList<CursoModel>> GetAll()
        {
            var resposta = await httpClient.GetAsync(endpoint);

            if (resposta.IsSuccessStatusCode)
            {
                var conteudoJson = await resposta.Content.ReadAsStringAsync();
                var cursos = JsonConvert.DeserializeObject<List<CursoModel>>(conteudoJson);
                return cursos;
            }
            else
            {
                throw new Exception("Não foi possivel consultar os cursos");
            }
           
        }
        public async Task<CursoModel> Get(int id)
        {

            var resposta = await httpClient.GetAsync($"{endpoint}/{id}");
            if (resposta.IsSuccessStatusCode)
            {
                var conteudoJson = await resposta.Content.ReadAsStringAsync();
                var curso = JsonConvert.DeserializeObject<CursoModel>(conteudoJson);
                return curso;
            }
            else
            {
                throw new Exception("Não foi possível consultar o curso");
            }

        }
        public async Task Delete(int id)
        {
            var resposta = await httpClient.DeleteAsync($"{endpoint}/{id}");
            if (!resposta.IsSuccessStatusCode)
            {
                throw new Exception("Não foi possível consultar o curso");
            }
         
        }
        public async Task Update(CursoModel cursoModel)
        {

            var conteudoJson = JsonConvert.SerializeObject(cursoModel);
            var conteudoJsonString = new StringContent(conteudoJson, Encoding.UTF8, "application/json");

            var resposta = await httpClient.PutAsync($"{endpoint}/{cursoModel.Id}", conteudoJsonString);

            if (!resposta.IsSuccessStatusCode)
            {
                throw new Exception("Não foi possível consultar o curso");
            }
          
        }
        public async Task<int> Insert(CursoModel cursoModel)
        {
            var conteudoJson = JsonConvert.SerializeObject(cursoModel);
            var conteudoJsonString = new StringContent(conteudoJson, Encoding.UTF8, "application/json");

            var resposta = await httpClient.PostAsync(endpoint, conteudoJsonString);

            if (!resposta.IsSuccessStatusCode)
            {
                throw new Exception("Não foi possível consultar o curso");
            }
            else
            {
                var conteudoResposta = await resposta.Content.ReadAsStringAsync();
                var cursoModelRetorno = JsonConvert.DeserializeObject<CursoModel>(conteudoResposta);
                return cursoModelRetorno.Id;
            }

        }





    }
}
