using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace restoran
{


    public partial class siparis_yap : Form
    {
        

        public siparis_yap()
        {
            InitializeComponent();
        }
       
        private void siparis_yap_Load(object sender, EventArgs e)
        {
            lblMasaNo2.Text = cGenel._ButtonValue;
        }

        public void SiparisEkle(string urunAdi, string adet, string fiyat)
        {
            int index = lvMutfak.Items.Count; // lvMutfak, siparis_yap formundaki ListView
            lvMutfak.Items.Add(urunAdi);
            lvMutfak.Items[index].SubItems.Add(adet);
            lvMutfak.Items[index].SubItems.Add(fiyat);
        }
        private void btnGeriDon_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCikis_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak İstediğinizden Emin Misiniz?", "Dikkat ! ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
    }

