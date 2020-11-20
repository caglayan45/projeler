using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltinToplamaOyunu
{
    public class Altin
    {
        int deger;
        int[,] Konum = new int[1, 2];
        bool gorunurluk = true;
        public PictureBox gorsel;

        public Altin(int deger, int X, int Y, int M, int N, Panel panelOyun)
        {
           
            this.deger = deger;
            this.Konum[0, 0] = X;
            this.Konum[0, 1] = Y;
            EkrandaGoster(M, N, panelOyun);
        }

        void EkrandaGoster(int M, int N, Panel panelOyun)
        {
            gorsel = new PictureBox();
            Label deger = new Label();
            Image altinResim = Image.FromFile("resimler\\altin.png");
            panelOyun.Controls.Add(gorsel);
            gorsel.Image = altinResim;
            gorsel.SizeMode = PictureBoxSizeMode.StretchImage;
            gorsel.Width = panelOyun.Width / M;
            gorsel.Height = panelOyun.Height / N;
            gorsel.BorderStyle = BorderStyle.FixedSingle;
            gorsel.BackgroundImageLayout = ImageLayout.Zoom;
            gorsel.BringToFront();
            deger.BackColor = Color.Transparent;
            deger.Font = new Font(FontFamily.GenericSerif, 20.0f, FontStyle.Bold);
            deger.ForeColor = Color.Black;
            deger.Size = gorsel.Size;
            deger.TextAlign = ContentAlignment.MiddleCenter;
            deger.Text = this.deger.ToString();
            gorsel.Controls.Add(deger);

            if (Konum[0, 0] % 2 == 0)
            {
                if (Konum[0, 1] % 2 == 0)
                    gorsel.BackColor = Color.Black;
                else
                    gorsel.BackColor = Color.White;
            }
            else
            {
                if (Konum[0, 1] % 2 == 0)
                    gorsel.BackColor = Color.White;
                else
                    gorsel.BackColor = Color.Black;
            }
            gorsel.Top = Konum[0, 1] * panelOyun.Height / N;
            gorsel.Left = Konum[0, 0] * panelOyun.Width / M;

            if (!this.gorunurluk)
                gorsel.Visible = false;
            else
                gorsel.Visible = true;
            Console.WriteLine("{0} , {1}  -  Değer : {2}", Konum[0,0], Konum[0, 1],deger);
        }

        public void SetGorunurluk(bool gorunurluk)
        {
            gorsel.Visible = gorunurluk;
            this.gorunurluk = gorunurluk;
        }

        public bool GetGorunurluk()
        {
            return this.gorunurluk;
        }

        public int GetDeger()
        {
            return this.deger;
        }

        public void SetDeger(int deger)
        {
            this.deger = deger;
        }

        public int GetX()
        {
            return this.Konum[0, 0];
        }

        public int GetY()
        {
            return this.Konum[0, 1];
        }

    }
}
