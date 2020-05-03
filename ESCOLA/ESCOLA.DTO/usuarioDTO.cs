using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESCOLA.DTO
{
    public class usuarioDTO
    {
        public int Id_user { get; set; }
        public string Nome_user { get; set; }
        public string Login_user { get; set; }
        public string Senha_user { get; set; }
        public string Email_user { get; set; }
        public DateTime Cad_user { get; set; }
        public char Flg_situacao { get; set; }
        public int Cod_perfil { get; set; }
    }
}
