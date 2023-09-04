using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.filmes.tarde.Domains;
using webapi.filmes.tarde.Interfaces;
using webapi.filmes.tarde.Repositories;

namespace webapi.filmes.tarde.Controllers
{
    /// <summary>
    /// Define que a rota de uma requisição será no seguinte formato
    /// dominio/api/nomeController
    /// exemplo: http://localhost:5000/api/Genero
    /// </summary>
    [Route("api/[controller]")]
    /// <summary>
    /// Define quem é o controlador da API
    /// </summary>
    [ApiController]
    /// <summary>
    /// Define o tipo de resposta da API como JSON
    /// </summary>
    [Produces("application/json")]
    [Authorize]
    public class GeneroController : ControllerBase
    {
        /// <summary>
        /// Objeto que irá receber os métodos definidos na interface
        /// </summary>
        private IGeneroRepository _generoRepository { get; set; }

        /// <summary>
        /// Instância do objeto _generoRepository para que haja referência aos métodos no repositório
        /// </summary>
        public GeneroController() => _generoRepository = new GeneroRepository();

        /// <summary>
        /// Endpoint que acessa o método de listar os gêneros
        /// </summary>
        /// <returns>Lista de gêneros e um Status Code</returns>
        /// 
        [HttpGet]
        [Authorize]//precisa estar logado para acessar a rota
        [Route("ListarTodos")]
        public IActionResult GetAll()
        {
            try
            {
                List<GeneroDomain> listaGeneros = _generoRepository.ListarTodos();

                // Retorna o status code 200 ok e a lista de gêneros no formato JSON
                return StatusCode(200, listaGeneros);
                //return Ok(listaGeneros);
            }
            catch (Exception error)
            {
                // Retorna um status code 400 - BadRequest e a mensagem de erro
                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Endpoint que acesso o método de buscar gênero a partir de um ID
        /// </summary>
        /// <returns>Gênero encontrado e um status code</returns>
        [HttpGet]
        [Route("BuscarPorId")]
        public IActionResult GetById(int id) // IActionResult - Espera que se retorne um status code
        {
            try
            {
                GeneroDomain genero = _generoRepository.BuscarPorId(id);

                return genero == null ? NotFound("O gênero buscado não foi encontrado") : StatusCode(200, genero);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint que acessa o método de cadastrar gênero
        /// </summary>
        /// <returns>Gênero a ser cadastrado e status code</returns>
        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Post(GeneroDomain novoGenero)
        {
            try
            {
                _generoRepository.Cadastrar(novoGenero);

                return Created("Objeto criado", novoGenero);
                //return StatusCode(201, novoGenero);
            } catch (Exception error)
            {
                // Retorna um status code BadRequest(400) e a mensagem de erro
                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Endpoint que acessa o método de deletar gênero
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Deletar/{id}")]
        //[Route("Deletar")]
        public IActionResult Delete(int id)
        {
            try
            {
                _generoRepository.Deletar(id);

                return StatusCode(204, id);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Endpoint que acessa o método de atualizar gênero através do corpo da requisição
        /// </summary>
        /// <param name="genero"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("AtualizarIdCorpo")]
        public IActionResult PutIdBody(GeneroDomain genero)
        {
            try
            {
                _generoRepository.Atualizar(genero);

                return StatusCode(200);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Endpoint que acessa o método de atualizar gênero através da URL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nome"></param>
        /// <returns></returns>
        [HttpPut("AtualizarIdURL/{id}")]
        public IActionResult PutIdUrl(int id, string nome)
        {
            try
            {
                GeneroDomain genero = _generoRepository.BuscarPorId(id);
                genero.Nome = nome;

                _generoRepository.Atualizar(genero);

                return StatusCode(200);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
