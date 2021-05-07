using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatbaaUygulaması
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }
        string kullaniciId = "";
        private void buttonGiris_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("kullanicilar.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string kullaniciAdi = "", kullaniciSifre = "",kullaniciBilgileri;
            do
            {
                kullaniciBilgileri = sr.ReadLine();
                if(kullaniciBilgileri != null)
                {
                    kullaniciId = kullaniciBilgileri.Split('-')[0];
                    kullaniciAdi = kullaniciBilgileri.Split('-')[1];
                    kullaniciSifre = kullaniciBilgileri.Split('-')[2];
                    if (kullaniciAdi.Equals(textBoxKullaniciAdi.Text) && kullaniciSifre.Equals(textBoxSifre.Text))
                    {
                        panelUygulama.BringToFront();
                        break;
                    }   
                }
            }
            while (kullaniciBilgileri != null);
            if(kullaniciBilgileri == null)
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre Yanlış");
            }
            else
            {
                labelHosgeldiniz.Text = "Hoşgeldiniz";
                textBoxKullaniciAdiGuncelle.Text = kullaniciAdi;
                textBoxKullaniciSifreGuncelle.Text = kullaniciSifre;
                urunleriListele();
                siparisleriListele();
            }
            sr.Close();
            fs.Close();
            
        }

        private void buttonUrunEkle_Click(object sender, EventArgs e)
        {
            if(textBoxUrunEkleAd.Text == null || textBoxUrunEkleAd.Text.ToString().Trim().Equals(""))
            {
                MessageBox.Show("Ürün Adı Boş Olamaz!");
                return;
            }
            string[] urunler = File.ReadAllLines("urunler.txt");
            int urunId = (File.ReadAllLines("urunler.txt").Length > 0) ? Int32.Parse(File.ReadAllLines("urunler.txt")[File.ReadAllLines("urunler.txt").Length - 1].Split('-')[0].ToString()) + 1 : 0;
            StreamWriter sw = File.AppendText("urunler.txt");
            sw.WriteLine(urunId.ToString() + "-" + textBoxUrunEkleAd.Text + "-" + numUrunBirimFiyat.Value.ToString());
            sw.Close();
            urunleriListele();
        }

        public void urunleriListele()
        {
            dataGridViewUrunler.Rows.Clear();
            checkedListBoxUrunSil.Items.Clear();
            string[] urunler = File.ReadAllLines("urunler.txt");
            for(int i = 0; i < urunler.Length; i++)
            {
                dataGridViewUrunler.Rows.Add(urunler[i].Split('-')[1], urunler[i].Split('-')[2]);
                checkedListBoxUrunSil.Items.Add(urunler[i].Split('-')[1]);
            }
        }

        public void sepetToplamiHesapla()
        {
            double toplam = 0;
            if (checkedListBoxSepet.Items.Count != 0)
            {
                for(int i = 0; i < checkedListBoxSepet.Items.Count; i++)
                {
                    toplam += Double.Parse(checkedListBoxSepet.Items[i].ToString().Split(':')[1].Trim());
                }
            }
            label1ToplamTutar.Text = toplam.ToString() + " TL";
        }

        public void siparisleriListele()
        {
            dataGridViewSiparisler.Rows.Clear();
            string[] siparisler = File.ReadAllLines("siparisler.txt");
            if(siparisler.Length > 0)
            {
                for (int i = 0; i < siparisler.Length; i++)
                {
                    dataGridViewSiparisler.Rows.Add(siparisler[i].Split('-')[0], siparisler[i].Split('-')[1], siparisler[i].Split('-')[2], siparisler[i].Split('-')[3], siparisler[i].Split('-')[4]);
                }
            }
            
        }

        private void dataGridViewUrunler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewUrunler.CurrentCell.Value == null)
                return;

            if (dataGridViewUrunler.CurrentCell.Value.ToString().Equals("Sepete Ekle"))
            {
                if (dataGridViewUrunler.CurrentRow.Cells[0].Value != null)
                {
                    int adet = 0;
                    string adetString = "";
                    do
                    {
                        adetString = Interaction.InputBox("Adet:", "Adet Giriniz", "1,2,3...", 200, 200);
                    } while (!Int32.TryParse(adetString, out adet));

                    string birimFiyat = dataGridViewUrunler.CurrentRow.Cells[1].Value.ToString();
                    float birimfiyat = float.Parse(birimFiyat);
                    double toplam =Math.Round((birimfiyat * adet),2);
                    checkedListBoxSepet.Items.Add(dataGridViewUrunler.CurrentRow.Cells[0].Value.ToString() + "x" + adet.ToString() + " adet Toplam: " + toplam.ToString());
                    sepetToplamiHesapla();
                }
            }
            else if (dataGridViewUrunler.CurrentCell.Value.ToString().Equals("Güncelle"))
            {
                if (dataGridViewUrunler.CurrentRow.Cells[0].Value == null || dataGridViewUrunler.CurrentRow.Cells[0].Value.ToString().Trim().Equals(""))
                {
                    MessageBox.Show("Ürün Adı Boş Olamaz!");
                }
                else
                {
                    if(dataGridViewUrunler.CurrentRow.Cells[1].Value == null || dataGridViewUrunler.CurrentRow.Cells[1].Value.ToString().Trim().Equals(""))
                    {
                        MessageBox.Show("Ürün Birim Fiyatı Boş Olamaz!");
                    }
                    else
                    {
                        double birimFiyat;
                        if(Double.TryParse(dataGridViewUrunler.CurrentRow.Cells[1].Value.ToString().Trim(),out birimFiyat))
                        {
                            string[] urunler = File.ReadAllLines("urunler.txt");
                            int guncellenecekUrunId = dataGridViewUrunler.CurrentRow.Index;
                            StreamWriter sw = new StreamWriter("urunler.txt");
                            for(int i = 0; i < urunler.Length; i++)
                            {
                                if(i == guncellenecekUrunId)
                                {
                                    sw.WriteLine(urunler[i].Split('-')[0] + "-" + dataGridViewUrunler.CurrentRow.Cells[0].Value.ToString().Trim() + "-" + birimFiyat.ToString());
                                }
                                else
                                {
                                    sw.WriteLine(urunler[i]);
                                }

                            }
                            sw.Close();
                            urunleriListele();

                        }
                        else
                        {
                            MessageBox.Show("Geçersiz Birim Fiyatı!");
                        }
                    }
                }
            }
            
        }

        private void buttonUrunSil_Click(object sender, EventArgs e)
        {
            string[] urunler = File.ReadAllLines("urunler.txt");
            StreamWriter sw = new StreamWriter("urunler.txt");
            for(int i = 0; i < urunler.Length; i++) 
            {
                for(int j = 0; j < checkedListBoxUrunSil.CheckedItems.Count; j++)
                {
                    if (urunler[i].Split('-')[1].Equals(checkedListBoxUrunSil.CheckedItems[j].ToString()))
                    {
                        i++;
                    }
                }
                if (checkedListBoxUrunSil.CheckedItems.Count < urunler.Length)
                    sw.WriteLine(urunler[i]);
                else
                    sw.Write("");
            }
            sw.Close();
            urunleriListele();
        }

        private void buttonSeciliUrunleriSil_Click(object sender, EventArgs e)
        {
            if(checkedListBoxSepet.CheckedItems.Count > 0)
            {
                for(int i = checkedListBoxSepet.CheckedItems.Count -1; i >= 0; i--)
                {
                    checkedListBoxSepet.Items.Remove(checkedListBoxSepet.CheckedItems[i]);
                }
                sepetToplamiHesapla();
            }
        }

        private void buttonSiparisiOnayla_Click(object sender, EventArgs e)
        {
            if(checkedListBoxSepet.Items.Count > 0)
            {
                if(textBoxSepetMusteri.Text != "")
                {
                    int siparisSayisi = (File.ReadAllLines("siparisler.txt").Length > 0) ?Int32.Parse(File.ReadAllLines("siparisler.txt")[File.ReadAllLines("siparisler.txt").Length-1].Split('-')[0].ToString())+1 : 0;
                    string sepettekiUrunler = "";
                    string sepettekiUrunlerinAdetleri = "";
                    for(int i = 0; i < checkedListBoxSepet.Items.Count; i++)
                    {
                        sepettekiUrunler += ":" + checkedListBoxSepet.Items[i].ToString().Split('x')[0];
                        sepettekiUrunlerinAdetleri += "_" + checkedListBoxSepet.Items[i].ToString().Split('x')[1].Split(' ')[0];
                    }
                    sepettekiUrunler += ":";
                    sepettekiUrunlerinAdetleri += "_";
                    StreamWriter sw = File.AppendText("siparisler.txt");
                    sw.WriteLine(siparisSayisi.ToString() + "-" + textBoxSepetMusteri.Text + "-" + sepettekiUrunler + "-" + sepettekiUrunlerinAdetleri + "-" + label1ToplamTutar.Text);
                    sw.Close();
                    dataGridViewSiparisler.Rows.Add(siparisSayisi.ToString(),textBoxSepetMusteri.Text,sepettekiUrunler,sepettekiUrunlerinAdetleri , label1ToplamTutar.Text);
                    checkedListBoxSepet.Items.Clear();
                    textBoxSepetMusteri.Text = "";
                    label1ToplamTutar.Text = "";
                }
                else
                {
                    MessageBox.Show("Müşteri Adını Girilmeli!");
                }
            }
            else
            {
                MessageBox.Show("Sepet Boş!");
            }
        }

        private void dataGridViewSiparisler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewSiparisler.CurrentCell.Value == null)
                return;

           if (dataGridViewSiparisler.CurrentCell.Value.ToString().Equals("Sipariş Sil"))
            {
                string[] siparisler = File.ReadAllLines("siparisler.txt");
                StreamWriter sw = new StreamWriter("siparisler.txt");
                for (int i = 0; i < siparisler.Length; i++)
                {
                    if (siparisler[i].Split('-')[0].Equals(dataGridViewSiparisler.CurrentRow.Cells[0].Value.ToString()))
                    {
                        continue;
                    }
                    sw.WriteLine(siparisler[i]);
                }
                sw.Close();
                siparisleriListele();
            }
        }

        private void buttonKayıtOl_Click(object sender, EventArgs e)
        {
            if(textBoxKullaniciAdi.Text == null || textBoxKullaniciAdi.Text.ToString().Trim().Equals(""))
            {
                MessageBox.Show("Kullanıcı Adı Boş olamaz");
            }
            else if(textBoxSifre.Text == null || textBoxSifre.Text.ToString().Trim().Equals(""))
            {
                MessageBox.Show("Şifre Boş olamaz");
            }
            else
            {
                int kullaniciSayisi = (File.ReadAllLines("kullanicilar.txt").Length > 0) ? Int32.Parse(File.ReadAllLines("kullanicilar.txt")[File.ReadAllLines("kullanicilar.txt").Length - 1].Split('-')[0].ToString()) + 1 : 0;
                StreamWriter sw = File.AppendText("kullanicilar.txt");
                sw.WriteLine(kullaniciSayisi.ToString() + "-" + textBoxKullaniciAdi.Text + "-" + textBoxSifre.Text);
                sw.Close();
                MessageBox.Show("Kayıt Başarılı");
                textBoxKullaniciAdi.Text = "";
                textBoxSifre.Text = "";
            }
        }

        private void loginForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("resim.jpg");
            FileStream fs1 = new FileStream("kullanicilar.txt",FileMode.OpenOrCreate);
            fs1.Close();
            FileStream fs2 = new FileStream("urunler.txt", FileMode.OpenOrCreate);
            fs2.Close();
            FileStream fs3 = new FileStream("siparisler.txt", FileMode.OpenOrCreate);
            fs3.Close();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value++;
            if(progressBar1.Value == progressBar1.Maximum)
            {
                timer1.Stop();
                panelProgresBar.Visible = false;
            }
        }

        private void buttonKullaniciBilgileriGuncelle_Click(object sender, EventArgs e)
        {
            string[] kullanicilar = File.ReadAllLines("kullanicilar.txt");
            StreamWriter sw = new StreamWriter("kullanicilar.txt");
            for(int i = 0; i < kullanicilar.Length; i++)
            {
                if (kullaniciId.Equals(kullanicilar[i].Split('-')[0]))
                {
                    sw.WriteLine(kullaniciId + "-" + textBoxKullaniciAdiGuncelle.Text + "-" + textBoxKullaniciSifreGuncelle.Text);
                    continue;
                }
                sw.WriteLine(kullanicilar[i]);
            }
            sw.Close();
            MessageBox.Show("Güncelleme Başarılı");
        }
    }
}
