using System.Data.SqlClient;
using webapi.filmes.tarde.Domains;
using webapi.filmes.tarde.Interfaces;

namespace webapi.filmes.tarde.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private string StringConexao = "Data Source = NOTE09-S14; Initial Catalog = Filmes_Rebeca; User Id = sa; Pwd = Senai@134";
        public void AtualizarIdCorpo(FilmeDomain filme)
        {
            //Declara a SqlConnection passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada
                string queryUpdateBody = "UPDATE Filme SET Titulo = @Título, IdGenero = @IdGenero WHERE IdFilme = @IdFilme";

                //Declara o SqlCommand passando a query que será executada e a conexão com o banco de dados
                using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                {
                    
                    cmd.Parameters.AddWithValue("@Título", filme.Título);                  
                    cmd.Parameters.AddWithValue("@IdGenero", filme.IdGenero);
                    cmd.Parameters.AddWithValue("@IdFilme", filme.IdFilme);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Executa a query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarIdUrl(int id, FilmeDomain filme)
        {
            


        }

        /// <summary>
        /// Método que busca um objeto através do seu ID
        /// </summary>
        /// <param name="id">Id do filme que será procurado</param>
        /// <returns>Retorna o objeto encontrado</returns>
        public FilmeDomain BuscarPorId(int id)
        {
            //Declara um objeto que sera guardado o filme buscado.
            FilmeDomain filmeBuscado = new FilmeDomain();

            //Declara uma nova SqlConnection passando como parâmetro a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Comando que será executado
                string querySelectAll = "SELECT IdFilme, Filme.IdGenero, Titulo, Nome FROM Filme LEFT JOIN Genero ON Genero.IdGenero = Filme.IdGenero";

                //Abre a conexão do banco de dados
                con.Open();

                //Declara o DataReader que percorre a tabela do banco de dados
                SqlDataReader rdr;

                //Declara o SqlCommand passando a query e a con como parâmetros
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    //Executa o reader e atribui ao RDR
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain()
                        {
                          
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                            Título = Convert.ToString(rdr["Titulo"]),

                            //Atribui a propriedade gênero um objeto do tipo GeneroDomain, trazendo as informações correspondentes aquele filme
                            Genero = new GeneroDomain()
                            {
                                IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                                Nome = Convert.ToString(rdr["Nome"])
                            }
                        };

                        //Condição para atribuir o filme que seja igual ao ID digitado
                        if (filme.IdFilme == id)
                        {
                            filmeBuscado = filme;

                            return filmeBuscado;
                        }
                    }
                }
            }
            return null;
        }

        public void Cadastrar(FilmeDomain novoFilme)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryInsert = "INSERT INTO Filme (IdGenero, Titulo) VALUES (@IdGenero, @Titulo)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@IdGenero", novoFilme.IdGenero);
                    cmd.Parameters.AddWithValue("@Titulo", novoFilme.Título);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDelete = "DELETE Filme WHERE IdFilme = @IdFilme";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@IdFilme", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<FilmeDomain> ListarTodos()
        {
            List<FilmeDomain> listaFilmes = new List<FilmeDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string querySelectAll = "SELECT IdFilme, Filme.IdGenero, Titulo, Nome FROM Filme LEFT JOIN Genero ON Genero.IdGenero = Filme.IdGenero";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    SqlDataReader rdr;

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain()
                        {
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),

                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),

                            Título = rdr["Galinha"].ToString()
                        };

                        listaFilmes.Add(filme);
                    }
                }
                return listaFilmes;
            }
        }
    }
}

