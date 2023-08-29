using System.Data.SqlClient;
using webapi.filmes.tarde.Domains;
using webapi.filmes.tarde.Interfaces;

namespace webapi.filmes.tarde.Repositories
{
    /// <summary>
    /// Classe responsável pelo repositório dos gêneros
    /// </summary>
    public class GeneroRepository : IGeneroRepository
    { 
        /// <summary>
        /// String de conexão com o banco de dados qu recebe os seguintes parâmetros:
        /// Data Source: Nome do servidor do banco
        /// Initial Catalog: Nome do baco 
        /// Autenticação:
        ///     -   Windows: Integrated Security: True
        ///     -   SQL SERVER: User Id = SA; Pwd = Senha
        /// </summary>
        private string StringConexao = "Data Source = NOTE09-S14; Initial Catalog = Filmes_Rebeca; User Id = sa; Pwd = Senai@134";

        public void AtualizarIdCorpo(GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //criadas duas string de conexao, uma para buscar e outra para atualizar caso haja o id buscado
                string querySearchById = "SELECT IdGenero, Nome FROM Genero WHERE IdGenero = @IdGenero";
                string queryUpdateById = "UPDATE Genero SET Nome = @Nome WHERE IdGenero = @IdGenero";

                using (SqlCommand cmdSearch = new SqlCommand(querySearchById, con))
                {
                    cmdSearch.Parameters.AddWithValue("@IdGenero", genero.IdGenero);

                    con.Open();

                    using (SqlDataReader rdr = cmdSearch.ExecuteReader())
                    {
                        if (rdr.Read())
                        {

                            rdr.Close(); // fecha o rdr antes de executar a alteracao de dados

                            using (SqlCommand cmdUpdate = new SqlCommand(queryUpdateById, con))
                            {
                                cmdUpdate.Parameters.AddWithValue("@IdGenero", genero.IdGenero);
                                cmdUpdate.Parameters.AddWithValue("@Nome", genero.Nome);

                                cmdUpdate.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }
        public void AtualizarIdUrl(int id, GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryUpdateByUrl = "UPDATE Genero SET Nome = @Nome WHERE IdGenero = @IdGenero";

                using (SqlCommand cmd = new SqlCommand(queryUpdateByUrl, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", genero.Nome);
                    cmd.Parameters.AddWithValue("@IdGenero", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public GeneroDomain BuscarPorId(int id)
        {
            GeneroDomain generoBuscado = new GeneroDomain();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string querySearch = "SELECT IdGenero, Nome FROM Genero WHERE IdGenero = @IdGenero";

                using (SqlCommand cmd = new SqlCommand(querySearch, con))
                {
                    cmd.Parameters.AddWithValue("@IdGenero", id);

                    con.Open();

                    SqlDataReader rdr;

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        generoBuscado = new GeneroDomain
                        {
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                            Nome = rdr["Nome"].ToString()
                        };
                    }
                }
            }
            return generoBuscado;
        }

        /// <summary>
        /// Cadastrar um novo gênero
        /// </summary>
        /// <param name="novoGenero"> Objeto com as informaçõess que serão cadastradas <param>
        public void Cadastrar(GeneroDomain novoGenero)
        {
            //Declara a conexão passando a string de conexão como parâmetro 
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a query que sera executada
                string queryInsert = "INSERT INTO Genero (Nome) VALUES (@Nome)";


                //Declara o sqlcommand com a query que sera executada e a conexao com o bd
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", novoGenero.Nome);

                    //Abre a conexao com o banco de dados
                    con.Open();

                    //Executar a query
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDelete = "DELETE FROM Genero WHERE IdGenero = @IdGenero";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@IdGenero", id);

                    con.Open();

                    cmd.BeginExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Método para listar todos os objetos gêneros
        /// </summary>
        /// <returns></returns>
        public List<GeneroDomain> ListarTodos()
        {
            //Cria uma lista de objetos do tipo gênero 
            List<GeneroDomain> listaGeneros = new List<GeneroDomain>();


            //Declara a SqlConnection passando a string de conexão como parâmetro 
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada
                string querySelectAll = "SELECT IdGenero, Nome FROM Genero";

                //Abre a conexão com o banco de dados
                con.Open();

                //Declara o SqlDataReader para percorrer a tabela do banco de dados 
                SqlDataReader rdr;

                //Declara o SqlCommand passando a query a ser executada e a conexão com o banco de dados
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    // Executa a query e armazena os dados no RDR
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        GeneroDomain genero = new GeneroDomain()
                        {
                            //atribui a propriedade IdGenero o valor recebido no RDR
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),

                            //Atribui a propriedade Nome o valor recebido no RDR
                            Nome = rdr["Nome"].ToString()
                        };

                        //adiciona cada objeto dentro da lista
                        listaGeneros.Add(genero);
                    }
                }
                //retorna a lista 
                return listaGeneros;
            }
        }
    }
}

