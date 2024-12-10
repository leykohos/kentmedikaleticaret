namespace EticaretYonetimi
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            urunadi = new TextBox();
            eskifiyat = new TextBox();
            yenifiyat = new TextBox();
            gorsellink = new TextBox();
            tagtxt = new TextBox();
            ekleBtn = new Button();
            indirimcheck = new CheckBox();
            urunler = new ListBox();
            listeleBtn = new Button();
            guncelle = new Button();
            silmebtn = new Button();
            urunlabel = new Label();
            uruneski = new Label();
            picturebox = new PictureBox();
            urunyeni = new Label();
            button1 = new Button();
            uruntag = new Label();
            groupBox1 = new GroupBox();
            label7 = new Label();
            stoknumeric = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)picturebox).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)stoknumeric).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.Location = new Point(48, 139);
            label1.Name = "label1";
            label1.Size = new Size(98, 28);
            label1.TabIndex = 0;
            label1.Text = "Ürün adı:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.Location = new Point(41, 176);
            label2.Name = "label2";
            label2.Size = new Size(106, 28);
            label2.TabIndex = 1;
            label2.Text = "Eski Fiyat:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.Location = new Point(-1, 207);
            label3.Name = "label3";
            label3.Size = new Size(149, 28);
            label3.TabIndex = 2;
            label3.Text = "İndirimli Fiyat:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label4.Location = new Point(26, 238);
            label4.Name = "label4";
            label4.Size = new Size(123, 28);
            label4.TabIndex = 3;
            label4.Text = "Görsel Link:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label5.Location = new Point(76, 279);
            label5.Name = "label5";
            label5.Size = new Size(72, 28);
            label5.TabIndex = 4;
            label5.Text = "Etiket:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label6.Location = new Point(34, 357);
            label6.Name = "label6";
            label6.Size = new Size(117, 28);
            label6.TabIndex = 5;
            label6.Text = "Flash Ürün:";
            // 
            // urunadi
            // 
            urunadi.Location = new Point(165, 139);
            urunadi.Name = "urunadi";
            urunadi.Size = new Size(125, 27);
            urunadi.TabIndex = 6;
            // 
            // eskifiyat
            // 
            eskifiyat.Location = new Point(165, 176);
            eskifiyat.Name = "eskifiyat";
            eskifiyat.Size = new Size(125, 27);
            eskifiyat.TabIndex = 7;
            // 
            // yenifiyat
            // 
            yenifiyat.Location = new Point(165, 207);
            yenifiyat.Name = "yenifiyat";
            yenifiyat.Size = new Size(125, 27);
            yenifiyat.TabIndex = 8;
            // 
            // gorsellink
            // 
            gorsellink.Location = new Point(165, 238);
            gorsellink.Name = "gorsellink";
            gorsellink.Size = new Size(125, 27);
            gorsellink.TabIndex = 9;
            // 
            // tagtxt
            // 
            tagtxt.Location = new Point(165, 279);
            tagtxt.Name = "tagtxt";
            tagtxt.Size = new Size(125, 27);
            tagtxt.TabIndex = 10;
            // 
            // ekleBtn
            // 
            ekleBtn.Location = new Point(16, 400);
            ekleBtn.Name = "ekleBtn";
            ekleBtn.Size = new Size(257, 79);
            ekleBtn.TabIndex = 12;
            ekleBtn.Text = "Ekle";
            ekleBtn.UseVisualStyleBackColor = true;
            ekleBtn.Click += ekleBtn_Click;
            // 
            // indirimcheck
            // 
            indirimcheck.AutoSize = true;
            indirimcheck.Location = new Point(167, 363);
            indirimcheck.Name = "indirimcheck";
            indirimcheck.Size = new Size(18, 17);
            indirimcheck.TabIndex = 13;
            indirimcheck.UseVisualStyleBackColor = true;
            // 
            // urunler
            // 
            urunler.FormattingEnabled = true;
            urunler.Location = new Point(316, 12);
            urunler.Name = "urunler";
            urunler.Size = new Size(474, 544);
            urunler.TabIndex = 14;
            urunler.DoubleClick += urunler_DoubleClick;
            // 
            // listeleBtn
            // 
            listeleBtn.Location = new Point(316, 582);
            listeleBtn.Name = "listeleBtn";
            listeleBtn.Size = new Size(474, 79);
            listeleBtn.TabIndex = 15;
            listeleBtn.Text = "Listele";
            listeleBtn.UseVisualStyleBackColor = true;
            listeleBtn.Click += listeleBtn_Click;
            // 
            // guncelle
            // 
            guncelle.Location = new Point(16, 493);
            guncelle.Name = "guncelle";
            guncelle.Size = new Size(257, 79);
            guncelle.TabIndex = 22;
            guncelle.Text = "Güncelle";
            guncelle.UseVisualStyleBackColor = true;
            guncelle.Click += guncelle_Click;
            // 
            // silmebtn
            // 
            silmebtn.Location = new Point(16, 582);
            silmebtn.Name = "silmebtn";
            silmebtn.Size = new Size(257, 79);
            silmebtn.TabIndex = 23;
            silmebtn.Text = "Sil";
            silmebtn.UseVisualStyleBackColor = true;
            silmebtn.Click += silmebtn_Click;
            // 
            // urunlabel
            // 
            urunlabel.AutoSize = true;
            urunlabel.BackColor = Color.Transparent;
            urunlabel.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            urunlabel.ForeColor = Color.Black;
            urunlabel.Location = new Point(17, 461);
            urunlabel.Name = "urunlabel";
            urunlabel.Size = new Size(135, 38);
            urunlabel.TabIndex = 17;
            urunlabel.Text = "Ürün Adı";
            // 
            // uruneski
            // 
            uruneski.AutoSize = true;
            uruneski.BackColor = Color.Transparent;
            uruneski.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Strikeout, GraphicsUnit.Point, 162);
            uruneski.ForeColor = Color.Red;
            uruneski.Location = new Point(17, 499);
            uruneski.Name = "uruneski";
            uruneski.Size = new Size(101, 28);
            uruneski.TabIndex = 19;
            uruneski.Text = "Eski Fiyat";
            // 
            // picturebox
            // 
            picturebox.Location = new Point(17, 26);
            picturebox.Name = "picturebox";
            picturebox.Size = new Size(267, 402);
            picturebox.SizeMode = PictureBoxSizeMode.CenterImage;
            picturebox.TabIndex = 16;
            picturebox.TabStop = false;
            // 
            // urunyeni
            // 
            urunyeni.AutoSize = true;
            urunyeni.BackColor = Color.Transparent;
            urunyeni.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            urunyeni.ForeColor = Color.FromArgb(255, 128, 0);
            urunyeni.Location = new Point(17, 527);
            urunyeni.Name = "urunyeni";
            urunyeni.Size = new Size(153, 41);
            urunyeni.TabIndex = 20;
            urunyeni.Text = "Yeni Fiyat";
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button1.Location = new Point(17, 587);
            button1.Name = "button1";
            button1.Size = new Size(267, 54);
            button1.TabIndex = 21;
            button1.Text = "Sepete Ekle";
            button1.UseVisualStyleBackColor = true;
            // 
            // uruntag
            // 
            uruntag.AutoSize = true;
            uruntag.BackColor = Color.Transparent;
            uruntag.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            uruntag.Location = new Point(41, 45);
            uruntag.Name = "uruntag";
            uruntag.Size = new Size(0, 31);
            uruntag.TabIndex = 24;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(uruntag);
            groupBox1.Controls.Add(urunlabel);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(uruneski);
            groupBox1.Controls.Add(urunyeni);
            groupBox1.Controls.Add(picturebox);
            groupBox1.ForeColor = Color.Orange;
            groupBox1.Location = new Point(812, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(322, 663);
            groupBox1.TabIndex = 25;
            groupBox1.TabStop = false;
            groupBox1.Text = "Ürün Önizlemesi";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label7.Location = new Point(41, 316);
            label7.Name = "label7";
            label7.Size = new Size(110, 28);
            label7.TabIndex = 26;
            label7.Text = "Stok Adet:";
            // 
            // stoknumeric
            // 
            stoknumeric.Location = new Point(165, 317);
            stoknumeric.Name = "stoknumeric";
            stoknumeric.Size = new Size(128, 27);
            stoknumeric.TabIndex = 27;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1557, 686);
            Controls.Add(stoknumeric);
            Controls.Add(label7);
            Controls.Add(groupBox1);
            Controls.Add(silmebtn);
            Controls.Add(guncelle);
            Controls.Add(listeleBtn);
            Controls.Add(urunler);
            Controls.Add(indirimcheck);
            Controls.Add(ekleBtn);
            Controls.Add(tagtxt);
            Controls.Add(gorsellink);
            Controls.Add(yenifiyat);
            Controls.Add(eskifiyat);
            Controls.Add(urunadi);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Form1";
            StartPosition = FormStartPosition.WindowsDefaultBounds;
            Text = "Kent Medikal Ürün Yönetim Paneli";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)picturebox).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)stoknumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox urunadi;
        private TextBox eskifiyat;
        private TextBox yenifiyat;
        private TextBox gorsellink;
        private TextBox tagtxt;
        private Button ekleBtn;
        private CheckBox indirimcheck;
        private ListBox urunler;
        private Button listeleBtn;
        private Button guncelle;
        private Button silmebtn;
        private Label urunlabel;
        private Label uruneski;
        private PictureBox picturebox;
        private Label urunyeni;
        private Button button1;
        private Label uruntag;
        private GroupBox groupBox1;
        private Label label7;
        private NumericUpDown stoknumeric;
    }
}
