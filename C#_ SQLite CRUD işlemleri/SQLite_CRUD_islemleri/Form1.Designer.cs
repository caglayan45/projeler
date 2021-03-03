namespace SQLite_CRUD_islemleri
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
            this.dgvOgrenciler = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOgrenciEkleNumara = new System.Windows.Forms.TextBox();
            this.txtOgrenciEkleSoyad = new System.Windows.Forms.TextBox();
            this.txtOgrenciEkleAd = new System.Windows.Forms.TextBox();
            this.btnOgrenciEkle = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOgrenciGuncelle = new System.Windows.Forms.Button();
            this.txtOgrenciGuncelleAd = new System.Windows.Forms.TextBox();
            this.txtOgrenciGuncelleSoyad = new System.Windows.Forms.TextBox();
            this.txtOgrenciGuncelleNumara = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOgrenciGuncelleId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnOgrenciSil = new System.Windows.Forms.Button();
            this.txtOgrenciSilId = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnOgrenciBulId = new System.Windows.Forms.Button();
            this.txtOgrenciBulId = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOgrenciler)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvOgrenciler
            // 
            this.dgvOgrenciler.BackgroundColor = System.Drawing.Color.White;
            this.dgvOgrenciler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOgrenciler.Location = new System.Drawing.Point(12, 12);
            this.dgvOgrenciler.Name = "dgvOgrenciler";
            this.dgvOgrenciler.ReadOnly = true;
            this.dgvOgrenciler.Size = new System.Drawing.Size(566, 183);
            this.dgvOgrenciler.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOgrenciEkle);
            this.groupBox1.Controls.Add(this.txtOgrenciEkleAd);
            this.groupBox1.Controls.Add(this.txtOgrenciEkleSoyad);
            this.groupBox1.Controls.Add(this.txtOgrenciEkleNumara);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 201);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 162);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Öğrenci Ekle";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ad : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Soyad : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(6, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Numara : ";
            // 
            // txtOgrenciEkleNumara
            // 
            this.txtOgrenciEkleNumara.Location = new System.Drawing.Point(82, 76);
            this.txtOgrenciEkleNumara.Name = "txtOgrenciEkleNumara";
            this.txtOgrenciEkleNumara.Size = new System.Drawing.Size(192, 20);
            this.txtOgrenciEkleNumara.TabIndex = 3;
            // 
            // txtOgrenciEkleSoyad
            // 
            this.txtOgrenciEkleSoyad.Location = new System.Drawing.Point(82, 50);
            this.txtOgrenciEkleSoyad.Name = "txtOgrenciEkleSoyad";
            this.txtOgrenciEkleSoyad.Size = new System.Drawing.Size(192, 20);
            this.txtOgrenciEkleSoyad.TabIndex = 4;
            // 
            // txtOgrenciEkleAd
            // 
            this.txtOgrenciEkleAd.Location = new System.Drawing.Point(82, 24);
            this.txtOgrenciEkleAd.Name = "txtOgrenciEkleAd";
            this.txtOgrenciEkleAd.Size = new System.Drawing.Size(192, 20);
            this.txtOgrenciEkleAd.TabIndex = 5;
            // 
            // btnOgrenciEkle
            // 
            this.btnOgrenciEkle.Location = new System.Drawing.Point(82, 102);
            this.btnOgrenciEkle.Name = "btnOgrenciEkle";
            this.btnOgrenciEkle.Size = new System.Drawing.Size(192, 23);
            this.btnOgrenciEkle.TabIndex = 6;
            this.btnOgrenciEkle.Text = "EKLE";
            this.btnOgrenciEkle.UseVisualStyleBackColor = true;
            this.btnOgrenciEkle.Click += new System.EventHandler(this.btnOgrenciEkle_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtOgrenciGuncelleId);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnOgrenciGuncelle);
            this.groupBox2.Controls.Add(this.txtOgrenciGuncelleAd);
            this.groupBox2.Controls.Add(this.txtOgrenciGuncelleSoyad);
            this.groupBox2.Controls.Add(this.txtOgrenciGuncelleNumara);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(298, 201);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 162);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Öğrenci Güncelle";
            // 
            // btnOgrenciGuncelle
            // 
            this.btnOgrenciGuncelle.Location = new System.Drawing.Point(82, 128);
            this.btnOgrenciGuncelle.Name = "btnOgrenciGuncelle";
            this.btnOgrenciGuncelle.Size = new System.Drawing.Size(192, 23);
            this.btnOgrenciGuncelle.TabIndex = 6;
            this.btnOgrenciGuncelle.Text = "GÜNCELLE";
            this.btnOgrenciGuncelle.UseVisualStyleBackColor = true;
            this.btnOgrenciGuncelle.Click += new System.EventHandler(this.btnOgrenciGuncelle_Click);
            // 
            // txtOgrenciGuncelleAd
            // 
            this.txtOgrenciGuncelleAd.Location = new System.Drawing.Point(82, 50);
            this.txtOgrenciGuncelleAd.Name = "txtOgrenciGuncelleAd";
            this.txtOgrenciGuncelleAd.Size = new System.Drawing.Size(192, 20);
            this.txtOgrenciGuncelleAd.TabIndex = 5;
            // 
            // txtOgrenciGuncelleSoyad
            // 
            this.txtOgrenciGuncelleSoyad.Location = new System.Drawing.Point(82, 76);
            this.txtOgrenciGuncelleSoyad.Name = "txtOgrenciGuncelleSoyad";
            this.txtOgrenciGuncelleSoyad.Size = new System.Drawing.Size(192, 20);
            this.txtOgrenciGuncelleSoyad.TabIndex = 4;
            // 
            // txtOgrenciGuncelleNumara
            // 
            this.txtOgrenciGuncelleNumara.Location = new System.Drawing.Point(82, 102);
            this.txtOgrenciGuncelleNumara.Name = "txtOgrenciGuncelleNumara";
            this.txtOgrenciGuncelleNumara.Size = new System.Drawing.Size(192, 20);
            this.txtOgrenciGuncelleNumara.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(6, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Numara : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(6, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "Soyad : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(6, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Ad : ";
            // 
            // txtOgrenciGuncelleId
            // 
            this.txtOgrenciGuncelleId.Location = new System.Drawing.Point(82, 24);
            this.txtOgrenciGuncelleId.Name = "txtOgrenciGuncelleId";
            this.txtOgrenciGuncelleId.Size = new System.Drawing.Size(192, 20);
            this.txtOgrenciGuncelleId.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(6, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "ID : ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnOgrenciSil);
            this.groupBox3.Controls.Add(this.txtOgrenciSilId);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(12, 369);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(280, 83);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Öğrenci Sil";
            // 
            // btnOgrenciSil
            // 
            this.btnOgrenciSil.Location = new System.Drawing.Point(82, 50);
            this.btnOgrenciSil.Name = "btnOgrenciSil";
            this.btnOgrenciSil.Size = new System.Drawing.Size(192, 23);
            this.btnOgrenciSil.TabIndex = 6;
            this.btnOgrenciSil.Text = "SİL";
            this.btnOgrenciSil.UseVisualStyleBackColor = true;
            this.btnOgrenciSil.Click += new System.EventHandler(this.btnOgrenciSil_Click);
            // 
            // txtOgrenciSilId
            // 
            this.txtOgrenciSilId.Location = new System.Drawing.Point(82, 24);
            this.txtOgrenciSilId.Name = "txtOgrenciSilId";
            this.txtOgrenciSilId.Size = new System.Drawing.Size(192, 20);
            this.txtOgrenciSilId.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.Location = new System.Drawing.Point(6, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "ID : ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnOgrenciBulId);
            this.groupBox4.Controls.Add(this.txtOgrenciBulId);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(298, 369);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(280, 77);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Öğrenci Bul";
            // 
            // btnOgrenciBulId
            // 
            this.btnOgrenciBulId.Location = new System.Drawing.Point(82, 50);
            this.btnOgrenciBulId.Name = "btnOgrenciBulId";
            this.btnOgrenciBulId.Size = new System.Drawing.Size(192, 23);
            this.btnOgrenciBulId.TabIndex = 6;
            this.btnOgrenciBulId.Text = "BUL";
            this.btnOgrenciBulId.UseVisualStyleBackColor = true;
            this.btnOgrenciBulId.Click += new System.EventHandler(this.btnOgrenciBulId_Click);
            // 
            // txtOgrenciBulId
            // 
            this.txtOgrenciBulId.Location = new System.Drawing.Point(82, 24);
            this.txtOgrenciBulId.Name = "txtOgrenciBulId";
            this.txtOgrenciBulId.Size = new System.Drawing.Size(192, 20);
            this.txtOgrenciBulId.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(6, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "ID : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(589, 455);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvOgrenciler);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Öğrenci Kayıt İşlemleri";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOgrenciler)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOgrenciler;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOgrenciEkle;
        private System.Windows.Forms.TextBox txtOgrenciEkleAd;
        private System.Windows.Forms.TextBox txtOgrenciEkleSoyad;
        private System.Windows.Forms.TextBox txtOgrenciEkleNumara;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtOgrenciGuncelleId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnOgrenciGuncelle;
        private System.Windows.Forms.TextBox txtOgrenciGuncelleAd;
        private System.Windows.Forms.TextBox txtOgrenciGuncelleSoyad;
        private System.Windows.Forms.TextBox txtOgrenciGuncelleNumara;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnOgrenciSil;
        private System.Windows.Forms.TextBox txtOgrenciSilId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnOgrenciBulId;
        private System.Windows.Forms.TextBox txtOgrenciBulId;
        private System.Windows.Forms.Label label8;
    }
}

