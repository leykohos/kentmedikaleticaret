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

                MessageBox.Show("Veri ba�ar�yla Firestore'a eklendi!", "Ba�ar�l�", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ListBox'� g�ncelle
                ListeyiGuncelle();
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
                // Firestore'dan �r�n listesini �ek
                var products = await firestoreIslem.verilistele();

                // ListBox'u temizle
                urunler.Items.Clear();
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        if (product.ContainsKey("title") && product.ContainsKey("ID")) // `ID` alan�n� kontrol ediyoruz
                        {
                            // ListBox'ta g�r�necek ��eleri ekleyin
                            urunler.Items.Add(new ListBoxItem
                            {
                                DisplayName = product["title"].ToString(),
                                ProductID = product["ID"].ToString() // Burada Firestore'un d�nd�rd��� benzersiz kimlik kullan�l�r
                            });
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
        }

        private async void urunler_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (urunler.SelectedItem != null && urunler.SelectedItem is ListBoxItem selectedItem)
                {
                    // Firestore'dan �r�n detaylar�n� �ek
                    var productDetails = await firestoreIslem.GetProductDetailsByID(selectedItem.ProductID);

                    if (productDetails != null)
                    {
                        // G�rseli PictureBox'a y�kleyelim
                        if (productDetails.ContainsKey("imageUrl") && productDetails["imageUrl"] != null)
                        {
                            string imageUrl = productDetails["imageUrl"].ToString();
                            await firestoreIslem.LoadImageIntoPictureBox(imageUrl, picturebox);
                        }
                        else
                        {
                            MessageBox.Show("Resim URL'si ge�ersiz veya mevcut de�il.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        // Di�er verileri TextBox'lara aktar�yoruz
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
                        MessageBox.Show("�r�n detaylar� y�klenirken bir hata olu�tu veya �r�n bulunamad�.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�r�n detaylar� y�klenirken hata olu�tu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void guncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (urunler.SelectedItem != null && urunler.SelectedItem is ListBoxItem selectedItem)
                {
                    // Kullan�c�dan yeni verileri al
                    string yeniIsim = urunadi.Text;
                    string yeniImage = gorsellink.Text;
                    string yeniDiscount = yenifiyat.Text;
                    string yeniOriginal = eskifiyat.Text;
                    string yeniTag = tagtxt.Text;
                    bool yeniIndirim = indirimcheck.Checked;
                    string yeniAdet = stoknumeric.Value.ToString();

                    // Firestore'da g�ncelle
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
                        MessageBox.Show("�r�n ba�ar�yla g�ncellendi.", "Ba�ar�l�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListeyiGuncelle(); // Listeyi tekrar g�ncelle
                    }
                    else
                    {
                        MessageBox.Show("�r�n g�ncellenirken bir hata olu�tu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        bool deleteResult = await firestoreIslem.DeleteProductByID(selectedItem.ProductID);

                        if (deleteResult)
                        {
                            MessageBox.Show("�r�n ba�ar�yla silindi.", "Ba�ar�l�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListeyiGuncelle(); // Listeyi tekrar g�ncelle
                            onizlemeguncelle();
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
            urunadi.Text = "�r�n Ad�";
            uruneski.Text = "Eski Fiyat";
            urunyeni.Text = "Yeni Fiyat";
            uruntag.Text = string.Empty;
        }
    }
}
