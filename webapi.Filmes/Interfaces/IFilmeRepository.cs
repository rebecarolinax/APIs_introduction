using webapi.filmes.tarde.Domains;

namespace webapi.filmes.tarde.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório FilmeRepository
    /// Define os métodos que serão implementados pelo repositório
    /// </summary>
    public interface IFilmeRepository
    {
        // CRUD - Create, Read, Update, Delete

        /// <summary>
        /// Cadastrar um novo filme
        /// </summary>
        /// <param name="novoFilme">Objeto que será cadastrado</param>
        void Cadastrar(FilmeDomain novoFilme);

        /// <summary>
        /// Retornar todos os filmes cadastrados
        /// </summary>
        /// <returns>Lista de filmes</returns>
        List<FilmeDomain> ListarTodos();

        /// <summary>
        /// Buscar um objeto através de seu Id
        /// </summary>
        /// <param name="id">Id do objeto e será buscado</param>
        /// <returns>Objeto que foi buscado</returns>
        FilmeDomain BuscarPorId(int id);

        /// <summary>
        /// Atualizar um filme existente passando o id pelo corpo da requisição
        /// </summary>
        /// <param name="filme">Objeto com as novas informações</param>
        void Atualizar(FilmeDomain filme);

        /// <summary>
        /// Deletar um filme existente
        /// </summary>
        /// <param name="id">Id do objeto a ser deletado</param>
        void Deletar(int id);
    }
}
