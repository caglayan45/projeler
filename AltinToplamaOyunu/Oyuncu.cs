using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltinToplamaOyunu
{
    public class Oyuncu
    {
        public int altinMiktari, hamleMaliyeti, hedefMaliyeti, hamleHakki;
        public char oyuncuTipi;
        public Konum konum;
        public PictureBox gorsel;
        public Altin hedefAltin;
        public static Oyuncu[] Oyuncular = new Oyuncu[4];
        Altin[] Altinlar;

        public Oyuncu(char oyuncuTipi, int altinMiktari, int hamleMaliyeti, int hedefMaliyeti, int hamleHakki, Konum konum, PictureBox gorsel, Altin[] Altinlar)
        {
            this.hedefAltin = null;
            this.Altinlar = Altinlar;
            this.hamleHakki = hamleHakki;
            this.hamleMaliyeti = hamleMaliyeti;
            this.hedefMaliyeti = hedefMaliyeti;
            this.oyuncuTipi = oyuncuTipi;
            this.altinMiktari = altinMiktari;
            this.konum = konum;
            this.gorsel = gorsel;
            
        }

        public static void OyunculariOlustur(Oyuncu[] oyuncular)
        {
            for (int i = 0; i < oyuncular.Length; i++)
            {
                Oyuncular[i] = oyuncular[i];
            }
            
        }
        public class Konum
        {
            public int x;
            public int y;
            public Konum(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public Altin HedefBelirle()
        {
            int[] altinUzakliklari = new int[Altinlar.Length];
            int enYakin = int.MaxValue, enYakinYer = 0, gizliAltinSayisi = 0;
            for(int j = 0; j < Altinlar.Length; j++)
            {
                if (!Altinlar[j].GetGorunurluk() && Altinlar[j].GetDeger() != int.MaxValue)
                    gizliAltinSayisi++;
                altinUzakliklari[j] = (Math.Abs(Altinlar[j].GetX() - konum.x) + (Math.Abs(Altinlar[j].GetY() - konum.y)));
                if (altinUzakliklari[j] < enYakin && Altinlar[j].GetGorunurluk()) {
                    enYakin = altinUzakliklari[j];
                    enYakinYer = j;
                }
            }

            if(oyuncuTipi == 'A')
            {
                Altinlar[enYakinYer].gorsel.Image = Image.FromFile("resimler//hedefMavi.png");
                Altinlar[enYakinYer].gorsel.BackgroundImage = Image.FromFile("resimler//altin.png");
                //Console.WriteLine("{0}'nın hedefi : - {1} , {2}", oyuncuTipi, Altinlar[enYakinYer].GetX(), Altinlar[enYakinYer].GetY());
                return Altinlar[enYakinYer];
            }else if (oyuncuTipi == 'B') {
                int enKarli = int.MinValue;
                int enKarliYer = 0;
                for (int i = 0; i < Altinlar.Length; i++)
                {

                    if (Altinlar[i].GetDeger() - (altinUzakliklari[i] * hamleMaliyeti + hedefMaliyeti) > enKarli && Altinlar[i].GetGorunurluk())
                    {
                        enKarli = Altinlar[i].GetDeger() - (altinUzakliklari[i] * hamleMaliyeti + hedefMaliyeti);
                        enKarliYer = i;
                    }

                }
                //Console.WriteLine("{0}'nin hedefi : - {1},{2} - {3}", oyuncuTipi, Altinlar[enKarliYer].GetX(), Altinlar[enKarliYer].GetY(), enKarli);
                Altinlar[enKarliYer].gorsel.Image = Image.FromFile("resimler//hedefYesil.png");
                Altinlar[enKarliYer].gorsel.BackgroundImage = Image.FromFile("resimler//altin.png");
                return Altinlar[enKarliYer];
            }else if (oyuncuTipi == 'C') {
                Altin[] gizliler = new Altin[gizliAltinSayisi];
                int[] uzakliklar = new int[gizliAltinSayisi];
                int k = 0;

                for (int i = 0; i < Altinlar.Length; i++)
                {
                    if (!Altinlar[i].GetGorunurluk() && Altinlar[i].GetDeger() != int.MaxValue)
                    {
                        gizliler[k] = Altinlar[i];
                        uzakliklar[k] = (Math.Abs(gizliler[k].GetX() - konum.x) + (Math.Abs(gizliler[k].GetY() - konum.y)));
                        k++;
                    }
                }

                for (int i = 0; i < gizliler.Length - 1; i++)
                {
                    for (int j = 0; j < gizliler.Length - i - 1; j++)
                    {
                        if (uzakliklar[i] > uzakliklar[j + 1])
                        {
                            int tempUzaklik = uzakliklar[i];
                            uzakliklar[i] = uzakliklar[j];
                            uzakliklar[j] = tempUzaklik;

                            Altin tempAltin = gizliler[i];
                            gizliler[i] = gizliler[j];
                            gizliler[j] = tempAltin;
                        }
                    }
                }
                int gizlenecekSayisi = 0;
                if (gizliler.Length >= 2)
                    gizlenecekSayisi = 2;
                else
                    gizlenecekSayisi = gizliler.Length;
                for (int i = 0; i < gizlenecekSayisi; i++)
                {
                    gizliler[i].SetGorunurluk(true);
                    altinToplamaOyunu.logEkrani.Items.Add("Oyuncu " + oyuncuTipi + " (" + gizliler[i].GetX() + " , " + gizliler[i].GetY() + ") konumundaki altını görünür yaptı.");
                    altinToplamaOyunu.logEkrani.Refresh();
                }
                int enKarli = int.MinValue;
                int enKarliYer = 0;
                for (int i = 0; i < Altinlar.Length; i++)
                {
                    if (Altinlar[i].GetDeger() - (altinUzakliklari[i] * hamleMaliyeti + hedefMaliyeti) > enKarli && Altinlar[i].GetGorunurluk())
                    {
                        enKarli = Altinlar[i].GetDeger() - (altinUzakliklari[i] * hamleMaliyeti + hedefMaliyeti);
                        enKarliYer = i;
                    }

                }
                //Console.WriteLine("{0}'nin hedefi :  - {1},{2} - {3}", oyuncuTipi, Altinlar[enKarliYer].GetX(), Altinlar[enKarliYer].GetY(), enKarli);
                Altinlar[enKarliYer].gorsel.Image = Image.FromFile("resimler//hedefKirmizi.png");
                Altinlar[enKarliYer].gorsel.BackgroundImage = Image.FromFile("resimler//altin.png");
                return Altinlar[enKarliYer];
            }else if (oyuncuTipi == 'D') {
                int enKarli = int.MinValue;
                int enKarliYer = -1;

                for (int i = 0; i < Altinlar.Length; i++)
                {
                    if (Altinlar[i] == Oyuncular[0].hedefAltin)
                    {
                        int secenOyuncununUzakligi = (Math.Abs(Altinlar[i].GetX() - Oyuncular[0].konum.x) + (Math.Abs(Altinlar[i].GetY() - Oyuncular[0].konum.y)));
                        int uzaklik = (Math.Abs(Altinlar[i].GetX() - konum.x) + (Math.Abs(Altinlar[i].GetY() - konum.y)));
                        if (uzaklik > hamleHakki)
                            if (uzaklik > secenOyuncununUzakligi)
                                continue;
                    }
                    else if (Altinlar[i] == Oyuncular[1].hedefAltin)
                    {
                        int secenOyuncununUzakligi = (Math.Abs(Altinlar[i].GetX() - Oyuncular[1].konum.x) + (Math.Abs(Altinlar[i].GetY() - Oyuncular[1].konum.y)));
                        int uzaklik = (Math.Abs(Altinlar[i].GetX() - konum.x) + (Math.Abs(Altinlar[i].GetY() - konum.y)));
                        if (uzaklik > hamleHakki)
                            if (uzaklik > secenOyuncununUzakligi)
                                continue;
                    }
                    else if (Altinlar[i] == Oyuncular[2].hedefAltin)
                    {
                        int secenOyuncununUzakligi = (Math.Abs(Altinlar[i].GetX() - Oyuncular[2].konum.x) + (Math.Abs(Altinlar[i].GetY() - Oyuncular[2].konum.y)));
                        int uzaklik = (Math.Abs(Altinlar[i].GetX() - konum.x) + (Math.Abs(Altinlar[i].GetY() - konum.y)));
                        if(uzaklik > hamleHakki)
                            if (uzaklik > secenOyuncununUzakligi)
                                continue;
                    }
                    if (Altinlar[i].GetDeger() - (altinUzakliklari[i] * hamleMaliyeti + hedefMaliyeti) > enKarli && Altinlar[i].GetGorunurluk())
                    {
                        enKarli = Altinlar[i].GetDeger() - (altinUzakliklari[i] * hamleMaliyeti + hedefMaliyeti);
                        enKarliYer = i;
                    }
                }
                //Console.WriteLine("{0}'nin hedefi : - {1},{2}", oyuncuTipi, Altinlar[enKarliYer].GetX(), Altinlar[enKarliYer].GetY());
                if (enKarliYer == -1)
                    return null;
                Altinlar[enKarliYer].gorsel.Image = Image.FromFile("resimler//hedefSari.png");
                Altinlar[enKarliYer].gorsel.BackgroundImage = Image.FromFile("resimler//altin.png");
                return Altinlar[enKarliYer];
            }

            return Altinlar[0];
        }

    }
}
