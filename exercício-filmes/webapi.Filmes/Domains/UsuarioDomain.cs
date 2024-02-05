using System.ComponentModel.DataAnnotations;

namespace webapi.filmes.tarde.Domains
{
    public class UsuarioDomain
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O email do usuário é obrigatório!")]
        public string Email { get; set; }

        [StringLength(20,MinimumLength = 3, ErrorMessage = "O campo senha precisa de no mínimo três e no máximo vinte caracteres!")]
        [Required(ErrorMessage = "A senha do usuário é obrigatório!")]
        public string Senha { get; set; } 

        [Required(ErrorMessage = "O nome do usuário é obrigatório!")]
        public string Nome { get; set; }
        public string Permissao { get; set; }
    }
}
