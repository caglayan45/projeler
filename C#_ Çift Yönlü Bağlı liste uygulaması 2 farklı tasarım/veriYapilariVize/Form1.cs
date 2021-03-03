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

namespace veriYapilariVize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        LinkedListt Listt;

        class node
        {
            public int veri;
            public node prev;
            public node next;
            public node(int veri)
            {
                this.veri = veri;
            }
        }

        int anyList = 0;

        class LinkedListt
        {
            public int kapasite;
            public node ilk, son, sonEklenen;
            public LinkedListt(int kapasite)
            {
                this.kapasite = kapasite;
                this.ilk = null;
                this.son = null;
            }

            public void listeOlustur(int veri)
            {
                node tempnode = new node(veri);
                if (ilk == null)
                {
                    ilk = tempnode;
                    son = tempnode;
                    ilk.prev = null;
                    son.next = null;

                }
                else if (ilk.next == null)
                {
                    ilk.next = tempnode;
                    son = ilk.next;
                    son.prev = ilk;
                    son.next = null;
                }
                else
                {
                    son.next = tempnode;
                    tempnode.prev = son;
                    son = tempnode;
                    son.next = null;
                }
            }

            public int add(int veri, int yer)
            {
                node tempnode = new node(veri);
                if (ilk == null)
                {
                    ilk = tempnode;
                    son = tempnode;
                    ilk.prev = null;
                    son.next = null;
                    sonEklenen = tempnode;
                    return 1;
                }
                else
                {
                    int isThere = 0;
                    node liste = ilk, temp;
                    
                    while (liste != null)
                    {
                        if (liste.veri == yer)
                        {
                            if (liste == son)
                            {
                                liste.next = tempnode;
                                tempnode.prev = liste;
                                tempnode.next = null;
                                son = tempnode;
                            }
                            else
                            {
                                temp = liste.next;
                                liste.next = tempnode;
                                tempnode.prev = liste;
                                tempnode.next = temp;
                                temp.prev = tempnode;
                            }
                            isThere = 1;
                            sonEklenen = tempnode;
                            return 1;
                        }
                        liste = liste.next;
                    }
                    if (isThere == 0)
                    {
                        MessageBox.Show("Girilen düğüm listede yok.");
                        return 0;
                    }
                }
                return -1;
            }
        }

        private void buttonOlustur_Click(object sender, EventArgs e)
        {
            if (anyList == 0 && !string.IsNullOrEmpty(txtDugumSayisi.Text))
            {
                int listLength = Convert.ToInt32(txtDugumSayisi.Text);
                Listt = new LinkedListt(listLength);
                Random rand = new Random();

                for (int i = 0; i < listLength; i++)
                {
                    int dugumDeger = rand.Next(1, 30);
                    Listt.listeOlustur(dugumDeger);
                    if (i != listLength - 1)
                        lblGroupBox1.Text += (dugumDeger.ToString() + "  <>  ");
                    else
                        lblGroupBox1.Text += dugumDeger.ToString();
                    groupBox1.Refresh();
                    Thread.Sleep(200);
                }
                anyList = 1;
            }
            else
            {
                MessageBox.Show("Liste önceden oluşturulmuş ya da boş yer mevcut.");
            }
        }

        private void buttonEkle_Click(object sender, EventArgs e)
        {
            if (anyList == 0 || string.IsNullOrEmpty(txtDeger.Text) || string.IsNullOrEmpty(txtYer.Text))
            {
                MessageBox.Show("Liste oluşturulmamış ya da boş yer mevcut.");
                return;
            }

            int kontrol = Listt.add(Convert.ToInt32(txtDeger.Text), Convert.ToInt32(txtYer.Text));
            if (kontrol == 1)
            {
                groupBox2.Controls.Clear();

                int k = 0, j = 1;

                node liste = Listt.ilk;
                while (liste != null)
                {
                    Label lbl = new Label();
                    lbl.Text = liste.veri.ToString();
                    if (liste == Listt.sonEklenen)
                        lbl.BorderStyle = BorderStyle.FixedSingle;
                    lbl.Font = new Font(Label.DefaultFont.FontFamily, 10, FontStyle.Bold);
                    lbl.TextAlign = ContentAlignment.MiddleCenter;
                    lbl.Width = 35;
                    lbl.Height = 25;
                    lbl.Location = new Point((k * 60) + 3, 54);
                    k++;
                    groupBox2.Controls.Add(lbl);

                    if(liste != Listt.son)
                    {
                        Label lb = new Label();
                        lb.Text = " <> ";
                        lb.Width = 35;
                        lb.Height = 25;
                        lb.Font = new Font(Label.DefaultFont.FontFamily, 10, FontStyle.Bold);
                        lb.TextAlign = ContentAlignment.MiddleCenter;
                        lb.Location = new Point((j * 30) + 1, 54);
                        j += 2;
                        lb.BringToFront();
                        groupBox2.Controls.Add(lb);
                    }
                    lbl.BringToFront();
                    groupBox2.Refresh();
                    Thread.Sleep(200);
                    liste = liste.next;
                }
            }
        }

        private void txtDugumSayisi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtDeger_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtYer_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
