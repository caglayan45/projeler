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
using System.IO;

namespace ETUT_UYGULAMASI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con;
        string silinecekKisiTC = "", dersID = "", etutSaati = "";
        List<Button> etutButonlari = new List<Button>();

        private void Form1_Load(object sender, EventArgs e)
        {
            //con = new SqlConnection(@"Data Source=CAGLAYAN;Initial Catalog=etut_takip;User ID=sa;Password=123;");
            FileStream fs = new FileStream("veritabani_baglantisi.txt", FileMode.Open,FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            con = new SqlConnection(sr.ReadLine());
            timer1.Start();
            etutButonlari.Add(btnEtut08);
            etutButonlari.Add(btnEtut09);
            etutButonlari.Add(btnEtut10);
            etutButonlari.Add(btnEtut11);
            etutButonlari.Add(btnEtut12);
            etutButonlari.Add(btnEtut13);
            etutButonlari.Add(btnEtut14);
            etutButonlari.Add(btnEtut15);
            etutButonlari.Add(btnEtut16);
            etutButonlari.Add(btnEtut17);
            etutButonlari.Add(btnEtut18);
            etutButonlari.Add(btnEtut19);
            etutButonlari.Add(btnEtut20);
            etutButonlari.Add(btnEtut21);
            etutButonlari.Add(btnEtut22);
            VerileriCek();
        }

        void OgrenciGoruntuleDataGridDuzenle(DataGridView dgv)
        {
            dgv.Columns[0].HeaderText = "ID";
            dgv.Columns[1].HeaderText = "TC KİMLİK";
            dgv.Columns[2].HeaderText = "İSİM";
            dgv.Columns[3].HeaderText = "SOYİSİM";
            dgv.Columns[4].HeaderText = "TELEFON";
            dgv.Columns[5].HeaderText = "CİNSİYET";
            dgv.Columns[6].HeaderText = "ADRES";
            dgv.Columns[7].HeaderText = "DOĞUM TARİHİ";
            dgv.Columns[6].Width = 240;
            dgv.Columns[7].Width = 115;
        }

        void EtutIslemleriDataGridDuzenle(DataGridView dgv)
        {
            dgv.Columns[0].HeaderText = "ID";
            dgv.Columns[1].HeaderText = "ÖĞRENCİ ADI";
            dgv.Columns[2].HeaderText = "ÖĞRENCİ SOYADI";
            dgv.Columns[3].HeaderText = "DERS ADI";
            dgv.Columns[4].HeaderText = "ÖĞRETMEN ADI";
            dgv.Columns[5].HeaderText = "ÖĞRETMEN SOYADI";
            dgv.Columns[6].HeaderText = "TARİH SAAT";
            dgv.Columns[7].HeaderText = "ÜCRET";
            dgv.Columns[1].Width = 120;
            dgv.Columns[2].Width = 140;
            dgv.Columns[4].Width = 160;
            dgv.Columns[5].Width = 160;
            dgv.Columns[7].Width = 60;
        }

        void NotIslemleriOgrenciComboBox()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ogr_tc,ogr_adi,ogr_soyadi FROM kisiler", con);
                SqlDataReader dr = cmd.ExecuteReader();
                comboBoxNotIslemleriOgrenci.Items.Clear();//öğrenci ekleme
                comboBoxNotIslemleriDers.Items.Clear();//ders ekleme
                while (dr.Read())
                {
                    string isim = dr["ogr_tc"].ToString() + "  " + dr["ogr_adi"].ToString() + " " + dr["ogr_soyadi"].ToString();
                    comboBoxNotIslemleriOgrenci.Items.Add(isim);
                }
                con.Close();

                con.Open();
                cmd = new SqlCommand("SELECT * FROM dersler", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBoxNotIslemleriDers.Items.Add(dr["ders_id"].ToString() + " - " + dr["ders_adi"].ToString());
                }
                con.Close();
                comboBoxNotIslemleriOgrenci.SelectedIndex = 0;
                comboBoxNotIslemleriDers.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                con.Close();
            }
            
        }

        void NotIslemleriAramaIslemiComboBox()
        {
            comboBoxNotIslemleriAramaOgrenci.Items.Clear();//öğrenci ekleme
            comboBoxNotIslemleriAramaDers.Items.Clear();//ders ekleme
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ogr_tc,ogr_adi,ogr_soyadi FROM kisiler", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string isim = dr["ogr_tc"].ToString() + "  " + dr["ogr_adi"].ToString() + " " + dr["ogr_soyadi"].ToString();
                comboBoxNotIslemleriAramaOgrenci.Items.Add(isim);
            }
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT * FROM dersler", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBoxNotIslemleriAramaDers.Items.Add(dr["ders_id"].ToString() + " - " + dr["ders_adi"].ToString());
            }
            con.Close();
            comboBoxNotIslemleriAramaOgrenci.SelectedIndex = 0;
            comboBoxNotIslemleriAramaDers.SelectedIndex = 0;
        }

        void EtutMesgulButonlar()
        {
            List<string> saat = new List<string>();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT tarih_saat FROM etut", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dateTimePickerEtutTarih.Value.ToString().Split(' ')[0].Equals(dr["tarih_saat"].ToString().Split(' ')[0]))
                    saat.Add(dr["tarih_saat"].ToString().Split(' ')[1]);
            }
            con.Close();
            foreach (var butonlar in etutButonlari)
            {
                butonlar.Enabled = true;
                butonlar.BackColor = Color.LimeGreen;
            }
            foreach (var saatler in saat)
            {
                foreach (var butonlar in etutButonlari)
                {
                    if (saatler == butonlar.Text + ":00")
                    {
                        butonlar.Enabled = false;
                        butonlar.BackColor = Color.Red;
                        break;
                    }
                }
            }
        }

        void EtutIslemleriComboBox()
        {
            comboBoxEtutOgrenci.Items.Clear();
            comboBoxEtutGoruntuleOgrenci.Items.Clear();
            comboBoxEtutOgretmen.Items.Clear();
            comboBoxEtutGoruntuleOgretmen.Items.Clear();
            comboBoxEtutDers.Items.Clear();
            comboBoxEtutGoruntuleDers.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ogr_tc,ogr_adi,ogr_soyadi FROM kisiler", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string isim = dr["ogr_tc"].ToString() + "  " + dr["ogr_adi"].ToString() + " " + dr["ogr_soyadi"].ToString();
                comboBoxEtutOgrenci.Items.Add(isim);
                comboBoxEtutGoruntuleOgrenci.Items.Add(isim);
            }
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT * FROM ogretmenler", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string isim = dr["ogrt_tc"].ToString() + " " + dr["ogrt_adi"].ToString() + " " + dr["ogrt_soyadi"].ToString();
                comboBoxEtutOgretmen.Items.Add(isim);
                comboBoxEtutGoruntuleOgretmen.Items.Add(isim);
            }
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT * FROM dersler", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string isim = dr["ders_id"].ToString() + " -  " + dr["ders_adi"].ToString();
                comboBoxEtutDers.Items.Add(isim);
                comboBoxEtutGoruntuleDers.Items.Add(isim);
            }
            con.Close();

            comboBoxEtutOgretmen.SelectedIndex = 0;
            comboBoxEtutGoruntuleOgretmen.SelectedIndex = 0;
            comboBoxEtutDers.SelectedIndex = 0;
            comboBoxEtutOgrenci.SelectedIndex = 0;
            comboBoxEtutGoruntuleOgrenci.SelectedIndex = 0;
            comboBoxEtutGoruntuleDers.SelectedIndex = 0;
        }

        string DersAdiGetir(string veri,int uzunluk)
        {
            string temp2 = "";
            for (int j = 2; j < uzunluk; j++)
            {
                if (j != uzunluk - 1)
                    temp2 += veri.Split(' ')[j] + " ";
                else
                    temp2 += veri.Split(' ')[j];
            }
            return temp2;
        }

        void EtutButonDuzenle(Button btn)
        {
            if (etutSaati == "")
            {
                btn.BackColor = Color.Orange;
                etutSaati = btn.Text + ":00";
            }
            else
            {
                foreach (var item in etutButonlari)
                {
                    if (etutSaati == item.Text + ":00")
                    {
                        item.BackColor = Color.LimeGreen;
                        btn.BackColor = Color.Orange;
                        etutSaati = btn.Text + ":00";
                    }
                }
            }
        }

        void NotIslemleriDataGridDuzenle(DataGridView dgv)
        {
            dgv.Columns[0].HeaderText = "TC KİMLİK";
            dgv.Columns[1].HeaderText = "İSİM";
            dgv.Columns[2].HeaderText = "SOYİSİM";
            dgv.Columns[3].HeaderText = "DERS ADI";
            dgv.Columns[4].HeaderText = "DERS NOTU";
            dgv.Columns[0].Width = 180;
            dgv.Columns[1].Width = 180;
            dgv.Columns[2].Width = 180;
            dgv.Columns[3].Width = 180;
            dgv.Columns[4].Width = 180;
            NotIslemleriOgrenciComboBox();
            NotIslemleriAramaIslemiComboBox();
        }

        void OgretmenIslemleriDataGridDuzenle(DataGridView dgv)
        {
            dgv.Columns[0].HeaderText = "TC KİMLİK";
            dgv.Columns[1].HeaderText = "İSİM";
            dgv.Columns[2].HeaderText = "SOYİSİM";
            dgv.Columns[0].Width = 180;
            dgv.Columns[1].Width = 180;
            dgv.Columns[2].Width = 180;
        }

        void DersIslemleriDataGridDuzenle(DataGridView dgv)
        {
            dgv.Columns[0].HeaderText = "DERS ID";
            dgv.Columns[1].HeaderText = "DERS ADI";
            dgv.Columns[0].Width = 180;
            dgv.Columns[1].Width = 180;
        }

        void DataGrideVeriCek(DataGridView dgv, string SelectCumlesi)
        {
            dgv.DataSource = "";
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(SelectCumlesi, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgv.DataSource = ds.Tables[0];
            con.Close();
        }

        void SQLcommandCalistir(string SQLCommand)
        {
            try
            {
                con.Open();
                SqlCommand sql = new SqlCommand(SQLCommand, con);
                sql.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                con.Close();
            }
            
        }

        void TextBoxSifirla(params TextBox[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                args[i].Text = "";
            }
        }

        void SadeceHarfGirisi(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == ' ')
                e.Handled = false;
            else
                e.Handled = true;
        }
        void SadeceRakamGirisi(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
                e.Handled = false;
            else
                e.Handled = true;
        }

        void VerileriCek()
        {
            DataGrideVeriCek(dataGridViewOgrencileriGoruntule, "SELECT * FROM kisiler");
            DataGrideVeriCek(dataGridViewOgrenciEkleCikar, "SELECT * FROM kisiler");
            DataGrideVeriCek(dataGridViewOgretmenIslemleri, "SELECT * FROM ogretmenler");
            DataGrideVeriCek(dataGridViewDersIslemleri, "SELECT * FROM dersler");
            DataGrideVeriCek(dataGridViewEtutEkleme, "SELECT E.id,K.ogr_adi,K.ogr_soyadi,D.ders_adi,O.ogrt_adi,O.ogrt_soyadi,E.tarih_saat,E.ucret FROM etut as E,kisiler as K,ogretmenler as O,dersler as D where E.ogr_tc = K.ogr_tc and E.ogrt_tc = O.ogrt_tc and D.ders_id = E.ders_id");
            DataGrideVeriCek(dataGridViewEtutGoruntule, "SELECT E.id,K.ogr_adi,K.ogr_soyadi,D.ders_adi,O.ogrt_adi,O.ogrt_soyadi,E.tarih_saat,E.ucret FROM etut as E,kisiler as K,ogretmenler as O,dersler as D where E.ogr_tc = K.ogr_tc and E.ogrt_tc = O.ogrt_tc and D.ders_id = E.ders_id");
            DataGrideVeriCek(dataGridViewNotIslemleri, "Select K.ogr_tc,K.ogr_adi,K.ogr_soyadi,D.ders_adi,N.ders_notu from kisiler as K, dersler as D, notlar as N where K.ogr_tc = N.ogr_tc and N.ders_id = D.ders_id");
            NotIslemleriDataGridDuzenle(dataGridViewNotIslemleri);
            EtutIslemleriDataGridDuzenle(dataGridViewEtutEkleme);
            EtutIslemleriDataGridDuzenle(dataGridViewEtutGoruntule);
            DersIslemleriDataGridDuzenle(dataGridViewDersIslemleri);
            OgretmenIslemleriDataGridDuzenle(dataGridViewOgretmenIslemleri);
            OgrenciGoruntuleDataGridDuzenle(dataGridViewOgrencileriGoruntule);
            OgrenciGoruntuleDataGridDuzenle(dataGridViewOgrenciEkleCikar);
            EtutIslemleriComboBox();
            EtutMesgulButonlar();
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKayitOlKullaniciAdi.Text) || string.IsNullOrEmpty(txtKayitOlSifre.Text) || string.IsNullOrEmpty(txtKayitOlSifreTekrar.Text))
                MessageBox.Show("Boş alan bırakmayınız.", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (txtKayitOlSifre.Text.Equals(txtKayitOlSifreTekrar.Text))
                {
                    SQLcommandCalistir("INSERT INTO kullanici_giris VALUES('" + txtKayitOlKullaniciAdi.Text + "','" + txtKayitOlSifre.Text + "')");
                    MessageBox.Show("Kayıt başarılı.", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    panelKullaniciGirisi.BringToFront();
                    txtKayitOlKullaniciAdi.Text = txtKayitOlSifre.Text = txtKayitOlSifreTekrar.Text = "";
                }
                else
                    MessageBox.Show("Şifreler aynı değil.", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabelKayitOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelKayitOl.BringToFront();
        }

        private void btnKullaniciGiris_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKullaniciGirisKullaniciAdi.Text) || string.IsNullOrEmpty(txtKullaniciGirisSifre.Text))
                MessageBox.Show("Boş alan bırakmayınız.", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                con.Open();
                SqlCommand sql = new SqlCommand("Select * from kullanici_giris where kullanici_adi='" + txtKullaniciGirisKullaniciAdi.Text + "' and sifre='" + txtKullaniciGirisSifre.Text + "'", con);
                SqlDataReader dr = sql.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Giriş başarılı.", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblBaslangic.Text = "Hoşgeldiniz, " + dr["kullanici_adi"].ToString();
                    panel1.BringToFront();
                    panelAnaSayfa.BringToFront();
                    txtKullaniciGirisKullaniciAdi.Text = txtKullaniciGirisSifre.Text = "";
                }
                else
                    MessageBox.Show("Kullanıcı adı ya da şifre yanlış.", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelSaatTarih.Text = DateTime.Now.ToString();
            labelSaatTarih.BringToFront();
        }

        private void anaSayfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelAnaSayfa.BringToFront();
        }

        private void öğrenciKayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelOgrenciKayitIslemleri.BringToFront();
        }

        private void öğrencToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelOgrencileriGoruntuleme.BringToFront();
        }

        private void notGörüntülemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelNotIslemleri.BringToFront();
        }

        private void yeniEtütToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelYeniEtüt.BringToFront();
        }

        private void etütGörüntülemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelEtutleriGoruntule.BringToFront();
        }

        private void öğretmenİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelOgretmenIslemleri.BringToFront();
        }

        private void dersİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelDersIslemleri.BringToFront();
        }

        private void tYTAYTDGSPuanHesaplamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelTYTHesap.BringToFront();
        }

        private void aYTPuanHesaplamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelAYTHesap.BringToFront();
        }

        private void dGSPuanHesaplamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelDGSHesap.BringToFront();
        }

        private void radioButtonOgrFiltreTC_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOgrFiltreTC.Checked)
            {
                txtOgrFiltreTC.Enabled = true;
                txtOgrFiltreIsim.Enabled = false;
                txtOgrFiltreSoyisim.Enabled = false;
                panelOgrFiltreCinsiyet.Enabled = false;
            }
        }

        private void radioButtonOgrFiltreIsim_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOgrFiltreIsim.Checked)
            {
                txtOgrFiltreIsim.Enabled = true;
                txtOgrFiltreTC.Enabled = false;
                txtOgrFiltreSoyisim.Enabled = false;
                panelOgrFiltreCinsiyet.Enabled = false;
            }
        }

        private void radioButtonOgrFiltreSoyisim_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOgrFiltreSoyisim.Checked)
            {
                txtOgrFiltreSoyisim.Enabled = true;
                txtOgrFiltreTC.Enabled = false;
                txtOgrFiltreIsim.Enabled = false;
                panelOgrFiltreCinsiyet.Enabled = false;
            }
        }

        private void radioButtonOgrFiltreCinsiyet_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOgrFiltreCinsiyet.Checked)
            {
                panelOgrFiltreCinsiyet.Enabled = true;
                txtOgrFiltreTC.Enabled = false;
                txtOgrFiltreIsim.Enabled = false;
                txtOgrFiltreSoyisim.Enabled = false;
            }
        }

        private void btnOgrFiltreAra_Click(object sender, EventArgs e)
        {
            if (radioButtonOgrFiltreTC.Checked)
            {
                if(!string.IsNullOrEmpty(txtOgrFiltreTC.Text))
                {
                    DataGrideVeriCek(dataGridViewOgrencileriGoruntule, "select * from kisiler where ogr_tc='" + txtOgrFiltreTC.Text + "'");
                    OgrenciGoruntuleDataGridDuzenle(dataGridViewOgrencileriGoruntule);
                }
                else
                    MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (radioButtonOgrFiltreIsim.Checked)
            {
                if (!string.IsNullOrEmpty(txtOgrFiltreIsim.Text))
                {
                    DataGrideVeriCek(dataGridViewOgrencileriGoruntule, "select * from kisiler where ogr_adi='" + txtOgrFiltreIsim.Text + "'");
                    OgrenciGoruntuleDataGridDuzenle(dataGridViewOgrencileriGoruntule);
                }
                else
                    MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (radioButtonOgrFiltreSoyisim.Checked)
            {
                if (!string.IsNullOrEmpty(txtOgrFiltreSoyisim.Text))
                {
                    DataGrideVeriCek(dataGridViewOgrencileriGoruntule, "select * from kisiler where ogr_soyadi='" + txtOgrFiltreSoyisim.Text + "'");
                    OgrenciGoruntuleDataGridDuzenle(dataGridViewOgrencileriGoruntule);
                }
                else
                    MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string cinsiyet = "";
                if (radioButtonOgrFiltreErkek.Checked)
                    cinsiyet = "Erkek";
                else
                    cinsiyet = "Kadın";
                dataGridViewOgrencileriGoruntule.DataSource = "";
                DataGrideVeriCek(dataGridViewOgrencileriGoruntule, "select * from kisiler where ogr_cinsiyet='" + cinsiyet + "'");
                OgrenciGoruntuleDataGridDuzenle(dataGridViewOgrencileriGoruntule);
            }
        }

        private void btnOgrenciGorntulemeSIL_Click(object sender, EventArgs e)
        {
            int cevap = (int)MessageBox.Show(silinecekKisiTC + " TC'li öğrenciyi silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(cevap == 6)
            {
                SQLcommandCalistir("DELETE FROM kisiler WHERE ogr_tc='" + silinecekKisiTC + "'");
                DataGrideVeriCek(dataGridViewOgrencileriGoruntule, "SELECT * FROM kisiler");
                OgrenciGoruntuleDataGridDuzenle(dataGridViewOgrencileriGoruntule);
                VerileriCek();
            }
        }

        private void dataGridViewOgrencileriGoruntule_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridViewOgrencileriGoruntule.CurrentRow != null)
                silinecekKisiTC = dataGridViewOgrencileriGoruntule.CurrentRow.Cells[1].Value.ToString();
        }

        private void radioButtonOgrKayitIslemleriEklemeIslemi_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOgrKayitIslemleriEklemeIslemi.Checked)
            {
                txtOgrKayitIslemleriTC.Enabled = true;
                TextBoxSifirla(txtOgrKayitIslemleriTC, txtOgrKayitIslemleriIsim, txtOgrKayitIslemleriSoyisim, txtOgrKayitIslemleriTelefon, txtOgrKayitIslemleriAdres);
                dateTimePickerKayitIslemleri.Value = DateTime.Today;
                btnOgrKayitIslemleriEkle.BringToFront();
            }
        }

        private void radioButtonOgrKayitIslemleriGuncellemeIslemi_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOgrKayitIslemleriGuncellemeIslemi.Checked)
            {
                txtOgrKayitIslemleriTC.Enabled = false;
                TextBoxSifirla(txtOgrKayitIslemleriTC, txtOgrKayitIslemleriIsim, txtOgrKayitIslemleriSoyisim, txtOgrKayitIslemleriTelefon, txtOgrKayitIslemleriAdres);
                txtOgrKayitIslemleriTC.Text = dataGridViewOgrenciEkleCikar.CurrentRow.Cells[1].Value.ToString();
                txtOgrKayitIslemleriIsim.Text = dataGridViewOgrenciEkleCikar.CurrentRow.Cells[2].Value.ToString();
                txtOgrKayitIslemleriSoyisim.Text = dataGridViewOgrenciEkleCikar.CurrentRow.Cells[3].Value.ToString();
                txtOgrKayitIslemleriTelefon.Text = dataGridViewOgrenciEkleCikar.CurrentRow.Cells[4].Value.ToString();
                if (dataGridViewOgrenciEkleCikar.CurrentRow.Cells[5].Value.ToString() == "Erkek")
                    radioButtonOgrKayitIslemleriErkek.Checked = true;
                else
                    radioButtonOgrKayitIslemleriKadin.Checked = true;
                txtOgrKayitIslemleriAdres.Text = dataGridViewOgrenciEkleCikar.CurrentRow.Cells[6].Value.ToString();
                dateTimePickerKayitIslemleri.Value = Convert.ToDateTime(dataGridViewOgrenciEkleCikar.CurrentRow.Cells[7].Value);
                btnOgrKayitIslemleriGuncelle.BringToFront();
            }
        }

        private void dataGridViewOgrenciEkleCikar_SelectionChanged(object sender, EventArgs e)
        {
            if (radioButtonOgrKayitIslemleriGuncellemeIslemi.Checked && dataGridViewOgrenciEkleCikar.CurrentRow != null)
            {
                txtOgrKayitIslemleriTC.Text = dataGridViewOgrenciEkleCikar.CurrentRow.Cells[1].Value.ToString();
                txtOgrKayitIslemleriIsim.Text = dataGridViewOgrenciEkleCikar.CurrentRow.Cells[2].Value.ToString();
                txtOgrKayitIslemleriSoyisim.Text = dataGridViewOgrenciEkleCikar.CurrentRow.Cells[3].Value.ToString();
                txtOgrKayitIslemleriTelefon.Text = dataGridViewOgrenciEkleCikar.CurrentRow.Cells[4].Value.ToString();
                if (dataGridViewOgrenciEkleCikar.CurrentRow.Cells[5].Value.ToString() == "Erkek")
                    radioButtonOgrKayitIslemleriErkek.Checked = true;
                else
                    radioButtonOgrKayitIslemleriKadin.Checked = true;
                txtOgrKayitIslemleriAdres.Text = dataGridViewOgrenciEkleCikar.CurrentRow.Cells[6].Value.ToString();
                if (dataGridViewOgrenciEkleCikar.CurrentRow.Cells[7].Value != DBNull.Value)
                    dateTimePickerKayitIslemleri.Value = Convert.ToDateTime(dataGridViewOgrenciEkleCikar.CurrentRow.Cells[7].Value);
                else
                    dateTimePickerKayitIslemleri.Value = DateTime.Today;

            }
        }

        private void btnOgrKayitIslemleriEkle_Click(object sender, EventArgs e)
        {
            if (radioButtonOgrKayitIslemleriEklemeIslemi.Checked && !string.IsNullOrEmpty(txtOgrKayitIslemleriTC.Text) && !string.IsNullOrEmpty(txtOgrKayitIslemleriIsim.Text) && !string.IsNullOrEmpty(txtOgrKayitIslemleriSoyisim.Text) && !string.IsNullOrEmpty(txtOgrKayitIslemleriTelefon.Text) && !string.IsNullOrEmpty(txtOgrKayitIslemleriAdres.Text))
            {
                string cinsiyet = "", yil = dateTimePickerKayitIslemleri.Value.Year.ToString(), ay = dateTimePickerKayitIslemleri.Value.Month.ToString(), gun = dateTimePickerKayitIslemleri.Value.Day.ToString();
                if (radioButtonOgrKayitIslemleriErkek.Checked)
                    cinsiyet = "Erkek";
                else
                    cinsiyet = "Kadın";
                SQLcommandCalistir("INSERT INTO kisiler(ogr_tc,ogr_adi,ogr_soyadi,ogr_tel,ogr_cinsiyet,ogr_adres,ogr_dogum_tarihi) VALUES('" + txtOgrKayitIslemleriTC.Text + "','" + txtOgrKayitIslemleriIsim.Text + "','" + txtOgrKayitIslemleriSoyisim.Text + "','" + txtOgrKayitIslemleriTelefon.Text + "','" + cinsiyet + "','" + txtOgrKayitIslemleriAdres.Text + "','" + yil + "-" + ay + "-" + gun + "')");
                DataGrideVeriCek(dataGridViewOgrenciEkleCikar, "SELECT * FROM kisiler");
                OgrenciGoruntuleDataGridDuzenle(dataGridViewOgrenciEkleCikar);
                DataGrideVeriCek(dataGridViewOgrencileriGoruntule, "SELECT * FROM kisiler");
                OgrenciGoruntuleDataGridDuzenle(dataGridViewOgrencileriGoruntule);
                MessageBox.Show("Kayıt Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TextBoxSifirla(txtOgrKayitIslemleriTC, txtOgrKayitIslemleriAdres, txtOgrKayitIslemleriIsim, txtOgrKayitIslemleriSoyisim, txtOgrKayitIslemleriTelefon);
                dateTimePickerKayitIslemleri.Value = DateTime.Today;
                NotIslemleriOgrenciComboBox();
                EtutIslemleriComboBox();
            }
            else
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtOgrKayitIslemleriTelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtOgrKayitIslemleriTelefon.TextLength <= 1 && e.KeyChar == '0')
                e.Handled = true;
            else if(char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtOgrKayitIslemleriTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtOgrKayitIslemleriIsim_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceHarfGirisi(e);
        }

        private void txtOgrKayitIslemleriSoyisim_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceHarfGirisi(e);
        }

        private void btnOgrKayitIslemleriGuncelle_Click(object sender, EventArgs e)
        {
            if (radioButtonOgrKayitIslemleriGuncellemeIslemi.Checked && !string.IsNullOrEmpty(txtOgrKayitIslemleriTC.Text) && !string.IsNullOrEmpty(txtOgrKayitIslemleriIsim.Text) && !string.IsNullOrEmpty(txtOgrKayitIslemleriSoyisim.Text) && !string.IsNullOrEmpty(txtOgrKayitIslemleriTelefon.Text) && !string.IsNullOrEmpty(txtOgrKayitIslemleriAdres.Text))
            {
                string cinsiyet = "", yil = dateTimePickerKayitIslemleri.Value.Year.ToString(), ay = dateTimePickerKayitIslemleri.Value.Month.ToString(), gun = dateTimePickerKayitIslemleri.Value.Day.ToString();
                if (radioButtonOgrKayitIslemleriErkek.Checked)
                    cinsiyet = "Erkek";
                else
                    cinsiyet = "Kadın";

                SQLcommandCalistir("UPDATE kisiler SET ogr_adi='" + txtOgrKayitIslemleriIsim.Text + "',ogr_soyadi='" + txtOgrKayitIslemleriSoyisim.Text + "',ogr_tel='" + txtOgrKayitIslemleriTelefon.Text + "',ogr_cinsiyet='" + cinsiyet + "',ogr_adres='" + txtOgrKayitIslemleriAdres.Text + "',ogr_dogum_tarihi='" + yil + "-" + ay + "-" + gun + " 'WHERE ogr_tc='" + txtOgrKayitIslemleriTC.Text + "'");
                VerileriCek();
                MessageBox.Show("Güncelleme Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtOgrFiltreTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void radioButtonNotIslemleriGuncellemeIslemi_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNotIslemleriGuncellemeIslemi.Checked)
            {
                comboBoxNotIslemleriOgrenci.Enabled = false;
            }
        }

        private void radioButtonNotIslemleriEklemeIslemi_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNotIslemleriEklemeIslemi.Checked)
            {
                comboBoxNotIslemleriOgrenci.Enabled = true;
                comboBoxNotIslemleriOgrenci.SelectedIndex = 0;
                comboBoxNotIslemleriDers.SelectedIndex = 0;
                txtNotIslemleriNot.Text = "";
            }
        }

        private void btnNotIslemleriEkleKaydet_Click(object sender, EventArgs e)
        {
            if (radioButtonNotIslemleriEklemeIslemi.Checked)
            {
                if (!string.IsNullOrEmpty(txtNotIslemleriNot.Text) && Convert.ToInt32(txtNotIslemleriNot.Text) <= 100 && Convert.ToInt32(txtNotIslemleriNot.Text) >= 0)
                {
                    string tc = comboBoxNotIslemleriOgrenci.SelectedItem.ToString(), ders_id = comboBoxNotIslemleriDers.SelectedItem.ToString();
                    tc = tc.Substring(0, 11);
                    ders_id = ders_id.Split(' ')[0];
                    SQLcommandCalistir("INSERT INTO notlar VALUES('" + tc + "'," + ders_id + "," + txtNotIslemleriNot.Text + ")");
                    DataGrideVeriCek(dataGridViewNotIslemleri, "Select K.ogr_tc,K.ogr_adi,K.ogr_soyadi,D.ders_adi,N.ders_notu from kisiler as K, dersler as D, notlar as N where K.ogr_tc = N.ogr_tc and N.ders_id = D.ders_id");
                    NotIslemleriDataGridDuzenle(dataGridViewNotIslemleri);
                }
                else
                    MessageBox.Show("Boş alan ya da yanlış bilgi var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (radioButtonNotIslemleriGuncellemeIslemi.Checked)
            {
                if (!string.IsNullOrEmpty(txtNotIslemleriNot.Text) && Convert.ToInt32(txtNotIslemleriNot.Text) <= 100 && Convert.ToInt32(txtNotIslemleriNot.Text) >= 0)
                {
                    string tc = comboBoxNotIslemleriOgrenci.SelectedItem.ToString(), ders_id = comboBoxNotIslemleriDers.SelectedItem.ToString();
                    tc = tc.Substring(0, 11);
                    ders_id = ders_id.Split(' ')[0];
                    SQLcommandCalistir("UPDATE notlar SET ders_notu=" + txtNotIslemleriNot.Text + ", ders_id='" + ders_id + "' WHERE ogr_tc=" + tc + " and ders_id='" + dersID + "'");
                    DataGrideVeriCek(dataGridViewNotIslemleri, "Select K.ogr_tc,K.ogr_adi,K.ogr_soyadi,D.ders_adi,N.ders_notu from kisiler as K, dersler as D, notlar as N where K.ogr_tc = N.ogr_tc and N.ders_id = D.ders_id");
                    NotIslemleriDataGridDuzenle(dataGridViewNotIslemleri);
                }
                else
                    MessageBox.Show("Boş alan ya da yanlış bilgi var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtNotIslemleriNot_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void radioButtonNotIslemleriAramaIslemi_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNotIslemleriAramaIslemi.Checked)
            {
                panelNotIslemleriAramaIslemi.Visible = true;
            }
            else
                panelNotIslemleriAramaIslemi.Visible = false;
        }

        private void radioButtonNotIslemleriAramaDers_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNotIslemleriAramaDers.Checked)
                comboBoxNotIslemleriAramaDers.Enabled = true;
            else
                comboBoxNotIslemleriAramaDers.Enabled = false;
        }

        private void radioButtonNotIslemleriAramaOgrenci_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNotIslemleriAramaOgrenci.Checked)
                comboBoxNotIslemleriAramaOgrenci.Enabled = true;
            else
                comboBoxNotIslemleriAramaOgrenci.Enabled = false;
        }

        private void radioButtonNotIslemleriAramaNot_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNotIslemleriAramaNot.Checked)
                txtNotIslemleriAramaMin.Enabled = txtNotIslemleriAramaMax.Enabled = true;
            else
                txtNotIslemleriAramaMin.Enabled = txtNotIslemleriAramaMax.Enabled = false;
        }

        private void btnNotIslemleriArama_Click(object sender, EventArgs e)
        {
            if (radioButtonNotIslemleriAramaOgrenci.Checked)
            {
                string ogr_tc = comboBoxNotIslemleriAramaOgrenci.SelectedItem.ToString();
                DataGrideVeriCek(dataGridViewNotIslemleri, "Select K.ogr_tc,K.ogr_adi,K.ogr_soyadi,D.ders_adi,N.ders_notu from kisiler as K, dersler as D, notlar as N where K.ogr_tc = N.ogr_tc and N.ders_id = D.ders_id and K.ogr_tc='" + ogr_tc.Substring(0, 11) + "'");
            }
            else if(radioButtonNotIslemleriAramaDers.Checked)
            {
                string ders_id = comboBoxNotIslemleriAramaDers.SelectedItem.ToString();
                DataGrideVeriCek(dataGridViewNotIslemleri, "Select K.ogr_tc,K.ogr_adi,K.ogr_soyadi,D.ders_adi,N.ders_notu from kisiler as K, dersler as D, notlar as N where K.ogr_tc = N.ogr_tc and N.ders_id = D.ders_id and N.ders_id='" + ders_id.Split(' ')[0] + "'");
            }
            else
            {
                if (Convert.ToInt32(txtNotIslemleriAramaMin.Text) < 0 || Convert.ToInt32(txtNotIslemleriAramaMin.Text) > 100 || Convert.ToInt32(txtNotIslemleriAramaMax.Text) < 0 || Convert.ToInt32(txtNotIslemleriAramaMax.Text) > 100)
                    MessageBox.Show("0 - 100 arası değerler giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    if (!string.IsNullOrEmpty(txtNotIslemleriAramaMin.Text) && !string.IsNullOrEmpty(txtNotIslemleriAramaMax.Text))
                    {//min - max
                        DataGrideVeriCek(dataGridViewNotIslemleri, "Select K.ogr_tc,K.ogr_adi,K.ogr_soyadi,D.ders_adi,N.ders_notu from kisiler as K, dersler as D, notlar as N where K.ogr_tc = N.ogr_tc and N.ders_id = D.ders_id and N.ders_notu BETWEEN " + txtNotIslemleriAramaMin.Text + " and " + txtNotIslemleriAramaMax.Text + "");
                    }
                    else if (!string.IsNullOrEmpty(txtNotIslemleriAramaMin.Text) && string.IsNullOrEmpty(txtNotIslemleriAramaMax.Text))
                    {//min - 100
                        DataGrideVeriCek(dataGridViewNotIslemleri, "Select K.ogr_tc,K.ogr_adi,K.ogr_soyadi,D.ders_adi,N.ders_notu from kisiler as K, dersler as D, notlar as N where K.ogr_tc = N.ogr_tc and N.ders_id = D.ders_id and N.ders_notu BETWEEN " + txtNotIslemleriAramaMin.Text + " and 100");
                    }
                    else if (string.IsNullOrEmpty(txtNotIslemleriAramaMin.Text) && !string.IsNullOrEmpty(txtNotIslemleriAramaMax.Text))
                    {//0 - max
                        DataGrideVeriCek(dataGridViewNotIslemleri, "Select K.ogr_tc,K.ogr_adi,K.ogr_soyadi,D.ders_adi,N.ders_notu from kisiler as K, dersler as D, notlar as N where K.ogr_tc = N.ogr_tc and N.ders_id = D.ders_id and N.ders_notu BETWEEN 0 and " + txtNotIslemleriAramaMax.Text + "");
                    }
                    else
                    {//0 - 100
                        DataGrideVeriCek(dataGridViewNotIslemleri, "Select K.ogr_tc,K.ogr_adi,K.ogr_soyadi,D.ders_adi,N.ders_notu from kisiler as K, dersler as D, notlar as N where K.ogr_tc = N.ogr_tc and N.ders_id = D.ders_id");
                    }
                }
            }
            NotIslemleriDataGridDuzenle(dataGridViewNotIslemleri);
        }

        private void txtNotIslemleriAramaMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtNotIslemleriAramaMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void radioButtonOgretmenIslemleriEkleme_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOgretmenIslemleriEkleme.Checked)
            {
                txtOgretmenIslemleriTC.Text = txtOgretmenIslemleriAdi.Text = txtOgretmenIslemleriSoyadi.Text = "";
                txtOgretmenIslemleriTC.Enabled = txtOgretmenIslemleriAdi.Enabled = txtOgretmenIslemleriSoyadi.Enabled = true;
            }
            else
            {
                txtOgretmenIslemleriTC.Enabled = txtOgretmenIslemleriAdi.Enabled = txtOgretmenIslemleriSoyadi.Enabled = false;
                txtOgretmenIslemleriTC.Text = dataGridViewOgretmenIslemleri.CurrentRow.Cells[0].Value.ToString();
                txtOgretmenIslemleriAdi.Text = dataGridViewOgretmenIslemleri.CurrentRow.Cells[1].Value.ToString();
                txtOgretmenIslemleriSoyadi.Text = dataGridViewOgretmenIslemleri.CurrentRow.Cells[2].Value.ToString();
            }
                
        }

        private void dataGridViewOgretmenIslemleri_SelectionChanged(object sender, EventArgs e)
        {
            if(radioButtonOgretmenIslemleriSilme.Checked && dataGridViewOgretmenIslemleri.CurrentRow != null)
            {
                txtOgretmenIslemleriTC.Text = dataGridViewOgretmenIslemleri.CurrentRow.Cells[0].Value.ToString();
                txtOgretmenIslemleriAdi.Text = dataGridViewOgretmenIslemleri.CurrentRow.Cells[1].Value.ToString();
                txtOgretmenIslemleriSoyadi.Text = dataGridViewOgretmenIslemleri.CurrentRow.Cells[2].Value.ToString();
            }
        }

        private void btnOgretmenIslemleriEkleSil_Click(object sender, EventArgs e)
        {
            if (radioButtonOgretmenIslemleriSilme.Checked)
            {
                int cevap = (int)MessageBox.Show(txtOgretmenIslemleriTC.Text + " " + txtOgretmenIslemleriAdi.Text + " " + txtOgretmenIslemleriSoyadi.Text + " Kişisini silmek istediğinize emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (cevap == 6)
                {
                    SQLcommandCalistir("DELETE FROM ogretmenler WHERE ogrt_tc='" + txtOgretmenIslemleriTC.Text + "'");
                    DataGrideVeriCek(dataGridViewOgretmenIslemleri, "SELECT * FROM ogretmenler");
                    OgretmenIslemleriDataGridDuzenle(dataGridViewOgretmenIslemleri);
                    VerileriCek();
                }
            }
            else
            {
                if(!string.IsNullOrEmpty(txtOgretmenIslemleriTC.Text) && !string.IsNullOrEmpty(txtOgretmenIslemleriAdi.Text) && !string.IsNullOrEmpty(txtOgretmenIslemleriSoyadi.Text))
                {
                    SQLcommandCalistir("INSERT INTO ogretmenler VALUES('" + txtOgretmenIslemleriTC.Text + "','" + txtOgretmenIslemleriAdi.Text + "','" + txtOgretmenIslemleriSoyadi.Text + "')");
                    DataGrideVeriCek(dataGridViewOgretmenIslemleri, "SELECT * FROM ogretmenler");
                    OgretmenIslemleriDataGridDuzenle(dataGridViewOgretmenIslemleri);
                    txtOgretmenIslemleriTC.Text = txtOgretmenIslemleriAdi.Text = txtOgretmenIslemleriSoyadi.Text = "";
                    VerileriCek();
                }
            }
        }

        private void txtOgretmenIslemleriTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtOgretmenIslemleriAdi_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceHarfGirisi(e);
        }

        private void txtOgretmenIslemleriSoyadi_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceHarfGirisi(e);
        }

        private void radioButtonDersIslemleriEkleme_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDersIslemleriEkleme.Checked)
            {
                txtDersIslemleriDersAdi.Text = txtDersIslemleriDersID.Text =  "";
                txtDersIslemleriDersAdi.Enabled = txtDersIslemleriDersID.Enabled = true;
            }
            else
            {
                txtDersIslemleriDersAdi.Enabled = txtDersIslemleriDersID.Enabled = false;
                txtDersIslemleriDersID.Text = dataGridViewDersIslemleri.CurrentRow.Cells[0].Value.ToString();
                txtDersIslemleriDersAdi.Text = dataGridViewDersIslemleri.CurrentRow.Cells[1].Value.ToString();
            }
        }

        private void dataGridViewDersIslemleri_SelectionChanged(object sender, EventArgs e)
        {
            if (radioButtonDersIslemleriSilme.Checked && dataGridViewDersIslemleri.CurrentRow != null)
            {
                txtDersIslemleriDersID.Text = dataGridViewDersIslemleri.CurrentRow.Cells[0].Value.ToString();
                txtDersIslemleriDersAdi.Text = dataGridViewDersIslemleri.CurrentRow.Cells[1].Value.ToString();
            }
        }

        private void btnDersIslemleriEkleSil_Click(object sender, EventArgs e)
        {
            if (radioButtonDersIslemleriSilme.Checked)
            {
                int cevap = (int)MessageBox.Show(txtDersIslemleriDersID.Text + " " + txtDersIslemleriDersAdi.Text + " dersini silmek istediğinize emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (cevap == 6)
                {
                    SQLcommandCalistir("DELETE FROM dersler WHERE ders_id='" + txtDersIslemleriDersID.Text + "'");
                    DataGrideVeriCek(dataGridViewDersIslemleri, "SELECT * FROM dersler");
                    DersIslemleriDataGridDuzenle(dataGridViewDersIslemleri);
                    VerileriCek();
                    EtutIslemleriComboBox();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtDersIslemleriDersID.Text) && !string.IsNullOrEmpty(txtDersIslemleriDersAdi.Text))
                {
                    SQLcommandCalistir("INSERT INTO dersler VALUES('" + txtDersIslemleriDersID.Text + "','" + txtDersIslemleriDersAdi.Text + "')");
                    DataGrideVeriCek(dataGridViewDersIslemleri, "SELECT * FROM dersler");
                    DersIslemleriDataGridDuzenle(dataGridViewDersIslemleri);
                    txtDersIslemleriDersAdi.Text = txtDersIslemleriDersID.Text = "";
                    VerileriCek();
                    EtutIslemleriComboBox();
                }
            }
        }

        private void txtOgrFiltreIsim_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceHarfGirisi(e);
        }

        private void txtOgrFiltreSoyisim_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceHarfGirisi(e);
        }

        private void txtDersIslemleriDersID_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtDersIslemleriDersAdi_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceHarfGirisi(e);
        }

        private void btnDGSHesapla_Click(object sender, EventArgs e)
        {
            double sozelNet = 0, sayisalNet = 0, sozelPuani = 0, sayisalPuani = 0, esitAgirlikPuani = 0;
            if (!string.IsNullOrEmpty(txtDGSonlisansPuani.Text) && !string.IsNullOrEmpty(txtDGSSayisalDogru.Text) && !string.IsNullOrEmpty(txtDGSSayisalYanlis.Text) && !string.IsNullOrEmpty(txtDGSSozelDogru.Text) && !string.IsNullOrEmpty(txtDGSSozelYanlis.Text) && ((Convert.ToInt32(txtDGSSayisalDogru.Text) + Convert.ToInt32(txtDGSSayisalYanlis.Text)) <= 60 || (Convert.ToInt32(txtDGSSayisalDogru.Text) + Convert.ToInt32(txtDGSSayisalYanlis.Text)) >= 0) && ((Convert.ToInt32(txtDGSSozelDogru.Text) + Convert.ToInt32(txtDGSSozelYanlis.Text)) <= 60 || (Convert.ToInt32(txtDGSSozelYanlis.Text) + Convert.ToInt32(txtDGSSozelDogru.Text)) >= 0) && Convert.ToInt32(txtDGSonlisansPuani.Text) >= 40 && Convert.ToInt32(txtDGSonlisansPuani.Text) <= 80)
            {
                sozelNet = (Convert.ToInt32(txtDGSSozelDogru.Text) - (Convert.ToInt32(txtDGSSozelYanlis.Text) * 0.25));
                sayisalNet = (Convert.ToInt32(txtDGSSayisalDogru.Text) - (Convert.ToInt32(txtDGSSayisalYanlis.Text) * 0.25));

                sayisalPuani = 144.4 + (sozelNet * 0.6) + (sayisalNet * 2.8) + (Convert.ToDouble(txtDGSonlisansPuani.Text) * 0.6);
                esitAgirlikPuani = 135.4 + (sozelNet * 1.5) + (sayisalNet * 1.6) + (Convert.ToDouble(txtDGSonlisansPuani.Text) * 0.6);
                sozelPuani = 126.1 + (sayisalNet * 0.6) + (sozelNet * 2.4) + (Convert.ToDouble(txtDGSonlisansPuani.Text) * 0.6);
                if (checkBoxDGS.Checked)
                {
                    sayisalPuani -= 6;
                    esitAgirlikPuani -= 6;
                    sozelPuani -= 6;
                }
                lblSayisalPuan.Text = sayisalPuani.ToString();
                lblSozelPuani.Text = sozelPuani.ToString();
                lblEsitAgirlikPuani.Text = esitAgirlikPuani.ToString();
                lbl1.Visible = lbl2.Visible = lbl3.Visible = lblSayisalPuan.Visible = lblSozelPuani.Visible = lblEsitAgirlikPuani.Visible = true;
            }
            else
                MessageBox.Show("Boş alan var ya da yanlış değer girdiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtDGSSayisalDogru_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtDGSSayisalYanlis_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtDGSSozelDogru_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtDGSSozelYanlis_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtDGSonlisansPuani_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnDGSTemizle_Click(object sender, EventArgs e)
        {
            txtDGSSayisalDogru.Text = txtDGSSayisalYanlis.Text = txtDGSSozelDogru.Text = txtDGSSozelYanlis.Text = "0";
            checkBoxDGS.Checked = false;
            txtDGSonlisansPuani.Text = "40";
            lbl1.Visible = lbl2.Visible = lbl3.Visible = lblSayisalPuan.Visible = lblSozelPuani.Visible = lblEsitAgirlikPuani.Visible = false;
        }

        private void txtTYTTurkceDogru_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtTYTobp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnTYTHesapla_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTYTFenDogru.Text) && !string.IsNullOrEmpty(txtTYTFenYanlis.Text) && !string.IsNullOrEmpty(txtTYTSosyalDogru.Text) && !string.IsNullOrEmpty(txtTYTSosyalYanlis.Text) && !string.IsNullOrEmpty(txtTYTMatematikDogru.Text) && !string.IsNullOrEmpty(txtTYTMatematikYanlis.Text) && !string.IsNullOrEmpty(txtTYTTurkceDogru.Text) && !string.IsNullOrEmpty(txtTYTTurkceYanlis.Text) && !string.IsNullOrEmpty(txtTYTobp.Text) && ((Convert.ToInt32(txtTYTFenDogru.Text) + Convert.ToInt32(txtTYTFenYanlis.Text)) <= 20 || (Convert.ToInt32(txtTYTFenDogru.Text) + Convert.ToInt32(txtTYTFenYanlis.Text)) >= 0) && ((Convert.ToInt32(txtTYTTurkceDogru.Text) + Convert.ToInt32(txtTYTTurkceYanlis.Text)) <= 40 || (Convert.ToInt32(txtTYTTurkceDogru.Text) + Convert.ToInt32(txtTYTTurkceYanlis.Text)) >= 0) && ((Convert.ToInt32(txtTYTMatematikDogru.Text) + Convert.ToInt32(txtTYTMatematikYanlis.Text)) <= 40 || (Convert.ToInt32(txtTYTMatematikDogru.Text) + Convert.ToInt32(txtTYTMatematikYanlis.Text)) >= 0) && ((Convert.ToInt32(txtTYTSosyalDogru.Text) + Convert.ToInt32(txtTYTSosyalYanlis.Text)) <= 20 || (Convert.ToInt32(txtTYTSosyalDogru.Text) + Convert.ToInt32(txtTYTSosyalYanlis.Text)) >= 0) && (Convert.ToInt32(txtTYTobp.Text) <= 500 || Convert.ToInt32(txtTYTobp.Text) >= 250))
            {
                double turkceNet = 0, matematikNet = 0, fenNet = 0, sosyalNet = 0, tytPuani = 0;
                turkceNet = (Convert.ToInt32(txtTYTTurkceDogru.Text) - (Convert.ToInt32(txtTYTTurkceYanlis.Text) * 0.25));
                matematikNet = (Convert.ToInt32(txtTYTMatematikDogru.Text) - (Convert.ToInt32(txtTYTMatematikYanlis.Text) * 0.25));
                fenNet = (Convert.ToInt32(txtTYTFenDogru.Text) - (Convert.ToInt32(txtTYTFenYanlis.Text) * 0.25));
                sosyalNet = (Convert.ToInt32(txtTYTSosyalDogru.Text) - (Convert.ToInt32(txtTYTSosyalYanlis.Text) * 0.25));
                tytPuani = 100 + (turkceNet * 3.14) + (matematikNet * 3.72) + (fenNet * 3.49) + (sosyalNet * 3.03) + (Convert.ToDouble(txtTYTobp.Text)/5 * 0.6);
                lblTYT.Text = tytPuani.ToString();
                lblTYT.Visible = lblTYTpuan.Visible = true;
            }
            else
                MessageBox.Show("Boş alan var ya da yanlış değer girdiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnTYTTemizle_Click(object sender, EventArgs e)
        {
            txtTYTTurkceDogru.Text = txtTYTTurkceYanlis.Text = txtTYTMatematikDogru.Text = txtTYTMatematikYanlis.Text = txtTYTFenDogru.Text = txtTYTFenYanlis.Text = txtTYTSosyalDogru.Text = txtTYTSosyalYanlis.Text = "0";
            txtTYTobp.Text = "250";
            lblTYT.Visible = lblTYTpuan.Visible = false;
        }

        private void txtAYTSozelTurkceYanlis_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void radioButtonAYTSozel_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAYTSozel.Checked)
            {
                panelAYTSozel.BringToFront();
            }
        }

        private void radioButtonAYTSayisal_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAYTSayisal.Checked)
            {
                panelAYTSayisal.BringToFront();
            }
        }

        private void radioButtonAYTEsitAgirlik_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAYTEsitAgirlik.Checked)
            {
                panelAYTEsitAgirlik.BringToFront();
            }
        }

        private void btnAYTHesapla_Click(object sender, EventArgs e)
        {
            if (radioButtonAYTEsitAgirlik.Checked)
            {
                if (!string.IsNullOrEmpty(txtEsitAgirlikTurkceDogru.Text) && !string.IsNullOrEmpty(txtEsitAgirlikTurkceYanlis.Text) && !string.IsNullOrEmpty(txtEsitAgirlikTemelMatDogru.Text) && !string.IsNullOrEmpty(txtEsitAgirlikTemelMatYanlis.Text) && !string.IsNullOrEmpty(txtEsitAgirlikSosyalDogru.Text) && !string.IsNullOrEmpty(txtEsitAgirlikSosyalYanlis.Text) && !string.IsNullOrEmpty(txtEsitAgirlikFenDogru.Text) && !string.IsNullOrEmpty(txtEsitAgirlikFenYanlis.Text) && !string.IsNullOrEmpty(txtEsitAgirlikMatematikDogru.Text) && !string.IsNullOrEmpty(txtEsitAgirlikMatematikYanlis.Text) && !string.IsNullOrEmpty(txtEsitAgirlikEdebiyatDogru.Text) && !string.IsNullOrEmpty(txtEsitAgirlikEdebiyatYanlis.Text) && !string.IsNullOrEmpty(txtEsitAgirlikTarih1Dogru.Text) && !string.IsNullOrEmpty(txtEsitAgirlikTarih1Yanlis.Text) && !string.IsNullOrEmpty(txtEsitAgirlikCografya1Dogru.Text) && !string.IsNullOrEmpty(txtEsitAgirlikCografya1Yanlis.Text) && ((Convert.ToInt32(txtEsitAgirlikTurkceDogru.Text) + Convert.ToInt32(txtEsitAgirlikTurkceYanlis.Text)) <= 40 || (Convert.ToInt32(txtEsitAgirlikTurkceDogru.Text) + Convert.ToInt32(txtEsitAgirlikTurkceYanlis.Text)) >= 0) && ((Convert.ToInt32(txtEsitAgirlikTemelMatDogru.Text) + Convert.ToInt32(txtEsitAgirlikTemelMatYanlis.Text)) <= 40 || (Convert.ToInt32(txtEsitAgirlikTemelMatDogru.Text) + Convert.ToInt32(txtEsitAgirlikTemelMatYanlis.Text)) >= 0) && ((Convert.ToInt32(txtEsitAgirlikSosyalDogru.Text) + Convert.ToInt32(txtEsitAgirlikSosyalYanlis.Text)) <= 20 || (Convert.ToInt32(txtEsitAgirlikFenDogru.Text) + Convert.ToInt32(txtEsitAgirlikSosyalYanlis.Text)) >= 0) && ((Convert.ToInt32(txtEsitAgirlikFenDogru.Text) + Convert.ToInt32(txtEsitAgirlikFenYanlis.Text)) <= 20 || (Convert.ToInt32(txtEsitAgirlikFenDogru.Text) + Convert.ToInt32(txtEsitAgirlikFenYanlis.Text)) >= 0) && ((Convert.ToInt32(txtEsitAgirlikMatematikDogru.Text) + Convert.ToInt32(txtEsitAgirlikMatematikYanlis.Text)) <= 40 || (Convert.ToInt32(txtEsitAgirlikMatematikDogru.Text) + Convert.ToInt32(txtEsitAgirlikMatematikYanlis.Text)) >= 0) && ((Convert.ToInt32(txtEsitAgirlikEdebiyatDogru.Text) + Convert.ToInt32(txtEsitAgirlikEdebiyatYanlis.Text)) <= 24 || (Convert.ToInt32(txtEsitAgirlikEdebiyatDogru.Text) + Convert.ToInt32(txtEsitAgirlikEdebiyatYanlis.Text)) >= 0) && ((Convert.ToInt32(txtEsitAgirlikTarih1Dogru.Text) + Convert.ToInt32(txtEsitAgirlikTarih1Yanlis.Text)) <= 10 || (Convert.ToInt32(txtEsitAgirlikTarih1Dogru.Text) + Convert.ToInt32(txtEsitAgirlikTarih1Yanlis.Text)) >= 0) && ((Convert.ToInt32(txtEsitAgirlikCografya1Dogru.Text) + Convert.ToInt32(txtEsitAgirlikCografya1Yanlis.Text)) <= 6 || (Convert.ToInt32(txtEsitAgirlikCografya1Dogru.Text) + Convert.ToInt32(txtEsitAgirlikCografya1Yanlis.Text)) >= 0))
                {
                    double turkceNet = 0, sosyalNet = 0, temelMatematikNet = 0, fenNet = 0, matematikNet = 0, edebiyatNet = 0, tarihNet = 0, cografya1Net = 0, aytPuani = 0;
                    turkceNet = (Convert.ToInt32(txtEsitAgirlikTurkceDogru.Text) - (Convert.ToInt32(txtEsitAgirlikTurkceYanlis.Text) * 0.25));
                    temelMatematikNet = (Convert.ToInt32(txtEsitAgirlikTemelMatDogru.Text) - (Convert.ToInt32(txtEsitAgirlikTemelMatYanlis.Text) * 0.25));
                    matematikNet = (Convert.ToInt32(txtEsitAgirlikMatematikDogru.Text) - (Convert.ToInt32(txtEsitAgirlikMatematikYanlis.Text) * 0.25));
                    fenNet = (Convert.ToInt32(txtEsitAgirlikFenDogru.Text) - (Convert.ToInt32(txtEsitAgirlikFenYanlis.Text) * 0.25));
                    sosyalNet = (Convert.ToInt32(txtEsitAgirlikSosyalDogru.Text) - (Convert.ToInt32(txtEsitAgirlikSosyalYanlis.Text) * 0.25));
                    edebiyatNet = (Convert.ToInt32(txtEsitAgirlikEdebiyatDogru.Text) - (Convert.ToInt32(txtEsitAgirlikEdebiyatYanlis.Text) * 0.25));
                    tarihNet = (Convert.ToInt32(txtEsitAgirlikTarih1Dogru.Text) - (Convert.ToInt32(txtEsitAgirlikTarih1Yanlis.Text) * 0.25));
                    cografya1Net = (Convert.ToInt32(txtEsitAgirlikCografya1Dogru.Text) - (Convert.ToInt32(txtEsitAgirlikCografya1Yanlis.Text) * 0.25));
                    aytPuani = 100 + (turkceNet * 1.31) + (temelMatematikNet * 1.57) + (sosyalNet * 1.28) + (fenNet * 1.47) + (matematikNet * 3.17) + (edebiyatNet * 3.0) + (tarihNet * 2.99) + (cografya1Net * 2.4) + (Convert.ToDouble(txtAYTobp.Text) / 5.0 * 0.6);
                    lblAYTpuanTuru.Text = "Eşit Ağırlık AYT Puanı : ";
                    lblAYTpuani.Text = aytPuani.ToString();
                    lblAYTpuanTuru.Visible = lblAYTpuani.Visible = true;
                }
                else
                    MessageBox.Show("Boş alan var ya da yanlış değer girdiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (radioButtonAYTSayisal.Checked)
            {
                if (!string.IsNullOrEmpty(txtATYSayisalTurkceDogru.Text) && !string.IsNullOrEmpty(txtAYTSayisalTurkceYanlis.Text) && !string.IsNullOrEmpty(txtATYSayisalTemelMatDogru.Text) && !string.IsNullOrEmpty(txtAYTSayisalTemelMatYanlis.Text) && !string.IsNullOrEmpty(txtATYSayisalSosyalDogru.Text) && !string.IsNullOrEmpty(txtAYTSayisalSosyalYanlis.Text) && !string.IsNullOrEmpty(txtATYSayisalFenDogru.Text) && !string.IsNullOrEmpty(txtAYTSayisalFenYanlis.Text) && !string.IsNullOrEmpty(txtATYSayisalMatematikDogru.Text) && !string.IsNullOrEmpty(txtAYTSayisalMatematikYanlis.Text) && !string.IsNullOrEmpty(txtATYSayisalFizikDogru.Text) && !string.IsNullOrEmpty(txtAYTSayisalFizikYanlis.Text) && !string.IsNullOrEmpty(txtATYSayisalKimyaDogru.Text) && !string.IsNullOrEmpty(txtAYTSayisalKimyaYanlis.Text) && !string.IsNullOrEmpty(txtATYSayisalBiyolojiDogru.Text) && !string.IsNullOrEmpty(txtAYTSayisalBiyolojiYanlis.Text) && ((Convert.ToInt32(txtATYSayisalTurkceDogru.Text) + Convert.ToInt32(txtAYTSayisalTurkceYanlis.Text)) <= 40 || (Convert.ToInt32(txtATYSayisalTurkceDogru.Text) + Convert.ToInt32(txtAYTSayisalTurkceYanlis.Text)) >= 0) && ((Convert.ToInt32(txtATYSayisalTemelMatDogru.Text) + Convert.ToInt32(txtAYTSayisalTemelMatYanlis.Text)) <= 40 || (Convert.ToInt32(txtATYSayisalTemelMatDogru.Text) + Convert.ToInt32(txtAYTSayisalTemelMatYanlis.Text)) >= 0) && ((Convert.ToInt32(txtATYSayisalSosyalDogru.Text) + Convert.ToInt32(txtAYTSayisalSosyalYanlis.Text)) <= 20 || (Convert.ToInt32(txtATYSayisalSosyalDogru.Text) + Convert.ToInt32(txtAYTSayisalSosyalYanlis.Text)) >= 0) && ((Convert.ToInt32(txtATYSayisalFenDogru.Text) + Convert.ToInt32(txtAYTSayisalFenYanlis.Text)) <= 20 || (Convert.ToInt32(txtATYSayisalFenDogru.Text) + Convert.ToInt32(txtAYTSayisalFenYanlis.Text)) >= 0) && ((Convert.ToInt32(txtATYSayisalMatematikDogru.Text) + Convert.ToInt32(txtAYTSayisalMatematikYanlis.Text)) <= 40 || (Convert.ToInt32(txtATYSayisalMatematikDogru.Text) + Convert.ToInt32(txtAYTSayisalMatematikYanlis.Text)) >= 0) && ((Convert.ToInt32(txtATYSayisalFizikDogru.Text) + Convert.ToInt32(txtAYTSayisalFizikYanlis.Text)) <= 14 || (Convert.ToInt32(txtATYSayisalFizikDogru.Text) + Convert.ToInt32(txtAYTSayisalFizikYanlis.Text)) >= 0) && ((Convert.ToInt32(txtATYSayisalKimyaDogru.Text) + Convert.ToInt32(txtAYTSayisalKimyaYanlis.Text)) <= 13 || (Convert.ToInt32(txtATYSayisalKimyaDogru.Text) + Convert.ToInt32(txtAYTSayisalKimyaYanlis.Text)) >= 0) && ((Convert.ToInt32(txtATYSayisalBiyolojiDogru.Text) + Convert.ToInt32(txtAYTSayisalBiyolojiYanlis.Text)) <= 13 || (Convert.ToInt32(txtATYSayisalBiyolojiDogru.Text) + Convert.ToInt32(txtAYTSayisalBiyolojiYanlis.Text)) >= 0))
                {
                    double turkceNet = 0, sosyalNet = 0, temelMatematikNet = 0, matematikNet = 0, fenNet = 0, fizikNet = 0, kimyaNet = 0, biyolojiNet = 0, aytPuani = 0;
                    turkceNet = (Convert.ToInt32(txtATYSayisalTurkceDogru.Text) - (Convert.ToInt32(txtAYTSayisalTurkceYanlis.Text) * 0.25));
                    temelMatematikNet = (Convert.ToInt32(txtATYSayisalTemelMatDogru.Text) - (Convert.ToInt32(txtAYTSayisalTemelMatYanlis.Text) * 0.25));
                    matematikNet = (Convert.ToInt32(txtATYSayisalMatematikDogru.Text) - (Convert.ToInt32(txtAYTSayisalMatematikYanlis.Text) * 0.25));
                    fenNet = (Convert.ToInt32(txtATYSayisalFenDogru.Text) - (Convert.ToInt32(txtAYTSayisalFenYanlis.Text) * 0.25));
                    fizikNet = (Convert.ToInt32(txtATYSayisalFizikDogru.Text) - (Convert.ToInt32(txtAYTSayisalFizikYanlis.Text) * 0.25));
                    kimyaNet = (Convert.ToInt32(txtATYSayisalKimyaDogru.Text) - (Convert.ToInt32(txtAYTSayisalKimyaYanlis.Text) * 0.25));
                    biyolojiNet = (Convert.ToInt32(txtATYSayisalBiyolojiDogru.Text) - (Convert.ToInt32(txtAYTSayisalBiyolojiYanlis.Text) * 0.25));
                    sosyalNet = (Convert.ToInt32(txtATYSayisalSosyalDogru.Text) - (Convert.ToInt32(txtAYTSayisalSosyalYanlis.Text) * 0.25));
                    aytPuani = 100 + (turkceNet * 1.23) + (temelMatematikNet * 1.48) + (sosyalNet * 1.2) + (fenNet * 1.38) + (matematikNet * 2.98) + (fizikNet * 3.11) + (kimyaNet * 3.13) + (biyolojiNet * 3.08) + (Convert.ToDouble(txtAYTobp.Text) / 5.0 * 0.6);
                    lblAYTpuanTuru.Text = "Sayısal AYT Puanı : ";
                    lblAYTpuani.Text = aytPuani.ToString();
                    lblAYTpuanTuru.Visible = lblAYTpuani.Visible = true;
                }
                else
                    MessageBox.Show("Boş alan var ya da yanlış değer girdiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (!string.IsNullOrEmpty(txtAYTSozelDKABDogru.Text) && !string.IsNullOrEmpty(txtAYTSozelDKABYanlis.Text) && !string.IsNullOrEmpty(txtAYTSozelTurkceDogru.Text) && !string.IsNullOrEmpty(txtAYTSozelTurkceYanlis.Text) && !string.IsNullOrEmpty(txtAYTSozelTemelMatDogru.Text) && !string.IsNullOrEmpty(txtAYTSozelTemelMatYanlis.Text) && !string.IsNullOrEmpty(txtAYTSozelSosyalDogru.Text) && !string.IsNullOrEmpty(txtAYTSozelSosyalYanlis.Text) && !string.IsNullOrEmpty(txtAYTSozelFenDogru.Text) && !string.IsNullOrEmpty(txtAYTSozelFenYanlis.Text) && !string.IsNullOrEmpty(txtAYTSozelEdebiyatDogru.Text) && !string.IsNullOrEmpty(txtAYTSozelEdebiyatYanlis.Text) && !string.IsNullOrEmpty(txtAYTSozelTarih1Dogru.Text) && !string.IsNullOrEmpty(txtAYTSozelTarih1Yanlis.Text) && !string.IsNullOrEmpty(txtAYTSozelCografya1Dogru.Text) && !string.IsNullOrEmpty(txtAYTSozelCografya1Yanlis.Text) && !string.IsNullOrEmpty(txtAYTSozelTarih2Dogru.Text) && !string.IsNullOrEmpty(txtAYTSozelTarih2Dogru.Text) && !string.IsNullOrEmpty(txtAYTSozelCografya2Dogru.Text) && !string.IsNullOrEmpty(txtAYTSozelCografya2Yanlis.Text) && !string.IsNullOrEmpty(txtAYTSozelFelsefeDogru.Text) && !string.IsNullOrEmpty(txtAYTSozelFelsefeYanlis.Text) && ((Convert.ToInt32(txtAYTSozelTurkceDogru.Text) + Convert.ToInt32(txtAYTSozelTurkceYanlis.Text)) <= 40 || (Convert.ToInt32(txtAYTSozelTurkceDogru.Text) + Convert.ToInt32(txtAYTSozelTurkceYanlis.Text)) >= 0) && ((Convert.ToInt32(txtAYTSozelTemelMatDogru.Text) + Convert.ToInt32(txtAYTSozelTemelMatYanlis.Text)) <= 40 || (Convert.ToInt32(txtAYTSozelTemelMatDogru.Text) + Convert.ToInt32(txtAYTSozelTemelMatYanlis.Text)) >= 0) && ((Convert.ToInt32(txtAYTSozelSosyalDogru.Text) + Convert.ToInt32(txtAYTSozelSosyalYanlis.Text)) <= 20 || (Convert.ToInt32(txtAYTSozelSosyalDogru.Text) + Convert.ToInt32(txtAYTSozelSosyalYanlis.Text)) >= 0) && ((Convert.ToInt32(txtAYTSozelFenDogru.Text) + Convert.ToInt32(txtAYTSozelFenYanlis.Text)) <= 20 || (Convert.ToInt32(txtAYTSozelFenDogru.Text) + Convert.ToInt32(txtAYTSozelFenYanlis.Text)) >= 0) && ((Convert.ToInt32(txtAYTSozelEdebiyatDogru.Text) + Convert.ToInt32(txtAYTSozelEdebiyatYanlis.Text)) <= 24 || (Convert.ToInt32(txtAYTSozelEdebiyatDogru.Text) + Convert.ToInt32(txtAYTSozelEdebiyatYanlis.Text)) >= 0) && ((Convert.ToInt32(txtAYTSozelTarih1Dogru.Text) + Convert.ToInt32(txtAYTSozelTarih1Yanlis.Text)) <= 10 || (Convert.ToInt32(txtAYTSozelTarih1Dogru.Text) + Convert.ToInt32(txtAYTSozelTarih1Yanlis.Text)) >= 0) && ((Convert.ToInt32(txtAYTSozelCografya1Dogru.Text) + Convert.ToInt32(txtAYTSozelCografya1Yanlis.Text)) <= 6 || (Convert.ToInt32(txtAYTSozelCografya1Dogru.Text) + Convert.ToInt32(txtAYTSozelCografya1Yanlis.Text)) >= 0) && ((Convert.ToInt32(txtAYTSozelTarih2Dogru.Text) + Convert.ToInt32(txtAYTSozelTarih2Yanlis.Text)) <= 11 || (Convert.ToInt32(txtAYTSozelTarih2Dogru.Text) + Convert.ToInt32(txtAYTSozelTarih2Yanlis.Text)) >= 0) && ((Convert.ToInt32(txtAYTSozelCografya2Dogru.Text) + Convert.ToInt32(txtAYTSozelCografya2Yanlis.Text)) <= 11 || (Convert.ToInt32(txtAYTSozelCografya2Dogru.Text) + Convert.ToInt32(txtAYTSozelCografya2Yanlis.Text)) >= 0) && ((Convert.ToInt32(txtAYTSozelFelsefeDogru.Text) + Convert.ToInt32(txtAYTSozelFelsefeYanlis.Text)) <= 12 || (Convert.ToInt32(txtAYTSozelFelsefeDogru.Text) + Convert.ToInt32(txtAYTSozelFelsefeYanlis.Text)) >= 0) && ((Convert.ToInt32(txtAYTSozelDKABDogru.Text) + Convert.ToInt32(txtAYTSozelDKABYanlis.Text)) <= 6 || (Convert.ToInt32(txtAYTSozelDKABDogru.Text) + Convert.ToInt32(txtAYTSozelDKABYanlis.Text)) >= 0))
                {
                    double turkceNet = 0, matematikNet = 0, fenNet = 0, sosyalNet = 0, edebiyatNet = 0, tarih1Net = 0, cografya1Net = 0, tarih2Net = 0, cografya2Net = 0, dkabNet = 0, felsefeNet = 0, aytPuani = 0;
                    turkceNet = (Convert.ToInt32(txtAYTSozelTurkceDogru.Text) - (Convert.ToInt32(txtAYTSozelTurkceYanlis.Text) * 0.25));
                    matematikNet = (Convert.ToInt32(txtAYTSozelTemelMatDogru.Text) - (Convert.ToInt32(txtAYTSozelTurkceYanlis.Text) * 0.25));
                    fenNet = (Convert.ToInt32(txtAYTSozelFenDogru.Text) - (Convert.ToInt32(txtAYTSozelFenYanlis.Text) * 0.25));
                    dkabNet = (Convert.ToInt32(txtAYTSozelDKABDogru.Text) - (Convert.ToInt32(txtAYTSozelDKABYanlis.Text) * 0.25));
                    sosyalNet = (Convert.ToInt32(txtAYTSozelSosyalDogru.Text) - (Convert.ToInt32(txtAYTSozelSosyalYanlis.Text) * 0.25));
                    edebiyatNet = (Convert.ToInt32(txtAYTSozelEdebiyatDogru.Text) - (Convert.ToInt32(txtAYTSozelEdebiyatYanlis.Text) * 0.25));
                    tarih1Net = (Convert.ToInt32(txtAYTSozelTarih1Dogru.Text) - (Convert.ToInt32(txtAYTSozelTarih1Yanlis.Text) * 0.25));
                    cografya1Net = (Convert.ToInt32(txtAYTSozelCografya1Dogru.Text) - (Convert.ToInt32(txtAYTSozelCografya1Yanlis.Text) * 0.25));
                    cografya2Net = (Convert.ToInt32(txtAYTSozelCografya2Dogru.Text) - (Convert.ToInt32(txtAYTSozelCografya2Yanlis.Text) * 0.25));
                    tarih2Net = (Convert.ToInt32(txtAYTSozelTarih2Dogru.Text) - (Convert.ToInt32(txtAYTSozelTarih2Yanlis.Text) * 0.25));
                    felsefeNet = (Convert.ToInt32(txtAYTSozelFelsefeDogru.Text) - (Convert.ToInt32(txtAYTSozelFelsefeYanlis.Text) * 0.25));
                    aytPuani = 100 + (turkceNet * 1.39) + (matematikNet * 1.67) + (sosyalNet * 1.36) + (fenNet * 1.56) + (edebiyatNet * 3.19) + (tarih1Net * 3.17) + (tarih2Net * 3.34) + (cografya1Net * 2.54) + (cografya2Net * 2.75) + (felsefeNet * 3.14) + (dkabNet * 3.32) + (Convert.ToDouble(txtAYTobp.Text) / 5.0 * 0.6);
                    lblAYTpuanTuru.Text = "Sözel AYT Puanı : ";
                    lblAYTpuani.Text = aytPuani.ToString();
                    lblAYTpuanTuru.Visible = lblAYTpuani.Visible = true;
                }
                else
                    MessageBox.Show("Boş alan var ya da yanlış değer girdiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAYTTemizle_Click(object sender, EventArgs e)
        {
            lblAYTpuanTuru.Visible = lblAYTpuani.Visible = false;
        }

        private void dateTimePickerEtutTarih_ValueChanged(object sender, EventArgs e)
        {
            EtutMesgulButonlar();
            etutSaati = "";
        }

        private void btnEtut22_Click(object sender, EventArgs e)
        {
            EtutButonDuzenle((Button)sender);
        }

        private void btnEtutEkle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEtutUcret.Text) && etutSaati != "")
            {
                string tarih = dateTimePickerEtutTarih.Value.ToString().Split(' ')[0];
                string tarih2 = tarih.Split('.')[2] + "-" + tarih.Split('.')[1] + "-" + tarih.Split('.')[0];
                tarih2 += " " + etutSaati;
                SQLcommandCalistir("INSERT INTO etut(ogr_tc,ders_id,ogrt_tc,tarih_saat,ucret) VALUES('" + comboBoxEtutOgrenci.SelectedItem.ToString().Split(' ')[0] + "'," + comboBoxEtutDers.SelectedItem.ToString().Split(' ')[0] + ",'" + comboBoxEtutOgretmen.SelectedItem.ToString().Split(' ')[0] + "','" + tarih2 + "'," + txtEtutUcret.Text + ")");
                VerileriCek();
                EtutMesgulButonlar();
                etutSaati = "";
            }
            else
                MessageBox.Show("Ücreti girmediniz ya da saat seçmediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void radioButtonEtutGoruntuleOgrenci_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEtutGoruntuleOgrenci.Checked)
                comboBoxEtutGoruntuleOgrenci.Enabled = true;
            else
                comboBoxEtutGoruntuleOgrenci.Enabled = false;
        }

        private void radioButtonEtutGoruntuleOgretmen_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEtutGoruntuleOgretmen.Checked)
                comboBoxEtutGoruntuleOgretmen.Enabled = true;
            else
                comboBoxEtutGoruntuleOgretmen.Enabled = false;
        }

        private void radioButtonEtutGoruntuleDers_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEtutGoruntuleDers.Checked)
                comboBoxEtutGoruntuleDers.Enabled = true;
            else
                comboBoxEtutGoruntuleDers.Enabled = false;
        }

        private void radioButtonEtutGoruntuleTarih_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEtutGoruntuleTarih.Checked)
                dateTimePickerEtutGotunrule.Enabled = true;
            else
                dateTimePickerEtutGotunrule.Enabled = false;
        }

        private void btnEtutGoruntuleAraSil_Click(object sender, EventArgs e)
        {
            if (radioButtonEtutGoruntuleAramaIslemi.Checked)
            {
                if (radioButtonEtutGoruntuleOgrenci.Checked)
                {
                    DataGrideVeriCek(dataGridViewEtutGoruntule, "SELECT E.id,K.ogr_adi,K.ogr_soyadi,D.ders_adi,O.ogrt_adi,O.ogrt_soyadi,E.tarih_saat,E.ucret FROM etut as E,kisiler as K,ogretmenler as O,dersler as D where E.ogr_tc = K.ogr_tc and E.ogrt_tc = O.ogrt_tc and D.ders_id = E.ders_id and E.ogr_tc='" + comboBoxEtutGoruntuleOgrenci.SelectedItem.ToString().Split(' ')[0] + "'");
                    EtutIslemleriDataGridDuzenle(dataGridViewEtutGoruntule);
                }
                else if (radioButtonEtutGoruntuleOgretmen.Checked)
                {
                    DataGrideVeriCek(dataGridViewEtutGoruntule, "SELECT E.id,K.ogr_adi,K.ogr_soyadi,D.ders_adi,O.ogrt_adi,O.ogrt_soyadi,E.tarih_saat,E.ucret FROM etut as E,kisiler as K,ogretmenler as O,dersler as D where E.ogr_tc = K.ogr_tc and E.ogrt_tc = O.ogrt_tc and D.ders_id = E.ders_id and E.ogrt_tc='" + comboBoxEtutGoruntuleOgretmen.SelectedItem.ToString().Split(' ')[0] + "'");
                    EtutIslemleriDataGridDuzenle(dataGridViewEtutGoruntule);
                }
                else if (radioButtonEtutGoruntuleDers.Checked)
                {
                    DataGrideVeriCek(dataGridViewEtutGoruntule, "SELECT E.id,K.ogr_adi,K.ogr_soyadi,D.ders_adi,O.ogrt_adi,O.ogrt_soyadi,E.tarih_saat,E.ucret FROM etut as E,kisiler as K,ogretmenler as O,dersler as D where E.ogr_tc = K.ogr_tc and E.ogrt_tc = O.ogrt_tc and D.ders_id = E.ders_id and E.ders_id='" + comboBoxEtutGoruntuleDers.SelectedItem.ToString().Split(' ')[0] + "'");
                    EtutIslemleriDataGridDuzenle(dataGridViewEtutGoruntule);
                }
                else
                {
                    string tarih = dateTimePickerEtutGotunrule.Value.ToString().Split(' ')[0];
                    string tarih2 = tarih.Split('.')[2] + "-" + tarih.Split('.')[1] + "-" + tarih.Split('.')[0];
                    string tarih3 = dateTimePickerEtutGotunrule.Value.AddDays(1).ToString().Split(' ')[0];
                    string tarih4 = tarih3.Split('.')[2] + "-" + tarih3.Split('.')[1] + "-" + tarih3.Split('.')[0];
                    
                    DataGrideVeriCek(dataGridViewEtutGoruntule, "SELECT E.id,K.ogr_adi,K.ogr_soyadi,D.ders_adi,O.ogrt_adi,O.ogrt_soyadi,E.tarih_saat,E.ucret FROM etut as E,kisiler as K,ogretmenler as O,dersler as D where E.ogr_tc = K.ogr_tc and E.ogrt_tc = O.ogrt_tc and D.ders_id = E.ders_id and E.tarih_saat BETWEEN '" + tarih2 + "' and '" + tarih4 + "'");
                    EtutIslemleriDataGridDuzenle(dataGridViewEtutGoruntule);
                }
            }
            else
            {
                if(dataGridViewEtutGoruntule.CurrentRow != null)
                {
                    int cevap = (int)MessageBox.Show(dataGridViewEtutGoruntule.CurrentRow.Cells[0].Value.ToString() + " ID'li kaydı silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (cevap == 6)
                    {
                        SQLcommandCalistir("DELETE FROM etut WHERE id=" + dataGridViewEtutGoruntule.CurrentRow.Cells[0].Value.ToString() + "");
                        DataGrideVeriCek(dataGridViewEtutGoruntule, "SELECT E.id,K.ogr_adi,K.ogr_soyadi,D.ders_adi,O.ogrt_adi,O.ogrt_soyadi,E.tarih_saat,E.ucret FROM etut as E,kisiler as K,ogretmenler as O,dersler as D where E.ogr_tc = K.ogr_tc and E.ogrt_tc = O.ogrt_tc and D.ders_id = E.ders_id");
                        EtutIslemleriDataGridDuzenle(dataGridViewEtutGoruntule);
                        VerileriCek();
                    }
                }
            }
        }

        private void btnAnaSayfaCikis_Click(object sender, EventArgs e)
        {
            panelKullaniciGirisi.BringToFront();
        }

        private void linkLabelKayitOlGeri_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelKullaniciGirisi.BringToFront();
            txtKayitOlKullaniciAdi.Text = txtKayitOlSifre.Text = txtKayitOlSifreTekrar.Text = "";
        }

        private void txtEtutUcret_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void dataGridViewNotIslemleri_SelectionChanged(object sender, EventArgs e)
        {
            if (radioButtonNotIslemleriGuncellemeIslemi.Checked && dataGridViewNotIslemleri.CurrentRow != null)
            { 
                for (int i = 0; i < comboBoxNotIslemleriDers.Items.Count; i++)
                {
                    string temp = comboBoxNotIslemleriDers.Items[i].ToString();
                    if(temp.Split(' ').Length > 3)
                    {
                        if (dataGridViewNotIslemleri.CurrentRow.Cells[3].Value.ToString() == DersAdiGetir(temp, temp.Split(' ').Length))
                        {
                            dersID = temp.Split(' ')[0];
                            comboBoxNotIslemleriDers.SelectedItem = comboBoxNotIslemleriDers.Items[i];
                            break;
                        }
                    }
                    else
                    {
                        if (dataGridViewNotIslemleri.CurrentRow.Cells[3].Value.ToString() == temp.Split(' ')[2])
                        {
                            dersID = temp.Split(' ')[0];
                            comboBoxNotIslemleriDers.SelectedItem = comboBoxNotIslemleriDers.Items[i];
                            break;
                        }
                    }
                    
                }
                txtNotIslemleriNot.Text = dataGridViewNotIslemleri.CurrentRow.Cells[4].Value.ToString();
                for (int i = 0; i < comboBoxNotIslemleriOgrenci.Items.Count; i++)
                {
                    string temp = comboBoxNotIslemleriOgrenci.Items[i].ToString();
                    if (dataGridViewNotIslemleri.CurrentRow.Cells[0].Value.ToString() == temp.Substring(0,11))
                    {
                        comboBoxNotIslemleriOgrenci.SelectedItem = comboBoxNotIslemleriOgrenci.Items[i];
                        break;
                    }
                }
            }
        }
    }
}
