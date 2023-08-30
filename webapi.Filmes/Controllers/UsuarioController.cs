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
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Endpoint que aciona o método login no repositorio
        /// </summary>
        /// <param name="email">email do usuario</param>
        /// <param name="senha">senha do usuario</param>
        /// <returns>retorna a resposta para o usuario(front-end)</returns>
        [HttpPost]
        public IActionResult Logar(string email, string senha)
        {
            try
            {
                //Cria um objeto que recebe os dados da requisicao
                UsuarioDomain usuarioEncontrado = _usuarioRepository.Login(email, senha);

                if (usuarioEncontrado == null)
                {
                    return NotFound("Nenhum usuário foi encontrado!");
                }


                //Retorna o objeto no formato JSON com o status code Ok(200)
                return Ok(usuarioEncontrado);

            }
            catch (Exception erro)
            {
                //Retorna um status code BadRequest(400) e a mensagem erro
                return BadRequest(erro.Message);
            }
        }
    }
}
