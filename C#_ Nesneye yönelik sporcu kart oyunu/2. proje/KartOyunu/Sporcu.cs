using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KartOyunu
{
    public abstract class Sporcu
    {
        public string sporcuIsim, sporcuTakim;
        public Panel oyuncuKarti;
        
        public Sporcu()
        {
            sporcuIsim = "Sporcu";
            sporcuTakim = "TAKIM";
        }

        public Sporcu(string adi, string takim)
        {
            this.sporcuIsim = adi;
            this.sporcuTakim = takim;
        }

        public abstract string SporcuPuaniGoster();

        public abstract Panel KartOlustur(Point konum, Size boyut, bool acikMi, int deger1, int deger2, int deger3, oyunForm form);

    }
}
