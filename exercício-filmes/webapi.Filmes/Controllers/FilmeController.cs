using Microsoft.AspNetCore.Authorization;
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
    public class FilmeController : ControllerBase
    {
        /// <summary>
        /// Objeto que irá receber os métodos definidos na interface
        /// </summary>
        private IFilmeRepository _filmeRepository { get; set; }

        /// <summary>
        /// Instância do objeto _filmeRepository para que haja referência aos métodos no repositório
        /// </summary>
        public FilmeController() => _filmeRepository = new FilmeRepository();

        /// <summary>
        /// Endpoint que acessa o método de listar os filmes
        /// </summary>
        /// <returns>Lista de gêneros e um Status Code</returns>
        [HttpGet]
        [Route("ListarTodos")]
        public IActionResult GetAll()
        {
            try
            {
                List<FilmeDomain> listaFilmes = _filmeRepository.ListarTodos();

                // Retorna o status code 200 ok e a lista de gêneros no formato JSON
                return StatusCode(200, listaFilmes);
                //return Ok(listaFilmes);
            }
            catch (Exception error)
            {
                // Retorna um status code 400 - BadRequest e a mensagem de erro
                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Endpoint que acesso o método de buscar filme a partir de um ID
        /// </summary>
        /// <returns>Gênero encontrado e um status code</returns>
        [HttpGet]
        [Route("BuscarPorId")]
        public IActionResult GetById(int id) // IActionResult - Espera que se retorne um status code
        {
            try
            {
                FilmeDomain filme = _filmeRepository.BuscarPorId(id);

                return filme == null ? NotFound("O filme buscado não foi encontrado") : StatusCode(200, filme);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint que acessa o método de cadastrar filme
        /// </summary>
        /// <param name="idGenero">Id do Gênero do filme</param>
        /// <param name="tituloFilme">Título do filme</param>
        /// <returns>Filme a ser cadastrado e status code</returns>
        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Post(FilmeDomain novoFilme)
        {
            try
            {
                _filmeRepository.Cadastrar(novoFilme);

                return Created("Objeto criado", novoFilme);
                //return StatusCode(201, novoFilme);
            }
            catch (Exception error)
            {
                // Retorna um status code BadRequest(400) e a mensagem de erro
                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Endpoint que acessa o método de deletar filme
        /// </summary>
        /// <param name="id">Id do filme a ser deletado</param>
        /// <returns>Id do filme deletado e status code</returns>
        [HttpDelete("Deletar/{id}")]
        //[Route("Deletar")]
        public IActionResult Delete(int id)
        {
            try
            {
                _filmeRepository.Deletar(id);

                return StatusCode(204, id);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Endpoint que acessa o método de atualizar filme através do corpo da requisição
        /// </summary>
        /// <param name="filme">Filme a ser atualizado</param>
        /// <returns>Status code</returns>
        [HttpPut]
        [Route("AtualizarIdCorpo")]
        public IActionResult PutIdBody(FilmeDomain filme)
        {
            try
            {             
                FilmeDomain filmeBuscado = _filmeRepository.BuscarPorId(filme.IdFilme);

                if (filmeBuscado == null)
                {
                    
                    return NotFound("Filme não encontrado!");
                }

                _filmeRepository.Atualizar(filme);

                return Ok();
            }
            catch (Exception erro)
            {
                //Retorna um BadREquest caso alguma exceção ocorra
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint que acessa o método de atualizar filme através da URL
        /// </summary>
        /// <param name="id">Id do filme a ser atualizado</param>
        /// <param name="titulo">Novo título do filme</param>
        /// <returns>Status code</returns>
        [HttpPut("AtualizarIdURL/{id}")]
        public IActionResult PutIdUrl(int id, string titulo)
        {
            try
            {
                FilmeDomain filme = _filmeRepository.BuscarPorId(id);
                filme.Titulo = titulo;

                _filmeRepository.Atualizar(filme);

                return StatusCode(200);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
