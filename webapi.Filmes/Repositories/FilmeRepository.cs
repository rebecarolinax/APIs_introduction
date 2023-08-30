using System.Data.SqlClient;
using webapi.filmes.tarde.Domains;
using webapi.filmes.tarde.Interfaces;

namespace webapi.filmes.tarde.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados que recebe os seguintes parâmetros:
        /// Data Source: Nome do servidor do banco
        /// Initial Catalog: Nome do banco de dados
        /// Autenticação:
        ///     - Windows: Integrated Security = True
        ///     - SqlServer: User Id = sa; Pwd = Senha
        /// </summary>
        private string StringConexao = "Data Source = NOTE09-S14; Initial Catalog = Filmes_Rebeca; User Id = sa; Pwd = Senai@134";

        public void Atualizar(FilmeDomain filme)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada
                string queryUpdateBody = "UPDATE Filme SET Titulo = @Titulo, IdGenero = @IdGenero WHERE IdFilme = @IdFilme";

                //Declara o SqlCommand passando a query que será executada e a conexão com o banco de dado
                using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                {
                    //Passa o valor do parametro 
                    cmd.Parameters.AddWithValue("@Titulo", filme.Titulo);

                    //Passa o valor do parametro 
                    cmd.Parameters.AddWithValue("@IdGenero", filme.IdGenero);

                    //Passa o valor do parametro 
                    cmd.Parameters.AddWithValue("@IdFilme", filme.IdFilme);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Executa a query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Buscar um gênero a partir de um id
        /// </summary>
        /// <param name="id">Id do gênero a ser buscado</param>
        /// <returns>Gênero que foi buscado</returns>
        public FilmeDomain? BuscarPorId(int id) => ListarTodos().FirstOrDefault(filme => filme.IdFilme == id);

        /// <summary>
        /// Cadastrar um novo filme
        /// </summary>
        /// <param name="novoFilme">Objeto da classe Filme que será cadastrado</param>
        public void Cadastrar(FilmeDomain novoFilme)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryInsert = $"INSERT INTO Filme(IdGenero, Titulo) VALUES ({novoFilme.IdGenero}, @Titulo)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Titulo", novoFilme.Titulo);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deletar um filme existente
        /// </summary>
        /// <param name="id">Id do filme a ser deletado</param>
        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDelete = $"DELETE FROM Filme WHERE Filme.IdFilme LIKE {id}";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery(); // Somente para executar INSERT, UPDATE e DELETE (não retorna dados)
                }
            }
        }

        /// <summary>
        /// Listar todos os objetos do tipo Filme
        /// </summary>
        /// <returns>Lista de objetos do tipo Filme</returns>
        public List<FilmeDomain> ListarTodos()
        {
            // Cria uma lista de filmes onde os filmes serão armazenados
            List<FilmeDomain> listaFilmes = new List<FilmeDomain>();

            // Declara a SqlConnection passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string querySelectAll = "SELECT Filme.IdFilme, Filme.Titulo, Genero.IdGenero, Genero.Nome FROM Filme INNER JOIN Genero ON Genero.IdGenero LIKE Filme.IdGenero";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para percorrer(ler) a tabela no banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand passando a query que será executada e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    // Enquanto houver registros para serem lidos no rdr, o laço se repetirá
                    while (rdr.Read())
                    {

                        FilmeDomain filme = new FilmeDomain()
                        {
                            // Atribui a propriedade IdFilme o valor da primeira coluna da tabela
                            IdFilme = Convert.ToInt32(rdr[0]),
                            Titulo = rdr[1].ToString(),
                            IdGenero = Convert.ToInt32(rdr[2]),
                            Genero = new GeneroDomain()
                            {
                                IdGenero = Convert.ToInt32(rdr[2]),
                                Nome = rdr[3].ToString()
                            }
                        };
                        listaFilmes.Add(filme);
                    }
                }
            }
            return listaFilmes;
        }
    }
}
