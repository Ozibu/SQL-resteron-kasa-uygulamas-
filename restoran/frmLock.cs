
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace restoran
{
    public partial class frmLock : Form
    {
        public frmLock()
        {
            InitializeComponent();
        }
        private void btnGiris_Click(object sender, EventArgs e)
        {
            frmGiris frm = new frmGiris();
            this.Close();
            frm.Show();
        }

        private void frmLock_Load(object sender, EventArgs e)
        {

        }
    }
}
