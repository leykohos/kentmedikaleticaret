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
            // Initialize Firestore
            string path = "C:\\Users\\Fatih\\Desktop\\csharpkey\\kentmedikalkey.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            firestoreDb = FirestoreDb.Create("kentmedikal-3f5d2");
        }

        public async Task veriekleme(string isim, string image, string discount, string original, string tag, bool indirim,string adet)
        {
            try
            {
                // Define the collection
                CollectionReference productsCollection = firestoreDb.Collection("products");

                // Create a document object with fields
                var product = new
                {
                    title = isim,
                    imageUrl = image,
                    discountPrice = discount,
                    originalPrice = original,
                    tag = tag,
                    flash = indirim,
                    quantity = adet
                };

                // Add the document to Firestore
                await productsCollection.AddAsync(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding data to Firestore: {ex.Message}");
            }
        }

        public async Task<List<Dictionary<string, object>>> verilistele()
        {
            try
            {
                CollectionReference productsCollection = firestoreDb.Collection("products");

                QuerySnapshot snapshot = await productsCollection.GetSnapshotAsync();
                List<Dictionary<string, object>> productList = new List<Dictionary<string, object>>();

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        // Firestore'dan dönen verileri Dictionary'ye çevir
                        Dictionary<string, object> productData = document.ToDictionary();

                        // Kimlik olarak Firestore'daki document ID'yi ekleyin
                        productData["ID"] = document.Id; // Document ID'yi ekleyin

                        // Listeye ekleyin
                        productList.Add(productData);
                    }
                }

                // Listeyi sınıf seviyesinde tanımlanan `products` alanına kopyala
                products = productList;

                Console.WriteLine("Data successfully retrieved from Firestore!");
                return productList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving data from Firestore: {ex.Message}");
                return null;
            }
        }

        public async Task<Dictionary<string, object>> GetProductDetailsByID(string documentID)
        {
            try
            {
                var productsCollection = firestoreDb.Collection("products");

                // Belge kimliğine göre bir referans al
                DocumentReference documentRef = productsCollection.Document(documentID);

                // Belgeyi çek
                DocumentSnapshot documentSnapshot = await documentRef.GetSnapshotAsync();

                if (documentSnapshot.Exists)
                {
                    Console.WriteLine("Ürün başarıyla bulundu.");
                    return documentSnapshot.ToDictionary();
                }

                Console.WriteLine("Ürün bulunamadı.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> UpdateProductByID(string documentID, string isim, string image, string discount, string original, string tag, bool indirim,string adet)
        {
            try
            {
                var productsCollection = firestoreDb.Collection("products");

                // Güncellenmesi gereken belgeye referans oluştur
                DocumentReference documentRef = productsCollection.Document(documentID);

                // Güncelleme için yeni veri
                var updatedData = new Dictionary<string, object>
        {
            { "title", isim },
            { "imageUrl", image },
            { "discountPrice", discount },
            { "originalPrice", original },
            { "tag", tag },
            { "flash", indirim },
            {"quantity",adet }
        };

                // Firestore'da belgeyi güncelle
                await documentRef.UpdateAsync(updatedData);

                Console.WriteLine("Ürün başarıyla güncellendi.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Güncelleme sırasında hata oluştu: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteProductByID(string documentID)
        {
            try
            {
                var productsCollection = firestoreDb.Collection("products");

                // Silinmesi gereken belgeye referans oluştur
                DocumentReference documentRef = productsCollection.Document(documentID);

                // Belgeyi sil
                await documentRef.DeleteAsync();

                Console.WriteLine("Ürün başarıyla silindi.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Silme sırasında hata oluştu: {ex.Message}");
                return false;
            }
        }
        public async Task LoadImageIntoPictureBox(string imageUrl, PictureBox pictureBox)
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
