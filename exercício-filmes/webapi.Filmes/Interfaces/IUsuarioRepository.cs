﻿using webapi.filmes.tarde.Domains;

namespace webapi.filmes.tarde.Interfaces
{
    public interface IUsuarioRepository
    {
        UsuarioDomain Login(string email, string senha);
    }
}
