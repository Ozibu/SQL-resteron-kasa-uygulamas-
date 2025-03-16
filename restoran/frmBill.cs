
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace restoran
{
    public partial class frmBill : Form
    {

        public frmBill()
        {
            InitializeComponent();
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }
        private void btnCikis_Click(object sender, EventArgs e)
        {
            //Çıkış butonuna basıldığında ekrana uyarı verecek ve eğer evet derse kullanıcı uygulama kapanacak.
            if (MessageBox.Show("Çıkmak İstediğinizden Emin Misiniz?", "Dikkat ! ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();//Uyarı veriyoruz eğer Evet'e basarsa kullanıcı uygulamadan çıkacak.
            }
        }
        cSiparis cs = new cSiparis();
        int odemeTurId = 0;
        private void frmBill_Load(object sender, EventArgs e)
        {
            // 1 = Masa
            // 2 = Paket Servis
            if (cGenel._servisTurNo == 1)//Eğer servis türü Masa ise
            {
                lblAdisyonId.Text = cGenel._adisyonId;
                txtIndirimTutari.TextChanged += new EventHandler(txtIndirimTutari_TextChanged);
                cs.getByOrder(lvUrunler, Convert.ToInt32(lblAdisyonId.Text));
                if (lvUrunler.Items.Count > 0)
                {
                    decimal toplam = 0;
                    for (int i = 0; i < lvUrunler.Items.Count; i++)
                    {
                        toplam += Convert.ToDecimal(lvUrunler.Items[i].SubItems[3].Text);
                    }
                    lblToplamTutar.Text = string.Format("{0:0.000}", toplam);
                    lblOdenecek.Text = string.Format("{0:0.000}", toplam);
                }
                txtIndirimTutari.Clear();
                decimal kdv = Convert.ToDecimal(lblOdenecek.Text) * 18 / 100;//Ödeme ekranı ilk açıldığında kdv tutarını göstermesi için
                lblKdv.Text = string.Format("{0:0.000}", kdv);
            }
            else if (cGenel._servisTurNo == 2)//Eğer Paket Servis ise
            {
                lblAdisyonId.Text = cGenel._adisyonId;
                cPaketler pc = new cPaketler();
                odemeTurId = pc.OdemeTurIdGetir(Convert.ToInt32(lblAdisyonId.Text));
                txtIndirimTutari.TextChanged += new EventHandler(txtIndirimTutari_TextChanged);
                cs.getByOrder(lvUrunler, Convert.ToInt32(lblAdisyonId.Text));
                if (odemeTurId == 1)
                {
                    rbNakit.Checked = true;
                }
                else if (odemeTurId == 2)
                {
                    rbKrediKarti.Checked = true;
                }
                else if (odemeTurId == 3)
                {
                    rbTicket.Checked = true;
                }
                if (lvUrunler.Items.Count > 0)
                {
                    decimal toplam = 0;
                    for (int i = 0; i < lvUrunler.Items.Count; i++)
                    {
                        toplam += Convert.ToDecimal(lvUrunler.Items[i].SubItems[3].Text);
                    }
                    lblToplamTutar.Text = string.Format("{0:0.000}", toplam);
                    lblOdenecek.Text = string.Format("{0:0.000}", toplam);
                }
                txtIndirimTutari.Clear();
                decimal kdv = Convert.ToDecimal(lblOdenecek.Text) * 18 / 100;//Ödeme ekranı ilk açıldığında kdv tutarını göstermesi için
                lblKdv.Text = string.Format("{0:0.000}", kdv);
            }
        }

        private void txtIndirimTutari_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(lblIndirim.Text) < Convert.ToDecimal(lblToplamTutar.Text))
                {
                    try
                    {
                        lblIndirim.Text = string.Format("{0:0.000}", Convert.ToDecimal(txtIndirimTutari.Text));

                    }
                    catch (Exception)
                    {

                        lblIndirim.Text = string.Format("{0:0.000}", 0);
                    }
                }
                else
                {
                    MessageBox.Show("İndirim Tutarı Toplam Tutardan Fazla Olamaz !");
                    txtIndirimTutari.Clear();

                }
            }
            catch (Exception)
            {

                lblIndirim.Text = string.Format("{0:0.000}", 0);
            }
        }

        private void chkIndirim_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndirim.Checked)
            {
                gbIndirim.Visible = true;
            }
            else
            {
                gbIndirim.Visible = false;
            }
        }
        private void lblIndirim_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(lblIndirim.Text) >= 0)
            {
                decimal odenecek = 0;
                lblOdenecek.Text = lblToplamTutar.Text;
                odenecek = Convert.ToDecimal(lblOdenecek.Text) - Convert.ToDecimal(lblIndirim.Text);
                lblOdenecek.Text = string.Format("{0:0.000}", odenecek);
            }
            decimal kdv = Convert.ToDecimal(lblOdenecek.Text) * 18 / 100;
            lblKdv.Text = string.Format("{0:0.000}", kdv);
        }
        cMasalar masalar = new cMasalar();
        cRezervasyon rezerve = new cRezervasyon();
        private void btnHesapKapat_Click(object sender, EventArgs e)
        {
            if (cGenel._servisTurNo == 1)
            {
                int masaId = masalar.TableGetbyNumber(cGenel._ButtonName);
                int musteriId = 0;
                if (masalar.TableGetbyState(masaId,4)==true)//Eğer masa rezervasyon ise ona göre işlem yap
                {
                    musteriId = rezerve.getByClientIdFromRezervasyon(masaId);
                }
                else
                {
                    musteriId = 1;//Standar müşteri
                }
                int odemeTurId = 0;//Ödeme türünü kontrol et
                if(rbNakit.Checked)
                {
                    odemeTurId = 1;
                }
                if (rbKrediKarti.Checked)
                {
                    odemeTurId = 2;
                }
                if(rbTicket.Checked)
                {
                    odemeTurId = 3;
                }
                cOdeme odeme = new cOdeme();
                odeme.AdisyonId= Convert.ToInt32(lblAdisyonId.Text);
                odeme.OdemeTurId = odemeTurId;
                odeme.MusteriId = musteriId;
                odeme.AraToplam = Convert.ToDecimal(lblOdenecek.Text);
                odeme.KvdTutari = Convert.ToDecimal(lblKdv.Text);
                odeme.Indirim = Convert.ToDecimal(lblIndirim.Text);
                odeme.GenelToplam = Convert.ToDecimal(lblToplamTutar.Text);
                bool result = odeme.billClose(odeme);
                if (result)
                {
                    MessageBox.Show("Hesap Kapatıldı.");
                    masalar.setChangeTableState(Convert.ToString(masaId), 1);//Hesap kapatıldıktan sonra masayı boşalt.
                    cRezervasyon c = new cRezervasyon();
                    c.rezervationClose(Convert.ToInt32(lblAdisyonId.Text));//Rezervasyon kapat
                    cAdisyon a = new cAdisyon();
                    a.additionClose(Convert.ToInt32(lblAdisyonId.Text));//Adisyon kapat
                    this.Close();
                    frmMasalar frm = new frmMasalar();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Hesap Kapatılırken Bir Hata Oluştu. Lütfen Yetkililere Bildiriniz!");
                }
            }
            else if (cGenel._servisTurNo == 2)//Paket sipariş
            {
                cOdeme odeme = new cOdeme();
                odeme.AdisyonId = Convert.ToInt32(lblAdisyonId.Text);
                odeme.OdemeTurId = odemeTurId;
                odeme.MusteriId = 1;//Paket sipariş ID
                odeme.AraToplam = Convert.ToDecimal(lblOdenecek.Text);
                odeme.KvdTutari = Convert.ToDecimal(lblKdv.Text);
                odeme.Indirim = Convert.ToDecimal(lblIndirim.Text);
                odeme.GenelToplam = Convert.ToDecimal(lblToplamTutar.Text);
                bool result = odeme.billClose(odeme);
                if (result)
                {
                    
                    cAdisyon a = new cAdisyon();
                    a.additionClose(Convert.ToInt32(lblAdisyonId.Text));//Adisyon kapat
                    cPaketler p = new cPaketler();
                    p.OrderServiceClose(Convert.ToInt32(lblAdisyonId.Text));
                    MessageBox.Show("Adisyon Kapatıldı.");
                    this.Close();
                    frmMasalar frm = new frmMasalar();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Hesap Kapatılırken Bir Hata Oluştu. Lütfen Yetkililere Bildiriniz!");
                }
            }
        }

        private void btnHesapOzeti_Click(object sender, EventArgs e)//Hesap özetini ekrana getir
        {
            printPreviewDialog1.ShowDialog();
        }
        Font baslik = new Font("Verdana", 15, FontStyle.Bold);
        Font altBaslik = new Font("Verdana", 12, FontStyle.Regular);
        Font icerik = new Font("Verdana", 10);
        SolidBrush sb = new SolidBrush(Color.Black);

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)//Hesap özetinin ekrana çıktısı
        {
            StringFormat st = new StringFormat();
            st.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("ÇİKO RESTORAN", baslik, sb, 350, 100, st);
            e.Graphics.DrawString("----------------------------------------------------------------------", altBaslik, sb, 350, 120, st);
            e.Graphics.DrawString("Ürün Adı:              Adet:              Fiyat:", altBaslik, sb, 150, 250, st);
            e.Graphics.DrawString("----------------------------------------------------------------------", altBaslik, sb, 150, 280, st);
            for (int i = 0; i < lvUrunler.Items.Count; i++)
            {
                e.Graphics.DrawString(lvUrunler.Items[i].SubItems[0].Text, icerik, sb, 150, 300 + i * 30, st);
                e.Graphics.DrawString(lvUrunler.Items[i].SubItems[1].Text, icerik, sb, 350, 300 + i * 30, st);
                e.Graphics.DrawString(lvUrunler.Items[i].SubItems[3].Text, icerik, sb, 420, 300 + i * 30, st);
            }
            e.Graphics.DrawString("----------------------------------------------------------------------", altBaslik, sb, 150, 300+30*lvUrunler.Items.Count, st);
            e.Graphics.DrawString("İndirim Tutarı   :------------- " + lblIndirim.Text+ "₺", altBaslik, sb, 250, 300 + 30 * (lvUrunler.Items.Count+1), st);
            e.Graphics.DrawString("KDV Tutarı       :------------- " + lblKdv.Text + "₺", altBaslik, sb, 250, 300 + 30 * (lvUrunler.Items.Count + 2), st);
            e.Graphics.DrawString("Toplam Tutar     :------------- " + lblToplamTutar.Text + "₺", altBaslik, sb, 250, 300 + 30 * (lvUrunler.Items.Count + 3), st);
            e.Graphics.DrawString("Ödediğiniz Tutar :------------- " + lblOdenecek.Text + "₺", altBaslik, sb, 250, 300 + 30 * (lvUrunler.Items.Count + 4), st);
        }
    }
}
