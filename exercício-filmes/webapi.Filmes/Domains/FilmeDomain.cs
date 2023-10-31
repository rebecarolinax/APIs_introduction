using System.ComponentModel.DataAnnotations;
using webapi.filmes.tarde.Controllers;
using webapi.filmes.tarde.Interfaces;
using webapi.filmes.tarde.Repositories;

namespace webapi.filmes.tarde.Domains
{
    /// <summary>
    /// Classe que representa a entidade(tabela) Filme
    /// </summary>
    public class FilmeDomain
    {
        public int IdFilme { get; set; }
        [Required(ErrorMessage = "É obrigatória a escolha de um gênero para o filme!")]
        public int IdGenero { get; set; }

        public GeneroDomain Genero { get; set; }

        [Required(ErrorMessage = "O tiítulo do filme é obrigatório!")]
        [StringLength(50)]
        public string? Titulo { get; set; }
    }
}
