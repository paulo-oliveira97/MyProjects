using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ESCOLA.DTO;
using log4net.Config;

namespace ESCOLA.DAL
{
    public class usuarioDAL
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public List<usuarioDTO> loadUser()
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = Conexao();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT * FROM TB_Usuario";

                SqlDataReader ER; //Objeto leitor
                List<usuarioDTO> listUsuarioDTO = new List<usuarioDTO>(); //objeto do tipo lista "tabela"

                ER = comando.ExecuteReader(); // realiza a conexao
                if (ER.HasRows) // Se tiver linha de retorno
                {
                    while (ER.Read())//Laço para varrer o ER - condição de parada "Até o fim das informações"
                    {
                        usuarioDTO USER = new usuarioDTO(); //criando o objeto e instanciando a classe "usuarioDTO"
                        USER.Id_user = Convert.ToInt32(ER["Id_user"]);
                        USER.Nome_user = Convert.ToString(ER["Nome_user"]);
                        USER.Login_user = Convert.ToString(ER["Login_user"]);
                        USER.Senha_user = Convert.ToString(ER["Senha_user"]);
                        USER.Email_user = Convert.ToString(ER["Email_user"]);
                        USER.Cad_user = Convert.ToDateTime(ER["Cad_user"]);
                        USER.Flg_situacao = Convert.ToChar(ER["Flg_situacao"]);
                        USER.Cod_perfil = Convert.ToInt32(ER["Cod_perfil"]);
                        listUsuarioDTO.Add(USER);
                    }
                }
                Conexao().Close();
                return listUsuarioDTO;// Retorna a lista para quem chamou
            }
            catch (Exception ex)
            {
                log.Error("Erro ao se comunicar com o banco de dados", ex);
                Conexao().Close();
                throw ex;
            }
        }
        public int insertUser(usuarioDTO USER)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = Conexao();
                comando.CommandText = "INSERT INTO TB_Usuario (Nome_user, Login_user, Senha_user, Email_user, Cad_user, Flg_situacao, Cod_perfil) " +
                                      "VALUES (@Nome_user, @Login_user, @Senha_user, @Email_user, @Cad_user, @Flg_situacao, @Cod_perfil)";
                comando.Parameters.Clear();
                comando.Parameters.Add("@Nome_user", SqlDbType.VarChar, 100).Value = USER.Nome_user;
                comando.Parameters.Add("@Login_user", SqlDbType.VarChar, 100).Value = USER.Login_user;
                comando.Parameters.Add("@Senha_user", SqlDbType.VarChar, 100).Value = USER.Senha_user;
                comando.Parameters.Add("@Email_user", SqlDbType.VarChar, 100).Value = USER.Email_user;
                comando.Parameters.Add("@Cad_user", SqlDbType.DateTime).Value = DateTime.Now;
                comando.Parameters.Add("@Flg_situacao", SqlDbType.Char, 1).Value = USER.Flg_situacao;
                comando.Parameters.Add("@Cod_perfil", SqlDbType.Int).Value = USER.Cod_perfil;
                comando.CommandType = CommandType.Text;
                comando.ExecuteNonQuery();
                Conexao().Close();
                return 1;
            }
            catch (Exception ex)
            {
                log.Error("Erro ao se comunicar com o banco de dados", ex);
                Conexao().Close();
                return 0;
            }
        }

        public int updateUser(usuarioDTO USER)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = Conexao(); 
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "UPDATE TB_Usuario SET Nome_user = @Nome_user, " +
                                                            "Login_user = @Login_user, " +
                                                            "Senha_user = @Senha_user, " +
                                                            "Email_user = @Email_user, " +
                                                            "Cad_user = @Cad_user, " +
                                                            "Flg_situacao = @Flg_situacao, " +
                                                            "Cod_perfil = @Cod_perfil " +
                                      "WHERE Id_user = @Id_user";
                comando.Parameters.Clear();
                comando.Parameters.Add("@Nome_user", SqlDbType.VarChar, 100).Value = USER.Nome_user;
                comando.Parameters.Add("@Login_user", SqlDbType.VarChar, 100).Value = USER.Login_user;
                comando.Parameters.Add("@Senha_user", SqlDbType.VarChar, 100).Value = USER.Senha_user;
                comando.Parameters.Add("@Email_user", SqlDbType.VarChar, 100).Value = USER.Email_user;
                comando.Parameters.Add("@Cad_user", SqlDbType.DateTime).Value = DateTime.Now;
                comando.Parameters.Add("@Flg_situacao", SqlDbType.Char, 1).Value = USER.Flg_situacao;
                comando.Parameters.Add("@Cod_perfil", SqlDbType.Int).Value = USER.Cod_perfil;
                comando.Parameters.Add("@Id_user", SqlDbType.Int).Value = USER.Id_user;
                comando.ExecuteNonQuery();
                Conexao().Close();
                return 1;
            }
            catch (Exception ex)
            {
                log.Error("Erro ao se comunicar com o banco de dados", ex);
                Conexao().Close();
                return 0;
            }
        }

        public int deleteUser(usuarioDTO USER)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = Conexao();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "DELETE FROM TB_Usuario WHERE Id_user = @Id_user";
                comando.Parameters.Clear();
                comando.Parameters.Add("@Id_user", SqlDbType.Int).Value = USER.Id_user;
                comando.ExecuteNonQuery();
                Conexao().Close();
                return 1;
            }
            catch (Exception ex)
            {
                log.Error("Erro ao se comunicar com o banco de dados", ex);
                Conexao().Close();
                return 0;
            }
        }

        private SqlConnection Conexao()
        {
            SqlConnection conexao = new SqlConnection();
            try
            {
                conexao.ConnectionString = Properties.Settings.Default.conexao1;
                conexao.Open();
                return conexao;
            }
            catch (Exception ex)
            {
                log.Error("Erro ao criar a conexão com o banco de dados", ex);
                throw ex;
            }
        }
    }
}
