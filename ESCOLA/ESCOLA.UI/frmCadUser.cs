using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ESCOLA.DTO;
using ESCOLA.BLL;


namespace ESCOLA.UI
{
    public partial class frmCadUser : Form
    {
        //log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(frmCadUser));
        string vModo = "";
        int idSelectdUser = -1;
        public frmCadUser()
        {
            InitializeComponent();
        }
        private void frmCadUser_Load(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Form CadUser carregado");
            lblStatus.Text = "";
            loadGrid();
        }
        private void loadGrid()
        {
            try
            {
                List<usuarioDTO> listUsuarioDTO = new List<usuarioDTO>();
                listUsuarioDTO = new usuarioBLL().loadUser();
                grdUser.AutoGenerateColumns = false;
                grdUser.DataSource = listUsuarioDTO;
                log.Info("Dados de usuários carregados");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados de usuários.\n" + Environment.NewLine + 
                    ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error(ex);
            }
        }
        public void limpaCampos()
        {
            txbNome.Clear();
            txbLogin.Clear();
            txbSenha.Clear();
            txbEmail.Clear();
            dtpCadastro.Value = DateTime.Now;
            cbbPerfil.SelectedIndex = -1;
            cbbFlgSitu.SelectedIndex = -1;
        }
        public void desabCampos()
        {
            txbNome.Enabled = false;
            txbLogin.Enabled = false;
            txbSenha.Enabled = false;
            txbEmail.Enabled = false;
            dtpCadastro.Enabled = false;
            cbbPerfil.Enabled = false;
            cbbFlgSitu.Enabled = false;
        }
        private void habCampos()
        {
            txbNome.Enabled = true;
            txbLogin.Enabled = true;
            txbSenha.Enabled = true;
            txbEmail.Enabled = true;
            dtpCadastro.Enabled = true;
            cbbPerfil.Enabled = true;
            cbbFlgSitu.Enabled = true;

        }
        private void habEdicao()
        {
            btnNew.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnConfirm.Enabled = true;
            btnCancel.Enabled = true;
            txbNome.Focus();
        }
        private void desabEdicao()
        {
            btnNew.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnConfirm.Enabled = false;
            btnCancel.Enabled = false;
        }
        private void grdUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int select = grdUser.CurrentRow.Index;
            txbNome.Text = Convert.ToString(grdUser["clnNome", select].Value);
            txbLogin.Text = Convert.ToString(grdUser["clnLogin", select].Value);
            txbSenha.Text = Convert.ToString(grdUser["clnSenha", select].Value);
            txbEmail.Text = Convert.ToString(grdUser["clnEmail", select].Value);
            dtpCadastro.Text = Convert.ToString(grdUser["clnCadastro", select].Value);
            idSelectdUser = Convert.ToInt32(grdUser["clnId", select].Value);
            cbbFlgSitu.SelectedIndex = Convert.ToInt32(Convert.ToString(grdUser["clnSituacao", select].Value))-1;
            cbbPerfil.SelectedIndex = Convert.ToInt32(Convert.ToString(grdUser["clnPerfil", select].Value))-1;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            vModo = "Cadastro";
            limpaCampos();
            habCampos();
            habEdicao();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (idSelectdUser < 0)
            {
                lblStatus.Text = "Selecione um usuário!";
                return;
            }
            vModo = "Alterar";
            habCampos();
            habEdicao();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            vModo = "Excluir";
            if (MessageBox.Show("Deseja mesmo excluir este registro? A ação não pode ser desfeita", "EXCLUIR", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    usuarioDTO USER = new usuarioDTO
                    {
                        Id_user = idSelectdUser
                    };
                    int x = new usuarioBLL().deleteUser(USER);
                    if (x > 0)
                    {
                        lblStatus.Text = "Excluído com sucesso!";
                    }
                    log.Info("Dados de usuário excluídos");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir dados de usuário.\n" + Environment.NewLine + 
                        ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex);
                }
            }
            limpaCampos();
            loadGrid();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (vModo == "Cadastro")
            {
                try
                {
                    usuarioDTO USER = new usuarioDTO
                    {
                        Nome_user = txbNome.Text,
                        Login_user = txbLogin.Text,
                        Senha_user = txbSenha.Text,
                        Email_user = txbEmail.Text,
                        Cad_user = System.DateTime.Now,
                        Flg_situacao = Convert.ToChar((cbbFlgSitu.SelectedIndex + 1).ToString()),
                        Cod_perfil = Convert.ToInt32(cbbPerfil.SelectedIndex + 1)
                    };
                    int x = new usuarioBLL().insertUser(USER);
                    if (x > 0)
                    {
                        lblStatus.Text = "Usuário cadastrado!";
                        log.Info("Usuário cadastrado");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Erro ao cadastrar informações de usuário.\n" + Environment.NewLine + 
                        ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex);
                }
            }

            if (vModo == "Alterar")
            {
                try
                {
                    if (idSelectdUser < 0)
                    {
                        lblStatus.Text = "Selecione um usuário!";
                        return;
                    }
                    usuarioDTO USER = new usuarioDTO
                    {
                        Id_user = idSelectdUser,
                        Nome_user = txbNome.Text,
                        Login_user = txbLogin.Text,
                        Senha_user = txbSenha.Text,
                        Email_user = txbEmail.Text,
                        Flg_situacao = Convert.ToChar((cbbFlgSitu.SelectedIndex + 1).ToString()),
                        Cod_perfil = Convert.ToInt32(cbbPerfil.SelectedIndex + 1),
                    };
                    int x = new usuarioBLL().updateUser(USER);
                    if (x > 0)
                    {
                        lblStatus.Text = "Alterado com sucesso!";
                    }
                    log.Info("Dados de usuário alterados");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao alterar informações de usuário.\n" + Environment.NewLine + 
                        ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex);
                }
            }

            vModo = "";
            loadGrid();
            limpaCampos();
            desabCampos();
            desabEdicao();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            limpaCampos();
            desabCampos();
            desabEdicao();
        }
    }
}