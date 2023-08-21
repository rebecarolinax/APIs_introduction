﻿using System.ComponentModel.DataAnnotations;

namespace webapi.filmes.tarde.Domains
{
    /// <summary>
    /// Classe que representa a entidade(tabela) Filme
    /// </summary>
    
    public class FilmeDomain
    {
        public int IdFilme { get; set; }

        [Required(ErrorMessageResourceName = "O título do filme é obrigatório!")]
        public string? Título { get; set; }
        public int IdGenero { get; set; }

        // Referência para a classe gênero
        public GeneroDomain? Genero { get; set; }

    }
}