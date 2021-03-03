using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace veriIsleme2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int silinecekSatirID = 0;
        string[] txtAllLines, csvAllLines;

        void DataGridGuncelle()
        {
            txtAllLines = File.ReadAllLines("forestfires.txt");
            dataGridView1.Columns.Clear();
            var query = txtAllLines.Skip(1).Select((r, index) => new
            {
                i = index,
                Data = r.Split(',')
            }).ToList();

            dataGridView1.Columns.Add("col0", "X");
            dataGridView1.Columns.Add("col1", "Y");
            dataGridView1.Columns.Add("col2", "Month");
            dataGridView1.Columns.Add("col3", "Day");
            dataGridView1.Columns.Add("col4", "FFMC");
            dataGridView1.Columns.Add("col5", "DMC");
            dataGridView1.Columns.Add("col6", "DC");
            dataGridView1.Columns.Add("col7", "ISI");
            dataGridView1.Columns.Add("col8", "temp");
            dataGridView1.Columns.Add("col9", "RH");
            dataGridView1.Columns.Add("col10", "wind");
            dataGridView1.Columns.Add("col11", "rain");
            dataGridView1.Columns.Add("col12", "area");
            for (int i = 0; i < query.Count; i++)
            {
                dataGridView1.Rows.Add(
                  query[i].Data[0], query[i].Data[1],
                  query[i].Data[2], query[i].Data[3], query[i].Data[4],
                  query[i].Data[5], query[i].Data[6],
                  query[i].Data[7], query[i].Data[8],
                  query[i].Data[9], query[i].Data[10], query[i].Data[11], query[i].Data[12]
                 );
            }
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 50;
        }

        void SadeceRakamGirisi(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            csvAllLines = File.ReadAllLines("forestfires.csv");
            StreamWriter Yaz = new StreamWriter("forestfires.txt");
            for (int i = 0; i < csvAllLines.Length; i++)
            {
                Yaz.WriteLine(csvAllLines[i]);
            }
            Yaz.Close();
            //string[] columnTitles = txtAllLines[0].Split(',');
            DataGridGuncelle();
            btnEkleKaydet.Enabled = btnSil.Enabled = txtX.Enabled = txtY.Enabled = txtWIND.Enabled = txtTEMP.Enabled = txtRH.Enabled = txtRAIN.Enabled = txtISI.Enabled = txtDC.Enabled = txtDMC.Enabled = txtFFMC.Enabled = txtAREA.Enabled = comboBoxDays.Enabled = comboBoxMonths.Enabled = radioButtonEkle.Enabled = radioButtonGuncelle.Enabled = true;
            button1.Enabled = false;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int cevap = (int)MessageBox.Show(txtAllLines[silinecekSatirID+1] + " satırını silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(cevap == 6)
            {
                StreamWriter Yaz = new StreamWriter("forestfires.txt");
                for (int i = 0; i < txtAllLines.Length; i++)
                {
                    if (i != silinecekSatirID+1)
                        Yaz.WriteLine(txtAllLines[i]);
                }
                Yaz.Close();
                DataGridGuncelle();
                MessageBox.Show("Kayıt başarıyla silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void txtX_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxMonths.SelectedIndex = 0;
            comboBoxDays.SelectedIndex = 0;
        }

        private void txtISI_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtTEMP_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtRH_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtWIND_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtRAIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtAREA_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void btnEkleKaydet_Click(object sender, EventArgs e)
        {
            if (radioButtonGuncelle.Checked)//güncelleme
            {
                if (!string.IsNullOrEmpty(txtX.Text) && !string.IsNullOrEmpty(txtY.Text) && !string.IsNullOrEmpty(txtFFMC.Text) && !string.IsNullOrEmpty(txtDMC.Text) && !string.IsNullOrEmpty(txtDC.Text) && !string.IsNullOrEmpty(txtISI.Text) && !string.IsNullOrEmpty(txtTEMP.Text) && !string.IsNullOrEmpty(txtRH.Text) && !string.IsNullOrEmpty(txtWIND.Text) && !string.IsNullOrEmpty(txtRAIN.Text) && !string.IsNullOrEmpty(txtAREA.Text))
                {
                    StreamWriter Yaz = new StreamWriter("forestfires.txt");
                    for (int i = 0; i < txtAllLines.Length; i++)
                    {
                        if (i == silinecekSatirID+1)
                            Yaz.WriteLine(txtX.Text + "," + txtY.Text + "," + comboBoxMonths.SelectedItem + "," + comboBoxDays.SelectedItem + "," + txtFFMC.Text + "," + txtDMC.Text + "," + txtDC.Text + "," + txtISI.Text + "," + txtTEMP.Text + "," + txtRH.Text + "," + txtWIND.Text + "," + txtRAIN.Text + "," + txtAREA.Text);
                        else
                            Yaz.WriteLine(txtAllLines[i]);
                    }
                    Yaz.Close();
                    DataGridGuncelle();
                    MessageBox.Show("Kayıt güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else//ekleme
            {
                if(!string.IsNullOrEmpty(txtX.Text) && !string.IsNullOrEmpty(txtY.Text) && !string.IsNullOrEmpty(txtFFMC.Text) && !string.IsNullOrEmpty(txtDMC.Text) && !string.IsNullOrEmpty(txtDC.Text) && !string.IsNullOrEmpty(txtISI.Text) && !string.IsNullOrEmpty(txtTEMP.Text) && !string.IsNullOrEmpty(txtRH.Text) && !string.IsNullOrEmpty(txtWIND.Text) && !string.IsNullOrEmpty(txtRAIN.Text) && !string.IsNullOrEmpty(txtAREA.Text))
                {
                    FileStream fs = new FileStream("forestfires.txt", FileMode.Append);
                    StreamWriter Yaz = new StreamWriter(fs);
                    Yaz.WriteLine(txtX.Text + "," + txtY.Text + "," + comboBoxMonths.SelectedItem + "," + comboBoxDays.SelectedItem + "," + txtFFMC.Text + "," + txtDMC.Text + "," + txtDC.Text + "," + txtISI.Text + "," + txtTEMP.Text + "," + txtRH.Text + "," + txtWIND.Text + "," + txtRAIN.Text + "," + txtAREA.Text);
                    Yaz.Close();
                    DataGridGuncelle();
                    txtX.Text = txtY.Text = txtWIND.Text = txtTEMP.Text = txtRH.Text = txtRAIN.Text = txtISI.Text = txtDC.Text = txtDMC.Text = txtFFMC.Text = txtAREA.Text = "";
                    comboBoxDays.SelectedIndex = 0;
                    comboBoxMonths.SelectedIndex = 0;
                    MessageBox.Show("Kayıt başarıyla eklendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void radioButtonEkle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEkle.Checked)
            {
                txtX.Text = txtY.Text = txtWIND.Text = txtTEMP.Text = txtRH.Text = txtRAIN.Text = txtISI.Text = txtDC.Text = txtDMC.Text = txtFFMC.Text = txtAREA.Text = "";
                comboBoxDays.SelectedIndex = 0;
                comboBoxMonths.SelectedIndex = 0;
            }
            else
            {
                txtX.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtY.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                comboBoxMonths.SelectedItem = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                comboBoxDays.SelectedItem = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtFFMC.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtDMC.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtDC.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                txtISI.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                txtTEMP.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                txtRH.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                txtWIND.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                txtRAIN.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                txtAREA.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            silinecekSatirID = dataGridView1.CurrentRow.Index;
            if (radioButtonGuncelle.Checked)
            {
                txtX.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtY.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                comboBoxMonths.SelectedItem = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                comboBoxDays.SelectedItem = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtFFMC.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtDMC.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtDC.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                txtISI.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                txtTEMP.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                txtRH.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                txtWIND.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                txtRAIN.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                txtAREA.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            }
        }
    }
}
