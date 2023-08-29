using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.filmes.tarde.Domains;
using webapi.filmes.tarde.Interfaces;
using webapi.filmes.tarde.Repositories;

namespace webapi.filmes.tarde.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Produces("application/json")]
    public class FilmeController : ControllerBase
    {
        private IFilmeRepository _filmeRepository { get; set; }
        public FilmeController()
        {
            _filmeRepository = new FilmeRepository();
        }

        /// <summary>
        /// Endpoint que aciona o método de cadastrar um filme
        /// </summary>
        /// <param name="novoFilme"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(FilmeDomain novoFilme)
        {
            try
            {
                _filmeRepository.Cadastrar(novoFilme);
                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint que aciona o método de deletar um filme
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _filmeRepository.Deletar(id);

                return StatusCode(200);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint que aciona um método de listar os filmes cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<FilmeDomain> ListaFilmes = _filmeRepository.ListarTodos();

                return Ok(ListaFilmes);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint que chama o método buscar por ID no repository
        /// </summary>
        /// <param name="id">ID que será buscado</param>
        /// <returns>Retorna um status code 200 - Ok passando o objeto encontrado</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                //Guarda o filme encontrado na variável
                FilmeDomain filmeBuscado = _filmeRepository.BuscarPorId(id);

                //Caso seja nulo, entra nessa condição
                if (filmeBuscado == null)
                {
                    //Retorna um NotFound com uma mensagem
                    return NotFound("Filme não encontrado");
                }

                //Retorna o filme buscado
                return Ok(filmeBuscado);
            }
            catch (Exception erro)
            {
                //Retorna um BadRequest caso alguma exceção ocorra
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint que chama o método AtualizarIdCorpo do Repository
        /// </summary>
        /// <param name="filme">objeto com os novos valores</param>
        /// <returns>Status code 200 - Ok</returns>
        [HttpPut]
        public IActionResult PutBody(FilmeDomain filme)
        {
            try
            {
                // Busca o filme digitado
                FilmeDomain filmeBuscado = _filmeRepository.BuscarPorId(filme.IdFilme);

                //Se ele for nulo, entra nessa condição
                if (filmeBuscado == null)
                {
                    //Retorna um NotFound com uma mensagem para o usuário
                    return NotFound("Filme não encontrado!");
                }

                //Caso passe pela condição, atualiza o filme selecionado
                _filmeRepository.AtualizarIdCorpo(filme);

                //Retorna o StatusCode 200 - Ok
                return Ok();
            }
            catch (Exception erro)
            {
                //Retorna um BadRequest caso alguma exceção ocorra
                return BadRequest(erro.Message);
            }
        }
    }






}

