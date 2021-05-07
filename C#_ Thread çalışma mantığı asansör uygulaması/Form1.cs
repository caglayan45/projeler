using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsansorUygulamasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Thread giris;
        Thread cikis;
        Thread asansor1;
        Thread asansor2;
        Thread asansor3;
        Thread asansor4;
        Thread asansor5;
        Thread kontrol;

        List<int> GirenKisiler = new List<int>();

        Katlar[] katlar;
        Asansor[] Asansorler;

        public class Katlar
        {
            public List<int> inecekler = new List<int>();
            public List<int> icindekiler = new List<int>();
        }

        public class Asansor
        {
            public enum YON
            {
                YUKARI,
                ASAGI
            }

            public bool calismaDurumu = false;
            public int hedefKat = 4, asansorNo, anlikKat = 0,kapasite = 10;
            public YON yonu;
            public List<int> icerdekiler = new List<int>();

            public Asansor(bool calismaDurumu, int asansorNo)
            {
                this.yonu = YON.YUKARI;
                this.calismaDurumu = calismaDurumu;
                this.asansorNo = asansorNo;
            }

            public void SetCalismaDurumu(bool calismaDurumu)
            {
                this.calismaDurumu = calismaDurumu;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            katlar = new Katlar[4];
            katlar[0] = new Katlar();
            katlar[1] = new Katlar();
            katlar[2] = new Katlar();
            katlar[3] = new Katlar();

            Asansorler = new Asansor[5];
            Asansorler[0] = new Asansor(true,1);
            Asansorler[1] = new Asansor(false,2);
            Asansorler[2] = new Asansor(false,3);
            Asansorler[3] = new Asansor(false,4);
            Asansorler[4] = new Asansor(false,5);

            Form1.CheckForIllegalCrossThreadCalls = false;
            giris = new Thread(new ThreadStart(girisMetot));
            cikis = new Thread(new ThreadStart(cikisMetot));
            asansor1 = new Thread(new ThreadStart(asansor1Metot));
            asansor2 = new Thread(new ThreadStart(asansor2Metot));
            asansor3 = new Thread(new ThreadStart(asansor3Metot));
            asansor4 = new Thread(new ThreadStart(asansor4Metot));
            asansor5 = new Thread(new ThreadStart(asansor5Metot));
            kontrol = new Thread(new ThreadStart(kontrolMetot));

            giris.Start();
            asansor1.Start();
            asansor2.Start();
            asansor3.Start();
            asansor4.Start();
            asansor5.Start();
            kontrol.Start();
            cikis.Start();
        }

        void KatLogYazdir()
        {
            int[] girisLog = new int[5];

            foreach (int kisi in GirenKisiler)
            {
                girisLog[kisi]++;
            }
            string bilgi = "";

            bilgi += ("Giriş katı : \t[" + girisLog[1] + ",1] , [" + girisLog[2] + ",2] , [" + girisLog[3] + ",3] , [" + girisLog[4] + ",4]\n\n");
            bilgi += ("Birinci kat \tİçeridekiler : " + katlar[0].icindekiler.Count + "  \tinecekler : " + katlar[0].inecekler.Count + "\n");
            bilgi += ("İkinci kat \t\tİçeridekiler : " + katlar[1].icindekiler.Count + "  \tinecekler : " + katlar[1].inecekler.Count + "\n");
            bilgi += ("Üçüncü kat \tİçeridekiler : " + katlar[2].icindekiler.Count + "  \tinecekler : " + katlar[2].inecekler.Count + "\n");
            bilgi += ("Dördüncü kat \tİçeridekiler : " + katlar[3].icindekiler.Count + "  \tinecekler : " + katlar[3].inecekler.Count + "\n\n");
            bilgi += ("Çalışan asansör sayısı : " + CalisanAsansor() + "\n");
            bilgi += ("Bekleyen sayısı : " + ToplamBekleyen() + "\n\n");

            bilgi += AsansorBilgiYazdir(Asansorler[0]);
            bilgi += AsansorBilgiYazdir(Asansorler[1]);
            bilgi += AsansorBilgiYazdir(Asansorler[2]);
            bilgi += AsansorBilgiYazdir(Asansorler[3]);
            bilgi += AsansorBilgiYazdir(Asansorler[4]);
            richTextBox1.Text = bilgi;
        }

        string AsansorBilgiYazdir(Asansor asansor)
        {
            string bilgi = "";
            bilgi += ("Aktiflik : " + asansor.calismaDurumu + "\n");
            bilgi += ("\t\tKat : " + asansor.anlikKat + "\n");
            bilgi += ("\t\tHedef Kat : " + asansor.hedefKat + "\n");
            bilgi += ("\t\tYönü : " + asansor.yonu + "\n");
            bilgi += ("\t\tKapasite : " + asansor.kapasite + "\n");
            bilgi += ("\t\tİçindekiler : " + asansor.icerdekiler.Count + "\n");
            int bir = 0, iki = 0, uc = 0, dort = 0, sifir = 0;
            for (int i = 0; i < asansor.icerdekiler.Count; i++)
            {
                switch (asansor.icerdekiler[i])
                {
                    case 0:
                        sifir++;
                        break;
                    case 1:
                        bir++;
                        break;
                    case 2:
                        iki++;
                        break;
                    case 3:
                        uc++;
                        break;
                    case 4:
                        dort++;
                        break;
                }
            }
            bilgi += ("\t\tİçindekiler : [(" + sifir + ", 0), (" + bir + ", 1), (" + iki + ", 2), (" + uc + ", 3), (" + dort + ", 4)]\n\n");
            return bilgi;
        }

        void SetCalismaDurumu(bool calismaDurumu, int asansorNo)
        {
            if (calismaDurumu)
            {
                switch (asansorNo)
                {
                    case 1:
                        Asansorler[0].SetCalismaDurumu(true);
                        break;
                    case 2:
                        Asansorler[1].SetCalismaDurumu(true);
                        break;
                    case 3:
                        Asansorler[2].SetCalismaDurumu(true);
                        break;
                    case 4:
                        Asansorler[3].SetCalismaDurumu(true);
                        break;
                    case 5:
                        Asansorler[4].SetCalismaDurumu(true);
                        break;
                }
            }
            else
            {
                switch (asansorNo)
                {
                    case 1:
                        Asansorler[0].SetCalismaDurumu(false);
                        break;
                    case 2:
                        Asansorler[1].SetCalismaDurumu(false);
                        break;
                    case 3:
                        Asansorler[2].SetCalismaDurumu(false);
                        break;
                    case 4:
                        Asansorler[3].SetCalismaDurumu(false);
                        break;
                    case 5:
                        Asansorler[4].SetCalismaDurumu(false);
                        break;
                }
            }
        }

        int ToplamBekleyen()
        {
            return GirenKisiler.Count + katlar[0].inecekler.Count + katlar[1].inecekler.Count + katlar[2].inecekler.Count + katlar[3].inecekler.Count;
        }

        int CalisanAsansor()
        {
            int sayac = 0;
            for (int i = 0; i < Asansorler.Length; i++)
            {
                if (Asansorler[i].calismaDurumu)
                    sayac++;
            }
            return sayac;
        }

        void IndiBindi(Asansor asansor)
        {
            int inenler = 0;
            int binenler = 0;
            lock (this)
            {
                if (asansor.anlikKat != 0)
                {
                    //indir
                    for (int i = 0; i < asansor.icerdekiler.Count; i++)
                    {
                        if (asansor.icerdekiler[i] == asansor.anlikKat)
                        {
                            inenler++;
                            katlar[asansor.anlikKat - 1].icindekiler.Add(asansor.icerdekiler[i]);
                            asansor.icerdekiler.Remove(asansor.icerdekiler[i]);
                        }
                    }

                    //bindir
                    if (asansor.calismaDurumu)
                    {
                        int kapasite = asansor.kapasite - asansor.icerdekiler.Count;
                        for (int i = 0; i < kapasite; i++)
                        {
                            if (katlar[asansor.anlikKat - 1].inecekler.Count > 0)
                            {
                                binenler++;
                                asansor.icerdekiler.Add(katlar[asansor.anlikKat - 1].inecekler[0]);
                                katlar[asansor.anlikKat - 1].inecekler.RemoveAt(0);
                            }
                        }
                    }
                }
                else//Giriş katıysa
                {
                    //indir
                    for (int i = 0; i < asansor.icerdekiler.Count; i++)
                    {
                        if (asansor.icerdekiler[i] == asansor.anlikKat)
                        {
                            inenler++;
                            asansor.icerdekiler.Remove(asansor.icerdekiler[i]);
                        }
                    }
                    //bindir
                    if (asansor.calismaDurumu)
                    {
                        int kapasite = asansor.kapasite - asansor.icerdekiler.Count;
                        for (int i = 0; i < kapasite; i++)
                        {
                            if (GirenKisiler.Count > 0)
                            {
                                binenler++;
                                asansor.icerdekiler.Add(GirenKisiler[0]);
                                GirenKisiler.RemoveAt(0);
                            }
                        }
                    }
                }
            }
            listBox2.Items.Add(asansor.asansorNo + ". asansör \t" + asansor.anlikKat + " katında  \t" + inenler + "  kişi indirdi.");
            listBox2.Items.Add(asansor.asansorNo + ". asansör \t" + asansor.anlikKat + " katından \t" + binenler + " kişi aldı.");
        }

        

        void girisMetot()
        {
            while (true)
            {
                Random rand = new Random();
                int kisiSayisi = rand.Next(1, 11);
                for (int i = 0; i < kisiSayisi; i++)
                {
                    GirenKisiler.Add(rand.Next(1, 5));
                }
                Thread.Sleep(500);
            }
        }

        int ToplamInsanlar()
        {
            return katlar[0].icindekiler.Count + katlar[1].icindekiler.Count + katlar[2].icindekiler.Count + katlar[3].icindekiler.Count;
        }

        void cikisMetot()
        {
            while (true)
            {
                Random rand = new Random();
                int inecekSayisi;
                do
                {
                    inecekSayisi = rand.Next(1, 6);
                } while (inecekSayisi > ToplamInsanlar());

                for (int i = 0; i < inecekSayisi; i++)
                {
                    int cikisKati = 0;
                    do
                        cikisKati = rand.Next(1, 5);
                    while (katlar[cikisKati-1].icindekiler.Count <= 0);
                    
                    katlar[cikisKati - 1].inecekler.Add(0);
                    katlar[cikisKati - 1].icindekiler.RemoveAt(0);
                }
                Thread.Sleep(1000);
            }
        }
        void asansor1Metot()
        {
            while (true)
            {
                IndiBindi(Asansorler[0]);
                if(ToplamBekleyen() > 0 || Asansorler[0].icerdekiler.Count > 0)
                {
                    if (Asansorler[0].anlikKat == 4)
                    {
                        Asansorler[0].hedefKat = 0;
                        Asansorler[0].yonu = Asansor.YON.ASAGI;
                    }
                    else if (Asansorler[0].anlikKat == 0)
                    {
                        Asansorler[0].hedefKat = 4;
                        Asansorler[0].yonu = Asansor.YON.YUKARI;
                    }
                    if (Asansorler[0].yonu == Asansor.YON.YUKARI)
                        Asansorler[0].anlikKat++;
                    else
                        Asansorler[0].anlikKat--;
                }
                Thread.Sleep(200);
            }
        }
        void asansor2Metot()
        {
            while (true)
            {
                if (Asansorler[1].calismaDurumu || Asansorler[1].icerdekiler.Count > 0)
                {
                    IndiBindi(Asansorler[1]);
                    if (Asansorler[1].anlikKat == 4)
                    {
                        Asansorler[1].hedefKat = 0;
                        Asansorler[1].yonu = Asansor.YON.ASAGI;
                    }
                    else if (Asansorler[1].anlikKat == 0)
                    {
                        Asansorler[1].hedefKat = 4;
                        Asansorler[1].yonu = Asansor.YON.YUKARI;
                    }
                    if (Asansorler[1].yonu == Asansor.YON.YUKARI)
                        Asansorler[1].anlikKat++;
                    else
                        Asansorler[1].anlikKat--;
                }
                Thread.Sleep(200);
            }
            
        }
        void asansor3Metot()
        {
            while (true)
            {
                if (Asansorler[2].calismaDurumu || Asansorler[2].icerdekiler.Count > 0)
                {
                    IndiBindi(Asansorler[2]);
                    if (Asansorler[2].anlikKat == 4)
                    {
                        Asansorler[2].hedefKat = 0;
                        Asansorler[2].yonu = Asansor.YON.ASAGI;
                    }
                    else if (Asansorler[2].anlikKat == 0)
                    {
                        Asansorler[2].hedefKat = 4;
                        Asansorler[2].yonu = Asansor.YON.YUKARI;
                    }
                    if (Asansorler[2].yonu == Asansor.YON.YUKARI)
                        Asansorler[2].anlikKat++;
                    else
                        Asansorler[2].anlikKat--;
                }
                Thread.Sleep(200);
            }
        }
        void asansor4Metot()
        {
            while (true)
            {
                if (Asansorler[3].calismaDurumu || Asansorler[3].icerdekiler.Count > 0)
                {
                    IndiBindi(Asansorler[3]);
                    if (Asansorler[3].anlikKat == 4)
                    {
                        Asansorler[3].hedefKat = 0;
                        Asansorler[3].yonu = Asansor.YON.ASAGI;
                    }
                    else if (Asansorler[3].anlikKat == 0)
                    {
                        Asansorler[3].hedefKat = 4;
                        Asansorler[3].yonu = Asansor.YON.YUKARI;
                    }
                    if (Asansorler[3].yonu == Asansor.YON.YUKARI)
                        Asansorler[3].anlikKat++;
                    else
                        Asansorler[3].anlikKat--;
                }
                Thread.Sleep(200);
            }
        }
        void asansor5Metot()
        {
            while (true)
            {
                if (Asansorler[4].calismaDurumu || Asansorler[4].icerdekiler.Count > 0)
                {
                    if (Asansorler[4].anlikKat == 4)
                    {
                        Asansorler[4].hedefKat = 0;
                        Asansorler[4].yonu = Asansor.YON.ASAGI;
                    }
                    else if (Asansorler[4].anlikKat == 0)
                    {
                        Asansorler[4].hedefKat = 4;
                        Asansorler[4].yonu = Asansor.YON.YUKARI;
                    }
                    if (Asansorler[4].yonu == Asansor.YON.YUKARI)
                        Asansorler[4].anlikKat++;
                    else
                        Asansorler[4].anlikKat--;
                }
                Thread.Sleep(200);
            }
        }

        void kontrolMetot()
        {
            while (true)
            {
                //if (ToplamBekleyen() > CalisanAsansor() * 20)
                if (ToplamBekleyen() > 20)
                {
                    switch (CalisanAsansor())
                    {
                        case 1:
                            SetCalismaDurumu(true, 2);
                            break;
                        case 2:
                            SetCalismaDurumu(true, 3);
                            break;
                        case 3:
                            SetCalismaDurumu(true, 4);
                            break;
                        case 4:
                            SetCalismaDurumu(true, 5);
                            break;
                    }
                }
                //else if(ToplamBekleyen() <= (CalisanAsansor() -1) * 20)
                else if (ToplamBekleyen() < 10)
                {
                    switch (CalisanAsansor())
                    {
                        case 2:
                            SetCalismaDurumu(false, 2);
                            break;
                        case 3:
                            SetCalismaDurumu(false, 3);
                            break;
                        case 4:
                            SetCalismaDurumu(false, 4);
                            break;
                        case 5:
                            SetCalismaDurumu(false, 5);
                            break;
                    }
                }
                KatLogYazdir();
                Thread.Sleep(200);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            giris.Abort();
            cikis.Abort();
            asansor1.Abort();
            asansor2.Abort();
            asansor3.Abort();
            asansor4.Abort();
            asansor5.Abort();
            kontrol.Abort();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void btnDurdur_Click(object sender, EventArgs e)
        {
            /*giris.Abort();
            cikis.Abort();
            asansor1.Abort();
            asansor2.Abort();
            asansor3.Abort();
            asansor4.Abort();
            asansor5.Abort();
            kontrol.Abort();
            KatLogYazdir();*/
            giris.Suspend();
            cikis.Suspend();
            asansor1.Suspend();
            asansor2.Suspend();
            asansor3.Suspend();
            asansor4.Suspend();
            asansor5.Suspend();
            kontrol.Suspend();
            KatLogYazdir();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cikis.Resume();
            asansor1.Resume();
            asansor2.Resume();
            asansor3.Resume();
            asansor4.Resume();
            asansor5.Resume();
            kontrol.Resume();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            giris.Resume();
        }
    }
}
