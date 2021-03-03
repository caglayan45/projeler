namespace veriYapilariVize
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonOlustur = new System.Windows.Forms.Button();
            this.txtDugumSayisi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeger = new System.Windows.Forms.TextBox();
            this.buttonEkle = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtYer = new System.Windows.Forms.TextBox();
            this.lblGroupBox1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblGroupBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1223, 128);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Liste 1 : ";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(12, 298);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1223, 128);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Liste 2 : ";
            // 
            // buttonOlustur
            // 
            this.buttonOlustur.BackColor = System.Drawing.Color.LightCoral;
            this.buttonOlustur.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonOlustur.Location = new System.Drawing.Point(12, 172);
            this.buttonOlustur.Name = "buttonOlustur";
            this.buttonOlustur.Size = new System.Drawing.Size(156, 41);
            this.buttonOlustur.TabIndex = 2;
            this.buttonOlustur.Text = "OLUŞTUR";
            this.buttonOlustur.UseVisualStyleBackColor = false;
            this.buttonOlustur.Click += new System.EventHandler(this.buttonOlustur_Click);
            // 
            // txtDugumSayisi
            // 
            this.txtDugumSayisi.Location = new System.Drawing.Point(96, 146);
            this.txtDugumSayisi.Name = "txtDugumSayisi";
            this.txtDugumSayisi.Size = new System.Drawing.Size(100, 20);
            this.txtDugumSayisi.TabIndex = 3;
            this.txtDugumSayisi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDugumSayisi_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Düğüm Sayısı : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 435);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Düğüm Değeri : ";
            // 
            // txtDeger
            // 
            this.txtDeger.Location = new System.Drawing.Point(96, 432);
            this.txtDeger.Name = "txtDeger";
            this.txtDeger.Size = new System.Drawing.Size(100, 20);
            this.txtDeger.TabIndex = 6;
            this.txtDeger.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDeger_KeyPress);
            // 
            // buttonEkle
            // 
            this.buttonEkle.BackColor = System.Drawing.Color.LightCoral;
            this.buttonEkle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonEkle.Location = new System.Drawing.Point(12, 458);
            this.buttonEkle.Name = "buttonEkle";
            this.buttonEkle.Size = new System.Drawing.Size(156, 41);
            this.buttonEkle.TabIndex = 5;
            this.buttonEkle.Text = "EKLE";
            this.buttonEkle.UseVisualStyleBackColor = false;
            this.buttonEkle.Click += new System.EventHandler(this.buttonEkle_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(202, 435);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Hangi Düğümden Sonra : ";
            // 
            // txtYer
            // 
            this.txtYer.Location = new System.Drawing.Point(338, 432);
            this.txtYer.Name = "txtYer";
            this.txtYer.Size = new System.Drawing.Size(100, 20);
            this.txtYer.TabIndex = 8;
            this.txtYer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYer_KeyPress);
            // 
            // lblGroupBox1
            // 
            this.lblGroupBox1.AutoSize = true;
            this.lblGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblGroupBox1.Location = new System.Drawing.Point(3, 54);
            this.lblGroupBox1.Name = "lblGroupBox1";
            this.lblGroupBox1.Size = new System.Drawing.Size(0, 17);
            this.lblGroupBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1247, 566);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtYer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDeger);
            this.Controls.Add(this.buttonEkle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDugumSayisi);
            this.Controls.Add(this.buttonOlustur);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximumSize = new System.Drawing.Size(1267, 609);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonOlustur;
        private System.Windows.Forms.TextBox txtDugumSayisi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDeger;
        private System.Windows.Forms.Button buttonEkle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtYer;
        private System.Windows.Forms.Label lblGroupBox1;
    }
}

