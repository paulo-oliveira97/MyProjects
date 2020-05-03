using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESCOLA.UI
{
    public partial class frmMDI : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private int childFormNumber = 0;
        public frmMDI()
        {
            InitializeComponent();
            log.Info("Form principal iniciado");
        }
        private void frmMDI_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Aplicação encerrada");
            Application.Exit();
        }

        private void userCadastroToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmCadUser cad = new frmCadUser();
            cad.Show();
        }

        private void novoToolStripButton_Click(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window" + childFormNumber++;
            childForm.Show();
        }

        private void abrirToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }
    }
}
