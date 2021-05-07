using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Npgsql;

namespace alisverisUygulamasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        NpgsqlConnection con = new NpgsqlConnection("Server=localhost; Port=5432; Database=denemeStokTakip; User Id=postgres; Password=15935788gs05;");
        string kullanici_adi = "";
        int kullanici_id = 0;

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            bool hata = false;
            if(String.IsNullOrEmpty(txtKayitOlKullaniciAdi.Text) || String.IsNullOrEmpty(txtKayitOlisim.Text) || String.IsNullOrEmpty(txtKayitOlSoyisim.Text) || String.IsNullOrEmpty(txtKayitOlSifre.Text) || String.IsNullOrEmpty(txtKayitOlSifreTekrar.Text) || String.IsNullOrEmpty(txtKayitOlTelefon.Text) || String.IsNullOrEmpty(txtKayitOlGuvenlikCevap.Text) || comboBoxKayitOlGuvenlikSorusu.Text == "Seçiniz..")
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }else
            {
                if(txtKayitOlKullaniciAdi.Text.Length < 3)
                {
                    MessageBox.Show("Geçerli bir kullanıcı adı giriniz(minimum 3 karakter).", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    hata = true;
                }
                if (kayitOlDogumTarihi.Value >= DateTime.Now)
                {
                    MessageBox.Show("Geçerli bir tarih seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    hata = true;
                }
                if (txtKayitOlSifre.Text != txtKayitOlSifreTekrar.Text)
                {
                    MessageBox.Show("Şifreler eşleşmiyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    hata = true;
                }
                if (txtKayitOlTelefon.Text.Length < 10)
                {
                    MessageBox.Show("Geçerli bir telefon numarası giriniz(başında sıfır olmadan 10 haneli).", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    hata = true;
                }
                if (!hata)
                {
                    int cinsiyet = 0, guvenlikSoruNo = 0 ;
                    if (kayitOlRadioButtonErkek.Checked)
                        cinsiyet = 1; 
                    else
                        cinsiyet = 0;

                    string dogumTarihi = kayitOlDogumTarihi.Value.Year.ToString() + "-" + kayitOlDogumTarihi.Value.Month.ToString() + "-" + kayitOlDogumTarihi.Value.Day.ToString();
                    con.Open();

                    NpgsqlCommand cmd1 = new NpgsqlCommand("select g_soru_no from stok.guvenlik_sorulari where g_sorusu = '"+ comboBoxKayitOlGuvenlikSorusu.SelectedItem +"'", con);
                    NpgsqlDataReader oku = cmd1.ExecuteReader();
                    if (oku.Read())
                    {
                        guvenlikSoruNo = Convert.ToInt32(oku["g_soru_no"]);
                    }
                    con.Close();

                    con.Open();
                    NpgsqlCommand cmd2 = new NpgsqlCommand("INSERT INTO stok.kullanicilar(sifre,kul_adi,isim,soyisim,telefon,dogum_tarihi,cinsiyet_id,g_soru_no,g_soru_cevabi) VALUES('" + txtKayitOlSifre.Text + "','" + txtKayitOlKullaniciAdi.Text + "','" + txtKayitOlisim.Text + "','" + txtKayitOlSoyisim.Text + "','" + txtKayitOlTelefon.Text + "','" + dogumTarihi + "'," + cinsiyet + "," + guvenlikSoruNo + ",'" + txtKayitOlGuvenlikCevap.Text + "')", con);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Kayıt Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    kayitOlTemizle();
                }
            }
        }

        private void kayitOlTemizle()
        {
            txtKayitOlKullaniciAdi.Text = "";
            txtKayitOlSifre.Text = "";
            txtKayitOlSifreTekrar.Text = "";
            txtKayitOlisim.Text = "";
            txtKayitOlSoyisim.Text = "";
            txtKayitOlTelefon.Text = "";
            txtKayitOlGuvenlikCevap.Text = "";
            kayitOlDogumTarihi.Value = DateTime.Now;
            comboBoxKayitOlGuvenlikSorusu.SelectedIndex = -1;
            comboBoxKayitOlGuvenlikSorusu.Text = "Seçiniz..";
        }

        private void KullaniciAdresleriComboBox()
        {
            comboBoxKullaniciUrunlerAdres.Items.Clear();
            comboBoxKullaniciUrunlerAdres.DataBindings.Clear();
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM stok.adresler where kul_no IN (SELECT kul_no FROM stok.kullanicilar where kul_adi='" + kullanici_adi + "')", con);
            NpgsqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                comboBoxKullaniciUrunlerAdres.Items.Add(oku["adres"]);
            }
            con.Close();
        }

        private void sifreDegistirTemizle()
        {
            txtSifreDegistirGuvenlikSorusuCevabi.Text = "";
            txtSifreDegistirKullaniciAdi.Text = "";
            txtSifreDegistirYeniSifre.Text = "";
            txtSifreDegistirYeniSifreTekrar.Text = "";
            comboBoxSifreDegistirGuvenlikSorusu.SelectedIndex = -1;
            comboBoxSifreDegistirGuvenlikSorusu.Text = "Seçiniz..";
        }

        private void txtKayitOlTelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtKayitOlTelefon.Text.Length > 9)
            {
                e.Handled = true;
            }else
            {
                switch (e.KeyChar)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '\b':
                        e.Handled = false;
                        break;
                    default:
                        e.Handled = true;
                        break;
                }
            }
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            if(txtKayitOlTelefon.Text.Length < 1 && e.KeyChar == '0')
            {
                e.Handled = true;
            }
        }

        private void UrunFiyatTakibi()
        {
            con.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from stok.urun_fiyat_degisikligi", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridViewYoneticiUrunFiyatTakibi.DataSource = ds.Tables[0];
            con.Close();
        }

        private void SilinenKullanicilar()
        {
            con.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from stok.silinen_kullanicilar", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridViewSilinenKullanicilar.DataSource = ds.Tables[0];
            con.Close();
            dataGridViewSilinenKullanicilar.Columns[0].Width = 50;
            dataGridViewSilinenKullanicilar.Columns[7].Width = 60;
            dataGridViewSilinenKullanicilar.Columns[6].Width = 70;
            dataGridViewSilinenKullanicilar.Columns[5].Width = 75;
            dataGridViewSilinenKullanicilar.Columns[9].Width = 60;

            con.Open();
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("select * from stok.silinen_adresler", con);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            dataGridViewSilinenAdresler.DataSource = ds1.Tables[0];
            con.Close();

            con.Open();
            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter("select * from stok.silinen_kargo", con);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridViewSilinenKargolar.DataSource = ds2.Tables[0];
            con.Close();
        }

        private void YoneticiKargo()
        {
            txtYoneticiGuncelleKargoAdi.DataBindings.Clear();
            txtYoneticiGuncelleKargoId.DataBindings.Clear();
            con.Open();
            BindingSource bs = new BindingSource();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from stok.kargo", con);
            NpgsqlCommandBuilder cb = new NpgsqlCommandBuilder(da);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bs.DataSource = dt;
            dataGridViewYoneticiKargo.DataSource = bs;
            txtYoneticiGuncelleKargoId.DataBindings.Add("text", bs, "kargo_id");
            txtYoneticiGuncelleKargoAdi.DataBindings.Add("text", bs, "kargo_adi");
            con.Close();
        }

        private void YoneticiKategori()
        {
            txtYoneticiGuncelleKategoriAdi.DataBindings.Clear();
            txtYoneticiGuncelleKategoriId.DataBindings.Clear();
            txtYoneticiGuncelleKategoriAciklama.DataBindings.Clear();
            con.Open();
            BindingSource bs = new BindingSource();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from stok.kategori", con);
            NpgsqlCommandBuilder cb = new NpgsqlCommandBuilder(da);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bs.DataSource = dt;
            dataGridViewYoneticiKategori.DataSource = bs;
            txtYoneticiGuncelleKategoriId.DataBindings.Add("text", bs, "k_id");
            txtYoneticiGuncelleKategoriAdi.DataBindings.Add("text", bs, "k_adi");
            txtYoneticiGuncelleKategoriAciklama.DataBindings.Add("text", bs, "k_aciklama");
            con.Close();
            dataGridViewYoneticiKategori.Columns[2].Width = 400;
        }

        private void YoneticiKullanicilar()
        {
            txtYoneticiSilinecekKullanici.DataBindings.Clear();
            con.Open();
            BindingSource bs = new BindingSource();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from stok.kullanicilar", con);
            NpgsqlCommandBuilder cb = new NpgsqlCommandBuilder(da);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bs.DataSource = dt;
            dataGridViewYoneticiKullanicilar.DataSource = bs;
            txtYoneticiSilinecekKullanici.DataBindings.Add("text", bs, "kul_adi");
            con.Close();
            dataGridViewYoneticiKullanicilar.Columns[0].Width = 50;
            dataGridViewYoneticiKullanicilar.Columns[7].Width = 60;
            dataGridViewYoneticiKullanicilar.Columns[8].Width = 60;
        }

        private void YoneticiTedarikciler()
        {
            txtYoneticiGuncelleTedarikciId.DataBindings.Clear();
            txtYoneticiGuncelleTedarikciAdi.DataBindings.Clear();
            txtYoneticiGuncelleTedarikciTelefon.DataBindings.Clear();
            txtYoneticiGuncelleTedarikciAdres.DataBindings.Clear();
            comboBoxYoneticiGuncelleTedarikciIl.DataBindings.Clear();
            comboBoxYoneticiGuncelleTedarikciIlce.DataBindings.Clear();
            con.Open();
            BindingSource bs = new BindingSource();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from stok.tedarikci", con);
            NpgsqlCommandBuilder cb = new NpgsqlCommandBuilder(da);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bs.DataSource = dt;
            dataGridViewYoneticiTedarikciler.DataSource = bs;
            txtYoneticiGuncelleTedarikciId.DataBindings.Add("text", bs, "t_id");
            txtYoneticiGuncelleTedarikciAdi.DataBindings.Add("text", bs, "t_adi");
            txtYoneticiGuncelleTedarikciTelefon.DataBindings.Add("text", bs, "telefon");
            txtYoneticiGuncelleTedarikciAdres.DataBindings.Add("text", bs, "adresi");
            comboBoxYoneticiGuncelleTedarikciIl.DataBindings.Add("text", bs, "plaka_no");
            comboBoxYoneticiGuncelleTedarikciIlce.DataBindings.Add("text", bs, "ilce_no");
            con.Close();

            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select * from stok.iller", con);
            NpgsqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                comboBoxYoneticiEkleTedarikciIl.Items.Add(oku["il_adi"].ToString());
            }     
            con.Close();
        }

        private void KullaniciSiparisler()
        {
            con.Open();
            BindingSource bs = new BindingSource();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from stok.siparisler where kul_id=" + kullanici_id + "", con);
            NpgsqlCommandBuilder cb = new NpgsqlCommandBuilder(da);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bs.DataSource = dt;
            dataGridViewKullaniciSiparislerim.DataSource = bs;
            con.Close();
            dataGridViewKullaniciSiparislerim.Columns[0].Width = 50;
            dataGridViewKullaniciSiparislerim.Columns[1].Width = 50;
            dataGridViewKullaniciSiparislerim.Columns[3].Width = 50;
            dataGridViewKullaniciSiparislerim.Columns[4].Width = 60;
            dataGridViewKullaniciSiparislerim.Columns[5].Width = 60;
        }

        private void KullaniciAdresler()
        {
            txtKullaniciAdresSilKulNo.DataBindings.Clear();
            txtKullaniciAdresSilAdresNo.DataBindings.Clear();
            con.Open();
            BindingSource bs = new BindingSource();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from stok.adresler where kul_no=" + kullanici_id + "", con);
            NpgsqlCommandBuilder cb = new NpgsqlCommandBuilder(da);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bs.DataSource = dt;
            dataGridViewKullaniciAdreslerim.DataSource = bs;
            txtKullaniciAdresSilKulNo.DataBindings.Add("Text", bs, "kul_no");
            txtKullaniciAdresSilAdresNo.DataBindings.Add("Text", bs, "adr_no");
            con.Close();
        }

        private void KullaniciUrunler()
        {
            labelUrunAdi.DataBindings.Clear();
            labelUrunId.DataBindings.Clear();
            labelStokDurumu.DataBindings.Clear();
            labelSiparisMiktari.DataBindings.Clear();
            labelUrunFiyati.DataBindings.Clear();
            con.Open();
            BindingSource bs = new BindingSource();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select urun_id,urun_adi,birim_fiyati,stok_adedi,siparis_adedi from stok.urunler", con);
            NpgsqlCommandBuilder cb = new NpgsqlCommandBuilder(da);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bs.DataSource = dt;
            dataGridViewKullaniciUrunler.DataSource = bs;
            labelUrunAdi.DataBindings.Add("Text", bs, "urun_adi");
            labelStokDurumu.DataBindings.Add("Text", bs, "stok_adedi");
            labelSiparisMiktari.DataBindings.Add("Text", bs, "siparis_adedi");
            labelUrunFiyati.DataBindings.Add("Text", bs, "birim_fiyati");
            labelUrunId.DataBindings.Add("Text", bs, "urun_id");
            con.Close();
        }

        private void YoneticiSiparisler()
        {
            con.Open();
            BindingSource bs = new BindingSource();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from stok.siparisler", con);
            NpgsqlCommandBuilder cb = new NpgsqlCommandBuilder(da);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bs.DataSource = dt;
            dataGridViewYoneticiSiparisler.DataSource = bs;
            con.Close();
            dataGridViewYoneticiSiparisler.Columns[0].Width = 50;
            dataGridViewYoneticiSiparisler.Columns[2].Width = 72;
            dataGridViewYoneticiSiparisler.Columns[5].Width = 60;
            dataGridViewYoneticiSiparisler.Columns[6].Width = 60;
            dataGridViewYoneticiSiparisler.Columns[7].Width = 90;
        }

        private void YoneticiUrunler()
        {
            listBoxYoneticiKategoriler.Items.Clear();
            listBoxYoneticiTedarikciler.Items.Clear();
            txtYoneticiUrunGuncelleId.DataBindings.Clear();
            txtYoneticiUrunGuncelleAdi.DataBindings.Clear();
            txtYoneticiUrunGuncelleKategoriId.DataBindings.Clear();
            txtYoneticiUrunGuncelleBirimFiyati.DataBindings.Clear();
            txtYoneticiUrunGuncelleStokAdedi.DataBindings.Clear();
            txtYoneticiUrunGuncelleSiparisAdedi.DataBindings.Clear();
            txtYoneticiUrunGuncelleTedarikciId.DataBindings.Clear();

            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select * from stok.kategori", con);
            NpgsqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                listBoxYoneticiKategoriler.Items.Add(oku["k_id"].ToString() + "." + oku["k_adi"].ToString());
            }
            con.Close();

            con.Open();
            NpgsqlCommand cmd1 = new NpgsqlCommand("select * from stok.tedarikci", con);
            NpgsqlDataReader oku1 = cmd1.ExecuteReader();
            while (oku1.Read())
            {
                listBoxYoneticiTedarikciler.Items.Add(oku1["t_id"].ToString() + "." + oku1["t_adi"].ToString());
            }
            con.Close();

            con.Open();
            BindingSource bs = new BindingSource();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from stok.urunler", con);
            NpgsqlCommandBuilder cb = new NpgsqlCommandBuilder(da);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bs.DataSource = dt;
            dataGridViewYoneticiUrunler.DataSource = bs;
            txtYoneticiUrunGuncelleId.DataBindings.Add("text", bs, "urun_id");
            txtYoneticiUrunGuncelleAdi.DataBindings.Add("text", bs, "urun_adi");
            txtYoneticiUrunGuncelleKategoriId.DataBindings.Add("text", bs, "kategori_id");
            txtYoneticiUrunGuncelleBirimFiyati.DataBindings.Add("text", bs, "birim_fiyati");
            txtYoneticiUrunGuncelleStokAdedi.DataBindings.Add("text", bs, "stok_adedi");
            txtYoneticiUrunGuncelleSiparisAdedi.DataBindings.Add("text", bs, "siparis_adedi");
            txtYoneticiUrunGuncelleTedarikciId.DataBindings.Add("text", bs, "tedarikci_id");
            con.Close();

            dataGridViewYoneticiUrunler.Columns[0].Width = 50;
            dataGridViewYoneticiUrunler.Columns[2].Width = 70;
            dataGridViewYoneticiUrunler.Columns[4].Width = 70;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select g_sorusu from stok.guvenlik_sorulari", con);
            NpgsqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                comboBoxKayitOlGuvenlikSorusu.Items.Add(oku["g_sorusu"].ToString());
                comboBoxSifreDegistirGuvenlikSorusu.Items.Add(oku["g_sorusu"].ToString());
            }
            con.Close();

            YoneticiUrunler();
            SilinenKullanicilar();
            YoneticiKullanicilar();
            YoneticiKargo();
            YoneticiKategori();
            YoneticiTedarikciler();
            PanelKullaniciGirisi.BringToFront();
        }

        private void linkLabelKayitOlIptal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PanelKullaniciGirisi.BringToFront();
            kayitOlTemizle();
        }

        private void linkKayitOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            kayitOlTemizle();
            panelKayitOl.BringToFront();
        }

        private void linkLabelSifreDegistirIptal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PanelKullaniciGirisi.BringToFront();
            sifreDegistirTemizle();
        }

        private void btnSifreDegistir_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSifreDegistirYeniSifreTekrar.Text) || String.IsNullOrEmpty(txtSifreDegistirYeniSifre.Text) || String.IsNullOrEmpty(txtSifreDegistirGuvenlikSorusuCevabi.Text) || comboBoxSifreDegistirGuvenlikSorusu.Text == "Seçiniz..")
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txtSifreDegistirYeniSifreTekrar.Text != txtSifreDegistirYeniSifre.Text)
                {
                    MessageBox.Show("Şifreler eşleşmiyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                int guvenlikSoruNo = 0, guvenlikSoruNoDogru = 0;
                string guvenlikCevap="";
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select g_soru_no,g_soru_cevabi from stok.kullanicilar where kul_adi = '" + txtSifreDegistirKullaniciAdi.Text + "'", con);
                NpgsqlDataReader oku = cmd.ExecuteReader();
                if (oku.Read())
                {
                    guvenlikSoruNo = Convert.ToInt32(oku["g_soru_no"]);
                    guvenlikCevap = oku["g_soru_cevabi"].ToString();
                }
                con.Close();

                con.Open();
                NpgsqlCommand cmd1 = new NpgsqlCommand("select g_soru_no from stok.guvenlik_sorulari where g_sorusu = '" + comboBoxSifreDegistirGuvenlikSorusu.Text + "'", con);
                NpgsqlDataReader oku1 = cmd1.ExecuteReader();
                if (oku1.Read())
                {
                    guvenlikSoruNoDogru = Convert.ToInt32(oku1["g_soru_no"]);
                }
                con.Close();
                if (txtSifreDegistirGuvenlikSorusuCevabi.Text.ToString() == guvenlikCevap && guvenlikSoruNoDogru == guvenlikSoruNo)
                {
                    con.Open();
                    NpgsqlCommand cmd2 = new NpgsqlCommand("UPDATE stok.kullanicilar SET sifre='" + txtSifreDegistirYeniSifre.Text + "' where kul_adi='" + txtSifreDegistirKullaniciAdi.Text + "'", con);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Şifre değiştirme başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sifreDegistirTemizle();
                }
            }
        }

        private void girisPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkSifremiUnuttum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sifremiUnuttumPanel.BringToFront();
            sifreDegistirTemizle();
        }

        private void btnUyeGiris_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtGirisKullaniciAdi.Text) || String.IsNullOrEmpty(txtGirisSifre.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from stok.kullanicilar where kul_adi='" + txtGirisKullaniciAdi.Text + "' and sifre='" + txtGirisSifre.Text + "'", con);
                NpgsqlDataReader oku = cmd.ExecuteReader();
                if (oku.Read())
                {
                    kullanici_id = Convert.ToInt32(oku["kul_no"]);
                    con.Close();
                    MessageBox.Show("Giriş başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Open();
                    NpgsqlCommand cmd1 = new NpgsqlCommand("select * from stok.kargo order by kargo_id ASC", con);
                    NpgsqlDataReader oku1 = cmd1.ExecuteReader();
                    comboBoxKullaniciUrunlerKargo.Items.Clear();
                    while (oku1.Read())
                    {
                        comboBoxKullaniciUrunlerKargo.Items.Add(oku1["kargo_adi"].ToString());
                    }
                    con.Close();
                    kullanici_adi = txtGirisKullaniciAdi.Text;
                    KullaniciAdresleriComboBox();
                    KullaniciUrunler();
                    KullaniciSiparisler();
                    KullaniciAdresler();
                    txtGirisKullaniciAdi.Text = "";
                    txtGirisSifre.Text = "";
                    tabControlKullaniciGirisi.BringToFront();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı ya da şifre yanlış.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                }
            }
        }

        private void btnYoneticiUrunGuncelle_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtYoneticiUrunGuncelleId.Text) || String.IsNullOrEmpty(txtYoneticiUrunGuncelleAdi.Text) || String.IsNullOrEmpty(txtYoneticiUrunGuncelleBirimFiyati.Text) || String.IsNullOrEmpty(txtYoneticiUrunGuncelleKategoriId.Text) || String.IsNullOrEmpty(txtYoneticiUrunGuncelleSiparisAdedi.Text) || String.IsNullOrEmpty(txtYoneticiUrunGuncelleStokAdedi.Text) || String.IsNullOrEmpty(txtYoneticiUrunGuncelleTedarikciId.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE stok.urunler SET urun_adi='" + txtYoneticiUrunGuncelleAdi.Text + "', kategori_id=" + txtYoneticiUrunGuncelleKategoriId.Text + ", birim_fiyati=" + txtYoneticiUrunGuncelleBirimFiyati.Text + ", stok_adedi=" + txtYoneticiUrunGuncelleStokAdedi.Text + ", tedarikci_id=" + txtYoneticiUrunGuncelleTedarikciId.Text + " where urun_id=" + txtYoneticiUrunGuncelleId.Text + "", con);   
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Güncelleme Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YoneticiUrunler();
                UrunFiyatTakibi();
            }
        }

        private void btnYoneticiUrunEkle_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtYoneticiUrunEkleId.Text) || String.IsNullOrEmpty(txtYoneticiUrunEkleAdi.Text) || String.IsNullOrEmpty(txtYoneticiUrunEkleBirimFiyati.Text) || String.IsNullOrEmpty(txtYoneticiUrunEkleKategoriId.Text) || String.IsNullOrEmpty(txtYoneticiUrunEkleSiparisAdedi.Text) || String.IsNullOrEmpty(txtYoneticiUrunEkleStokAdedi.Text) || String.IsNullOrEmpty(txtYoneticiUrunEkleTedarikciId.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO stok.urunler(urun_id,urun_adi,kategori_id,birim_fiyati,stok_adedi,siparis_adedi,tedarikci_id) VALUES(" + txtYoneticiUrunEkleId.Text + ",'" + txtYoneticiUrunEkleAdi.Text + "'," + txtYoneticiUrunEkleKategoriId.Text + "," + txtYoneticiUrunEkleBirimFiyati.Text + "," + txtYoneticiUrunEkleStokAdedi.Text + "," + txtYoneticiUrunEkleSiparisAdedi.Text + "," + txtYoneticiUrunEkleTedarikciId.Text + ")", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YoneticiUrunler();
                txtYoneticiUrunEkleId.Text = "";
                txtYoneticiUrunEkleAdi.Text = "";
                txtYoneticiUrunEkleKategoriId.Text = "";
                txtYoneticiUrunEkleStokAdedi.Text = "";
                txtYoneticiUrunEkleTedarikciId.Text = "";
                txtYoneticiUrunEkleBirimFiyati.Text = "";
            }
        }

        private void btnYoneticiUrunSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtYoneticiUrunGuncelleId.Text))
            {
                MessageBox.Show("Bir kayıt seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if ((MessageBox.Show(txtYoneticiUrunGuncelleAdi.Text + " adlı ürünü silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM stok.urunler where urun_id=" + txtYoneticiUrunGuncelleId.Text + "", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Silme işlemi başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    YoneticiUrunler();
                }
            }
        }

        private void txtYoneticiUrunEkleBirimFiyati_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '\b':
                    e.Handled = false;
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void btnYoneticiKullaniciSil_Click(object sender, EventArgs e)
        {
            con.Open();
            if ((MessageBox.Show(txtYoneticiSilinecekKullanici.Text + " kullanıcı adlı kişiyi silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM stok.kullanicilar where kul_adi='" + txtYoneticiSilinecekKullanici.Text + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Silme işlemi başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YoneticiKullanicilar();
                SilinenKullanicilar();
            }
            else
            {
                con.Close();
            }
        }

        private void btnYoneticiKargoGuncelle_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtYoneticiGuncelleKargoId.Text) || String.IsNullOrEmpty(txtYoneticiGuncelleKargoAdi.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE stok.kargo SET kargo_adi='" + txtYoneticiGuncelleKargoAdi.Text + "' where kargo_id=" + txtYoneticiGuncelleKargoId.Text + "", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Güncelleme Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YoneticiKargo();
            }
        }

        private void btnYoneticiKargoEkle_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtYoneticiEkleKargoId.Text) || String.IsNullOrEmpty(txtYoneticiEkleKargoAdi.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO stok.kargo VALUES(" + txtYoneticiEkleKargoId.Text + ",'" + txtYoneticiEkleKargoAdi.Text + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YoneticiKargo();
                txtYoneticiEkleKargoAdi.Text = "";
                txtYoneticiEkleKargoId.Text = "";
            }
        }

        private void btnYoneticiKargoSil_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtYoneticiGuncelleKargoId.Text) || String.IsNullOrEmpty(txtYoneticiGuncelleKargoAdi.Text))
            {
                MessageBox.Show("Bir kayıt seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if ((MessageBox.Show(txtYoneticiGuncelleKargoAdi.Text + " adlı kargoyu silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM stok.kargo WHERE kargo_id=" + txtYoneticiGuncelleKargoId.Text + ")", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Silme işlemi başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    YoneticiKargo();
                }
            }
        }

        private void btnYoneticiKategoriGuncelle_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtYoneticiGuncelleKategoriAdi.Text) || String.IsNullOrEmpty(txtYoneticiGuncelleKategoriId.Text) || String.IsNullOrEmpty(txtYoneticiGuncelleKategoriAciklama.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE stok.kategori SET k_adi='" + txtYoneticiGuncelleKargoAdi.Text + "', k_aciklama='" + txtYoneticiGuncelleKategoriAciklama.Text + "' where k_id=" + txtYoneticiGuncelleKategoriId.Text + "", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Güncelleme Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YoneticiKategori();
            }
        }

        private void btnYoneticiKategoriSil_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtYoneticiGuncelleKategoriAdi.Text) || String.IsNullOrEmpty(txtYoneticiGuncelleKategoriId.Text) || String.IsNullOrEmpty(txtYoneticiGuncelleKategoriAciklama.Text))
            {
                MessageBox.Show("Bir kayıt seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if ((MessageBox.Show(txtYoneticiGuncelleKategoriAdi.Text + " adlı kategoriyi silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM stok.kategori WHERE k_id=" + txtYoneticiGuncelleKategoriId.Text + "", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Silme işlemi başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    YoneticiKategori();
                }
            }
        }

        private void btnYoneticiKategoriEkle_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtYoneticiEkleKategoriAdi.Text) || String.IsNullOrEmpty(txtYoneticiEkleKategoriId.Text) || String.IsNullOrEmpty(txtYoneticiEkleKategoriAciklama.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO stok.kategori VALUES(" + txtYoneticiEkleKategoriId.Text + ",'" + txtYoneticiEkleKategoriAdi.Text + "','" + txtYoneticiEkleKategoriAciklama.Text + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YoneticiKategori();
                txtYoneticiEkleKategoriAdi.Text = "";
                txtYoneticiEkleKategoriId.Text = "";
                txtYoneticiEkleKategoriAciklama.Text = "";
            }
        }

        private void tabControlYoneticiGirisi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnYoneticiGiris_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtYoneticiGirisKullaniciAdi.Text) || String.IsNullOrEmpty(txtYoneticiGirisSifre.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM stok.yonetici where id='" + txtYoneticiGirisKullaniciAdi.Text + "' and sifre='" + txtYoneticiGirisSifre.Text + "'", con);
                NpgsqlDataReader oku = cmd.ExecuteReader();
                if (oku.Read())
                {
                    MessageBox.Show("Giriş başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControlYoneticiGirisi.BringToFront();
                    tabControlYoneticiGirisi.SelectedIndex = 0;
                    txtYoneticiGirisKullaniciAdi.Text = "";
                    txtYoneticiGirisSifre.Text = "";
                    con.Close();
                    UrunFiyatTakibi();
                    YoneticiSiparisler();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı ya da şifre hatalı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    con.Close();
                }
                
            }
        }

        private void linkLabelKullaniciGirisiToYoneticiGirisi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelYoneticiGiris.BringToFront();
        }

        private void linkLabelYoneticiGirisIptal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PanelKullaniciGirisi.BringToFront();
        }

        private void label55_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxYoneticiGuncelleTedarikciIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBoxYoneticiGuncelleTedarikciIl_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnYoneticiTedarikciEkle_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtYoneticiEkleTedarikciId.Text) || String.IsNullOrEmpty(txtYoneticiEkleTedarikciAdi.Text) || String.IsNullOrEmpty(txtYoneticiEkleTedarikciAdres.Text) || String.IsNullOrEmpty(txtYoneticiEkleTedarikciTelefon.Text) || comboBoxYoneticiEkleTedarikciIl.Text == "Seçiniz.." || comboBoxYoneticiEkleTedarikciIlce.Text == "Seçiniz.." || comboBoxYoneticiEkleTedarikciIlce.SelectedIndex == -1)
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int il_plaka_no = 0, ilce_no = 0;
                con.Open();
                NpgsqlCommand cmd1 = new NpgsqlCommand("select * from stok.iller where il_adi='" + comboBoxYoneticiEkleTedarikciIl.SelectedItem + "'", con);
                NpgsqlDataReader oku = cmd1.ExecuteReader();
                if (oku.Read())
                {
                    il_plaka_no = Convert.ToInt32(oku["plaka_no"]);
                }
                con.Close();
                con.Open();
                NpgsqlCommand cmd2 = new NpgsqlCommand("select * from stok.ilceler where ilce_adi='" + comboBoxYoneticiEkleTedarikciIlce.SelectedItem + "'", con);
                NpgsqlDataReader oku2 = cmd2.ExecuteReader();
                if (oku2.Read())
                {
                    ilce_no = Convert.ToInt32(oku2["ilce_no"]);
                }
                con.Close();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO stok.tedarikci(t_id,t_adi,telefon,adresi,plaka_no,ilce_no) VALUES(" + txtYoneticiEkleTedarikciId.Text + ",'" + txtYoneticiEkleTedarikciAdi.Text + "','" + txtYoneticiEkleTedarikciTelefon.Text + "','" + txtYoneticiEkleTedarikciAdres.Text + "'," + il_plaka_no + "," + ilce_no + ")", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YoneticiTedarikciler();
                txtYoneticiEkleTedarikciId.Text = "";
                txtYoneticiEkleTedarikciAdi.Text = "";
                txtYoneticiEkleTedarikciAdres.Text = "";
                txtYoneticiEkleTedarikciTelefon.Text = "";
                comboBoxYoneticiEkleTedarikciIl.Text = "Seçiniz..";
                comboBoxYoneticiEkleTedarikciIlce.Text = "Seçiniz..";
                comboBoxYoneticiEkleTedarikciIlce.Enabled = false;
            }
        }

        private void btnYoneticiTedarikciGuncelle_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtYoneticiGuncelleTedarikciId.Text) || String.IsNullOrEmpty(txtYoneticiGuncelleTedarikciAdi.Text) || String.IsNullOrEmpty(txtYoneticiGuncelleTedarikciAdres.Text) || String.IsNullOrEmpty(txtYoneticiGuncelleTedarikciTelefon.Text) || String.IsNullOrEmpty(comboBoxYoneticiGuncelleTedarikciIl.Text) || comboBoxYoneticiGuncelleTedarikciIl.Text == "Seçiniz.." || String.IsNullOrEmpty(comboBoxYoneticiEkleTedarikciIlce.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE stok.tedarikci SET t_adi='" + txtYoneticiGuncelleTedarikciAdi.Text + "', telefon='" + txtYoneticiGuncelleTedarikciTelefon.Text + "', adresi='" + txtYoneticiGuncelleTedarikciAdres.Text + "', plaka_no=" + comboBoxYoneticiGuncelleTedarikciIl.Text + ", ilce_no=" + comboBoxYoneticiGuncelleTedarikciIlce.Text + " where t_id=" + txtYoneticiGuncelleTedarikciId.Text + "", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YoneticiTedarikciler();
            }
        }

        private void comboBoxYoneticiEkleTedarikciIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxYoneticiEkleTedarikciIlce.Items.Clear();
            comboBoxYoneticiEkleTedarikciIlce.Text = "Seçiniz..";
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select * from stok.ilceler where plaka_no IN(select plaka_no from stok.iller where il_adi='" + comboBoxYoneticiEkleTedarikciIl.SelectedItem + "') order by ilce_adi ASC", con);
            NpgsqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                comboBoxYoneticiEkleTedarikciIlce.Items.Add(oku["ilce_adi"].ToString());
                comboBoxYoneticiEkleTedarikciIlce.Enabled = true;
            }
            con.Close();
        }

        private void btnYoneticiSifreDegistir_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtYoneticiSifreDegistirMevcut.Text) || String.IsNullOrEmpty(txtYoneticiSifreDegistirYeni.Text) || String.IsNullOrEmpty(txtYoneticiSifreDegistirTekrar.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(txtYoneticiSifreDegistirYeni.Text != txtYoneticiSifreDegistirTekrar.Text)
            {
                MessageBox.Show("Şifreler eşleşmiyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("Select * from stok.yonetici where sifre='" + txtYoneticiSifreDegistirMevcut.Text + "'", con);
                NpgsqlDataReader oku = cmd.ExecuteReader();
                if (!oku.Read())
                {
                    MessageBox.Show("Mevcut şifre doğru değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    con.Close();
                }
                else
                {
                    con.Close();
                    con.Open();
                    NpgsqlCommand cmd1 = new NpgsqlCommand("UPDATE stok.yonetici SET sifre='" + txtYoneticiSifreDegistirYeni.Text + "' where sifre='" + txtYoneticiSifreDegistirMevcut.Text + "'", con);
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("Şifre değiştirme başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtYoneticiSifreDegistirMevcut.Text = "";
                    txtYoneticiSifreDegistirYeni.Text = "";
                    txtYoneticiSifreDegistirTekrar.Text = "";
                    con.Close();
                }
            }
        }

        private void btnYoneticiCikis_Click(object sender, EventArgs e)
        {
            PanelKullaniciGirisi.BringToFront();
        }

        private void btnKullaniciCikis_Click(object sender, EventArgs e)
        {
            PanelKullaniciGirisi.BringToFront();
            txtKullaniciSifreDegistirMevcut.Text = "";
            txtKullaniciSifreDegistirYeni.Text = "";
            txtKullaniciSifreDegistirTekrar.Text = "";
        }

        private void btnKullaniciSifreDegistir_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtKullaniciSifreDegistirMevcut.Text) || String.IsNullOrEmpty(txtKullaniciSifreDegistirYeni.Text) || String.IsNullOrEmpty(txtKullaniciSifreDegistirTekrar.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtKullaniciSifreDegistirYeni.Text != txtKullaniciSifreDegistirTekrar.Text)
            {
                MessageBox.Show("Şifreler eşleşmiyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("Select * from stok.kullanicilar where sifre='" + txtKullaniciSifreDegistirMevcut.Text + "'", con);
                NpgsqlDataReader oku = cmd.ExecuteReader();
                if (!oku.Read())
                {
                    MessageBox.Show("Mevcut şifre doğru değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    con.Close();
                }
                else
                {
                    con.Close();
                    con.Open();
                    NpgsqlCommand cmd1 = new NpgsqlCommand("UPDATE stok.kullanicilar SET sifre='" + txtKullaniciSifreDegistirYeni.Text + "' where sifre='" + txtKullaniciSifreDegistirMevcut.Text + "'", con);
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("Şifre değiştirme başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtKullaniciSifreDegistirMevcut.Text = "";
                    txtKullaniciSifreDegistirYeni.Text = "";
                    txtKullaniciSifreDegistirTekrar.Text = "";
                    con.Close();
                }
            }
        }

        private void btnKullaniciUrunSatinAl_Click(object sender, EventArgs e)
        {
            if(comboBoxKullaniciUrunlerAdres.Text == "Seçiniz.." || string.IsNullOrEmpty(comboBoxKullaniciUrunlerAdres.Text) || comboBoxKullaniciUrunlerAdres.SelectedIndex == -1 || comboBoxKullaniciUrunlerKargo.Text == "Seçiniz.." || string.IsNullOrEmpty(comboBoxKullaniciUrunlerKargo.Text) || comboBoxKullaniciUrunlerKargo.SelectedIndex == -1) 
            {
                MessageBox.Show("Adres ve kargo seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int siparisAdedi = 0, stokDurumu = 0;
                string inputBoxGirisi = "";
                stokDurumu = Convert.ToInt32(labelStokDurumu.Text);
                do
                {
                    inputBoxGirisi = Interaction.InputBox("Kaç adet sipariş vermek istiyorsunuz?(Stok adedi : " + stokDurumu.ToString() + ")", "Adet Bilgisi", "Örn : 3", 300, 300);
                } while (!(int.TryParse(inputBoxGirisi, out siparisAdedi)) || siparisAdedi > stokDurumu || siparisAdedi < 0);
                DialogResult cevap = MessageBox.Show(labelUrunAdi.Text + " adlı üründen "+ siparisAdedi +" adet satın almak istediğinizden emin misiniz?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(cevap == DialogResult.No)
                {
                    MessageBox.Show("İşlem iptal edildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                con.Open();
                NpgsqlCommand cmd2 = new NpgsqlCommand("select * from stok.adresler where adres='" + comboBoxKullaniciUrunlerAdres.SelectedItem + "'", con);
                NpgsqlDataReader oku = cmd2.ExecuteReader();
                int adres_no = 0;
                if(oku.Read())
                {
                    adres_no = Convert.ToInt32(oku["adr_no"]);
                }
                else
                {
                    MessageBox.Show("Geçerli bir adres seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                con.Close();

                con.Open();
                NpgsqlCommand cmd3 = new NpgsqlCommand("select * from stok.kargo where kargo_adi='" + comboBoxKullaniciUrunlerKargo.SelectedItem + "'", con);
                NpgsqlDataReader oku3 = cmd3.ExecuteReader();
                int kargo_no = 0;
                if (oku3.Read())
                {
                    kargo_no = Convert.ToInt32(oku["kargo_id"]);
                }
                else
                {
                    MessageBox.Show("Geçerli bir kargo seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                con.Close();

                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("CALL stok.stok_azalt_procedure(" + labelUrunId.Text + "," + siparisAdedi + ")", con);
                cmd.ExecuteNonQuery();
                con.Close();
                KullaniciUrunler();

                con.Open();
                NpgsqlCommand cmd1 = new NpgsqlCommand("CALL stok.siparis_ekle_procedure(" + kullanici_id + "," + labelUrunId.Text + "," + kargo_no + "," + siparisAdedi + "," + Convert.ToInt32(labelUrunFiyati.Text) * siparisAdedi + "," + adres_no + ");", con);
                cmd1.ExecuteNonQuery();
                con.Close();
                KullaniciSiparisler();
            }
        }

        private void btnKullaniciAdresSil_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtKullaniciAdresSilKulNo.Text) || string.IsNullOrEmpty(txtKullaniciAdresSilAdresNo.Text))
            {
                MessageBox.Show("Geçerli bir adres seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult sonuc = MessageBox.Show(txtKullaniciAdresSilAdresNo.Text + " numaralı adresi silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(sonuc == DialogResult.No)
                {
                    return;
                }
                else
                {
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM stok.adresler where kul_no=" + kullanici_id + " and adr_no=" + txtKullaniciAdresSilAdresNo.Text + "", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Silme işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    KullaniciAdresler();
                    txtKullaniciAdresSilAdresNo.Text = "";
                    txtKullaniciAdresSilKulNo.Text = "";
                }
            }
        }

        private void btnKullaniciAdresEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKullaniciAdresEkleAdresNo.Text) || string.IsNullOrEmpty(txtKullaniciAdresEklePlakaNo.Text) || string.IsNullOrEmpty(txtKullaniciAdresEkleIlceNo.Text) || string.IsNullOrEmpty(txtKullaniciAdresEkleAdres.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("call stok.adres_ekle_procedure(" + kullanici_id + "::smallint," + txtKullaniciAdresEkleAdresNo.Text + "::smallint," + txtKullaniciAdresEklePlakaNo.Text + "::smallint," + txtKullaniciAdresEkleIlceNo.Text + "::smallint,'" + txtKullaniciAdresEkleAdres.Text + "'::text)", con);
                cmd.ExecuteNonQuery();
                con.Close();
                KullaniciAdresler();
                txtKullaniciAdresEkleAdresNo.Text = "";
                txtKullaniciAdresEklePlakaNo.Text = "";
                txtKullaniciAdresEkleIlceNo.Text = "";
                txtKullaniciAdresEkleAdres.Text = "";
            }
        }
    }
}
