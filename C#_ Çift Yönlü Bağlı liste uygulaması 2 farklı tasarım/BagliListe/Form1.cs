using Microsoft.VisualBasic;
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

namespace BagliListe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool listeOlustuMu = false;
        public BagliListe liste;

        public class dugum
        {
            public int deger;
            public dugum onceki;
            public dugum sonraki;
            public dugum(int deger)
            {
                this.deger = deger;
            }
        }

        public class BagliListe
        {
            public int kapasite;
            public dugum ilk, son, sonEklenen;
            public BagliListe(int kapasite)
            {
                this.kapasite = kapasite;
                this.ilk = null;
                this.son = null;
            }

            public void olusturma(int deger)
            {
                dugum tempDugum = new dugum(deger);
                if (ilk == null)
                {
                    ilk = tempDugum;
                    son = tempDugum;
                    ilk.onceki = null;
                    son.sonraki = null;

                }
                else if (ilk.sonraki == null)
                {
                    ilk.sonraki = tempDugum;
                    son = ilk.sonraki;
                    son.onceki = ilk;
                    son.sonraki = null;
                }
                else
                {
                    son.sonraki = tempDugum;
                    tempDugum.onceki = son;
                    son = tempDugum;
                    son.sonraki = null;
                }
            }

            public int ekle(int deger, int yer)
            {
                dugum tempDugum = new dugum(deger);
                if(ilk == null)
                {
                    ilk = tempDugum;
                    son = tempDugum;
                    ilk.onceki = null;
                    son.sonraki = null;
                    sonEklenen = tempDugum;
                    return 1;
                }
                else
                {
                    dugum dolas = ilk,tempDegis;
                    bool varMi = false;
                    while(dolas != null)
                    {
                        if(dolas.deger == yer)
                        {
                            if(dolas == son)
                            {
                                dolas.sonraki = tempDugum;
                                tempDugum.onceki = dolas;
                                tempDugum.sonraki = null;
                                son = tempDugum;
                            }
                            else
                            {
                                tempDegis = dolas.sonraki;
                                dolas.sonraki = tempDugum;
                                tempDugum.onceki = dolas;
                                tempDugum.sonraki = tempDegis;
                                tempDegis.onceki = tempDugum;
                            }
                            varMi = true;
                            sonEklenen = tempDugum;
                            return 1;
                        }
                        dolas = dolas.sonraki;
                    }
                    if (!varMi)
                    {
                        MessageBox.Show("Girilen yer listede mevcut değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return 0;
                    }
                }
                return -1;
            }
        }

        private void btnListeOlustur_Click(object sender, EventArgs e)
        {
            if (!listeOlustuMu)
            {
                int listeUzunlugu = 0;
                string inputGirisi = "";
                do
                {
                    inputGirisi = Interaction.InputBox("Kaç adet düğümlü liste oluşturmak istiyorsunuz ?", "Adet Bilgisi", "Örn: 5", 300, 300);
                } while (!(int.TryParse(inputGirisi, out listeUzunlugu)));
                liste = new BagliListe(listeUzunlugu);
                Random rand = new Random();
                int k = 1, j = 3;

                Label lb2 = new Label();
                lb2.Text = "Null   <-> ";
                lb2.Width = 60;
                lb2.Height = 15;
                lb2.Location = new Point(0, 11);
                panel1.Controls.Add(lb2);
                lb2.BringToFront();

                for (int i = 0; i < listeUzunlugu; i++)
                {
                    int deger = rand.Next(1, 50);
                    liste.olusturma(deger);
                    Label lbl = new Label();
                    lbl.Text = deger.ToString();
                    lbl.BackColor = Color.White;
                    lbl.Width = 30;
                    lbl.Height = 15;
                    lbl.BorderStyle = BorderStyle.FixedSingle;
                    lbl.Location = new Point((k * 60), 11);
                    k++;
                    panel1.Controls.Add(lbl);


                    Label lb = new Label();
                    lb.Text = " <-> ";
                    lb.Width = 30;
                    lb.Height = 15;
                    lb.Location = new Point((j * 30)+2, 11);
                    j += 2;
                    lb.BringToFront();
                    lbl.BringToFront();
                    panel1.Controls.Add(lb);

                    panel1.Refresh();
                    Thread.Sleep(300);
                }
                listeOlustuMu = true;
                Label lb3 = new Label();
                lb3.Text = "Null";
                lb3.Width = 60;
                lb3.Height = 15;
                lb3.Location = new Point(((j-1) * 30) + 2, 11);
                panel1.Controls.Add(lb3);
                lb3.BringToFront();
            }
            else
            {
                MessageBox.Show("Zaten bir liste mevcut.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDugumEkle_Click(object sender, EventArgs e)
        {
            if (!listeOlustuMu)
            {
                MessageBox.Show("Önce liste oluşmalı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int eklenecekYer = 0, eklenecekDeger = 0;
            string inputGirisi = "";
            do
            {
                inputGirisi = Interaction.InputBox("Hangi düğümün sağına eklenecek ?", "Yer Bilgisi", "Örn: 23", 300, 300);
            } while (!(int.TryParse(inputGirisi, out eklenecekYer)));
            do
            {
                inputGirisi = Interaction.InputBox("Eklenecek Değer ?", "Düğüm Değeri", "Örn: 7", 300, 300);
            } while (!(int.TryParse(inputGirisi, out eklenecekDeger)));
            int kontrol = liste.ekle(eklenecekDeger, eklenecekYer);
            if (kontrol == 1)
            {
                panel2.Controls.Clear();

                int k = 1, j = 3;

                Label lb2 = new Label();
                lb2.Text = "Null   <-> ";
                lb2.Width = 60;
                lb2.Height = 15;
                lb2.Location = new Point(0, 11);
                panel2.Controls.Add(lb2);
                lb2.BringToFront();
                dugum dolas = liste.ilk;
                while (dolas != null)
                {
                    Label lbl = new Label();
                    lbl.Text = dolas.deger.ToString();
                    lbl.Width = 30;
                    lbl.Height = 15;
                    if (dolas == liste.sonEklenen)
                        lbl.BackColor = Color.Yellow;
                    else
                        lbl.BackColor = Color.White;
                    lbl.BorderStyle = BorderStyle.FixedSingle;
                    lbl.Location = new Point((k * 60), 11);
                    k++;
                    panel2.Controls.Add(lbl);


                    Label lb = new Label();
                    lb.Text = " <-> ";
                    lb.Width = 30;
                    lb.Height = 15;
                    lb.Location = new Point((j * 30) + 2, 11);
                    j += 2;
                    lb.BringToFront();
                    lbl.BringToFront();
                    panel2.Controls.Add(lb);

                    panel2.Refresh();
                    Thread.Sleep(300);
                    dolas = dolas.sonraki;
                }
                listeOlustuMu = true;
                Label lb3 = new Label();
                lb3.Text = "Null";
                lb3.Width = 60;
                lb3.Height = 15;
                lb3.Location = new Point(((j - 1) * 30) + 2, 11);
                panel2.Controls.Add(lb3);
                lb3.BringToFront();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile("list.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}
