using Microsoft.AspNetCore.Http.HttpResults;
using System.Data.SqlClient;
using webapi.filmes.tarde.Domains;
using webapi.filmes.tarde.Interfaces;

namespace webapi.filmes.tarde.Repositories
{
    public class GeneroRepository : IGeneroRepository
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

        /// <summary>
        /// Atualiza as informações de um gênero
        /// </summary>
        /// <param name="genero">Gênero a ser atualizado</param>
        public void Atualizar(GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryUpdate = $"UPDATE Genero SET Nome = @Nome WHERE IdGenero LIKE {genero.IdGenero}";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", genero.Nome);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Buscar um gênero a partir de um id
        /// </summary>
        /// <param name="id">Id do gênero a ser buscado</param>
        /// <returns>Gênero que foi buscado</returns>
        public GeneroDomain? BuscarPorId(int id) => ListarTodos().FirstOrDefault(genero => genero.IdGenero == id);

        //{
        //    List<GeneroDomain> listaGeneros = ListarTodos();
        //    GeneroDomain? generoBuscado = listaGeneros.FirstOrDefault(genero => genero.IdGenero == id);
        //    return generoBuscado;
        //}

        /// <summary>
        /// Cadastrar um novo gênero
        /// </summary>
        /// <param name="novoGenero">Objeto com as informações que serão cadastradas</param>
        public void Cadastrar(GeneroDomain novoGenero)
        {
            // Declara a SqlConnection passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string queryInsert = $"INSERT INTO Genero(Nome) VALUES (@Nome)";

                // Declara o SqlCommand passando a query que será executada e a conexão
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", novoGenero.Nome);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa a query
                    cmd.ExecuteNonQuery(); // Somente para executar INSERT, UPDATE e DELETE (não retorna dados)
                }
            }
        }

        /// <summary>
        /// Deletar um gênero existente
        /// </summary>
        /// <param name="id">Id do gênero a ser deletado</param>
        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string queryDelete = $"DELETE FROM Genero WHERE Genero.IdGenero LIKE {id}";

                // Declara o SqlCommand passando a query que será executada e a conexão
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa a query
                    cmd.ExecuteNonQuery(); // Somente para executar INSERT, UPDATE e DELETE (não retorna dados)
                }
            }
        }

        /// <summary>
        /// Listar todos os objetos do tipo Gênero
        /// </summary>
        /// <returns>Lista de objetos do tipo Gênero</returns>
        public List<GeneroDomain> ListarTodos()
        {
            // Cria uma lista de gêneros onde os gêneros serão armazenados
            List<GeneroDomain> listaGeneros = new List<GeneroDomain>();

            // Declara a SqlConnection passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string querySelectAll = "SELECT IdGenero, Nome FROM Genero";

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
                        GeneroDomain genero = new GeneroDomain()
                        {
                            // Atribui a propriedade IdGenero o valor da primeira coluna da tabela
                            IdGenero = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString(),
                        };

                        listaGeneros.Add(genero);
                    }
                }
            }
            return listaGeneros;
        }
    }
}
