namespace veriIsleme2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSil = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxMonths = new System.Windows.Forms.ComboBox();
            this.comboBoxDays = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFFMC = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDMC = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDC = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRH = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTEMP = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtISI = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAREA = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRAIN = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtWIND = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.radioButtonEkle = new System.Windows.Forms.RadioButton();
            this.radioButtonGuncelle = new System.Windows.Forms.RadioButton();
            this.btnEkleKaydet = new System.Windows.Forms.Button();
            this.btnCikis = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MistyRose;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(539, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(256, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "Verileri Çek";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 257);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1217, 311);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // btnSil
            // 
            this.btnSil.BackColor = System.Drawing.Color.MistyRose;
            this.btnSil.Enabled = false;
            this.btnSil.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSil.Location = new System.Drawing.Point(1119, 215);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(110, 36);
            this.btnSil.TabIndex = 2;
            this.btnSil.Text = "Seçilen Satırı Sil";
            this.btnSil.UseVisualStyleBackColor = false;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "X : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y : ";
            // 
            // txtX
            // 
            this.txtX.Enabled = false;
            this.txtX.Location = new System.Drawing.Point(74, 57);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(60, 20);
            this.txtX.TabIndex = 5;
            this.txtX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtX_KeyPress);
            // 
            // txtY
            // 
            this.txtY.Enabled = false;
            this.txtY.Location = new System.Drawing.Point(74, 83);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(60, 20);
            this.txtY.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "MONTH : ";
            // 
            // comboBoxMonths
            // 
            this.comboBoxMonths.Enabled = false;
            this.comboBoxMonths.FormattingEnabled = true;
            this.comboBoxMonths.Items.AddRange(new object[] {
            "jan",
            "feb",
            "mar",
            "apr",
            "may",
            "jun",
            "jul",
            "aug",
            "sep",
            "oct",
            "nov",
            "dec"});
            this.comboBoxMonths.Location = new System.Drawing.Point(74, 109);
            this.comboBoxMonths.Name = "comboBoxMonths";
            this.comboBoxMonths.Size = new System.Drawing.Size(60, 21);
            this.comboBoxMonths.TabIndex = 8;
            // 
            // comboBoxDays
            // 
            this.comboBoxDays.Enabled = false;
            this.comboBoxDays.FormattingEnabled = true;
            this.comboBoxDays.Items.AddRange(new object[] {
            "mon",
            "tue",
            "wed",
            "thu",
            "fri",
            "sat",
            "sun"});
            this.comboBoxDays.Location = new System.Drawing.Point(74, 136);
            this.comboBoxDays.Name = "comboBoxDays";
            this.comboBoxDays.Size = new System.Drawing.Size(60, 21);
            this.comboBoxDays.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "DAY : ";
            // 
            // txtFFMC
            // 
            this.txtFFMC.Enabled = false;
            this.txtFFMC.Location = new System.Drawing.Point(74, 163);
            this.txtFFMC.Name = "txtFFMC";
            this.txtFFMC.Size = new System.Drawing.Size(60, 20);
            this.txtFFMC.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "FFMC : ";
            // 
            // txtDMC
            // 
            this.txtDMC.Enabled = false;
            this.txtDMC.Location = new System.Drawing.Point(74, 189);
            this.txtDMC.Name = "txtDMC";
            this.txtDMC.Size = new System.Drawing.Size(60, 20);
            this.txtDMC.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "DMC : ";
            // 
            // txtDC
            // 
            this.txtDC.Enabled = false;
            this.txtDC.Location = new System.Drawing.Point(74, 215);
            this.txtDC.Name = "txtDC";
            this.txtDC.Size = new System.Drawing.Size(60, 20);
            this.txtDC.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 218);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "DC : ";
            // 
            // txtRH
            // 
            this.txtRH.Enabled = false;
            this.txtRH.Location = new System.Drawing.Point(222, 109);
            this.txtRH.Name = "txtRH";
            this.txtRH.Size = new System.Drawing.Size(60, 20);
            this.txtRH.TabIndex = 22;
            this.txtRH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRH_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(170, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "RH : ";
            // 
            // txtTEMP
            // 
            this.txtTEMP.Enabled = false;
            this.txtTEMP.Location = new System.Drawing.Point(222, 83);
            this.txtTEMP.Name = "txtTEMP";
            this.txtTEMP.Size = new System.Drawing.Size(60, 20);
            this.txtTEMP.TabIndex = 20;
            this.txtTEMP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTEMP_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(170, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "TEMP : ";
            // 
            // txtISI
            // 
            this.txtISI.Enabled = false;
            this.txtISI.Location = new System.Drawing.Point(222, 57);
            this.txtISI.Name = "txtISI";
            this.txtISI.Size = new System.Drawing.Size(60, 20);
            this.txtISI.TabIndex = 18;
            this.txtISI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtISI_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(170, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "ISI : ";
            // 
            // txtAREA
            // 
            this.txtAREA.Enabled = false;
            this.txtAREA.Location = new System.Drawing.Point(222, 187);
            this.txtAREA.Name = "txtAREA";
            this.txtAREA.Size = new System.Drawing.Size(60, 20);
            this.txtAREA.TabIndex = 28;
            this.txtAREA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAREA_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(170, 190);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "AREA : ";
            // 
            // txtRAIN
            // 
            this.txtRAIN.Enabled = false;
            this.txtRAIN.Location = new System.Drawing.Point(222, 161);
            this.txtRAIN.Name = "txtRAIN";
            this.txtRAIN.Size = new System.Drawing.Size(60, 20);
            this.txtRAIN.TabIndex = 26;
            this.txtRAIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRAIN_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(170, 164);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "RAIN : ";
            // 
            // txtWIND
            // 
            this.txtWIND.Enabled = false;
            this.txtWIND.Location = new System.Drawing.Point(222, 135);
            this.txtWIND.Name = "txtWIND";
            this.txtWIND.Size = new System.Drawing.Size(60, 20);
            this.txtWIND.TabIndex = 24;
            this.txtWIND.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWIND_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(170, 138);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "WIND : ";
            // 
            // radioButtonEkle
            // 
            this.radioButtonEkle.AutoSize = true;
            this.radioButtonEkle.Checked = true;
            this.radioButtonEkle.Enabled = false;
            this.radioButtonEkle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.radioButtonEkle.Location = new System.Drawing.Point(12, 12);
            this.radioButtonEkle.Name = "radioButtonEkle";
            this.radioButtonEkle.Size = new System.Drawing.Size(124, 21);
            this.radioButtonEkle.TabIndex = 29;
            this.radioButtonEkle.TabStop = true;
            this.radioButtonEkle.Text = "Ekleme İşlemi";
            this.radioButtonEkle.UseVisualStyleBackColor = true;
            this.radioButtonEkle.CheckedChanged += new System.EventHandler(this.radioButtonEkle_CheckedChanged);
            // 
            // radioButtonGuncelle
            // 
            this.radioButtonGuncelle.AutoSize = true;
            this.radioButtonGuncelle.Enabled = false;
            this.radioButtonGuncelle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.radioButtonGuncelle.Location = new System.Drawing.Point(142, 12);
            this.radioButtonGuncelle.Name = "radioButtonGuncelle";
            this.radioButtonGuncelle.Size = new System.Drawing.Size(157, 21);
            this.radioButtonGuncelle.TabIndex = 30;
            this.radioButtonGuncelle.Text = "Güncelleme İşlemi";
            this.radioButtonGuncelle.UseVisualStyleBackColor = true;
            // 
            // btnEkleKaydet
            // 
            this.btnEkleKaydet.BackColor = System.Drawing.Color.MistyRose;
            this.btnEkleKaydet.Enabled = false;
            this.btnEkleKaydet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnEkleKaydet.Location = new System.Drawing.Point(159, 215);
            this.btnEkleKaydet.Name = "btnEkleKaydet";
            this.btnEkleKaydet.Size = new System.Drawing.Size(123, 36);
            this.btnEkleKaydet.TabIndex = 31;
            this.btnEkleKaydet.Text = "EKLE / KAYDET";
            this.btnEkleKaydet.UseVisualStyleBackColor = false;
            this.btnEkleKaydet.Click += new System.EventHandler(this.btnEkleKaydet_Click);
            // 
            // btnCikis
            // 
            this.btnCikis.BackColor = System.Drawing.Color.MistyRose;
            this.btnCikis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCikis.Location = new System.Drawing.Point(1102, 12);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.Size = new System.Drawing.Size(123, 36);
            this.btnCikis.TabIndex = 32;
            this.btnCikis.Text = "ÇIKIŞ";
            this.btnCikis.UseVisualStyleBackColor = false;
            this.btnCikis.Click += new System.EventHandler(this.btnCikis_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1237, 576);
            this.Controls.Add(this.btnCikis);
            this.Controls.Add(this.btnEkleKaydet);
            this.Controls.Add(this.radioButtonGuncelle);
            this.Controls.Add(this.radioButtonEkle);
            this.Controls.Add(this.txtAREA);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtRAIN);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtWIND);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtRH);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtTEMP);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtISI);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtDC);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDMC);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFFMC);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxDays);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxMonths);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximumSize = new System.Drawing.Size(1257, 619);
            this.MinimumSize = new System.Drawing.Size(1257, 619);
            this.Name = "Form1";
            this.Text = "Veri İşleme";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxMonths;
        private System.Windows.Forms.ComboBox comboBoxDays;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFFMC;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDMC;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDC;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRH;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTEMP;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtISI;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtAREA;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRAIN;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtWIND;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RadioButton radioButtonEkle;
        private System.Windows.Forms.RadioButton radioButtonGuncelle;
        private System.Windows.Forms.Button btnEkleKaydet;
        private System.Windows.Forms.Button btnCikis;
    }
}

