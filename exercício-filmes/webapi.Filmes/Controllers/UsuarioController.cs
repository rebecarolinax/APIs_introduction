using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public IActionResult Logar(UsuarioDomain usuario)
        {
            try
            {
               UsuarioDomain usuarioEncontrado = _usuarioRepository.Login(usuario.Email, usuario.Senha);

                if (usuarioEncontrado == null)
                {
                    return NotFound("Nenhum usuário foi encontrado!");                  
                }

                //Caso retorne o usuário encontrado, prossegue para a criação do Token

                //1° - Definir as informações(Claims) que serão fornecidos no PayLoad
                var claims = new[]
                {
                    //Formato(tipo e valor)
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioEncontrado.IdUsuario.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, usuarioEncontrado.Email),
                    new Claim(JwtRegisteredClaimNames.Name, usuarioEncontrado.Nome),
                    new Claim(ClaimTypes.Role,usuarioEncontrado.Permissao),

                    //Existe uma possibilidade de criar uma Claim personalizada
                    new Claim("Claim Personalizada", "Valor Personalizado")
                };

                //2° - Definir as chaves de acesso ao token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));

                //3° - Definir as credenciais do token (Header)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //4° - Gerar o token
                var token = new JwtSecurityToken
                    (
                    //emissor do token
                    issuer: "webapi.filmes.tarde",

                    //destinatário
                    audience: "webapi.filmes.tarde",

                    //dados definidos nas Claims(PayLoad)
                    claims : claims,

                    //tempo de expiração
                    expires: DateTime.Now.AddMinutes(5),

                    //credenciais do token
                    signingCredentials : creds
                    );

                //5° - Retornar o token criado

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });

            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
