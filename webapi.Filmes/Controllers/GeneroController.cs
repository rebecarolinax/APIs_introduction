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

        /// <summary>
        /// Busca o gênero através do ID 
        /// </summary>
        /// <param name="id">ID do gênero a buscar</param>
        /// <returns>O objeto referente ao ID</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Cria um objeto generoBuscado 
            GeneroDomain generoBuscado = _generoRepository.BuscarPorId(id);

            // Verifica se nenhum gênero foi encontrado
            if (generoBuscado == null)
            {
                // Caso não seja encontrado, retorna um status code 404 com a mensagem
                return NotFound("Não encontrado!");
            }

            // Retorna o gênero buscado com um status code 200 - Ok
            return Ok(generoBuscado);
        }

        /// <summary>
        /// Cadastra um novo gênero
        /// </summary>
        /// <param name="novoGenero">O objeto que será cadastrado</param>
        /// <returns>Um cadastro de objeto</returns>
        [HttpPost]
        public IActionResult Post(GeneroDomain novoGenero)
        {
            try
            {
                //Chama o método Cadastrar() do repositório
                _generoRepository.Cadastrar(novoGenero);

                return StatusCode(201);    
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Deleta um gênero existente
        /// </summary>
        /// <param name="id">ID do gênero que será deletado</param>
        /// <returns>Exclusão do gênero</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                //Chama o método Deletar() do reposítorio
                _generoRepository.Deletar(id);  
                
                // Retorna um status code 204 - No Content
                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Atualiza um gênero passando o ID pela URL 
        /// </summary>
        /// <param name="id">ID do gênero que será atualizado e passado pela URL</param>
        /// <param name="generoAtualizado">Objeto já atualizado</param>
        /// <returns>Atualiza um gênero</returns>
        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, GeneroDomain generoAtualizado)
        {
            try
            {
                // Chamada o método AtualizarIdUrl()
                _generoRepository.AtualizarIdUrl(id, generoAtualizado);

                // Retorna um status code 204 
                return NoContent();
            }
         
            catch (Exception erro)
            {
                // Retorna um status code 400 
                return BadRequest(erro.Message);
            }
        }

        [HttpPut]
       public IActionResult PutIdBody(GeneroDomain generoAtualizadoCorpo)
        {
            try
            {
                _generoRepository.AtualizarIdCorpo(generoAtualizadoCorpo);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }


    }

}
