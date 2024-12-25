using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace EticaretYonetimi
{
    public class islem
    {
        private FirestoreDb firestoreDb;

        // Sınıf seviyesinde bir ürünler listesi tanımlıyoruz
        private List<Dictionary<string, object>> products = new List<Dictionary<string, object>>();

        public islem()
        {
            //firebase projeye ekleme
            string path = "C:\\Users\\Fatih\\Desktop\\csharpkey\\kentmedikalkey.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            firestoreDb = FirestoreDb.Create("kentmedikal-3f5d2");
        }

        public async Task veriEkleme(string isim, string image, string indirimli, string original, string tag, bool indirim,string adet,string markaadi)
        {
            try
            {
                //koleksiyona eriştik
                CollectionReference productsCollection = firestoreDb.Collection("products");

                //göndereceğimiz veriler 
                var product = new
                {
                    title = isim,
                    imageUrl = image,
                    discountPrice = indirimli,
                    originalPrice = original,
                    tag = tag,
                    flash = indirim,
                    quantity = adet,    
                    brand = markaadi
                };

                //veri ekledik    
                await productsCollection.AddAsync(product);
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Ekleme sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public async Task<List<Dictionary<string, object>>> veriListele()
        {
            try
            {
                //koleksiyona eriştik
                CollectionReference productsCollection = firestoreDb.Collection("products");

                //verileri aldık
                QuerySnapshot snapshot = await productsCollection.GetSnapshotAsync();
                List<Dictionary<string, object>> productList = new List<Dictionary<string, object>>();

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        //dönüşü
                        Dictionary<string, object> productData = document.ToDictionary();
                        productData["ID"] = document.Id; 
                        productList.Add(productData);
                    }
                }

                products = productList;

                
                return productList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanı bağlantısı sağlanamadı!: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public async Task<Dictionary<string, object>> urunDetaylariniAl(string documentID)
        {
            try
            {
                CollectionReference productsCollection = firestoreDb.Collection("products");     //ref         
                DocumentReference documentRef = productsCollection.Document(documentID); //verilen ID ye göre dokumanı çek
                DocumentSnapshot documentSnapshot = await documentRef.GetSnapshotAsync(); // dokuman verilerini al

                if (documentSnapshot.Exists)//kontrol
                {
                    return documentSnapshot.ToDictionary();
                }

               
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Detay alma sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public async Task<bool> veriGuncelleme(string documentID, string isim, string image, string discount, string original, string tag, bool indirim,string adet,string markaname)
        {
            try
            {
                CollectionReference productsCollection = firestoreDb.Collection("products");
                DocumentReference documentRef = productsCollection.Document(documentID);

                // güncelleme için yeni veri
                var updatedData = new Dictionary<string, object>
                    {
                        { "title", isim },
                        { "imageUrl", image },
                        { "discountPrice", discount },
                        { "originalPrice", original },
                        { "tag", tag },
                        { "flash", indirim },
                        {"quantity",adet },
                        {"brand" ,markaname}
                    };

                //güncelle
                await documentRef.UpdateAsync(updatedData);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Güncelleme sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public async Task<bool> veriSilme(string documentID)
        {
            try
            {
                CollectionReference productsCollection = firestoreDb.Collection("products");
                
                DocumentReference documentRef = productsCollection.Document(documentID);

                //silme
                await documentRef.DeleteAsync();

                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Silme sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public async Task gorselCekme(string imageUrl, PictureBox pictureBox)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    byte[] imageBytes = await client.DownloadDataTaskAsync(imageUrl);

                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        using (var ms = new System.IO.MemoryStream(imageBytes))
                        {
                            pictureBox.Invoke((MethodInvoker)delegate
                            {
                                pictureBox.Image = Image.FromStream(ms);
                            });
                        }
                    }
                    else
                    {
                        MessageBox.Show("Resim yüklenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Resim yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
