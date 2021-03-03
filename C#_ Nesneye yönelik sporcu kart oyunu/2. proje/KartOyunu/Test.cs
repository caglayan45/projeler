using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KartOyunu
{
    public class Test
    {
        Bilgisayar bilgisayar;
        Kullanıcı kullanıcı;
        public static Panel bilgisayarSecilenKart, kullaniciSecilenKart;
        public static Point bilgisayarSecilenKartKonum, kullaniciSecilenKartKonum;
        Random rand;
        Futbolcu[] futbolcular;
        Basketbolcu[] basketbolcular;
        public static bool kartKontrol = false, kullaniciKartSectiMi;
        public int aktifKartSayisi = 8;
        public void Oyun(oyunForm form)
        {
            bilgisayar = new Bilgisayar(0, 0, "Bilgisayar");
            kullanıcı = new Kullanıcı(1, 0, "Kullanıcı");

            rand = new Random();
            futbolcular = new Futbolcu[8];
            basketbolcular = new Basketbolcu[8];
            int s = 90;

            futbolcular[0] = new Futbolcu("DROGBA", "FUTBOL", rand.Next(s,100), rand.Next(s, 100), rand.Next(s, 100), new Point(1650, 370), new Size(210, 300), false, form);
            futbolcular[1] = new Futbolcu("METIN OKTAY", "FUTBOL", rand.Next(s, 100), rand.Next(s, 100), rand.Next(s, 100), new Point(1650, 370), new Size(210, 300), false, form);
            futbolcular[2] = new Futbolcu("RONALDINHO", "FUTBOL", rand.Next(s, 100), rand.Next(s, 100), rand.Next(s, 100), new Point(1650, 370), new Size(210, 300), false, form);
            futbolcular[3] = new Futbolcu("HENRY", "FUTBOL", rand.Next(s, 100), rand.Next(s, 100), rand.Next(s, 100), new Point(1650, 370), new Size(210, 300), false, form);
            futbolcular[4] = new Futbolcu("ZLATAN", "FUTBOL", rand.Next(s, 100), rand.Next(s, 100), rand.Next(s, 100), new Point(1650, 370), new Size(210, 300), false, form);
            futbolcular[5] = new Futbolcu("BECKENBAUER", "FUTBOL", rand.Next(s, 100), rand.Next(s, 100), rand.Next(s, 100), new Point(1650, 370), new Size(210, 300), false, form);
            futbolcular[6] = new Futbolcu("HAGI", "FUTBOL", rand.Next(s, 100), rand.Next(s, 100), rand.Next(s, 100), new Point(1650, 370), new Size(210, 300), false, form);
            futbolcular[7] = new Futbolcu("XAVI", "FUTBOL", rand.Next(s, 100), rand.Next(s, 100), rand.Next(s, 100), new Point(1650, 370), new Size(210, 300), false, form);

            basketbolcular[0] = new Basketbolcu("DAVIS", "BASKETBOL", rand.Next(s,100), rand.Next(s, 100), rand.Next(s, 100), new Point(50, 370), new Size(210, 300), false, form);
            basketbolcular[1] = new Basketbolcu("SHAQ", "BASKETBOL", rand.Next(s, 100), rand.Next(s, 100), rand.Next(s, 100), new Point(50, 370), new Size(210, 300), false, form);
            basketbolcular[2] = new Basketbolcu("KOBE", "BASKETBOL", rand.Next(s, 100), rand.Next(s, 100), rand.Next(s, 100), new Point(50, 370), new Size(210, 300), false, form);
            basketbolcular[3] = new Basketbolcu("CURRY", "BASKETBOL", rand.Next(s, 100), rand.Next(s, 100), rand.Next(s, 100), new Point(50, 370), new Size(210, 300), false, form);
            basketbolcular[4] = new Basketbolcu("JORDAN", "BASKETBOL", rand.Next(s, 100), rand.Next(s, 100), rand.Next(s, 100), new Point(50, 370), new Size(210, 300), false, form);
            basketbolcular[5] = new Basketbolcu("CEDI OSMAN", "BASKETBOL", rand.Next(s, 100), rand.Next(s, 100), rand.Next(s, 100), new Point(50, 370), new Size(210, 300), false, form);
            basketbolcular[6] = new Basketbolcu("HARDEN", "BASKETBOL", rand.Next(s, 100), rand.Next(s, 100), rand.Next(s, 100), new Point(50, 370), new Size(210, 300), false, form);
            basketbolcular[7] = new Basketbolcu("LEBRON", "BASKETBOL", rand.Next(s, 100), rand.Next(s, 100), rand.Next(s, 100), new Point(50, 370), new Size(210, 300), false, form);

            oyunForm.lblKul.Text = "Kullanıcı skoru : " + kullanıcı.GetSkor();
            oyunForm.lblPc.Text = "Bilgisayar skoru : " + bilgisayar.GetSkor();
        }

        public void KartlariDagit()
        {
            List<int> indisler = new List<int>(8);
            for (int i = 0; i < 4; i++)
            {
                int sayi = -1;
                do
                {
                    sayi = rand.Next(8);
                } while (indisler.Contains(sayi));
                indisler.Add(sayi);
                bilgisayar.DesteyeFutbolcuEkle(futbolcular[sayi]);
                kartDagitAnimasyon(futbolcular[sayi].oyuncuKarti, new Point(i * 210, 0),false);

                do
                {
                    sayi = rand.Next(8);
                } while (indisler.Contains(sayi));
                indisler.Add(sayi);
                kullanıcı.DesteyeFutbolcuEkle(futbolcular[sayi]);
                kartDagitAnimasyon(futbolcular[sayi].oyuncuKarti, new Point(i * 210, 740),true);
            }

            List<int> indisler1 = new List<int>(8);
            for (int i = 0; i < 4; i++)
            {
                int sayi = -1;
                do
                {
                    sayi = rand.Next(8);
                } while (indisler1.Contains(sayi));
                indisler1.Add(sayi);
                bilgisayar.DesteyeBasketbolcuEkle(basketbolcular[sayi]);
                kartDagitAnimasyon(basketbolcular[sayi].oyuncuKarti, new Point((i + 4) * 210, 0),false);

                do
                {
                    sayi = rand.Next(8);
                } while (indisler1.Contains(sayi));
                indisler1.Add(sayi);
                kullanıcı.DesteyeBasketbolcuEkle(basketbolcular[sayi]);
                kartDagitAnimasyon(basketbolcular[sayi].oyuncuKarti, new Point((i + 4) * 210, 740),true);
            }
        }

        public void OyunDongusu()
        {
            if (kullaniciKartSectiMi)
            {
                oyunForm.t.Stop();
                if (kartKontrol)
                {
                    bilgisayar.KartSec(kartKontrol);
                    Random r = new Random();
                    int ozellikSec = r.Next(3);

                    int K_SVR = Convert.ToInt32(((Label)kullaniciSecilenKart.Controls.Find("ozellik", true)[0]).Text.Split(' ')[1]);
                    int K_PEN = Convert.ToInt32(((Label)kullaniciSecilenKart.Controls.Find("ozellik", true)[0]).Text.Split(' ')[3]);
                    int K_KKG = Convert.ToInt32(((Label)kullaniciSecilenKart.Controls.Find("ozellik", true)[0]).Text.Split(' ')[5]);

                    int B_SVR = Convert.ToInt32(((Label)bilgisayarSecilenKart.Controls.Find("ozellik", true)[0]).Text.Split(' ')[1]);
                    int B_PEN = Convert.ToInt32(((Label)bilgisayarSecilenKart.Controls.Find("ozellik", true)[0]).Text.Split(' ')[3]);
                    int B_KKG = Convert.ToInt32(((Label)bilgisayarSecilenKart.Controls.Find("ozellik", true)[0]).Text.Split(' ')[5]);

                    switch (ozellikSec)
                    {
                        case 0:
                            if (K_SVR > B_SVR)
                            {
                                MessageBox.Show("Serbest vuruş Kullanıcı Kazandı");
                                for (int i = 0; i < kullanıcı.Futbolcular.Count; i++)
                                {
                                    if (kullanıcı.Futbolcular[i].oyuncuKarti == kullaniciSecilenKart)
                                    {
                                        kullanıcı.Futbolcular[i].SetKartKullanildiMi(true);
                                        kullanıcı.Futbolcular[i].oyuncuKarti.Visible = false;
                                        kullanıcı.SetSkor(kullanıcı.GetSkor() + 10);
                                    }
                                }
                                for (int i = 0; i < bilgisayar.Futbolcular.Count; i++)
                                {
                                    if (bilgisayar.Futbolcular[i].oyuncuKarti == bilgisayarSecilenKart)
                                    {
                                        bilgisayar.Futbolcular[i].SetKartKullanildiMi(true);
                                        bilgisayar.Futbolcular[i].oyuncuKarti.Visible = false;
                                    }
                                }
                                aktifKartSayisi--;
                            }
                            else if (K_SVR < B_SVR)
                            {
                                MessageBox.Show("Serbest vuruş Bilgisayar Kazandı");
                                for (int i = 0; i < kullanıcı.Futbolcular.Count; i++)
                                {
                                    if (kullanıcı.Futbolcular[i].oyuncuKarti == kullaniciSecilenKart)
                                    {
                                        kullanıcı.Futbolcular[i].SetKartKullanildiMi(true);
                                        kullanıcı.Futbolcular[i].oyuncuKarti.Visible = false;
                                    }
                                }
                                for (int i = 0; i < bilgisayar.Futbolcular.Count; i++)
                                {
                                    if (bilgisayar.Futbolcular[i].oyuncuKarti == bilgisayarSecilenKart)
                                    {
                                        bilgisayar.Futbolcular[i].SetKartKullanildiMi(true);
                                        bilgisayar.Futbolcular[i].oyuncuKarti.Visible = false;
                                        bilgisayar.SetSkor(bilgisayar.GetSkor() + 10);
                                    }
                                }
                                aktifKartSayisi--;
                            }
                            else
                            {
                                MessageBox.Show("Serbest vuruş Berabere");
                                kartDagitAnimasyon(kullaniciSecilenKart, kullaniciSecilenKartKonum, true);
                                kartDagitAnimasyon(bilgisayarSecilenKart, bilgisayarSecilenKartKonum, false);
                            }
                            break;
                        case 1:
                            if (K_PEN > B_PEN)
                            {
                                MessageBox.Show("Penaltı Kullanıcı Kazandı");
                                for (int i = 0; i < kullanıcı.Futbolcular.Count; i++)
                                {
                                    if (kullanıcı.Futbolcular[i].oyuncuKarti == kullaniciSecilenKart)
                                    {
                                        kullanıcı.Futbolcular[i].SetKartKullanildiMi(true);
                                        kullanıcı.Futbolcular[i].oyuncuKarti.Visible = false;
                                        kullanıcı.SetSkor(kullanıcı.GetSkor() + 10);
                                    }
                                }
                                for (int i = 0; i < bilgisayar.Futbolcular.Count; i++)
                                {
                                    if (bilgisayar.Futbolcular[i].oyuncuKarti == bilgisayarSecilenKart)
                                    {
                                        bilgisayar.Futbolcular[i].SetKartKullanildiMi(true);
                                        bilgisayar.Futbolcular[i].oyuncuKarti.Visible = false;
                                    }
                                }
                                aktifKartSayisi--;
                            }
                            else if (K_PEN < B_PEN)
                            {
                                MessageBox.Show("Penaltı Bilgisayar Kazandı");
                                for (int i = 0; i < kullanıcı.Futbolcular.Count; i++)
                                {
                                    if (kullanıcı.Futbolcular[i].oyuncuKarti == kullaniciSecilenKart)
                                    {
                                        kullanıcı.Futbolcular[i].SetKartKullanildiMi(true);
                                        kullanıcı.Futbolcular[i].oyuncuKarti.Visible = false;
                                    }
                                }
                                for (int i = 0; i < bilgisayar.Futbolcular.Count; i++)
                                {
                                    if (bilgisayar.Futbolcular[i].oyuncuKarti == bilgisayarSecilenKart)
                                    {
                                        bilgisayar.Futbolcular[i].SetKartKullanildiMi(true);
                                        bilgisayar.Futbolcular[i].oyuncuKarti.Visible = false;
                                        bilgisayar.SetSkor(bilgisayar.GetSkor() + 10);
                                    }
                                }
                                aktifKartSayisi--;
                            }
                            else
                            {
                                MessageBox.Show("Penaltı Berabere");

                                kartDagitAnimasyon(kullaniciSecilenKart, kullaniciSecilenKartKonum, true);
                                kartDagitAnimasyon(bilgisayarSecilenKart, bilgisayarSecilenKartKonum, false);
                            }
                            break;
                        case 2:
                            if (K_KKG > B_KKG)
                            {
                                MessageBox.Show("Kaleciyle karşı karşıya Kullanıcı Kazandı");
                                for (int i = 0; i < kullanıcı.Futbolcular.Count; i++)
                                {
                                    if (kullanıcı.Futbolcular[i].oyuncuKarti == kullaniciSecilenKart)
                                    {
                                        kullanıcı.Futbolcular[i].SetKartKullanildiMi(true);
                                        kullanıcı.Futbolcular[i].oyuncuKarti.Visible = false;
                                        kullanıcı.SetSkor(kullanıcı.GetSkor() + 10);
                                    }
                                }
                                for (int i = 0; i < bilgisayar.Futbolcular.Count; i++)
                                {
                                    if (bilgisayar.Futbolcular[i].oyuncuKarti == bilgisayarSecilenKart)
                                    {
                                        bilgisayar.Futbolcular[i].SetKartKullanildiMi(true);
                                        bilgisayar.Futbolcular[i].oyuncuKarti.Visible = false;
                                    }
                                }
                                aktifKartSayisi--;
                            }
                            else if (K_KKG < B_KKG)
                            {
                                MessageBox.Show("Kaleciyle karşı karşıya Bilgisayar Kazandı");
                                for (int i = 0; i < kullanıcı.Futbolcular.Count; i++)
                                {
                                    if (kullanıcı.Futbolcular[i].oyuncuKarti == kullaniciSecilenKart)
                                    {
                                        kullanıcı.Futbolcular[i].SetKartKullanildiMi(true);
                                        kullanıcı.Futbolcular[i].oyuncuKarti.Visible = false;
                                    }
                                }
                                for (int i = 0; i < bilgisayar.Futbolcular.Count; i++)
                                {
                                    if (bilgisayar.Futbolcular[i].oyuncuKarti == bilgisayarSecilenKart)
                                    {
                                        bilgisayar.Futbolcular[i].SetKartKullanildiMi(true);
                                        bilgisayar.Futbolcular[i].oyuncuKarti.Visible = false;
                                        bilgisayar.SetSkor(bilgisayar.GetSkor() + 10);
                                    }
                                }
                                aktifKartSayisi--;
                            }
                            else
                            {
                                MessageBox.Show("Kaleciyle karşı karşıya Berabere");

                                kartDagitAnimasyon(kullaniciSecilenKart, kullaniciSecilenKartKonum, true);
                                kartDagitAnimasyon(bilgisayarSecilenKart, bilgisayarSecilenKartKonum, false);
                            }
                            break;
                    }
                }
                else
                {
                    bilgisayar.KartSec(kartKontrol);
                    Random r = new Random();
                    int ozellikSec = r.Next(3);

                    int K_UC = Convert.ToInt32(((Label)kullaniciSecilenKart.Controls.Find("ozellik", true)[0]).Text.Split(' ')[1]);
                    int K_IKI = Convert.ToInt32(((Label)kullaniciSecilenKart.Controls.Find("ozellik", true)[0]).Text.Split(' ')[3]);
                    int K_SRA = Convert.ToInt32(((Label)kullaniciSecilenKart.Controls.Find("ozellik", true)[0]).Text.Split(' ')[5]);

                    int B_UC = Convert.ToInt32(((Label)bilgisayarSecilenKart.Controls.Find("ozellik", true)[0]).Text.Split(' ')[1]);
                    int B_IKI = Convert.ToInt32(((Label)bilgisayarSecilenKart.Controls.Find("ozellik", true)[0]).Text.Split(' ')[3]);
                    int B_SRA = Convert.ToInt32(((Label)bilgisayarSecilenKart.Controls.Find("ozellik", true)[0]).Text.Split(' ')[5]);

                    switch (ozellikSec)
                    {
                        case 0:
                            if (K_UC > B_UC)
                            {
                                MessageBox.Show("Üçlük Kullanıcı Kazandı");
                                for (int i = 0; i < kullanıcı.Basketbolcular.Count; i++)
                                {
                                    if (kullanıcı.Basketbolcular[i].oyuncuKarti == kullaniciSecilenKart)
                                    {
                                        kullanıcı.Basketbolcular[i].SetKartKullanildiMi(true);
                                        kullanıcı.Basketbolcular[i].oyuncuKarti.Visible = false;
                                        kullanıcı.SetSkor(kullanıcı.GetSkor() + 10);
                                    }
                                }
                                for (int i = 0; i < bilgisayar.Basketbolcular.Count; i++)
                                {
                                    if (bilgisayar.Basketbolcular[i].oyuncuKarti == bilgisayarSecilenKart)
                                    {
                                        bilgisayar.Basketbolcular[i].SetKartKullanildiMi(true);
                                        bilgisayar.Basketbolcular[i].oyuncuKarti.Visible = false;
                                    }
                                }
                                aktifKartSayisi--;
                            }
                            else if (K_UC < B_UC)
                            {
                                MessageBox.Show("Üçlük Bilgisayar Kazandı");
                                for (int i = 0; i < kullanıcı.Basketbolcular.Count; i++)
                                {
                                    if (kullanıcı.Basketbolcular[i].oyuncuKarti == kullaniciSecilenKart)
                                    {
                                        kullanıcı.Basketbolcular[i].SetKartKullanildiMi(true);
                                        kullanıcı.Basketbolcular[i].oyuncuKarti.Visible = false;
                                    }
                                }
                                for (int i = 0; i < bilgisayar.Basketbolcular.Count; i++)
                                {
                                    if (bilgisayar.Basketbolcular[i].oyuncuKarti == bilgisayarSecilenKart)
                                    {
                                        bilgisayar.Basketbolcular[i].SetKartKullanildiMi(true);
                                        bilgisayar.Basketbolcular[i].oyuncuKarti.Visible = false;
                                        bilgisayar.SetSkor(bilgisayar.GetSkor() + 10);
                                    }
                                }
                                aktifKartSayisi--;
                            }
                            else
                            {
                                MessageBox.Show("Üçlük Berabere");
                                kartDagitAnimasyon(kullaniciSecilenKart, kullaniciSecilenKartKonum, true);
                                kartDagitAnimasyon(bilgisayarSecilenKart, bilgisayarSecilenKartKonum, false);
                            }
                            break;
                        case 1:
                            if (K_IKI > B_IKI)
                            {
                                MessageBox.Show("İkilik Kullanıcı Kazandı");
                                for (int i = 0; i < kullanıcı.Basketbolcular.Count; i++)
                                {
                                    if (kullanıcı.Basketbolcular[i].oyuncuKarti == kullaniciSecilenKart)
                                    {
                                        kullanıcı.Basketbolcular[i].SetKartKullanildiMi(true);
                                        kullanıcı.Basketbolcular[i].oyuncuKarti.Visible = false;
                                        kullanıcı.SetSkor(kullanıcı.GetSkor() + 10);
                                    }
                                }
                                for (int i = 0; i < bilgisayar.Basketbolcular.Count; i++)
                                {
                                    if (bilgisayar.Basketbolcular[i].oyuncuKarti == bilgisayarSecilenKart)
                                    {
                                        bilgisayar.Basketbolcular[i].SetKartKullanildiMi(true);
                                        bilgisayar.Basketbolcular[i].oyuncuKarti.Visible = false;
                                    }
                                }
                                aktifKartSayisi--;
                            }
                            else if (K_IKI < B_IKI)
                            {
                                MessageBox.Show("İkilik Bilgisayar Kazandı");
                                for (int i = 0; i < kullanıcı.Basketbolcular.Count; i++)
                                {
                                    if (kullanıcı.Basketbolcular[i].oyuncuKarti == kullaniciSecilenKart)
                                    {
                                        kullanıcı.Basketbolcular[i].SetKartKullanildiMi(true);
                                        kullanıcı.Basketbolcular[i].oyuncuKarti.Visible = false;
                                    }
                                }
                                for (int i = 0; i < bilgisayar.Basketbolcular.Count; i++)
                                {
                                    if (bilgisayar.Basketbolcular[i].oyuncuKarti == bilgisayarSecilenKart)
                                    {
                                        bilgisayar.Basketbolcular[i].SetKartKullanildiMi(true);
                                        bilgisayar.Basketbolcular[i].oyuncuKarti.Visible = false;
                                        bilgisayar.SetSkor(bilgisayar.GetSkor() + 10);
                                    }
                                }
                                aktifKartSayisi--;
                            }
                            else
                            {
                                MessageBox.Show("İkilik Berabere");

                                kartDagitAnimasyon(kullaniciSecilenKart, kullaniciSecilenKartKonum, true);
                                kartDagitAnimasyon(bilgisayarSecilenKart, bilgisayarSecilenKartKonum, false);
                            }
                            break;
                        case 2:
                            if (K_SRA > B_SRA)
                            {
                                MessageBox.Show("Serbest atış Kullanıcı Kazandı");
                                for (int i = 0; i < kullanıcı.Basketbolcular.Count; i++)
                                {
                                    if (kullanıcı.Basketbolcular[i].oyuncuKarti == kullaniciSecilenKart)
                                    {
                                        kullanıcı.Basketbolcular[i].SetKartKullanildiMi(true);
                                        kullanıcı.Basketbolcular[i].oyuncuKarti.Visible = false;
                                        kullanıcı.SetSkor(kullanıcı.GetSkor() + 10);
                                    }
                                }
                                for (int i = 0; i < bilgisayar.Basketbolcular.Count; i++)
                                {
                                    if (bilgisayar.Basketbolcular[i].oyuncuKarti == bilgisayarSecilenKart)
                                    {
                                        bilgisayar.Basketbolcular[i].SetKartKullanildiMi(true);
                                        bilgisayar.Basketbolcular[i].oyuncuKarti.Visible = false;
                                    }
                                }
                                aktifKartSayisi--;
                            }
                            else if (K_SRA < B_SRA)
                            {
                                MessageBox.Show("Serbest atış Bilgisayar Kazandı");
                                for (int i = 0; i < kullanıcı.Basketbolcular.Count; i++)
                                {
                                    if (kullanıcı.Basketbolcular[i].oyuncuKarti == kullaniciSecilenKart)
                                    {
                                        kullanıcı.Basketbolcular[i].SetKartKullanildiMi(true);
                                        kullanıcı.Basketbolcular[i].oyuncuKarti.Visible = false;
                                    }
                                }
                                for (int i = 0; i < bilgisayar.Basketbolcular.Count; i++)
                                {
                                    if (bilgisayar.Basketbolcular[i].oyuncuKarti == bilgisayarSecilenKart)
                                    {
                                        bilgisayar.Basketbolcular[i].SetKartKullanildiMi(true);
                                        bilgisayar.Basketbolcular[i].oyuncuKarti.Visible = false;
                                        bilgisayar.SetSkor(bilgisayar.GetSkor() + 10);
                                    }
                                }
                                aktifKartSayisi--;
                            }
                            else
                            {
                                MessageBox.Show("Serbest atış Berabere");

                                kartDagitAnimasyon(kullaniciSecilenKart, kullaniciSecilenKartKonum, true);
                                kartDagitAnimasyon(bilgisayarSecilenKart, bilgisayarSecilenKartKonum, false);
                            }
                            break;
                    }
                }
                kullaniciKartSectiMi = false;
                oyunForm.lblKul.Text = "Kullanıcı skoru : " + kullanıcı.GetSkor();
                oyunForm.lblPc.Text = "Bilgisayar skoru : " + bilgisayar.GetSkor();
            }
            else
                oyunForm.t.Stop();
            if (aktifKartSayisi == 0)
            {
                if (kullanıcı.GetSkor() > bilgisayar.GetSkor())
                    MessageBox.Show("Kullanıcı Kazandı!");
                else if (kullanıcı.GetSkor() < bilgisayar.GetSkor())
                    MessageBox.Show("Bilgisayar Kazandı!");
                else
                    MessageBox.Show("Berabere!");
            }
        }

        public static void kartDagitAnimasyon(Panel sporcu, Point gidecekKonum,bool acikMi)
        {
            sporcu.BringToFront();
            int c = 10;
            if (acikMi)
            {
                sporcu.Controls.Find("arkaPlan", true)[0].Visible = false;
                sporcu.Controls.Find("arkaPlan", true)[0].SendToBack();
            }
            else
            {
                sporcu.Controls.Find("arkaPlan", true)[0].Visible = true;
                sporcu.Controls.Find("arkaPlan", true)[0].BringToFront();
            }
            while (sporcu.Location.X != gidecekKonum.X && sporcu.Location.Y != gidecekKonum.Y)
            {
                Thread.Sleep(25);
                int newX, newY;
                newX = sporcu.Location.X;
                newY = sporcu.Location.Y;
                newX += ((gidecekKonum.X) - (sporcu.Location.X)) / c;
                newY += ((gidecekKonum.Y) - sporcu.Location.Y) / c;
                sporcu.Location = new Point(newX, newY);
                c--;
            }
        }

    }
}
