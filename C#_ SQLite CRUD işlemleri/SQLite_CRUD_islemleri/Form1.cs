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

namespace SQLite_CRUD_islemleri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SQLiteConnection con = new SQLiteConnection("Data Source=ogrenciler.db;");

        void OgrencileriGotruntule()
        {
            con.Close();
            con.Open();
            dgvOgrenciler.DataSource = "";
            using (SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT * FROM ogrenciler", con))
            {
                using (DataSet ds = new DataSet())
                {
                    da.Fill(ds);
                    dgvOgrenciler.DataSource = ds.Tables[0];
                }
            }
            con.Close();
            dgvOgrenciler.Columns[0].HeaderText = "ID";
            dgvOgrenciler.Columns[1].HeaderText = "ISIM";
            dgvOgrenciler.Columns[2].HeaderText = "SOYISIM";
            dgvOgrenciler.Columns[3].HeaderText = "NUMARA";
            dgvOgrenciler.Columns[0].Width = 50;
            dgvOgrenciler.Columns[1].Width = 150;
            dgvOgrenciler.Columns[2].Width = 150;
            dgvOgrenciler.Columns[3].Width = 75;
        }

        void OgrenciEkle(string ad, string soyad, string numara)
        {
            try
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO ogrenciler(ad,soyad,numara) VALUES('" + ad + "','" + soyad + "','" + numara + "')", con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception e)
            {
                con.Close();
                MessageBox.Show(e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
            OgrencileriGotruntule();
        }

        void OgrenciGuncelle(int id,string ad, string soyad, string numara)
        {
            try
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("UPDATE ogrenciler SET ad='" + ad + "',soyad='" + soyad + "',numara='" + numara + "' WHERE id=" + id + "", con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception e)
            {
                con.Close();
                MessageBox.Show(e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
            OgrencileriGotruntule();
        }

        void OgrenciSil(int id)
        {
            try
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM ogrenciler WHERE id=" + id + "", con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                con.Close();
                MessageBox.Show(e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
            OgrencileriGotruntule();
        }

        string OgrenciBul(int id)
        {
            string kisi = "Öğrenci bulunamadı.";
            try
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM ogrenciler WHERE id=" + id + "", con))
                {
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                            kisi = dr["id"].ToString() + " " + dr["ad"].ToString() + " " + dr["soyad"].ToString() + " " + dr["numara"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                con.Close();
                MessageBox.Show(e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
            OgrencileriGotruntule();
            return kisi;
        }

        private void btnOgrenciEkle_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtOgrenciEkleAd.Text) || string.IsNullOrEmpty(txtOgrenciEkleSoyad.Text) || string.IsNullOrEmpty(txtOgrenciEkleNumara.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OgrenciEkle(txtOgrenciEkleAd.Text, txtOgrenciEkleSoyad.Text, txtOgrenciEkleNumara.Text);
        }

        private void btnOgrenciGuncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOgrenciGuncelleId.Text) || string.IsNullOrEmpty(txtOgrenciGuncelleAd.Text) || string.IsNullOrEmpty(txtOgrenciGuncelleSoyad.Text) || string.IsNullOrEmpty(txtOgrenciGuncelleNumara.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OgrenciGuncelle(Convert.ToInt32(txtOgrenciGuncelleId.Text), txtOgrenciGuncelleAd.Text, txtOgrenciGuncelleSoyad.Text, txtOgrenciGuncelleNumara.Text);
        }

        private void btnOgrenciSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOgrenciSilId.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OgrenciSil(Convert.ToInt32(txtOgrenciSilId.Text));
        }

        private void btnOgrenciBulId_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOgrenciBulId.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show(OgrenciBul(Convert.ToInt32(txtOgrenciBulId.Text)), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OgrencileriGotruntule();
        }
    }
}
