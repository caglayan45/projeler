using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KartOyunu
{
    public class Futbolcu : Sporcu
    {
        int penalti,serbestAtis,kaleciyleKarsiKarsiya;
        bool kartKullanildiMi = false;

        public Futbolcu() : base()
        {
            sporcuIsim = "Futbolcu";
            sporcuTakim = "FUTBOL";
        }

        public Futbolcu(string adi, string takim, int penalti, int serbestAtis, int kaleciyleKarsiKarsiya, Point konum, Size boyut, bool acikMi, oyunForm form) : base(adi,takim)
        {
            sporcuIsim = adi;
            sporcuTakim = takim;
            oyuncuKarti = KartOlustur(konum, boyut, acikMi, serbestAtis, penalti, kaleciyleKarsiKarsiya, form);
            this.penalti = penalti;
            this.serbestAtis = serbestAtis;
            this.kaleciyleKarsiKarsiya = kaleciyleKarsiKarsiya;
        }

        public override Panel KartOlustur(Point konum, Size boyut, bool acikMi, int deger1, int deger2, int deger3, oyunForm form)
        {
            Panel kart = new Panel();
            //KartOyunu.Form1.ActiveForm.Controls.Add(kart);

            kart.Size = boyut;
            kart.Location = konum;
            kart.BackgroundImageLayout = ImageLayout.Stretch;
            kart.AutoSizeMode = AutoSizeMode.GrowOnly;

            Panel arkaPlan = new Panel();
            arkaPlan.Name = "arkaPlan";
            kart.Controls.Add(arkaPlan);
            arkaPlan.BackgroundImageLayout = ImageLayout.Stretch;
            arkaPlan.AutoSize = true;
            arkaPlan.Size = kart.Size;
            arkaPlan.Visible = false;

            Label isim = new Label();
            isim.Name = "isim";
            isim.BackColor = Color.Transparent;
            isim.TextAlign = ContentAlignment.MiddleCenter;
            isim.ForeColor = Color.White;

            Label ozellik = new Label();
            ozellik.Name = "ozellik";
            ozellik.BackColor = Color.Transparent;
            ozellik.ForeColor = Color.White;
            ozellik.TextAlign = ContentAlignment.MiddleCenter;

            isim.Width = (kart.Width <= 105) ? 90 : 180;
            isim.Location = new Point(0, (kart.Height <= 150) ? 1 : 12);
            isim.Font = new Font(FontFamily.GenericSansSerif, (kart.Width <= 105) ? 7.5f : 15.0f, FontStyle.Bold);

            ozellik.Width = (kart.Width <= 105) ? 80 : 150;
            ozellik.Location = new Point((kart.Width <= 105) ? 5 : 10, (kart.Height <= 150) ? 118 : 245);
            ozellik.Font = new Font(FontFamily.GenericSansSerif, (kart.Width <= 105) ? 5.0f : 6.7f, FontStyle.Bold);

            kart.Name = base.sporcuTakim;
            kart.Controls.Add(isim);
            kart.Controls.Add(ozellik);
            ozellik.Text = "SRV: " + deger1 + " PEN: " + deger2 + " KKG: " + deger3;
            switch (base.sporcuIsim)
            {
                case "BECKENBAUER":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\futbol\\beckenbauer.png");
                    break;
                case "DROGBA":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\futbol\\drogba.png");
                    break;
                case "HAGI":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\futbol\\hagi.png");
                    break;
                case "HENRY":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\futbol\\henry.png");
                    break;
                case "METIN OKTAY":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\futbol\\metinoktay.png");
                    break;
                case "RONALDINHO":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\futbol\\ronaldinho.png");
                    break;
                case "XAVI":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\futbol\\xavi.png");
                    break;
                case "ZLATAN":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\futbol\\zlatan.png");
                    break;
                default:
                    break;
            }
            arkaPlan.BackgroundImage = Image.FromFile("resimler\\futbol\\fulbolKartArka.png");
            if (acikMi)
            {
                arkaPlan.SendToBack();
                arkaPlan.Visible = false;
            }
            else
            {
                arkaPlan.BringToFront();
                arkaPlan.Visible = true;
            }
            kart.BringToFront();//bu ifin üzerine geçebilir kontrol et
            form.Controls.Add(kart);
            return kart;
        }

        public bool KartKullanildiMi()
        {
            return this.kartKullanildiMi;
        }

        public void SetKartKullanildiMi(bool k)
        {
            this.kartKullanildiMi = k;
        }

        public int GetPenalti()
        {
            return this.penalti;
        }

        public void SetPenalti(int penalti)
        {
            this.penalti = penalti;
        }

        public int GetSerbestAtis()
        {
            return this.serbestAtis;
        }

        public void SetSerbestAtis(int serbestAtis)
        {
            this.serbestAtis = serbestAtis;
        }

        public int GetKaleciyleKarsiKarsiya()
        {
            return this.kaleciyleKarsiKarsiya;
        }

        public void SetKaleciyleKarsiKarsiya(int kaleciyleKarsiKarsiya)
        {
            this.kaleciyleKarsiKarsiya = kaleciyleKarsiKarsiya;
        }

        public void SetAdi(string adi)
        {
            base.sporcuIsim = adi;
        }

        public string GetAdi()
        {
            return base.sporcuIsim;
        }

        public void SetTakim(string takim)
        {
            base.sporcuTakim = takim;
        }

        public string GetTakim()
        {
            return base.sporcuTakim;
        }

        public override string SporcuPuaniGoster()
        {
            return "Penaltı : " + this.penalti + " - Serbest atış : " + this.serbestAtis + " - Kaleciyle karşı karşıya : " + this.kaleciyleKarsiKarsiya;
        }
    }
}
