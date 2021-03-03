using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KartOyunu
{
    public class Basketbolcu : Sporcu
    {
        int ikilik, ucluk, serbestAtis;
        bool kartKullanildiMi = false;

        public Basketbolcu() : base()
        {
            sporcuIsim = "Basketbolcu";
            sporcuTakim = "BASKETBOL";
        }

        public Basketbolcu(string adi, string takim, int ikilik, int ucluk, int serbestAtis, Point konum, Size boyut, bool acikMi, oyunForm form) : base(adi, takim)
        {
            sporcuIsim = adi;
            sporcuTakim = takim;
            oyuncuKarti = KartOlustur(konum, boyut, acikMi, ucluk, ikilik, serbestAtis, form);
            this.ikilik = ikilik;
            this.ucluk = ucluk;
            this.serbestAtis = serbestAtis;
        }

        public override Panel KartOlustur(Point konum, Size boyut, bool acikMi, int deger1, int deger2, int deger3,oyunForm form)
        {
            Panel kart = new Panel();

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

            kart.Controls.Add(isim);
            kart.Controls.Add(ozellik);

            ozellik.Text = "UC: " + deger1 + " IKI: " + deger2 + " SRA: " + deger3;
            switch (base.sporcuIsim)
            {
                case "CEDI OSMAN":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\basketbol\\cedi.png");
                    break;
                case "CURRY":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\basketbol\\cury.png");
                    break;
                case "DAVIS":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\basketbol\\davis.png");
                    break;
                case "HARDEN":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\basketbol\\harden.png");
                    break;
                case "JORDAN":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\basketbol\\jordan.png");
                    break;
                case "KOBE":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\basketbol\\kobe.png");
                    break;
                case "LEBRON":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\basketbol\\lebron.png");
                    break;
                case "SHAQ":
                    isim.Text = base.sporcuIsim;
                    kart.BackgroundImage = Image.FromFile("resimler\\basketbol\\shaq.png");
                    break;
                default:
                    break;
            }
            arkaPlan.BackgroundImage = Image.FromFile("resimler\\basketbol\\basketKartArka.png");
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
            kart.BringToFront();
            form.Controls.Add(kart);
            return kart;
        }

        public bool KartKullanildiMi()
        {
            return this.kartKullanildiMi;
        }

        public void SetKartKullanildiMi(bool kartKullanildiMi)
        {
            this.kartKullanildiMi = kartKullanildiMi;
        }

        public int GetIkilik()
        {
            return this.ikilik;
        }

        public void SetIkilik(int ikilik)
        {
            this.ikilik = ikilik;
        }

        public int GetUcluk()
        {
            return this.ucluk;
        }

        public void SetUcluk(int ucluk)
        {
            this.ucluk = ucluk;
        }

        public int GetSerbestAtis()
        {
            return this.serbestAtis;
        }

        public void SetSerbestAtis(int serbestAtis)
        {
            this.serbestAtis = serbestAtis;
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
            return "İkilik : " + this.ikilik + " - Üçlük : " + this.ucluk + " - Serbest atış : " + this.serbestAtis;
        }
    }
}
