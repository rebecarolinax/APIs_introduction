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
        /// Endpoint que aciona o método login no repositório
        /// </summary>
        /// <param name="email">email do usuário</param>
        /// <param name="senha">senha do usuário</param>
        /// <returns>retorna a resposta para o usuário(front-end)</returns>
        

        [HttpPost]
        public IActionResult Logar(string email, string senha)
        {
            try
            {
               UsuarioDomain usuarioEncontrado = _usuarioRepository.Login(email, senha);

                if (usuarioEncontrado == null)
                {
                    return NotFound("Nenhum usuário foi encontrado!");                  
                }

                return Ok(usuarioEncontrado);

            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
