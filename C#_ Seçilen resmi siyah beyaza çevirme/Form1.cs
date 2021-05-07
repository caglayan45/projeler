using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResmiSiyahBeyazYapma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool kontrol = false;

        private void btnResimSec_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Resim Dosyaları|" + "*.bmp;*.jpeg;*.jpg;*.gif;*.tif;*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                kontrol = true;
            }
        }

        private void btnDonustur_Click(object sender, EventArgs e)
        {
            if (!kontrol)
            {
                MessageBox.Show("Lütfen önce resim seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            progressBar1.Maximum = bmp.Width * bmp.Height;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color renk = bmp.GetPixel(i, j);
                    renk = Color.FromArgb(((Convert.ToInt32(renk.R) + Convert.ToInt32(renk.G) + Convert.ToInt32(renk.B)) / 3), ((Convert.ToInt32(renk.R) + Convert.ToInt32(renk.G) + Convert.ToInt32(renk.B)) / 3), ((Convert.ToInt32(renk.R) + Convert.ToInt32(renk.G) + Convert.ToInt32(renk.B)) / 3));
                    bmp.SetPixel(i, j, renk);
                    if (i % 10 == 0)
                        progressBar1.Value = i * bmp.Height + j;
                }
            }
            pictureBox1.Image = bmp;
            progressBar1.Visible = false;
        }
    }
}
