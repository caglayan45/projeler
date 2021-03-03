using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KartOyunu
{
    class Kullanıcı : Oyuncu
    {
        public List<Futbolcu> Futbolcular = new List<Futbolcu>(4);
        public List<Basketbolcu> Basketbolcular = new List<Basketbolcu>(4);

        Kullanıcı() : base()
        {
            oyuncuID = -1;
            skor = -1;
            oyuncuAdi = "Kullanıcı";
        }

        public Kullanıcı(int oyuncuID, int skor, string oyuncuAdi) : base(oyuncuID, skor, oyuncuAdi)
        {
            base.oyuncuID = oyuncuID;
            base.skor = skor;
            base.oyuncuAdi = oyuncuAdi;
        }

        public override string SkorGoster()
        {
            return oyuncuAdi + " oyuncusunun skoru : " + this.skor;
        }

        private void DoubleClick(object sender, EventArgs e)
        {
            Test.kullaniciSecilenKartKonum = ((Panel)sender).Location;
            Test.kartDagitAnimasyon((Panel)sender, new System.Drawing.Point(980, 370), true);
            Test.kullaniciSecilenKart = ((Panel)sender);
            Test.kullaniciKartSectiMi = true;
            if (((Panel)sender).Name == "FUTBOL")
                Test.kartKontrol = true;
            else
                Test.kartKontrol = false;
            oyunForm.t.Start();
        }

        public void DesteyeFutbolcuEkle(Futbolcu Kart)
        {
            Kart.oyuncuKarti.MouseDoubleClick += DoubleClick;
            Futbolcular.Add(Kart);
            base.kartListesi.Add(Kart);
        }

        public void DesteyeBasketbolcuEkle(Basketbolcu Kart)
        {
            Kart.oyuncuKarti.MouseDoubleClick += DoubleClick;
            Basketbolcular.Add(Kart);
            base.kartListesi.Add(Kart);
        }

        public override void KartSec()
        {

        }
        public override void KartSec(bool kontrol)
        {

        }

        public int GetOyuncuID()
        {
            return base.oyuncuID;
        }

        public void SetOyuncuID(int oyuncuID)
        {
            base.oyuncuID = oyuncuID;
        }

        public int GetSkor()
        {
            return base.skor;
        }

        public void SetSkor(int skor)
        {
            base.skor = skor;
        }

        public string GetOyuncuAdi()
        {
            return base.oyuncuAdi;
        }

        public void SetOyuncuAdi(string oyuncuAdi)
        {
            base.oyuncuAdi = oyuncuAdi;
        }

    }
}
