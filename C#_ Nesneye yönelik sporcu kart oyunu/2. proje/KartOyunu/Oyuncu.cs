using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KartOyunu
{
    public abstract class Oyuncu
    {
        public int oyuncuID, skor;
        public string oyuncuAdi;
        public List<Sporcu> kartListesi = new List<Sporcu>(16);

        public Oyuncu()
        {
            
        }

        public Oyuncu(int oyuncuID, int skor, string oyuncuAdi)
        {
            this.oyuncuAdi = oyuncuAdi;
            this.oyuncuID = oyuncuID;
            this.skor = skor;
        }

        public abstract string SkorGoster();
        public abstract void KartSec(bool kontrol);
        public abstract void KartSec();


    }
}
