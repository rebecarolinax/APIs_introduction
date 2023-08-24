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
            throw new NotImplementedException();
        }

        public void AtualizarIdUrl(int id, GeneroDomain genero)
        {
            throw new NotImplementedException();
        }
        public GeneroDomain BuscarPorId(int id)
        {
            // Declara a SqlConnection 
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query 
                string querySelectById = "SELECT IdGenero, Nome FROM Genero WHERE IdGenero = @ID";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    // Função que permite passar o valor para o parâmetro @ID (Que usa um objeto e um valor unidos(Objeto=Gênero Valor=ID) (documentação Microsoft)
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        // Se sim, instancia um novo objeto
                        GeneroDomain generoBuscado = new GeneroDomain()
                        {
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                            Nome = rdr["Nome"].ToString()
                        };

                        // Retorna o generoBuscado com os dados obtidos
                        return generoBuscado;
                    }
                    // Se não, retorna nulo
                    return null;
                }
            }
        }

    /// <summary>
    /// Cadastrar um novo gênero
    /// </summary>
    /// <param name="novoGenero">Objeto com as informações que serão cadastradas</param>
    public void Cadastrar(GeneroDomain novoGenero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a query que será executada
                string queryInsert = "INSERT INTO Genero(Nome) VALUES('" + novoGenero.Nome + "')";


                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idGenero)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Listar todos os objetos do tipo gênero
        /// </summary>
        /// <returns>Listas de objetos do tipo gênero</returns>
        public List<GeneroDomain> ListarTodos()
        {
            //Cria uma lista de gêneros onde será armazenados os gêneros
            List<GeneroDomain> ListaGeneros = new List<GeneroDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada(select)
                string querySelectAll = "SELECT IdGenero, Nome FROM Genero";

                //abre a conexão com banco de dados
                con.Open();

                //Declara o SqlDataReader para percorrer ou ler a tabela no banco de dados
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    //Enquanto houver registros o laço se repetirá
                    while ( rdr.Read()) 
                    {
                        GeneroDomain genero = new GeneroDomain()
                        {
                            IdGenero = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString(),
                        };

                        //Adiciona o objeto criado dentro da lista
                        ListaGeneros.Add(genero);
                    }
                }
                //Retorna a lista de gêneros
                return ListaGeneros;
            }
        }
    }
}
