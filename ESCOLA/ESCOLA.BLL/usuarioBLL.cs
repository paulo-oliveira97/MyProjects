using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESCOLA.DTO;
using ESCOLA.DAL;

namespace ESCOLA.BLL
{
    public class usuarioBLL
    {
        public List<usuarioDTO> loadUser()//declarando método/tipo de dados lista/retorno/ nome
        {
            try// tratamento de erro
            {
                return new usuarioDAL().loadUser();// criando método a ser retornado
            }
            catch (Exception ex)
            {
                throw ex; //uma situação anomala (excessão) durante a execução do programa 
            }
        }
        public int insertUser(usuarioDTO USER)
        {
            try
            {
                return new usuarioDAL().insertUser(USER);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int updateUser(usuarioDTO USER)
        {
            try
            {
                return new usuarioDAL().updateUser(USER);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int deleteUser(usuarioDTO USER)
        {
            try
            {
                return new usuarioDAL().deleteUser(USER);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}