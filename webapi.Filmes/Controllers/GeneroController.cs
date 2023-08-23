using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.filmes.tarde.Domains;
using webapi.filmes.tarde.Interfaces;
using webapi.filmes.tarde.Repositories;

namespace webapi.filmes.tarde.Controllers
{
    /// <summary>
    /// Define que a rota de uma requisição será no seguinte formato:
    /// dominio/api/nomeController
    /// http://localhost:5000/api/Genero
    /// </summary>
    [Route("api/[controller]")]

    /// <summary>
    /// Define que é um controlador de API
    /// </summary>
    [ApiController]

    /// <summary>
    /// Define que o tipo de resposta da API é JSON
    /// </summary>
    [Produces("application/json")]  
    public class GeneroController : ControllerBase
    {
        /// <summary>
        /// Objeto(_generoRepository) que irá receber os métodos definidos na interface
        /// </summary>
        private IGeneroRepository _generoRepository { get; set; }

        /// <summary>
        /// Instância do objeto(_generoRepository) para que haja referência aos métodos do repositório
        /// </summary>
        public GeneroController()
        {
            _generoRepository= new GeneroRepository();
        }

        /// <summary>
        /// EndPoint que acessa o método de listar os gêneros
        /// </summary>
        /// <returns>uma lista de gêneros com um StatusCode</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                 //Cria uma lista para receber os gêneros
                 List<GeneroDomain> listaGeneros = _generoRepository.ListarTodos();

                 //Retorna o statuscode(200) e a listagem de gêneros no formato JSON 
                 return Ok(listaGeneros);
            }
            
            catch (Exception erro)
            {
                //Retorna status code BadRequest(400) e a mensagem do erro
                return BadRequest(erro.Message);
            }
           
        }


    }

}
