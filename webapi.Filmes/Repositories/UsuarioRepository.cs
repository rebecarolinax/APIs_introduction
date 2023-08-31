using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using webapi.filmes.tarde.Domains;
using webapi.filmes.tarde.Interfaces;

namespace webapi.filmes.tarde.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private string StringConexao = "Data Source = NOTE09-S14; Initial Catalog = Filmes_Rebeca; User Id = sa; Pwd = Senai@134";

        public UsuarioDomain Login(string email, string senha)
        {

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryLogin = "SELECT IdUsuario, Email, Senha, Nome, Permissao FROM Usuario WHERE Email = @email AND Senha = @senha";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryLogin, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    cmd.Parameters.AddWithValue("@senha", senha);
                    
                    SqlDataReader rdr;
                    
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        UsuarioDomain usuarioEncontrado = new UsuarioDomain()
                        {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),

                            Email = rdr["Email"].ToString(),

                            Nome = rdr["Nome"].ToString(),

                            Permissao = rdr["Permissao"].ToString()
                        };
                        return usuarioEncontrado;
                    }
                    return null;
                }
            }
        }
    }
}
