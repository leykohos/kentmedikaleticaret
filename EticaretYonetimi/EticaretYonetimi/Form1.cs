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
        }

        private void ekleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Formdan verileri al
                string isim = urunadi.Text;
                string image = gorsellink.Text;
                string discount = yenifiyat.Text;
                string original = eskifiyat.Text;
                string tag = tagtxt.Text;
                bool indirim = indirimcheck.Checked;
                string adet = stoknumeric.Value.ToString();

                // Firestore'a veri ekle
                firestoreIslem.veriekleme(isim, image, discount, original, tag, indirim,adet);

                MessageBox.Show("Veri baþarýyla Firestore'a eklendi!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ListBox'ý güncelle
                ListeyiGuncelle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri eklenirken bir hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ListeyiGuncelle()
        {
            try
            {
                // Firestore'dan ürün listesini çek
                var products = await firestoreIslem.verilistele();

                // ListBox'u temizle
                urunler.Items.Clear();
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        if (product.ContainsKey("title") && product.ContainsKey("ID")) // `ID` alanýný kontrol ediyoruz
                        {
                            // ListBox'ta görünecek öðeleri ekleyin
                            urunler.Items.Add(new ListBoxItem
                            {
                                DisplayName = product["title"].ToString(),
                                ProductID = product["ID"].ToString() // Burada Firestore'un döndürdüðü benzersiz kimlik kullanýlýr
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Listeleme sýrasýnda hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listeleBtn_Click(object sender, EventArgs e)
        {
            ListeyiGuncelle();
        }

        private async void urunler_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (urunler.SelectedItem != null && urunler.SelectedItem is ListBoxItem selectedItem)
                {
                    // Firestore'dan ürün detaylarýný çek
                    var productDetails = await firestoreIslem.GetProductDetailsByID(selectedItem.ProductID);

                    if (productDetails != null)
                    {
                        // Görseli PictureBox'a yükleyelim
                        if (productDetails.ContainsKey("imageUrl") && productDetails["imageUrl"] != null)
                        {
                            string imageUrl = productDetails["imageUrl"].ToString();
                            await firestoreIslem.LoadImageIntoPictureBox(imageUrl, picturebox);
                        }
                        else
                        {
                            MessageBox.Show("Resim URL'si geçersiz veya mevcut deðil.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        // Diðer verileri TextBox'lara aktarýyoruz
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
                    }
                    else
                    {
                        MessageBox.Show("Ürün detaylarý yüklenirken bir hata oluþtu veya ürün bulunamadý.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürün detaylarý yüklenirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void guncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (urunler.SelectedItem != null && urunler.SelectedItem is ListBoxItem selectedItem)
                {
                    // Kullanýcýdan yeni verileri al
                    string yeniIsim = urunadi.Text;
                    string yeniImage = gorsellink.Text;
                    string yeniDiscount = yenifiyat.Text;
                    string yeniOriginal = eskifiyat.Text;
                    string yeniTag = tagtxt.Text;
                    bool yeniIndirim = indirimcheck.Checked;
                    string yeniAdet = stoknumeric.Value.ToString();

                    // Firestore'da güncelle
                    bool result = await firestoreIslem.UpdateProductByID(
                        selectedItem.ProductID,
                        yeniIsim,
                        yeniImage,
                        yeniDiscount,
                        yeniOriginal,
                        yeniTag,
                        yeniIndirim,
                        yeniAdet
                    );

                    if (result)
                    {
                        MessageBox.Show("Ürün baþarýyla güncellendi.", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListeyiGuncelle(); // Listeyi tekrar güncelle
                    }
                    else
                    {
                        MessageBox.Show("Ürün güncellenirken bir hata oluþtu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir ürün seçin.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Güncelleme sýrasýnda bir hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void silmebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (urunler.SelectedItem != null && urunler.SelectedItem is ListBoxItem selectedItem)
                {
                    var result = MessageBox.Show("Bu ürünü silmek istediðinizden emin misiniz?", "Silme Onayý", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        bool deleteResult = await firestoreIslem.DeleteProductByID(selectedItem.ProductID);

                        if (deleteResult)
                        {
                            MessageBox.Show("Ürün baþarýyla silindi.", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListeyiGuncelle(); // Listeyi tekrar güncelle
                            onizlemeguncelle();
                        }
                        else
                        {
                            MessageBox.Show("Ürün silinirken bir hata oluþtu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir ürün seçin.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Silme sýrasýnda bir hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void onizlemeguncelle()
        {
            picturebox.Image = null;
            urunadi.Text = "Ürün Adý";
            uruneski.Text = "Eski Fiyat";
            urunyeni.Text = "Yeni Fiyat";
            uruntag.Text = string.Empty;
        }
    }
}
