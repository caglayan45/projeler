using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace AltinToplamaOyunu
{
    public partial class altinToplamaOyunu : Form
    {

        public static int M = 0, N = 0; 
        int hamleHakki = 0, oyuncuBaslangicAltinMiktari = 0, altinSayisi = 0, gizliAltinSayisi = 0;
        float gizliAltinOrani = 0, altinOrani = 0;
        public Oyuncu[] Oyuncular = new Oyuncu[4];
        public OyunTahtasi oyunTahtasi;
        public Altin[] Altinlar;
        public static PictureBox[] Kareler;
        public static ListBox logEkrani;
        public static Label[] oyuncuLabellar = new Label[4];
        void VerileriCek()
        {
            M = (int)numericMDegeri.Value;
            N = (int)numericNDegeri.Value;
            altinOrani = (float)numericAltinOrani.Value / (float)100.0;
            if (((int)(M * N * altinOrani)) * 100 < (float)(M * N * altinOrani) * (float)100.0)
            {
                altinSayisi = (int)Math.Ceiling(M * N * altinOrani);
            }
            else
                altinSayisi = (int)(M * N * altinOrani);
            
            gizliAltinOrani = (float)numericGizliAltinOrani.Value / (float)100.0;
            if (((int)(altinSayisi * gizliAltinOrani)) * 100 < (float)(altinSayisi * gizliAltinOrani) * (float)100.0)
            {
                //MessageBox.Show(((int)(altinSayisi * gizliAltinOrani)) * 100 + " < " + (altinSayisi * gizliAltinOrani) * 100.0);
                gizliAltinSayisi = (int)Math.Ceiling(altinSayisi * gizliAltinOrani);
            }
            else
                gizliAltinSayisi = (int)(altinSayisi * gizliAltinOrani);

            hamleHakki = (int)numericHamleHakki.Value;
            oyuncuBaslangicAltinMiktari = (int)numericBaslangicAltin.Value;
        }

        void TahtayiCiz()
        {
            VerileriCek();
            Form Form1 = altinToplamaOyunu.ActiveForm;
            Form1.Visible = false;
            Form Form2 = new Form();
            Form2.Text = "Oyun Tahtası Yükleniyor..";
            Form2.Width = 420;
            Form2.Height = 110;
            Form2.StartPosition = FormStartPosition.CenterScreen;
            ProgressBar pb = new ProgressBar();
            Form2.Controls.Add(pb);
            pb.Top = 10;
            pb.Left = 10;
            pb.Minimum = 0;
            pb.Maximum = M * N + altinSayisi;
            pb.Width = 380;
            pb.Height = 50;
            Form2.Visible = true;

            panelGiris.SendToBack();
            panelGiris.Enabled = false;
            Kareler = new PictureBox[M * N];

            for (int i = 0; i < Kareler.Length; i++)
            {
                Kareler[i] = new PictureBox();
                panelOyun.Controls.Add(Kareler[i]);
                Kareler[i].Width = panelOyun.Width / M;
                Kareler[i].Height = panelOyun.Height / N;
                Kareler[i].BorderStyle = BorderStyle.None;
                
            }

            int j = 0;
            for (int m = 0; m < M; m++)//SÜTUN
            {
                for (int n = 0; n < N; n++)//SATIR
                {
                    if (m % 2 == 0)
                    {
                        if (n % 2 == 0)
                            Kareler[j].BackColor = Color.Black;
                        else
                            Kareler[j].BackColor = Color.White;
                    }
                    else
                    {
                        if (n % 2 == 1)
                            Kareler[j].BackColor = Color.Black;
                        else
                            Kareler[j].BackColor = Color.White;
                    }
                    Kareler[j].Left = m * panelOyun.Width / M;
                    Kareler[j].Top = n * panelOyun.Width / N;
                    j++;
                    pb.Value++;
                }
            }

            Altinlar = new Altin[altinSayisi];
            oyunTahtasi = new OyunTahtasi(M, N, Altinlar, altinSayisi, gizliAltinSayisi,panelOyun);
            Altinlar = oyunTahtasi.GetAltinlar();

            Console.WriteLine("Altın miktarı : {0}", altinSayisi);
            Console.WriteLine("Gizli altın miktarı : {0}", gizliAltinSayisi);
            Form2.Visible = false;
            Form1.Visible = true;

        }

        private void altinToplamaOyunu_Load(object sender, EventArgs e)
        {
            logEkrani = listBoxLogEkrani;
            oyuncuLabellar[0] = labelOyuncuA;
            oyuncuLabellar[1] = labelOyuncuB;
            oyuncuLabellar[2] = labelOyuncuC;
            oyuncuLabellar[3] = labelOyuncuD;

            oyuncuLabellar[0].Text = "Oyuncu A: ";
            oyuncuLabellar[1].Text = "Oyuncu B: ";
            oyuncuLabellar[2].Text = "Oyuncu C: ";
            oyuncuLabellar[3].Text = "Oyuncu D: ";
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            panelOyun.Refresh();
        }

        void OyunculariOlustur()
        {
            Image[] imgOyuncular = new Image[4];
            imgOyuncular[0] = Image.FromFile("resimler\\bluePlayer.png");
            imgOyuncular[1] = Image.FromFile("resimler\\greenPlayer.png");
            imgOyuncular[2] = Image.FromFile("resimler\\redPlayer.png");
            imgOyuncular[3] = Image.FromFile("resimler\\yellowPlayer.png");

            PictureBox[] pcbOyuncular = new PictureBox[4];
            
            for (int i = 0; i < pcbOyuncular.Length; i++)
            {
                pcbOyuncular[i] = new PictureBox();
                panelOyun.Controls.Add(pcbOyuncular[i]);
                pcbOyuncular[i].Image = imgOyuncular[i];
                pcbOyuncular[i].SizeMode = PictureBoxSizeMode.StretchImage;
                pcbOyuncular[i].Width = (panelOyun.Width / M)/2;
                pcbOyuncular[i].Height = (panelOyun.Height / N)/2;
                pcbOyuncular[i].BorderStyle = BorderStyle.None;
                pcbOyuncular[i].BringToFront();
                pcbOyuncular[i].Visible = true;
            }
            

            pcbOyuncular[0].Top = 0 + pcbOyuncular[0].Height / 2;
            pcbOyuncular[0].Left = 0 + pcbOyuncular[0].Width / 2;
            pcbOyuncular[0].BackColor = Color.Black;

            pcbOyuncular[1].Top = 0 + pcbOyuncular[1].Height / 2;
            pcbOyuncular[1].Left = ((M - 1) * panelOyun.Width / M) + pcbOyuncular[1].Width / 2;
            pcbOyuncular[1].BackColor = ((M - 1) % 2 == 0) ? Color.Black : Color.White;

            pcbOyuncular[2].Top = ((N - 1) * panelOyun.Height / N) + pcbOyuncular[0].Height / 2;
            pcbOyuncular[2].Left = 0 + pcbOyuncular[2].Width / 2;
            pcbOyuncular[2].BackColor = ((N - 1) % 2 == 0) ? Color.Black : Color.White;

            pcbOyuncular[3].Top = ((N - 1) * panelOyun.Height / N) + pcbOyuncular[3].Height / 2;
            pcbOyuncular[3].Left = ((M - 1) * panelOyun.Width / M) + pcbOyuncular[3].Width / 2;
            if (N % 2 == 0 && M % 2 == 0)
                pcbOyuncular[3].BackColor = Color.Black;
            else if (N % 2 == 1 && M % 2 == 0)
                pcbOyuncular[3].BackColor = Color.White;
            else if (N % 2 == 0 && M % 2 == 1)
                pcbOyuncular[3].BackColor = Color.White;
            else if (N % 2 == 1 && M % 2 == 1) 
                pcbOyuncular[3].BackColor = Color.Black;
            //pcbOyuncular[3].BackColor = (N % 2 == 0 && M % 2 == 0) ? Color.Black : Color.White;
            //pcbOyuncular[3].BackColor = (N-1 % 2 == 0 && M % 2 == 0) ? Color.White : Color.Black;
            //pcbOyuncular[3].BackColor = (N % 2 == 0 && M-1 % 2 == 0) ? Color.Black : Color.White;


            Oyuncular[0] = new Oyuncu('A', oyuncuBaslangicAltinMiktari, (int)numericA_Hamle.Value, (int)numericA_Hedef.Value, hamleHakki, new Oyuncu.Konum(0, 0), pcbOyuncular[0], Altinlar);
            Oyuncular[1] = new Oyuncu('B', oyuncuBaslangicAltinMiktari, (int)numericB_Hamle.Value, (int)numericB_Hedef.Value, hamleHakki, new Oyuncu.Konum(M - 1, 0), pcbOyuncular[1], Altinlar);
            Oyuncular[2] = new Oyuncu('C', oyuncuBaslangicAltinMiktari, (int)numericC_Hamle.Value, (int)numericC_Hedef.Value, hamleHakki, new Oyuncu.Konum(0, N - 1), pcbOyuncular[2], Altinlar);
            Oyuncular[3] = new Oyuncu('D', oyuncuBaslangicAltinMiktari, (int)numericD_Hamle.Value, (int)numericD_Hedef.Value, hamleHakki, new Oyuncu.Konum(M - 1, N - 1), pcbOyuncular[3], Altinlar);

            Oyuncu.OyunculariOlustur(Oyuncular);
            panelLog.BringToFront();
        }

        public altinToplamaOyunu()
        {
            InitializeComponent();
        }

        private void buttonOyunBaslat_Click(object sender, EventArgs e)
        {
            VerileriCek();
            TahtayiCiz();
            OyunculariOlustur();
            panelLog.Refresh();
            oyuncuLabellar[0].Text = "Oyuncu A: " + (int)numericBaslangicAltin.Value;
            oyuncuLabellar[1].Text = "Oyuncu B: " + (int)numericBaslangicAltin.Value;
            oyuncuLabellar[2].Text = "Oyuncu C: " + (int)numericBaslangicAltin.Value;
            oyuncuLabellar[3].Text = "Oyuncu D: " + (int)numericBaslangicAltin.Value;
            oyuncuLabellar[0].Refresh();
            oyuncuLabellar[1].Refresh();
            oyuncuLabellar[2].Refresh();
            oyuncuLabellar[3].Refresh();
            timer1.Start();
            oyunTahtasi.Oyun(panelOyun);
            timer1.Stop();
            
        }

        private void panelGiris_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
