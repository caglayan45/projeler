using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace YilanOyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //kontroller
        bool oyunBasladiMi = false, oyunModu = false, yemVarMi = false, kontrol = false, yenidenBaslamaKontrol = false;
        string kisi = "",log = "";//txtye kaydedilecek bilgiler
        PictureBox yem;
        PictureBox[] yilanParcalari;
        Yilan yilan;
        Random rand = new Random();
        int dk = 0, sn = 0, yemSaniyesi = 0;//süre, dakika saniye
        double puan = 0.00;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (oyunBasladiMi)
            {
                switch (e.KeyCode)//oyun başladı ise basılan yön tuşuna göre yönü güncelleme
                {
                    case Keys.Up:
                        if (yilan.yonu != Yilan.YON.asagi)
                            yilan.hareketler.Add(Yilan.YON.yukari);
                        break;
                    case Keys.Down:
                        if (yilan.yonu != Yilan.YON.yukari)
                            yilan.hareketler.Add(Yilan.YON.asagi);
                        break;
                    case Keys.Right:
                        if (yilan.yonu != Yilan.YON.sol)
                            yilan.hareketler.Add(Yilan.YON.sag);
                        break;
                    case Keys.Left:
                        if (yilan.yonu != Yilan.YON.sag)
                            yilan.hareketler.Add(Yilan.YON.sol);
                        break;
                }
            }
        }

        private void radioButtonKolay_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonKolay.Checked)//oyun modunu değiştir
                oyunModu = false;//kolay
            else
                oyunModu = true;//zor
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panelOyun.BorderStyle = BorderStyle.FixedSingle;//oyun paneline çerçeve ekleme
        }

        PictureBox ParcaEkle()//yem yediğinde yılana parca ekleme
        {
            PictureBox parca = new PictureBox();
            parca.Size = new Size(10, 10);
            parca.BackColor = Color.Green;
            parca.Location = yilan.GetPos(yilanParcalari.Length - 1);
            panelOyun.Controls.Add(parca);
            return parca;
        }

        void YilanGuncelle()//yilanin hareketine göre pozisyonunu güncelleme
        {
            for (int i = 0; i < yilanParcalari.Length; i++)
            {
                yilanParcalari[i].Location = yilan.GetPos(i);
            }
        }

        bool UzerindeMi(Point konum)//yılan üzerinde yem üretmeme kontrolü
        {
            for (int i = 0; i < yilanParcalari.Length; i++)
            {
                if (konum == yilanParcalari[i].Location)
                    return true;
            }
            return false;
        }

        void LogKaydet()//bilgileri dosyaya kaydetme
        {
            if(dk < 1)
            {
                if(sn < 10)
                    log = kisi + "\t\t\t00:0" + sn + "\t\tSkor : " + Math.Round(puan, 2);
                else
                    log = kisi + "\t\t\t00:" + sn + " \t\tSkor : " + Math.Round(puan, 2);
            }else if (dk < 10)
            {
                if (sn < 10)
                    log = kisi + "\t\t\t0" + dk + ":0" + sn + "\t\tSkor : " + Math.Round(puan, 2);
                else
                    log = kisi + "\t\t\t0" + dk + ":" + sn + "\t\tSkor : " + Math.Round(puan, 2);
            }
            else
            {
                if (sn < 10)
                    log = kisi + "\t\t\t" + dk + ":0" + sn + "\t\tSkor : " + Math.Round(puan, 2);
                else
                    log = kisi + "\t\t\t" + dk + ":" + sn + "\t\tSkor : " + Math.Round(puan, 2);
            }
            if (oyunModu)
                log += "\t\t\tOyun Modu : Zor";
            else
                log += "\t\t\tOyun Modu : Kolay";

            string yol = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\\YilanLog";
            Directory.CreateDirectory(yol);//AppData/Roaming'de klasör oluşturma.
            FileStream fs = new FileStream(yol + "\\log.txt", FileMode.Append, FileAccess.Write);//oluşturulan klasörün içine txt yi yoksa oluşturma
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(log);
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        void YemOlustur()//yem yendiyse yem oluşturma
        {
            if (!yemVarMi)
            {
                if(!kontrol)//oyun başladığında yemi panele ekleme
                {
                    panelOyun.Controls.Add(yem);
                    kontrol = true;
                }
                Point konum = new Point(0,0);
                do
                {
                    konum.X = rand.Next(panelOyun.Width / 10) * 10;
                    konum.Y = rand.Next(panelOyun.Height / 10) * 10;

                } while (UzerindeMi(konum));//yılanın üzerinde yem olmaması kontrolü
                yem.Location = konum;
                yemVarMi = true;
            }
        }

        void YemiYediMi()//yem yenip yenmeme kontrolü
        {
            if (yilan.GetPos(0) == yem.Location)//yendiyse puanı verme yilana parça ekleme
            {
                if (yemSaniyesi < 1)
                    puan += 100;
                else if(yemSaniyesi < 100)
                    puan += 100.0 / yemSaniyesi;
                if (yem.Location == new Point(0, 0) || yem.Location == new Point(470, 0) || yem.Location == new Point(0, 670) || yem.Location == new Point(470, 670))
                    puan += 10.0;
                lblPuan.Text = "Puan : " + Math.Round(puan,2);
                yemSaniyesi = 0;
                yilan.Buyu();
                Array.Resize(ref yilanParcalari, yilanParcalari.Length + 1);
                yilanParcalari[yilanParcalari.Length - 1] = ParcaEkle();
                yemVarMi = false;
            }
        }

        private void YeniOyun()//yeni oyun başladığında ilk değerler
        {
            timerYemSuresi.Start();
            timerGecenSure.Start();
            yemSaniyesi = sn = dk = 0;
            panelOyun.Controls.Clear();
            kontrol = yemVarMi = false;
            txtKisi.ReadOnly = oyunBasladiMi = true;
            yilan = new Yilan();
            yilan.yonu = Yilan.YON.sol;
            yilanParcalari = new PictureBox[3];
            yem = new PictureBox();
            yem.Size = new Size(10, 10);
            yem.BackColor = Color.DarkRed;
            yilanParcalari[0] = ParcaEkle();
            yilanParcalari[0].BackColor = Color.Blue;
            yilanParcalari[1] = ParcaEkle();
            yilanParcalari[2] = ParcaEkle();
            timer1.Start();
            timerOyunZorlugu.Start();
            this.Focus();
            for (int i = 0; i < 7; i++)
            {//oyun başladığı anda key focusunu forma almak
                SendKeys.Send("{Left}");
            }
        }

        private void btnSkorGoruntule_Click(object sender, EventArgs e)
        {
            string yol = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\\YilanLog";
            Directory.CreateDirectory(yol);//AppData/Roaming'de klasör oluşturma.
            FileStream fs = new FileStream(yol + "\\log.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            if (fs != null)
            {
                int cevap = Convert.ToInt32(MessageBox.Show("Dosyayı açmak istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning));
                if(cevap == 6)
                {
                    System.Diagnostics.Process.Start(yol + "\\log.txt");
                }
            }
            else
                MessageBox.Show("Henüz kayıt yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            sw.Close();
            fs.Close();
        }

        private void timerOyunZorlugu_Tick(object sender, EventArgs e)//oyun zorluğuna göre yılanın hızı
        {
            yilan.Ilerle();
            YilanGuncelle();
        }

        void Yenilgi()//dışarı çıkma ya da kendini yeme kontrolü
        {
            if(yilan.GetPos(0).X < 0 || yilan.GetPos(0).X > panelOyun.Width - 10 || yilan.GetPos(0).Y < 0 || yilan.GetPos(0).Y > panelOyun.Height - 10)
            {//dışarı çıkarsa
                timer1.Stop();
                timerOyunZorlugu.Stop();
                timerGecenSure.Stop();
                timerYemSuresi.Stop();
                LogKaydet();
                puan = sn = dk = 0;
                yemVarMi = false;
                int cevap = Convert.ToInt32(MessageBox.Show("Yenildiniz, yeniden oynamak ister misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning));
                oyunBasladiMi = false;
                if (cevap == 6)
                {
                    lblPuan.Text = "Puan : 0";
                    lblGecenSure.Text = "Geçen süre : 00:00";
                    YeniOyun();
                }
                else
                {
                    kisi = "";
                    lblPuan.Text = "Puan : 0";
                    lblGecenSure.Text = "Geçen süre : 00:00";
                    panelBilgiGenel.Visible = true;
                    txtKisi.ReadOnly = false;
                } 
                return;
            }

            for (int i = 1; i < yilan.buyukluk; i++)//kendini yerse
            {
                if (yilan.GetPos(0) == yilan.GetPos(i))
                {
                    timer1.Stop();
                    timerOyunZorlugu.Stop();
                    timerGecenSure.Stop();
                    timerYemSuresi.Stop();
                    LogKaydet();
                    puan = sn = dk = 0;
                    yemVarMi = false;
                    int cevap = Convert.ToInt32(MessageBox.Show("Yenildiniz, yeniden oynamak ister misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning));
                    oyunBasladiMi = false;
                    if (cevap == 6)
                    {
                        lblPuan.Text = "Puan : 0";
                        lblGecenSure.Text = "Geçen süre : 00:00";
                        YeniOyun();
                    }
                    else
                    {
                        kisi = "";
                        lblPuan.Text = "Puan : 0";
                        lblGecenSure.Text = "Geçen süre : 00:00";
                        panelBilgiGenel.Visible = true;
                        txtKisi.ReadOnly = false;
                    }
                    return;
                }
            }
        }

        private void timerYemSuresi_Tick(object sender, EventArgs e)//puan için yem süresi
        {
            yemSaniyesi++;
        }

        private void timer1_Tick(object sender, EventArgs e)//yem yeme, oluşturma ve yengili kontrolü
        {
            YemiYediMi();
            YemOlustur();
            Yenilgi();
        }

        private void timerGecenSure_Tick(object sender, EventArgs e)//süreyi arttırma ve label a yazma
        {
            sn++;
            if(sn == 60)
            {
                sn = 0;
                dk++;
            }
            if (dk < 10)
            {
                if (sn < 10)
                    lblGecenSure.Text = "Geçen Süre : 0" + dk.ToString() + ":0" + sn.ToString();
                else
                    lblGecenSure.Text = "Geçen Süre : 0" + dk.ToString() + ":" + sn.ToString();
            }
            else
            {
                if (sn < 10)
                    lblGecenSure.Text = "Geçen Süre : " + dk.ToString() + ":0" + sn.ToString();
                else
                    lblGecenSure.Text = "Geçen Süre : " + dk.ToString() + ":" + sn.ToString();
            }
            
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!oyunBasladiMi && kisi != "")//Oyun başlama kontrolü
            {
                if (e.KeyChar == 'B' || e.KeyChar == 'b')//B tuşu kontrolü
                {
                    if (oyunModu)
                        timerOyunZorlugu.Interval = 50;//zor
                    else
                        timerOyunZorlugu.Interval = 100;//kolay
                    lblPuan.Text = "Puan : 0";
                    YeniOyun();
                    panelBilgiGenel.Visible = false;
                }
            }
            else if(oyunBasladiMi && kisi != "")
            {
                if ((e.KeyChar == 'D' || e.KeyChar == 'd') && !yenidenBaslamaKontrol)
                {
                    timer1.Stop();
                    timerOyunZorlugu.Stop();
                    timerGecenSure.Stop();
                    timerYemSuresi.Stop();
                    MessageBox.Show("Oyun durduruldu.");
                    yenidenBaslamaKontrol = true;
                    return;
                }else if ((e.KeyChar == 'D' || e.KeyChar == 'd') && yenidenBaslamaKontrol)
                {
                    timer1.Start();
                    timerOyunZorlugu.Start();
                    timerGecenSure.Start();
                    timerYemSuresi.Start();
                    yenidenBaslamaKontrol = false;
                    return;
                }
            }
        }

        private void btnYardim_Click(object sender, EventArgs e)//oyun bilgilendirme mesajı
        {
            MessageBox.Show("-Osman Yusuf Bodur tarafından yazılmıştır.\n\n-İlk önce oyunu açtığınızda kişi ismini yazıp kişiyi kaydet butonuna basmanız gerekiyor. Ardından B/b tuşuna basarak oyunu başlatabilirsiniz.\n\n-Oyunu yön tuşları ile yılana yön vererek en kısa sürede yemi yiyip puan alarak oynayabilirsiniz. Oyun sırasında D/d tuşuna basarak oyunu duraklatabilir, tekrar D/d tuşuna basarak oyuna kaldığınız yerden devam edebilirsiniz.\n\n-Oyun alanının etrafındaki çerçeveden dışarı çıkmaya çalışmak ve kendinizi yemek kaybetme sebebidir. Kaybettikten sonra gelen tekrar oynamak ister misiniz sorusuna evet yanıtını verirseniz aynı kişi ismi ile yeni bir oyuna başlayabilir, hayır yanıtını verirseniz isminizi yenilemeniz ya da aynı isim için tekrar kişiyi kaydet butonuna tıklamanız gerekir.\n\n-Geçmiş oyunlarınızın kayıtlarını sonuçları göster butonuna tıklayarak görüntüleyebilirsiniz. İyi oyunlar.", "Oyun Yardım", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtKisi.Text))//txt boş değilse kişiyi kaydet
            {
                kisi = txtKisi.Text;
                MessageBox.Show(kisi + " kaydedildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panelOyun.Focus();
            }
        }
    }
}
