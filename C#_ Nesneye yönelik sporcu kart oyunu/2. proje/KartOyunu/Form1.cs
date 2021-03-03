using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KartOyunu
{
    public partial class oyunForm : Form
    {
        public oyunForm()
        {
            InitializeComponent();
        }

        public Test Oyun;
        public static Timer t;
        public static Label lblPc, lblKul;

        private void Form1_Load(object sender, EventArgs e)
        {
            t = timer1;
            lblKul = labelKullanici;
            lblPc = labelBilgisayar;
            Oyun = new Test();
            Oyun.Oyun(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            Oyun.KartlariDagit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Oyun.OyunDongusu();
        }
    }
}
