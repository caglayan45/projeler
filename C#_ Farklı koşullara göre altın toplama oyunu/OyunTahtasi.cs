using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace AltinToplamaOyunu
{
    public class OyunTahtasi
    {
        public int AltinSayisi, gizliAltinSayisi;
        public static int SayacAltinSayisi;
        int[,] Tahta;
        int A_ToplamAdim = 0, B_ToplamAdim = 0, C_ToplamAdim = 0, D_ToplamAdim = 0;
        int A_HarcananAltin = 0, B_HarcananAltin = 0, C_HarcananAltin = 0, D_HarcananAltin = 0;
        int A_ToplananAltin = 0, B_ToplananAltin = 0, C_ToplananAltin = 0, D_ToplananAltin = 0;
        string A_Dosya = "istatistikler//A_oyuncu_istatistik.txt", B_Dosya = "istatistikler//B_oyuncu_istatistik.txt", C_Dosya = "istatistikler//C_oyuncu_istatistik.txt", D_Dosya = "istatistikler//D_oyuncu_istatistik.txt";
        FileStream fs_A, fs_B, fs_C, fs_D;
        StreamWriter sw_A, sw_B, sw_C, sw_D;

        int M, N;
        Altin[] Altinlar;
       
        public OyunTahtasi(int M, int N, Altin[] Altinlar, int AltinSayisi, int gizliAltinSayisi,Panel panelOyun)
        {
            this.M = M;//SÜTUN
            this.N = N;//SATIR
            this.Altinlar = Altinlar;
            this.AltinSayisi = AltinSayisi;
            SayacAltinSayisi = AltinSayisi;
            this.gizliAltinSayisi = gizliAltinSayisi;
            Tahta = new int[N,M];

            for(int i = 0; i < M; i++)//SÜTUN
            {
                for (int j = 0; j < N; j++)//SATIR
                {
                    Tahta[j, i] = -1;
                }
            }
            this.Altinlar = new Altin[this.AltinSayisi];
            Random randKonum = new Random();
            Random randAltin = new Random();

            for (int i = 0; i < this.AltinSayisi; i++)
            {
                int X = 0, Y = 0;
                do
                {
                    X = randKonum.Next(M);//SÜTUN
                    Y = randKonum.Next(N);//SATIR
                } while ((X == 0 && Y == 0) || (X == 0 && Y == N - 1) || (X == M - 1 && Y == 0) || (X == M - 1 && Y == N - 1) || KonumKontrol(X,Y));
                this.Altinlar[i] = new Altin(randAltin.Next(1, 5) * 5, X, Y, M, N, panelOyun);
                Tahta[Y, X] = 1;
            }
        }

        bool KonumKontrol(int X, int Y)
        {
            if (Tahta[Y, X] == -1)
                return false;
            return true;
        }

        public Altin[] GetAltinlar()
        {
            int[] sayilar = new int[this.gizliAltinSayisi];
            for (int i = 0; i < this.gizliAltinSayisi; i++)
                sayilar[i] = -1;
            int sayi = -2;
            Random rand = new Random();
            for (int i = 0; i < this.gizliAltinSayisi; i++)
            {
            git:
                sayi = rand.Next(AltinSayisi);
                for (int j = 0; j < this.gizliAltinSayisi; j++)
                {
                    if (sayi == sayilar[j])
                        goto git;
                }
                sayilar[i] = sayi;
                Altinlar[sayi].SetGorunurluk(false);
            }
            return this.Altinlar;
        }

        bool UzerindeMi(Oyuncu oyuncuKonumu, Altin hedefAltin)
        {
            return (oyuncuKonumu.konum.x == hedefAltin.GetX() && oyuncuKonumu.konum.y == hedefAltin.GetY()) ? true : false;
        }

        void GizliAltinVarmi(Oyuncu oyuncuKonumu)
        {
            for (int i = 0; i < Altinlar.Length; i++)
            {
                if (Altinlar[i].GetDeger() == int.MaxValue || Altinlar[i].GetGorunurluk())
                    continue;
                if (oyuncuKonumu.konum.x == Altinlar[i].GetX() && oyuncuKonumu.konum.y == Altinlar[i].GetY())
                    Altinlar[i].SetGorunurluk(true);
            }
        }

        void ArkaPlanToggle(Oyuncu oyuncu)
        {
            if (oyuncu.gorsel.BackColor == Color.White)
                oyuncu.gorsel.BackColor = Color.Black;
            else
                oyuncu.gorsel.BackColor = Color.White;
        }

        void AdimArttir(Oyuncu oyuncu)
        {
            switch (oyuncu.oyuncuTipi)
            {
                case 'A':
                    A_ToplamAdim++;
                    break;
                case 'B':
                    B_ToplamAdim++;
                    break;
                case 'C':
                    C_ToplamAdim++;
                    break;
                case 'D':
                    D_ToplamAdim++;
                    break;
            }
        }

        void HarcananAltinArttir(Oyuncu oyuncu, int AltinMiktari)
        {
            switch (oyuncu.oyuncuTipi)
            {
                case 'A':
                    A_HarcananAltin += AltinMiktari;
                    break;
                case 'B':
                    B_HarcananAltin += AltinMiktari;
                    break;
                case 'C':
                    C_HarcananAltin += AltinMiktari;
                    break;
                case 'D':
                    D_HarcananAltin += AltinMiktari;
                    break;
            }
        }

        void ToplananAltinArttir(Oyuncu oyuncu, int AltinMiktari)
        {
            switch (oyuncu.oyuncuTipi)
            {
                case 'A':
                    A_ToplananAltin += AltinMiktari;
                    break;
                case 'B':
                    B_ToplananAltin += AltinMiktari;
                    break;
                case 'C':
                    C_ToplananAltin += AltinMiktari;
                    break;
                case 'D':
                    D_ToplananAltin += AltinMiktari;
                    break;
            }
        }
        
        void IstatistikOlustur(Oyuncu oyuncu, string veri)
        {
            switch (oyuncu.oyuncuTipi)
            {
                case 'A':
                    sw_A.WriteLine(veri);
                    break;
                case 'B':
                    sw_B.WriteLine(veri);
                    break;
                case 'C':
                    sw_C.WriteLine(veri);
                    break;
                case 'D':
                    sw_D.WriteLine(veri);
                    break;
            }
        }

        void Yuru(Oyuncu oyuncu,Altin hedefAltin, Panel panelOyun)
        {
            for(int i = 0; i < oyuncu.hamleHakki; i++)
            {   
                if (oyuncu.altinMiktari < oyuncu.hamleMaliyeti)
                {
                    break;
                } 
                panelOyun.Refresh();
                if(hedefAltin.GetX() - oyuncu.konum.x < 0)
                {
                    HarcananAltinArttir(oyuncu, oyuncu.hamleMaliyeti);
                    AdimArttir(oyuncu);
                    oyuncu.konum.x -= 1;
                    ArkaPlanToggle(oyuncu);
                    oyuncu.altinMiktari -= oyuncu.hamleMaliyeti;
                    altinToplamaOyunu.logEkrani.Items.Add("Oyuncu " + oyuncu.oyuncuTipi + "(" + oyuncu.konum.x  + " , " + oyuncu.konum.y + ") konumuna gitti");
                    altinToplamaOyunu.logEkrani.Refresh();
                    IstatistikOlustur(oyuncu, "Oyuncu " + oyuncu.oyuncuTipi + "(" + oyuncu.konum.x + " , " + oyuncu.konum.y + ") konumuna gitti. [-" + oyuncu.hamleMaliyeti + "]");
                    altinToplamaOyunu.oyuncuLabellar[(int)oyuncu.oyuncuTipi - 65].Text = "Oyuncu : " + oyuncu.oyuncuTipi + " " + oyuncu.altinMiktari;
                    altinToplamaOyunu.oyuncuLabellar[(int)oyuncu.oyuncuTipi - 65].Refresh();
                    GizliAltinVarmi(oyuncu);
                    oyuncu.gorsel.Left -= oyuncu.gorsel.Width*2;
                    continue;
                }else if (hedefAltin.GetX() - oyuncu.konum.x > 0)
                {
                    HarcananAltinArttir(oyuncu, oyuncu.hamleMaliyeti);
                    AdimArttir(oyuncu);
                    oyuncu.konum.x += 1;
                    ArkaPlanToggle(oyuncu);
                    oyuncu.altinMiktari -= oyuncu.hamleMaliyeti;
                    altinToplamaOyunu.logEkrani.Items.Add("Oyuncu " + oyuncu.oyuncuTipi + "(" + oyuncu.konum.x + " , " + oyuncu.konum.y + ") konumuna gitti");
                    altinToplamaOyunu.logEkrani.Refresh();
                    IstatistikOlustur(oyuncu, "Oyuncu " + oyuncu.oyuncuTipi + "(" + oyuncu.konum.x + " , " + oyuncu.konum.y + ") konumuna gitti. [-" + oyuncu.hamleMaliyeti + "]");
                    altinToplamaOyunu.oyuncuLabellar[(int)oyuncu.oyuncuTipi - 65].Text = "Oyuncu : " + oyuncu.oyuncuTipi + " " + oyuncu.altinMiktari;
                    altinToplamaOyunu.oyuncuLabellar[(int)oyuncu.oyuncuTipi - 65].Refresh();
                    GizliAltinVarmi(oyuncu);
                    oyuncu.gorsel.Left += oyuncu.gorsel.Width * 2;
                    continue;
                }
                
                if(hedefAltin.GetY() - oyuncu.konum.y < 0)
                {
                    HarcananAltinArttir(oyuncu, oyuncu.hamleMaliyeti);
                    AdimArttir(oyuncu);
                    oyuncu.konum.y -= 1;
                    ArkaPlanToggle(oyuncu);
                    oyuncu.altinMiktari -= oyuncu.hamleMaliyeti;
                    altinToplamaOyunu.logEkrani.Items.Add("Oyuncu " + oyuncu.oyuncuTipi + "(" + oyuncu.konum.x + " , " + oyuncu.konum.y + ") konumuna gitti");
                    altinToplamaOyunu.logEkrani.Refresh();
                    IstatistikOlustur(oyuncu, "Oyuncu " + oyuncu.oyuncuTipi + "(" + oyuncu.konum.x + " , " + oyuncu.konum.y + ") konumuna gitti. [-" + oyuncu.hamleMaliyeti + "]");
                    altinToplamaOyunu.oyuncuLabellar[(int)oyuncu.oyuncuTipi - 65].Text = "Oyuncu : " + oyuncu.oyuncuTipi + " " + oyuncu.altinMiktari;
                    altinToplamaOyunu.oyuncuLabellar[(int)oyuncu.oyuncuTipi - 65].Refresh();
                    GizliAltinVarmi(oyuncu);
                    oyuncu.gorsel.Top -= oyuncu.gorsel.Height * 2;
                    continue;
                }else if (hedefAltin.GetY() - oyuncu.konum.y > 0)
                {
                    HarcananAltinArttir(oyuncu, oyuncu.hamleMaliyeti);
                    AdimArttir(oyuncu);
                    oyuncu.konum.y += 1;
                    ArkaPlanToggle(oyuncu);
                    oyuncu.altinMiktari -= oyuncu.hamleMaliyeti;
                    altinToplamaOyunu.logEkrani.Items.Add("Oyuncu " + oyuncu.oyuncuTipi + "(" + oyuncu.konum.x + " , " + oyuncu.konum.y + ") konumuna gitti");
                    altinToplamaOyunu.logEkrani.Refresh();
                    IstatistikOlustur(oyuncu, "Oyuncu " + oyuncu.oyuncuTipi + "(" + oyuncu.konum.x + " , " + oyuncu.konum.y + ") konumuna gitti. [-" + oyuncu.hamleMaliyeti + "]");
                    altinToplamaOyunu.oyuncuLabellar[(int)oyuncu.oyuncuTipi - 65].Text = "Oyuncu : " + oyuncu.oyuncuTipi + " " + oyuncu.altinMiktari;
                    altinToplamaOyunu.oyuncuLabellar[(int)oyuncu.oyuncuTipi - 65].Refresh();
                    GizliAltinVarmi(oyuncu);
                    oyuncu.gorsel.Top += oyuncu.gorsel.Height * 2;
                    continue;
                }
            }
        }

        public void Oyun(Panel panelOyun)
        {
            fs_A = new FileStream(A_Dosya, FileMode.Truncate, FileAccess.Write);
            fs_B = new FileStream(B_Dosya, FileMode.Truncate, FileAccess.Write);
            fs_C = new FileStream(C_Dosya, FileMode.Truncate, FileAccess.Write);
            fs_D = new FileStream(D_Dosya, FileMode.Truncate, FileAccess.Write);

            sw_A = new StreamWriter(fs_A);
            sw_B = new StreamWriter(fs_B);
            sw_C = new StreamWriter(fs_C);
            sw_D = new StreamWriter(fs_D);

            int i = 0;
            Oyuncu[] Oyuncular = Oyuncu.Oyuncular;
            IstatistikOlustur(Oyuncular[0], "Oyuncu " + Oyuncular[0].oyuncuTipi + "'nın altın miktarı : " + Oyuncular[0].altinMiktari + "\n*******************************");
            IstatistikOlustur(Oyuncular[1], "Oyuncu " + Oyuncular[1].oyuncuTipi + "'nin altın miktarı : " + Oyuncular[1].altinMiktari + "\n*******************************");
            IstatistikOlustur(Oyuncular[2], "Oyuncu " + Oyuncular[2].oyuncuTipi + "'nin altın miktarı : " + Oyuncular[2].altinMiktari + "\n*******************************");
            IstatistikOlustur(Oyuncular[3], "Oyuncu " + Oyuncular[3].oyuncuTipi + "'nin altın miktarı : " + Oyuncular[3].altinMiktari + "\n*******************************");
            while ((Oyuncular[0].altinMiktari > 0 || Oyuncular[1].altinMiktari > 0 || Oyuncular[2].altinMiktari > 0 || Oyuncular[3].altinMiktari > 0) && SayacAltinSayisi > 0)
            {
                if (Oyuncular[i % 4].altinMiktari <= 0)
                {
                    altinToplamaOyunu.logEkrani.Refresh();
                    altinToplamaOyunu.oyuncuLabellar[i % 4].Refresh();
                    i++;
                    continue;
                }
                //MessageBox.Show("Sıradaki Oyuncu : " + Oyuncular[i % 4].oyuncuTipi + " oyuncusu, devam edilsin mi ?");
                if (Oyuncular[i%4].hedefAltin == null || Oyuncular[i % 4].hedefAltin.GetDeger() < 5 || Oyuncular[i % 4].hedefAltin.GetDeger() > 20)
                {
                    Oyuncular[i % 4].hedefAltin = Oyuncular[i % 4].HedefBelirle();
                    if(Oyuncular[i % 4].hedefAltin == null)//D oyuncusu için ufak bir şart
                    {
                        i++;
                        continue;
                    }
                    HarcananAltinArttir(Oyuncular[i % 4], Oyuncular[i % 4].hedefMaliyeti);
                    panelOyun.Refresh();
                    altinToplamaOyunu.logEkrani.Items.Add("Oyuncu " +(char)((i % 4)+65) +" hedef belirledi: ("+ Oyuncular[i % 4].hedefAltin.GetX() + " , "+ Oyuncular[i % 4].hedefAltin.GetY() + ")");
                    altinToplamaOyunu.logEkrani.Refresh();
                    IstatistikOlustur(Oyuncular[i % 4], "Oyuncu " + (char)((i % 4) + 65) + " hedef belirledi: (" + Oyuncular[i % 4].hedefAltin.GetX() + " , " + Oyuncular[i % 4].hedefAltin.GetY() + "), [-" + Oyuncular[i % 4].hedefMaliyeti + "]");
                    Oyuncular[i % 4].altinMiktari -= Oyuncular[i % 4].hedefMaliyeti;
                    altinToplamaOyunu.oyuncuLabellar[i % 4].Text = "Oyuncu : " + (char)((i % 4) + 65) + " "+Oyuncular[i % 4].altinMiktari;
                    altinToplamaOyunu.oyuncuLabellar[i % 4].Refresh();
                    Yuru(Oyuncular[i % 4], Oyuncular[i % 4].hedefAltin, panelOyun);
                    panelOyun.Refresh();
                    if (UzerindeMi(Oyuncular[i % 4], Oyuncular[i % 4].hedefAltin))
                    {
                        Oyuncular[i % 4].altinMiktari += Oyuncular[i % 4].hedefAltin.GetDeger();
                        ToplananAltinArttir(Oyuncular[i % 4], Oyuncular[i % 4].hedefAltin.GetDeger());
                        IstatistikOlustur(Oyuncular[i % 4], "Oyuncu " + Oyuncular[i % 4].oyuncuTipi + "(" + Oyuncular[i % 4].hedefAltin.GetX() + " , " + Oyuncular[i % 4].hedefAltin.GetY() + ") konumundaki altını aldı. [+" + Oyuncular[i % 4].hedefAltin.GetDeger() + "]");
                        IstatistikOlustur(Oyuncular[i % 4], "Oyuncu " + Oyuncular[i % 4].oyuncuTipi + " altın miktarı : " + Oyuncular[i % 4].altinMiktari + "\n--------------------------------------");
                        Oyuncular[i % 4].hedefAltin.SetDeger(int.MaxValue);
                        Oyuncular[i % 4].hedefAltin.SetGorunurluk(false);
                        altinToplamaOyunu.oyuncuLabellar[i % 4].Text = "Oyuncu : " + (char)((i % 4) + 65) + " " + Oyuncular[i % 4].altinMiktari;
                        altinToplamaOyunu.oyuncuLabellar[i % 4].Refresh();
                        altinToplamaOyunu.logEkrani.Items.Add("Oyuncu " + Oyuncular[i%4].oyuncuTipi + "(" + Oyuncular[i % 4].hedefAltin.GetX() + " , " + Oyuncular[i % 4].hedefAltin.GetY() + ") konumundaki altını aldı.");
                        altinToplamaOyunu.logEkrani.Refresh();
                        for (int j = 0; j < 4; j++)
                        {
                            if( i % 4 != j) 
                                if (Oyuncular[j].hedefAltin == Oyuncular[i % 4].hedefAltin)
                                    Oyuncular[j].hedefAltin = null;
                        }
                        Oyuncular[i % 4].hedefAltin = null;
                        SayacAltinSayisi--;
                        panelOyun.Refresh();
                    }
                }
                else
                {
                    Yuru(Oyuncular[i % 4], Oyuncular[i % 4].hedefAltin, panelOyun);
                    if (UzerindeMi(Oyuncular[i % 4], Oyuncular[i % 4].hedefAltin))
                    {
                        Oyuncular[i % 4].altinMiktari += Oyuncular[i % 4].hedefAltin.GetDeger();
                        ToplananAltinArttir(Oyuncular[i % 4], Oyuncular[i % 4].hedefAltin.GetDeger());
                        IstatistikOlustur(Oyuncular[i % 4], "Oyuncu " + Oyuncular[i % 4].oyuncuTipi + "(" + Oyuncular[i % 4].hedefAltin.GetX() + " , " + Oyuncular[i % 4].hedefAltin.GetY() + ") konumundaki altını aldı. [+" + Oyuncular[i % 4].hedefAltin.GetDeger() + "]");
                        IstatistikOlustur(Oyuncular[i % 4], "Oyuncu " + Oyuncular[i % 4].oyuncuTipi + " altın miktarı : " + Oyuncular[i % 4].altinMiktari + "\n--------------------------------------");
                        Oyuncular[i % 4].hedefAltin.SetDeger(int.MaxValue);
                        Oyuncular[i % 4].hedefAltin.SetGorunurluk(false);
                        altinToplamaOyunu.oyuncuLabellar[i % 4].Text = "Oyuncu : " + (char)((i % 4) + 65) + " " + Oyuncular[i % 4].altinMiktari;
                        altinToplamaOyunu.oyuncuLabellar[i % 4].Refresh();
                        altinToplamaOyunu.logEkrani.Items.Add("Oyuncu " + Oyuncular[i % 4].oyuncuTipi + "(" + Oyuncular[i % 4].hedefAltin.GetX() + " , " + Oyuncular[i % 4].hedefAltin.GetY() + ") konumundaki altını aldı.");
                        altinToplamaOyunu.logEkrani.Refresh();
                        for (int j = 0; j < 4; j++)
                        {
                            if (Oyuncular[j].hedefAltin == Oyuncular[i % 4].hedefAltin)
                                Oyuncular[j].hedefAltin = null;
                        }
                        Oyuncular[i % 4].hedefAltin = null;
                        SayacAltinSayisi--;
                        panelOyun.Refresh();
                    }
                }  
                i++;
                panelOyun.Refresh();
                altinToplamaOyunu.logEkrani.Items.Add("----------------------------------");
                altinToplamaOyunu.logEkrani.Refresh();
            }
            IstatistikOlustur(Oyuncular[0], "----------------------------------");
            IstatistikOlustur(Oyuncular[0], "Altın Miktarı : " + Oyuncular[0].altinMiktari);
            IstatistikOlustur(Oyuncular[0], "Harcanan Altın : " + A_HarcananAltin);
            IstatistikOlustur(Oyuncular[0], "Atılan Adım : " + A_ToplamAdim);
            IstatistikOlustur(Oyuncular[0], "Toplanan Altın : " + A_ToplananAltin);

            IstatistikOlustur(Oyuncular[1], "----------------------------------");
            IstatistikOlustur(Oyuncular[1], "Altın Miktarı : " + Oyuncular[1].altinMiktari);
            IstatistikOlustur(Oyuncular[1], "Harcanan Altın : " + B_HarcananAltin);
            IstatistikOlustur(Oyuncular[1], "Atılan Adım : " + B_ToplamAdim);
            IstatistikOlustur(Oyuncular[1], "Toplanan Altın : " + B_ToplananAltin);

            IstatistikOlustur(Oyuncular[2], "----------------------------------");
            IstatistikOlustur(Oyuncular[2], "Altın Miktarı : " + Oyuncular[2].altinMiktari);
            IstatistikOlustur(Oyuncular[2], "Harcanan Altın : " + C_HarcananAltin);
            IstatistikOlustur(Oyuncular[2], "Atılan Adım : " + C_ToplamAdim);
            IstatistikOlustur(Oyuncular[2], "Toplanan Altın : " + C_ToplananAltin);

            IstatistikOlustur(Oyuncular[3], "----------------------------------");
            IstatistikOlustur(Oyuncular[3], "Altın Miktarı : " + Oyuncular[3].altinMiktari);
            IstatistikOlustur(Oyuncular[3], "Harcanan Altın : " + D_HarcananAltin);
            IstatistikOlustur(Oyuncular[3], "Atılan Adım : " + D_ToplamAdim);
            IstatistikOlustur(Oyuncular[3], "Toplanan Altın : " + D_ToplananAltin);

            altinToplamaOyunu.logEkrani.Items.Add("A Oyunucusu ; ");
            altinToplamaOyunu.logEkrani.Items.Add("\tHarcanan Altın : " + A_HarcananAltin);
            altinToplamaOyunu.logEkrani.Items.Add("\tAtılan Adım : " + A_ToplamAdim);
            altinToplamaOyunu.logEkrani.Items.Add("\tToplanan Altın : " + A_ToplananAltin);
            altinToplamaOyunu.logEkrani.Items.Add("----------------------------------");
            altinToplamaOyunu.logEkrani.Items.Add("B Oyunucusu ; ");
            altinToplamaOyunu.logEkrani.Items.Add("\tHarcanan Altın : " + B_HarcananAltin);
            altinToplamaOyunu.logEkrani.Items.Add("\tAtılan Adım : " + B_ToplamAdim);
            altinToplamaOyunu.logEkrani.Items.Add("\tToplanan Altın : " + B_ToplananAltin);
            altinToplamaOyunu.logEkrani.Items.Add("----------------------------------");
            altinToplamaOyunu.logEkrani.Items.Add("C Oyunucusu ; ");
            altinToplamaOyunu.logEkrani.Items.Add("\tHarcanan Altın : " + C_HarcananAltin);
            altinToplamaOyunu.logEkrani.Items.Add("\tAtılan Adım : " + C_ToplamAdim);
            altinToplamaOyunu.logEkrani.Items.Add("\tToplanan Altın : " + C_ToplananAltin);
            altinToplamaOyunu.logEkrani.Items.Add("----------------------------------");
            altinToplamaOyunu.logEkrani.Items.Add("D Oyunucusu ; ");
            altinToplamaOyunu.logEkrani.Items.Add("\tHarcanan Altın : " + D_HarcananAltin);
            altinToplamaOyunu.logEkrani.Items.Add("\tAtılan Adım : " + D_ToplamAdim);
            altinToplamaOyunu.logEkrani.Items.Add("\tToplanan Altın : " + D_ToplananAltin);
            altinToplamaOyunu.logEkrani.Refresh();
            int yer = 0, yer1 = -1;
            int altin = Oyuncular[yer].altinMiktari;
            for (int k = 1; k < 4; k++)
            {
                if(Oyuncular[k].altinMiktari > altin)
                {
                    yer = k;
                    altin = Oyuncular[k].altinMiktari;
                }else if(Oyuncular[k].altinMiktari == altin)
                {
                    yer1 = k;
                }
            }
            if (yer1 == -1)
                MessageBox.Show("Kazanan Oyuncu : " + Oyuncular[yer].oyuncuTipi + " oyuncusu");
            else
                MessageBox.Show(Oyuncular[yer].oyuncuTipi + " oyuncusu ile " + Oyuncular[yer1].oyuncuTipi + " oyuncusu berabere kaldı.");

            sw_A.Close();
            sw_B.Close();
            sw_C.Close();
            sw_D.Close();

            fs_A.Close();
            fs_B.Close();
            fs_C.Close();
            fs_D.Close();
        }

    }
}
