﻿using System;
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
    public partial class frmSplash : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public frmSplash()
        {
            InitializeComponent();
            log.Info("Aplicação iniciada");
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            frmMDI m = new frmMDI();
            m.Show();
            this.Hide();
        }
    }
}