namespace BagliListe
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
            this.btnListeOlustur = new System.Windows.Forms.Button();
            this.btnDugumEkle = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnListeOlustur
            // 
            this.btnListeOlustur.Location = new System.Drawing.Point(56, 133);
            this.btnListeOlustur.Name = "btnListeOlustur";
            this.btnListeOlustur.Size = new System.Drawing.Size(151, 41);
            this.btnListeOlustur.TabIndex = 0;
            this.btnListeOlustur.Text = "Liste Oluştur";
            this.btnListeOlustur.UseVisualStyleBackColor = true;
            this.btnListeOlustur.Click += new System.EventHandler(this.btnListeOlustur_Click);
            // 
            // btnDugumEkle
            // 
            this.btnDugumEkle.Location = new System.Drawing.Point(56, 298);
            this.btnDugumEkle.Name = "btnDugumEkle";
            this.btnDugumEkle.Size = new System.Drawing.Size(151, 41);
            this.btnDugumEkle.TabIndex = 2;
            this.btnDugumEkle.Text = "Düğüm Ekle";
            this.btnDugumEkle.UseVisualStyleBackColor = true;
            this.btnDugumEkle.Click += new System.EventHandler(this.btnDugumEkle_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(56, 192);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(821, 37);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(56, 356);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(821, 37);
            this.panel2.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 540);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnDugumEkle);
            this.Controls.Add(this.btnListeOlustur);
            this.MaximumSize = new System.Drawing.Size(905, 579);
            this.MinimumSize = new System.Drawing.Size(905, 579);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnListeOlustur;
        private System.Windows.Forms.Button btnDugumEkle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

