using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartOyunu
{
    class Bilgisayar : Oyuncu
    {
        public List<Futbolcu> Futbolcular = new List<Futbolcu>(4);
        public List<Basketbolcu> Basketbolcular = new List<Basketbolcu>(4);

        public Bilgisayar() : base()
        {
            oyuncuID = -1;
            skor = -1;
            oyuncuAdi = "Bilgisayar";
        }

        public Bilgisayar(int oyuncuID, int skor, string oyuncuAdi) : base(oyuncuID, skor, oyuncuAdi)
        {
            base.oyuncuID = oyuncuID;
            base.skor = skor;
            base.oyuncuAdi = oyuncuAdi;
        }

        public override string SkorGoster()
        {
            return oyuncuAdi + " oyuncusunun skoru : " + this.skor;
        }

        public void DesteyeFutbolcuEkle(Futbolcu Kart)//sporcu futbolcu ya da basketbolcu olabilir
        {
            Futbolcular.Add(Kart);
            base.kartListesi.Add(Kart);
        }

        public void DesteyeBasketbolcuEkle(Basketbolcu Kart)//sporcu futbolcu ya da basketbolcu olabilir
        {
            Basketbolcular.Add(Kart);
            base.kartListesi.Add(Kart);
        }

        public override void KartSec(bool kontrol)
        {
            Random rand = new Random();
            int sec = 0;
            if (kontrol)
            {
                do
                {
                    sec = rand.Next(Futbolcular.Count);

                } while (Futbolcular[sec].KartKullanildiMi());

                Test.bilgisayarSecilenKartKonum = Futbolcular[sec].oyuncuKarti.Location;
                Test.kartDagitAnimasyon(Futbolcular[sec].oyuncuKarti, new System.Drawing.Point(660, 370), true);
                Test.bilgisayarSecilenKart = Futbolcular[sec].oyuncuKarti;
            }
            else
            {
                do
                {
                    sec = rand.Next(Basketbolcular.Count);

                } while (Basketbolcular[sec].KartKullanildiMi());

                Test.bilgisayarSecilenKartKonum = Basketbolcular[sec].oyuncuKarti.Location;
                Test.kartDagitAnimasyon(Basketbolcular[sec].oyuncuKarti, new System.Drawing.Point(660, 370), true);
                Test.bilgisayarSecilenKart = Basketbolcular[sec].oyuncuKarti;
            }
        }

        public override void KartSec()
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
