using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EticaretYonetimi
{
    public partial class Form1 : Form
    {

        private islem firestoreIslem;
        public Form1()
        {
            InitializeComponent();
            firestoreIslem = new islem();
            ListeyiGuncelle();
            ListeleFlashUrunler();
        }
      
        private void ekleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string isim = urunadi.Text;
                string image = gorsellink.Text;
                string discount = yenifiyat.Text;
                string original = eskifiyat.Text;
                string tag = tagtxt.Text;
                bool indirim = indirimcheck.Checked;
                string adet = stoknumeric.Value.ToString();
                string markaisim = markatxt.Text;
                if (indirim)//indirim true ise kontrolleri yap sonras�nda i�lemi ger�ekle�tir. True ise flash �r�n olmu� oluyor ve hi�bir textbox bo� b�rak�lamaz.
                {
                    if (string.IsNullOrEmpty(isim) || string.IsNullOrEmpty(image) || string.IsNullOrEmpty(discount) ||
                    string.IsNullOrEmpty(original) || string.IsNullOrEmpty(tag) || string.IsNullOrEmpty(adet) ||
                    string.IsNullOrEmpty(markaisim))
                    {
                        MessageBox.Show("L�tfen t�m alanlar� doldurun!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        firestoreIslem.veriEkleme(isim, image, discount, original, tag, indirim, adet, markaisim);
                        MessageBox.Show("Veri ba�ar�yla Firestore'a eklendi!", "Ba�ar�l�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListeyiGuncelle();
                        ListeleFlashUrunler();
                        temizleme();
                        onizlemeguncelle();
                    }
                }
                else//false d�nm�� oldu bu y�zden indirimli fiyat kontrolu yapm�yoruz.
                {
                    if (string.IsNullOrEmpty(isim) || string.IsNullOrEmpty(image) ||string.IsNullOrEmpty(original) || 
                        string.IsNullOrEmpty(tag) || string.IsNullOrEmpty(adet) ||
                    string.IsNullOrEmpty(markaisim))
                    {
                        MessageBox.Show("L�tfen t�m alanlar� doldurun!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        firestoreIslem.veriEkleme(isim, image, discount, original, tag, indirim, adet, markaisim);
                        MessageBox.Show("Veri ba�ar�yla Firestore'a eklendi!", "Ba�ar�l�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListeyiGuncelle();
                        temizleme();
                        onizlemeguncelle();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri eklenirken bir hata olu�tu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void ListeyiGuncelle()
        {
            try
            {
                //listeyi al  
                var products = await firestoreIslem.veriListele();

                urunler.Items.Clear();
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        if (product.ContainsKey("title") && product.ContainsKey("flash")) 
                        {
                            bool isFlash = Convert.ToBoolean(product["flash"]);
                            if (isFlash == false)
                            {
                                urunler.Items.Add(new ListBoxItem
                                {
                                    DisplayName = product["title"].ToString(),
                                    ProductName = product["brand"].ToString(),
                                    ProductID = product["ID"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Listeleme s�ras�nda hata olu�tu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void ListeleFlashUrunler()
        {
            try
            {
                //listeyi al
                var products = await firestoreIslem.veriListele();

                flashurunler.Items.Clear();
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        if (product.ContainsKey("title") && product.ContainsKey("flash"))
                        {
                            bool isFlash = Convert.ToBoolean(product["flash"]);

                            if (isFlash)
                            {
                                flashurunler.Items.Add(new ListBoxItem
                                {
                                    DisplayName = product["title"].ToString(),
                                    ProductName = product["brand"].ToString(),
                                    ProductID = product["ID"].ToString()
                                }); ;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Listeleme s�ras�nda hata olu�tu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listeleBtn_Click(object sender, EventArgs e)
        {
            ListeyiGuncelle();
            ListeleFlashUrunler();
            onizlemeguncelle();
            temizleme();
        }

        private async void urunler_DoubleClick(object sender, EventArgs e)
        {
           
            try
            {
                flashurunler.SelectedIndex = -1;
                if (urunler.SelectedItem != null && urunler.SelectedItem is ListBoxItem selectedItem)
                {
                    var productDetails = await firestoreIslem.urunDetaylariniAl(selectedItem.ProductID);
                    onizlemeguncelle();
                    if (productDetails != null)
                    {
                        if (productDetails.ContainsKey("imageUrl") && productDetails["imageUrl"] != null)// verinin gelip gelmedi�i kontrol
                        {
                            string imageUrl = productDetails["imageUrl"].ToString();
                            await firestoreIslem.gorselCekme(imageUrl, pictureBox1);
                        }
                        else
                        {
                            MessageBox.Show("Resim URL'si ge�ersiz veya mevcut de�il.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        
                        if (productDetails.ContainsKey("title"))//textbox
                            urunadi.Text = productDetails["title"].ToString();
                        if (productDetails.ContainsKey("title"))
                            uruntitlelbl.Text = productDetails["title"].ToString(); //onizleme title
                        if (productDetails.ContainsKey("originalPrice"))
                            eskifiyat.Text = productDetails["originalPrice"].ToString();
                        if (productDetails.ContainsKey("originalPrice"))
                            urunfiyatlbl.Text = productDetails["originalPrice"].ToString() + "TL";
                        if (productDetails.ContainsKey("tag"))
                            tagtxt.Text = productDetails["tag"].ToString();
                        if (productDetails.ContainsKey("tag"))
                            tag1.Text = productDetails["tag"].ToString();
                        if (productDetails.ContainsKey("flash"))
                            indirimcheck.Checked = Convert.ToBoolean(productDetails["flash"]);
                        if (productDetails.ContainsKey("imageUrl"))
                            gorsellink.Text = productDetails["imageUrl"].ToString();
                        if (productDetails.ContainsKey("quantity"))
                            stoknumeric.Value = Convert.ToInt32(productDetails["quantity"]);
                        if (productDetails.ContainsKey("brand"))
                            markatxt.Text = productDetails["brand"].ToString();
                        if (productDetails.ContainsKey("brand"))
                            urunmarkalbl.Text = productDetails["brand"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("�r�n detaylar� y�klenirken bir hata olu�tu veya �r�n bulunamad�.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    flashkontrol();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�r�n detaylar� y�klenirken hata olu�tu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void flashkontrol()
        {
            if (indirimcheck.Checked == false)
            {
                yenifiyat.Enabled = false;
            }
            if (indirimcheck.Checked == true)
            {
                yenifiyat.Enabled = true;
            }

        }
        private async void guncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (urunler.SelectedItem != null && urunler.SelectedItem is ListBoxItem selectedItem)
                {
                   
                    string yeniIsim = urunadi.Text;
                    string yeniGorsel = gorsellink.Text;
                    string yeniIndirimFiyat = yenifiyat.Text;
                    string yeniOriginalFiyat = eskifiyat.Text;
                    string yeniTag = tagtxt.Text;
                    bool yeniIndirim = indirimcheck.Checked;
                    string yeniAdet = stoknumeric.Value.ToString();
                    string yeniMarka = markatxt.Text;
                    
                        if(string.IsNullOrEmpty(yeniIsim) || string.IsNullOrEmpty(yeniGorsel) ||  string.IsNullOrEmpty(yeniOriginalFiyat) || 
                            string.IsNullOrEmpty(yeniTag) || string.IsNullOrEmpty(yeniAdet) || string.IsNullOrEmpty(yeniMarka) || yeniIndirim == true && string.IsNullOrEmpty(yeniIndirimFiyat)) 
                        {
                        MessageBox.Show("L�tfen t�m alanlar� doldurun!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                         {
                             bool result = await firestoreIslem.veriGuncelleme(
                             selectedItem.ProductID,
                              yeniIsim,
                              yeniGorsel,
                              yeniIndirimFiyat,
                              yeniOriginalFiyat,
                              yeniTag,
                              yeniIndirim,
                              yeniAdet,
                              yeniMarka
);
                        if (result)
                        {
                            MessageBox.Show("�r�n ba�ar�yla g�ncellendi.", "Ba�ar�l�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListeyiGuncelle(); 
                            ListeleFlashUrunler();
                            temizleme();
                            onizlemeguncelle();
                        }
                        else
                        {
                            MessageBox.Show("�r�n g�ncellenirken bir hata olu�tu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else if (flashurunler.SelectedItem != null && flashurunler.SelectedItem is ListBoxItem selectedItemb)
                {
                    
                    string yeniIsim = urunadi.Text;
                    string yeniImage = gorsellink.Text;
                    string yeniDiscount = yenifiyat.Text;
                    string yeniOriginal = eskifiyat.Text;
                    string yeniTag = tagtxt.Text;
                    bool yeniIndirim = indirimcheck.Checked;
                    string yeniAdet = stoknumeric.Value.ToString();
                    string yeniMarka = markatxt.Text;
                    if (string.IsNullOrEmpty(yeniIsim) || string.IsNullOrEmpty(yeniImage) ||  string.IsNullOrEmpty(yeniOriginal) || string.IsNullOrEmpty(yeniDiscount) ||
                           string.IsNullOrEmpty(yeniTag) || string.IsNullOrEmpty(yeniAdet) || string.IsNullOrEmpty(yeniMarka))
                    {
                        MessageBox.Show("L�tfen t�m alanlar� doldurun!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        bool result = await firestoreIslem.veriGuncelleme(
                        selectedItemb.ProductID,
                        yeniIsim,
                        yeniImage,
                        yeniDiscount,
                        yeniOriginal,
                        yeniTag,
                        yeniIndirim,
                        yeniAdet,
                        yeniMarka
                        );
                        if (result)
                        {
                            MessageBox.Show("�r�n ba�ar�yla g�ncellendi.", "Ba�ar�l�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListeyiGuncelle();
                            ListeleFlashUrunler();
                            temizleme();
                            onizlemeguncelle();
                        }
                        else
                        {
                            MessageBox.Show("�r�n g�ncellenirken bir hata olu�tu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("L�tfen bir �r�n se�in.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"G�ncelleme s�ras�nda bir hata olu�tu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void silmebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (urunler.SelectedItem != null && urunler.SelectedItem is ListBoxItem selectedItem)
                {
                    var result = MessageBox.Show("Bu �r�n� silmek istedi�inizden emin misiniz?", "Silme Onay�", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        bool deleteResult = await firestoreIslem.veriSilme(selectedItem.ProductID);

                        if (deleteResult)
                        {
                            MessageBox.Show("�r�n ba�ar�yla silindi.", "Ba�ar�l�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListeyiGuncelle(); 
                            ListeleFlashUrunler();
                            onizlemeguncelle();
                            temizleme();
                        }
                        else
                        {
                            MessageBox.Show("�r�n silinirken bir hata olu�tu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if(flashurunler.SelectedItem != null && flashurunler.SelectedItem is ListBoxItem selectedItema)
                {
                    var result = MessageBox.Show("Bu �r�n� silmek istedi�inizden emin misiniz?", "Silme Onay�", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        bool deleteResult = await firestoreIslem.veriSilme(selectedItema.ProductID);

                        if (deleteResult)
                        {
                            MessageBox.Show("�r�n ba�ar�yla silindi.", "Ba�ar�l�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListeyiGuncelle(); 
                            ListeleFlashUrunler();
                            onizlemeguncelle();
                            temizleme();
                        }
                        else
                        {
                            MessageBox.Show("�r�n silinirken bir hata olu�tu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                else
                {
                    MessageBox.Show("L�tfen bir �r�n se�in.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Silme s�ras�nda bir hata olu�tu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void onizlemeguncelle()
        {
            picturebox.Image = null;
            urunlabel.Text = "�r�n Ad�";
            uruneski.Text = "Eski Fiyat";
            urunyeni.Text = "Yeni Fiyat";
            uruntag.Text = string.Empty;
            urunmarkalbl.Text = "Marka";
            uruntitlelbl.Text = "�r�n Ad�";
            urunfiyatlbl.Text = "�r�n Fiyat�";
            pictureBox1.Image = null;
            markaonizlemesilbl.Text = "Marka";
            tag1.Text = string.Empty;
        }
        private void temizleme()
        {
            urunadi.Text = string.Empty;
            eskifiyat.Text = string.Empty;
            yenifiyat.Text = string.Empty;
            gorsellink.Text = string.Empty;
            tagtxt.Text = string.Empty;
            stoknumeric.Text = string.Empty;
            indirimcheck.Checked = false;
            markatxt.Text = string.Empty;
            flashurunler.SelectedIndex = -1;
            urunler.SelectedIndex = -1;
        }

        private void temizlebtn_Click(object sender, EventArgs e)
        {
            temizleme();
            onizlemeguncelle();
        }

        private void indirimcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (indirimcheck.Checked == false)
            {
                yenifiyat.Enabled = false;
            }
            if (indirimcheck.Checked == true)
            {
                yenifiyat.Enabled = true;
            }
        }
       

        private async void flashurunler_DoubleClick(object sender, EventArgs e)
        {
           
            try
            {
                urunler.SelectedIndex = -1;
                if (flashurunler.SelectedItem != null && flashurunler.SelectedItem is ListBoxItem selectedItem)
                {
                    var productDetails = await firestoreIslem.urunDetaylariniAl(selectedItem.ProductID);
                    onizlemeguncelle();
                    if (productDetails != null)
                    {
                        if (productDetails.ContainsKey("imageUrl") && productDetails["imageUrl"] != null)
                        {
                            string imageUrl = productDetails["imageUrl"].ToString();
                            await firestoreIslem.gorselCekme(imageUrl, picturebox);
                        }
                        else
                        {
                            MessageBox.Show("Resim URL'si ge�ersiz veya mevcut de�il.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        
                        if (productDetails.ContainsKey("title"))
                            urunadi.Text = productDetails["title"].ToString();
                        if (productDetails.ContainsKey("title"))
                            urunlabel.Text = productDetails["title"].ToString();
                        if (productDetails.ContainsKey("discountPrice"))
                            yenifiyat.Text = productDetails["discountPrice"].ToString();
                        if (productDetails.ContainsKey("discountPrice"))
                            urunyeni.Text = productDetails["discountPrice"].ToString() + "TL";
                        if (productDetails.ContainsKey("originalPrice"))
                            eskifiyat.Text = productDetails["originalPrice"].ToString();
                        if (productDetails.ContainsKey("originalPrice"))
                            uruneski.Text = productDetails["originalPrice"].ToString() + "TL";
                        if (productDetails.ContainsKey("tag"))
                            tagtxt.Text = productDetails["tag"].ToString();
                        if (productDetails.ContainsKey("tag"))
                            uruntag.Text = productDetails["tag"].ToString();
                        if (productDetails.ContainsKey("flash"))
                            indirimcheck.Checked = Convert.ToBoolean(productDetails["flash"]);
                        if (productDetails.ContainsKey("imageUrl"))
                            gorsellink.Text = productDetails["imageUrl"].ToString();
                        if (productDetails.ContainsKey("quantity"))
                            stoknumeric.Value = Convert.ToInt32(productDetails["quantity"]);
                        if (productDetails.ContainsKey("brand"))
                            markatxt.Text = productDetails["brand"].ToString();
                        if (productDetails.ContainsKey("brand"))
                            markaonizlemesilbl.Text = productDetails["brand"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("�r�n detaylar� y�klenirken bir hata olu�tu veya �r�n bulunamad�.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    flashkontrol();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�r�n detaylar� y�klenirken hata olu�tu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
