
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace restoran
{

    public partial class frmSiparis : Form
    {
        public frmSiparis()
        {
            InitializeComponent();
        }

        private siparis_yap mutfakEkrani = new siparis_yap();

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            lvMenu.View = View.Details;

        }

        private void listView1_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }



        //Hesap makinesinin butonlarını birbirine bağlamak için fonksiyon yazıyoruz. Buton tıklama olaylarını aynı event'a bağlayacağız.
        void islem(Object sender, EventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case "btn1":
                    txtAdet.Text += (1).ToString();
                    break;
                case "btn2":
                    txtAdet.Text += (2).ToString();
                    break;
                case "btn3":
                    txtAdet.Text += (3).ToString();
                    break;
                case "btn4":
                    txtAdet.Text += (4).ToString();
                    break;
                case "btn5":
                    txtAdet.Text += (5).ToString();
                    break;
                case "btn6":
                    txtAdet.Text += (6).ToString();
                    break;
                case "btn7":
                    txtAdet.Text += (7).ToString();
                    break;
                case "btn8":
                    txtAdet.Text += (8).ToString();
                    break;
                case "btn9":
                    txtAdet.Text += (9).ToString();
                    break;
                case "btn0":
                    txtAdet.Text += (0).ToString();
                    break;

                default:
                    MessageBox.Show("Sayı Gir ");
                    break;
            }
        }
        int tableID, AdditionId;
        private void frmSiparis_Load(object sender, EventArgs e)
        {

            mutfakEkrani.Show();

            //Tıkladığımız masanın ismi olan btnMasa1, btnMasa2'nin sonunda ki 1 ve 2'leri alıyor.
            lblMasaNo.Text = cGenel._ButtonValue;
            cMasalar ms = new cMasalar();
            tableID = ms.TableGetbyNumber(cGenel._ButtonName);
            if (ms.TableGetbyState(tableID, 2) == true || ms.TableGetbyState(tableID, 4) == true)
            {
                cAdisyon Ad = new cAdisyon();
                AdditionId = Ad.getByAddition(tableID);
                cSiparis orders = new cSiparis();
                orders.getByOrder(lvSiparisler, AdditionId);
            }

            btn1.Click += new EventHandler(islem);
            btn2.Click += new EventHandler(islem);
            btn3.Click += new EventHandler(islem);
            btn4.Click += new EventHandler(islem);
            btn5.Click += new EventHandler(islem);
            btn6.Click += new EventHandler(islem);
            btn7.Click += new EventHandler(islem);
            btn8.Click += new EventHandler(islem);
            btn9.Click += new EventHandler(islem);
            btn0.Click += new EventHandler(islem);

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            //Çıkış butonuna basıldığında ekrana uyarı verecek ve eğer evet derse kullanıcı uygulama kapanacak.
            if (MessageBox.Show("Çıkmak İstediğinizden Emin Misiniz?", "Dikkat !  ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();//Uyarı veriyoruz eğer Evet'e basarsa kullanıcı uygulamadan çıkacak.
            }
        }
        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }
        cUrunCesitleri Uc = new cUrunCesitleri();
        private void btnAnaYemek3_Click(object sender, EventArgs e)
        {

            Uc.getByProductTypes(lvMenu, btnAnaYemek3);
        }

        private void btnIcecekler8_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnIcecekler8);
        }

        private void btnTatlilar7_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnTatlilar7);
        }

        private void btnSalatalar6_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnSalatalar6);
        }

        private void btnFastFood5_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnFastFood5);
        }

        private void btnCorbalar1_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnCorbalar1);
        }

        private void btnMakarnalar4_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnMakarnalar4);
        }

        private void btnArasicak2_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnArasicak2);
        }
        int sayac = 0, sayac2 = 0;

        ArrayList silinler = new ArrayList();
        private void lvSiparisler_DoubleClick(object sender, EventArgs e)//Siparişlerin silinmesi için Listview'de ürüne çift tıklandığında silinmesi
        {
            if (lvSiparisler.Items.Count > 0)
            {
                if (lvSiparisler.SelectedItems[0].SubItems[4].Text != "0")
                {
                    cSiparis saveOrder = new cSiparis();
                    saveOrder.setDeleteOrder(Convert.ToInt32(lvSiparisler.SelectedItems[0].SubItems[4].Text));
                }
                else
                {
                    for (int i = 0; i < lvYeniEklenenler.Items.Count; i++)
                    {
                        if (lvYeniEklenenler.Items[i].SubItems[4].Text == lvSiparisler.SelectedItems[0].SubItems[5].Text)
                        {
                            lvYeniEklenenler.Items.RemoveAt(i);
                        }
                    }
                }
                lvSiparisler.Items.RemoveAt(lvSiparisler.SelectedItems[0].Index);
            }
        }
        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            if (txtAra.Text == "")
            {
                txtAra.Text = "";
            }
            else
            {
                cUrunCesitleri cu = new cUrunCesitleri();
                cu.getByProductSearch(lvMenu, Convert.ToInt32(txtAra.Text));
            }
        }
        private void btnSiparis_Click(object sender, EventArgs e)
        {
            /*
            1 - Masa Boş
            2 - Masa Dolu
            3 - Masa Rezerve
            4 - Masa Açıkrezerve
            */
            if (lvSiparisler.Items.Count == 0)
            {
                MessageBox.Show("Lütfen sipariş için önce ürün ekleyiniz!");
            }
            else
            {
                cMasalar masa = new cMasalar();
                cAdisyon newAddition = new cAdisyon();
                cSiparis saveOrder = new cSiparis();
                frmMasalar ms = new frmMasalar();
                bool sonuc = false;
                if (masa.TableGetbyState(tableID, 1) == true)
                {
                    newAddition.ServisTurNo = 1;

                    newAddition.PersonelId = 1;
                    newAddition.MasaId = tableID;
                    newAddition.Tarih = DateTime.Now;
                    sonuc = newAddition.setByAdditionNew(newAddition);
                    masa.setChangeTableState(cGenel._ButtonName, 2);
                    //Adisyon açıldı ve masa bilgileri gönderildi.
                    if (lvSiparisler.Items.Count > 0)
                    {
                        for (int i = 0; i < lvSiparisler.Items.Count; i++)
                        {
                            saveOrder.MasaId = tableID;
                            saveOrder.UrunId = Convert.ToInt32(lvSiparisler.Items[i].SubItems[2].Text);
                            saveOrder.AdisyonID = newAddition.getByAddition(tableID);
                            saveOrder.Adet = Convert.ToInt32(lvSiparisler.Items[i].SubItems[1].Text);
                            saveOrder.setSaveOrder(saveOrder);
                        }
                        this.Close();
                        ms.Show();
                    }
                }
                else if (masa.TableGetbyState(tableID, 2) == true || masa.TableGetbyState(tableID, 4) == true)//Masa doluysa yeni eklenen siparişleri adisyona eklemek için
                {
                    if (lvSiparisler.Items.Count == 0)
                    {
                        MessageBox.Show("Lütfen sipariş için önce ürün ekleyiniz!");
                    }
                    else
                    {
                        if (lvYeniEklenenler.Items.Count > 0)
                        {
                            for (int i = 0; i < lvYeniEklenenler.Items.Count; i++)
                            {
                                saveOrder.MasaId = tableID;
                                saveOrder.UrunId = Convert.ToInt32(lvYeniEklenenler.Items[i].SubItems[1].Text);
                                saveOrder.AdisyonID = newAddition.getByAddition(tableID);
                                saveOrder.Adet = Convert.ToInt32(lvYeniEklenenler.Items[i].SubItems[2].Text);
                                saveOrder.setSaveOrder(saveOrder);
                            }
                        }
                        if (silinler.Count > 0)
                        {
                            foreach (string item in silinler)
                            {
                                saveOrder.setDeleteOrder(Convert.ToInt32(item));
                            }
                        }
                        this.Close();
                        ms.Show();
                    }

                }
                else if (masa.TableGetbyState(tableID, 3) == true)
                {

                    //newAddition.ServisTurNo = 1;
                    //newAddition.PersonelId = 1;
                    //newAddition.MasaId = tableID;
                    //newAddition.Tarih = DateTime.Now;
                    //sonuc = newAddition.setByAdditionNew(newAddition);
                    masa.setChangeTableState(cGenel._ButtonName, 4);
                    //Adisyon açıldı ve masa bilgileri gönderildi.
                    if (lvSiparisler.Items.Count > 0)
                    {
                        for (int i = 0; i < lvSiparisler.Items.Count; i++)
                        {
                            saveOrder.MasaId = tableID;
                            saveOrder.UrunId = Convert.ToInt32(lvSiparisler.Items[i].SubItems[2].Text);
                            saveOrder.AdisyonID = newAddition.getByAddition(tableID);
                            saveOrder.Adet = Convert.ToInt32(lvSiparisler.Items[i].SubItems[1].Text);
                            saveOrder.setSaveOrder(saveOrder);
                        }
                        this.Close();
                        ms.Show();
                    }
                }
            }
        }
        private siparis_yap _siparisForm;  // siparis_yap formunun referansı

        // Siparis_yap formunu açma veya var olanı getirme
        private void OpenSiparisYapForm()
        {
            if (_siparisForm == null || _siparisForm.IsDisposed)
            {
                _siparisForm = new siparis_yap();  // yeni form örneği oluştur
                _siparisForm.Show();  // formu göster
            }
        }

        private void lvSiparisler_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblMasaNo_Click(object sender, EventArgs e)
        {

        }

        private void lvMenu_DoubleClick(object sender, EventArgs e)
        {

            if (txtAdet.Text == "")
            {
                txtAdet.Text = "1";
            }

            if (lvMenu.Items.Count > 0)
            {
                // Sipariş bilgilerini al
                string urunAdi = lvMenu.SelectedItems[0].Text;
                string adet = txtAdet.Text;
                string fiyat = (Convert.ToDecimal(lvMenu.SelectedItems[0].SubItems[1].Text) * Convert.ToDecimal(adet)).ToString();

                // lvSiparisler'e ekle
                int sayac = lvSiparisler.Items.Count;
                lvSiparisler.Items.Add(urunAdi);
                lvSiparisler.Items[sayac].SubItems.Add(adet);
                lvSiparisler.Items[sayac].SubItems.Add(lvMenu.SelectedItems[0].SubItems[2].Text);
                lvSiparisler.Items[sayac].SubItems.Add(fiyat);
                lvSiparisler.Items[sayac].SubItems.Add("0");

                // Mutfak ekranına siparişi gönder
                mutfakEkrani.SiparisEkle(urunAdi, adet, fiyat);

                txtAdet.Text = "";
            }
        }



        private void btnOdeme_Click(object sender, EventArgs e)
        {
            cGenel._servisTurNo = 1;
            cGenel._adisyonId = AdditionId.ToString();//Ödeme butonuna bastığında adisyon ID'sini alıyoruz.
            frmBill frm = new frmBill();
            this.Close();
            frm.Show();
        }
    }
}
