using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManavOtomasyonuTxt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string kullaniciAdi = "", sifre = "",kullanicilarDosya = "kullanicilar.txt";
        List<string> kullanicilar = new List<string>();
        List<string> urunler = new List<string>();
        List<string> siparisler = new List<string>();
        int guncellenecekID = -1;

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.BringToFront();
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            timer1.Start();

            FileStream fs = new FileStream(kullanicilarDosya,FileMode.OpenOrCreate,FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadLine();
            while (line != null)
            {
                kullanicilar.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
            UrunleriCek();
            SiparisleriCek();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 1;
            if (progressBar1.Value == 100)
            {
                Thread.Sleep(300);
                timer1.Stop();
                panelGiris.BringToFront();
            }
        }

        void IdDuzenle(string dosyaYolu)
        {
            List<string> temp = new List<string>();
            FileStream fs = new FileStream(dosyaYolu, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadLine();
            while (line != null)
            {
                temp.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
            StreamWriter sw = new StreamWriter(dosyaYolu);
            for (int i = 0; i < temp.Count; i++)
            {
                if (dosyaYolu == "urunler.txt")
                    sw.WriteLine(i + "-" + temp[i].Split('-')[1] + "-" + temp[i].Split('-')[2]);
                else if (dosyaYolu == "siparisler.txt")
                    sw.WriteLine(i + "-" + temp[i].Split('-')[1] + "-" + temp[i].Split('-')[2] + "-" + temp[i].Split('-')[3]);
            }
            sw.Close();
        }

        void UrunleriCek()
        {
            dgvSiparisVer.Rows.Clear();
            urunler.Clear();
            FileStream fs = new FileStream("urunler.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadLine();
            while (line != null)
            {
                dgvSiparisVer.Rows.Add(line.Split('-')[0], line.Split('-')[1], line.Split('-')[2]);
                urunler.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
        }

        void SiparisleriCek()
        {
            dgvSiparisler.Rows.Clear();
            siparisler.Clear();
            FileStream fs = new FileStream("siparisler.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadLine();
            while (line != null)
            {
                dgvSiparisler.Rows.Add(line.Split('-')[0], line.Split('-')[1], line.Split('-')[2], line.Split('-')[3]);
                siparisler.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
        }

        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUrunEkleBirimFiyati.Text) || String.IsNullOrEmpty(txtUrunEkleUrunAdi.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            StreamWriter sw = File.AppendText("urunler.txt");
            sw.WriteLine(urunler.Count + "-" + txtUrunEkleUrunAdi.Text + "-" + txtUrunEkleBirimFiyati.Text);
            MessageBox.Show("Ürün Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            sw.Close();
            UrunleriCek();
            txtUrunEkleUrunAdi.Text = txtUrunEkleBirimFiyati.Text = "";
        }

        private void btnSepettenCikar_Click(object sender, EventArgs e)
        {
            if (checkedListBoxSepet.CheckedItems.Count > 0)
            {
                for (int i = checkedListBoxSepet.CheckedItems.Count - 1; i >= 0; i--)
                {
                    int ucret = Convert.ToInt32(checkedListBoxSepet.CheckedItems[i].ToString().Split(' ')[checkedListBoxSepet.CheckedItems[i].ToString().Split(' ').Length - 1]);
                    txtSepetToplam.Text = (Convert.ToDouble(txtSepetToplam.Text) - ucret).ToString();
                    checkedListBoxSepet.Items.Remove(checkedListBoxSepet.CheckedItems[i]);
                }
                for (int i = 0; i < checkedListBoxSepet.Items.Count; i++)
                    checkedListBoxSepet.SetItemChecked(i, false);
            }
        }

        private void dgvSiparisVer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSiparisVer.CurrentRow != null && dgvSiparisVer.CurrentRow.Cells[0].Value != null)
            {
                guncellenecekID = Convert.ToInt32(dgvSiparisVer.CurrentRow.Cells[0].Value);
                txtUrunGuncelleAdi.Text = dgvSiparisVer.CurrentRow.Cells[1].Value.ToString();
                txtUrunGuncelleFiyati.Text = dgvSiparisVer.CurrentRow.Cells[2].Value.ToString();
                txtUrunSilAdi.Text = dgvSiparisVer.CurrentRow.Cells[1].Value.ToString();
                txtUrunSilFiyati.Text = dgvSiparisVer.CurrentRow.Cells[2].Value.ToString();
            }
        }

        private void btnSiparisVer_Click(object sender, EventArgs e)
        {
            StreamWriter sw = File.AppendText("siparisler.txt");
            int sayac = siparisler.Count;
            for (int i = 0; i < checkedListBoxSepet.Items.Count; i++)
            {
                sw.WriteLine(sayac++ + "-" + kullaniciAdi + "-" + urunler[Convert.ToInt32(checkedListBoxSepet.Items[i].ToString().Split(' ')[0])].Split('-')[1] + "-" + checkedListBoxSepet.Items[i].ToString().Split(' ')[checkedListBoxSepet.Items[i].ToString().Split(' ').Length - 1]);
            }
            sw.Close();
            if (checkedListBoxSepet.Items.Count > 0)
            {
                SiparisleriCek();
                checkedListBoxSepet.Items.Clear();
                MessageBox.Show("Sipariş verildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvSiparisVer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSiparisVer.CurrentRow != null && dgvSiparisVer.CurrentRow.Cells[0].Value != null)
            {
                double adet = 0;
                string veri;
                do
                {
                    veri = Interaction.InputBox("Kaç kilogram : ", "Adet Bilgisi", "örn : 2,5", 300, 300);
                }
                while (!Double.TryParse(veri, out adet) || veri.Contains("."));
                string[] urun = urunler[Convert.ToInt32(dgvSiparisVer.CurrentRow.Cells[0].Value)].Split('-');
                checkedListBoxSepet.Items.Add(urun[0] + " " + urun[1] + " " + urun[2] + "*" + adet.ToString() + " " + Convert.ToDouble(urun[2]) * adet);
                txtSepetToplam.Text = (Convert.ToDouble(txtSepetToplam.Text) + (Convert.ToDouble(urun[2]) * adet)).ToString();
            }
        }

        private void btnUrunGuncelle_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUrunGuncelleAdi.Text) || String.IsNullOrEmpty(txtUrunGuncelleFiyati.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FileStream fs = new FileStream("urunler.txt", FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < urunler.Count; i++)
            {
                if (urunler[i].Split('-')[0] == guncellenecekID.ToString())
                {
                    sw.WriteLine(i + "-" +txtUrunGuncelleAdi.Text + "-" + txtUrunGuncelleFiyati.Text);
                }
                else
                    sw.WriteLine(urunler[i]);
            }
            sw.Close();
            fs.Close();
            IdDuzenle("urunler.txt");
            UrunleriCek();
            txtUrunGuncelleAdi.Text = txtUrunGuncelleFiyati.Text = txtUrunSilAdi.Text = txtUrunSilFiyati.Text = "";
        }

        private void btnUrunSil_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUrunGuncelleAdi.Text) || String.IsNullOrEmpty(txtUrunGuncelleFiyati.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            StreamWriter sw = new StreamWriter("urunler.txt");
            for (int i = 0; i < urunler.Count; i++)
            {
                if (i != guncellenecekID)
                {
                    sw.WriteLine(urunler[i]);
                }
            }
            sw.Close();
            IdDuzenle("urunler.txt");
            UrunleriCek();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtGuncelleKullaniciAdi.Text) || String.IsNullOrEmpty(txtGuncelleSifre.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            StreamWriter sw = new StreamWriter("kullanicilar.txt");
            for (int i = 0; i < kullanicilar.Count; i++)
            {
                if (kullanicilar[i].Split('-')[0].Equals(kullaniciAdi))
                {
                    sw.WriteLine(txtGuncelleKullaniciAdi.Text + "-" + txtGuncelleSifre.Text);
                    continue;
                }
                sw.WriteLine(kullanicilar[i]);
            }
            sw.Close();
            MessageBox.Show("Güncelleme Başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtGuncelleKullaniciAdi.Text = txtGuncelleSifre.Text = "";
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            panelGiris.BringToFront();
        }

        private void btnGirisKayit_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtGirisKayitKullaniciAdi.Text) || String.IsNullOrEmpty(txtGirisKayitSifre.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(btnGirisKayit.Text == "Giriş Yap")
            {
                for (int i = 0; i < kullanicilar.Count; i++)
                {
                    if(txtGirisKayitKullaniciAdi.Text == kullanicilar[i].Split('-')[0] && txtGirisKayitSifre.Text == kullanicilar[i].Split('-')[1])
                    {
                        kullaniciAdi = txtGirisKayitKullaniciAdi.Text;
                        sifre = txtGirisKayitSifre.Text;
                        MessageBox.Show("Giriş Başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tabControlSiparisVerUrunEkle.BringToFront();
                        txtGirisKayitKullaniciAdi.Text = txtGirisKayitSifre.Text = "";
                        break;
                    }
                }
                if (kullanicilar.Count == 0 || kullaniciAdi == "")
                {
                    MessageBox.Show("Kullanıcı Bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                StreamWriter sw = File.AppendText(kullanicilarDosya);
                sw.WriteLine(txtGirisKayitKullaniciAdi.Text + "-" + txtGirisKayitSifre.Text);
                kullanicilar.Add(txtGirisKayitKullaniciAdi.Text + "-" + txtGirisKayitSifre.Text);
                MessageBox.Show("Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sw.Close();
                txtGirisKayitKullaniciAdi.Text = txtGirisKayitSifre.Text = "";
            }
        }

        private void linkLabelGirisKayit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtGirisKayitKullaniciAdi.Text = txtGirisKayitSifre.Text = "";
            if (linkLabelGirisKayit.Text == "Kayıt Ol")
            {
                linkLabelGirisKayit.Text = "Giriş'e Dön";
                groupBoxGirisKayit.Text = "Kayıt Ol";
                btnGirisKayit.Text = "Kayıt Ol";
            }
            else
            {
                linkLabelGirisKayit.Text = "Kayıt Ol";
                groupBoxGirisKayit.Text = "Giriş Yap";
                btnGirisKayit.Text = "Giriş Yap";
            }
        }
    }
}
