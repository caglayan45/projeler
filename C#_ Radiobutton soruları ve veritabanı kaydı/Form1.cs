using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace SoruVeritabani
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<int> cevaplar = new List<int>();
        string kullaniciAdi = "", sonuc = "";
        SQLiteConnection con = new SQLiteConnection("Data Source=kullanicilar.db;");

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 548;
            this.Height = 199;
            panelGiris.BringToFront();
            for (int i = 0; i < 7; i++)
                cevaplar.Add(0);
        }

        void VerileriCek()
        {
            con.Open();
            dataGridView1.DataSource = "";
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT * FROM hizmetler", con);
            using (DataSet ds = new DataSet())
            {
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            con.Close();
            DataGridDuzenle();
        }

        void DataGridDuzenle()
        {
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "KULLANICI ADI";
            dataGridView1.Columns[2].HeaderText = "HİZMET TİPİ";
        }

        void SonucBelirle()
        {
            if (cevaplar[0] == 1)
                sonuc = "Özel fırsatlar uygulanabilir.";
            else if (cevaplar[0] == 0 && cevaplar[1] == 1 && cevaplar[2] == 0)
                sonuc = "Özel fırsatlar uygulanabilir.";
            else if (cevaplar[0] == 0 && cevaplar[1] == 1 && cevaplar[2] == 1 && cevaplar[6] == 1)
                sonuc = "Özel fırsatlar uygulanabilir.";
            else if (cevaplar[0] == 0 && cevaplar[1] == 1 && cevaplar[2] == 1 && cevaplar[4] == 1 && cevaplar[6] == 0)
                sonuc = "Özel fırsatlar uygulanabilir.";
            else if (cevaplar[0] == 0 && cevaplar[1] == 0 && cevaplar[2] == 0 && cevaplar[4] == 1)
                sonuc = "Özel fırsatlar uygulanabilir.";
            else if (cevaplar[0] == 0 && cevaplar[1] == 0 && cevaplar[2] == 1 && cevaplar[4] == 0)
                sonuc = "Özel fırsatlar uygulanabilir.";
            else if (cevaplar[0] == 0 && cevaplar[1] == 0 && cevaplar[2] == 1 && cevaplar[4] == 1 && cevaplar[6] == 0)
                sonuc = "Özel fırsatlar uygulanabilir.";
            else if (cevaplar[0] == 0 && cevaplar[1] == 0 && cevaplar[2] == 0 && cevaplar[4] == 0)
                sonuc = "Standart hizmetler uygulanır.";
            else if (cevaplar[0] == 0 && cevaplar[1] == 0 && cevaplar[2] == 1 && cevaplar[4] == 1 && cevaplar[6] == 1)
                sonuc = "Standart hizmetler uygulanır.";
            else if (cevaplar[0] == 0 && cevaplar[1] == 1 && cevaplar[2] == 1 && cevaplar[4] == 0 && cevaplar[6] == 0)
                sonuc = "Standart hizmetler uygulanır.";
            else
                sonuc = "Standart hizmetler uygulanır.";
            lblSonuc.Text = sonuc;
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO hizmetler(kullanici_adi,hizmet_tipi) VALUES('" + kullaniciAdi + "','" + sonuc + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            VerileriCek();
        }

        private void btnSoru1_Click(object sender, EventArgs e)
        {
            if (radioButtonSoru1Evet.Checked)
                cevaplar[0] = 1;
            else
                cevaplar[0] = 0;
            panel2.BringToFront();
        }

        private void btnSoru7Onceki_Click(object sender, EventArgs e)
        {
            panel6.BringToFront();
        }

        private void btnSoru2Onceki_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
        }

        private void btnSoru2Sonraki_Click(object sender, EventArgs e)
        {
            if (radioButtonSoru2Evet.Checked)
                cevaplar[1] = 1;
            else
                cevaplar[1] = 0;
            panel3.BringToFront();
        }

        private void btnSoru3Onceki_Click(object sender, EventArgs e)
        {
            panel2.BringToFront();
        }

        private void btnSoru3Sonraki_Click(object sender, EventArgs e)
        {
            if (radioButtonSoru3Evet.Checked)
                cevaplar[2] = 1;
            else
                cevaplar[2] = 0;
            panel4.BringToFront();
        }

        private void btnSoru4Onceki_Click(object sender, EventArgs e)
        {
            panel3.BringToFront();
        }

        private void btnSoru4Sonraki_Click(object sender, EventArgs e)
        {
            if (radioButtonSoru4Evet.Checked)
                cevaplar[3] = 1;
            else
                cevaplar[3] = 0;
            panel5.BringToFront();
        }

        private void btnSoru5Onceki_Click(object sender, EventArgs e)
        {
            panel4.BringToFront();
        }

        private void btnSoru5Sonraki_Click(object sender, EventArgs e)
        {
            if (radioButtonSoru5Evet.Checked)
                cevaplar[4] = 1;
            else
                cevaplar[4] = 0;
            panel6.BringToFront();
        }

        private void btnSoru6Onceki_Click(object sender, EventArgs e)
        {
            panel5.BringToFront();
        }

        private void btnSoru6Sonraki_Click(object sender, EventArgs e)
        {
            if (radioButtonSoru6Evet.Checked)
                cevaplar[5] = 1;
            else
                cevaplar[5] = 0;
            panel7.BringToFront();
        }

        private void btnSoru7Gonder_Click(object sender, EventArgs e)
        {
            if (radioButtonSoru7Evet.Checked)
                cevaplar[6] = 1;
            else
                cevaplar[6] = 0;
            SonucBelirle();
            panelSonuc.BringToFront();
        }

        private void btnBasaDon_Click(object sender, EventArgs e)
        {
            txtKullaniciAdi.Text = "";
            this.Height = 199;
            dataGridView1.Visible = false;
            panelGiris.BringToFront();
        }

        private void btnDataGirdGoster_Click(object sender, EventArgs e)
        {
            this.Height = 542;
            dataGridView1.Visible = true;
            dataGridView1.BringToFront();
        }

        private void btnBasaDonBirinci_Click(object sender, EventArgs e)
        {
            txtKullaniciAdi.Text = "";
            this.Height = 199;
            dataGridView1.Visible = false;
            panelGiris.BringToFront();
        }

        private void btnBaslat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKullaniciAdi.Text))
            {
                MessageBox.Show("Lütfen bir kullanıcı adı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            kullaniciAdi = txtKullaniciAdi.Text;
            panel1.BringToFront();
        }
    }
}
