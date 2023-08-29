using webapi.filmes.tarde.Domains;

namespace webapi.filmes.tarde.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFilmeRepository
    {
        //TipoDeRetorno NomeMetodo(TipoParametro NomeParametro)

        /// <summary>
        /// Cadastrar um novo filme
        /// </summary>
        /// <param name="novoFilme">Objeto que será cadastrado</param>
        void Cadastrar(FilmeDomain novoFilme);

        /// <summary>
        /// Retornar todos os filmes cadastrados
        /// </summary>
        /// <returns>Uma de lista de objetos</returns>
        List<FilmeDomain> ListarTodos();

        /// <summary>
        /// Buscar um filme pelo seu ID
        /// </summary>
        /// <param name="id">ID do objeto buscado</param>
        /// <returns>Objeto que foi buscado</returns>
        FilmeDomain BuscarPorId(int id);

        /// <summary>
        /// Atualizar um filme existente passando o ID passando pelo corpo da requisição
        /// </summary>
        /// <param name="filme">Objeto filme com as novas informações</param>
        void AtualizarIdCorpo(FilmeDomain filme);

        /// <summary>
        /// Atualizar um filme existente passando o ID pela URL da requisição
        /// </summary>
        /// <param name="id">ID do objeto a ser atualizado</param>
        /// <param name="filme">Objeto com as novas informações</param>1
        void AtualizarIdUrl(int id, FilmeDomain filme);

        /// <summary>
        /// Deletar um filme existente
        /// </summary>
        /// <param name="idFilme">ID do filme a ser deletado</param>
        void Deletar(int idFilme);

    }
}
