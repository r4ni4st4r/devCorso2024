using Newtonsoft.Json;
using MvcAuth.Models;

namespace MvcAuth.Services
{
    public class ProductsService
    {
        private const string PRODUCTSPATH = "wwwroot/json/products.json";
        private const string CATEGORIESPATH = "wwwroot/json/categories.json";
        private const string PICTURESPATH = "wwwroot/json/pictures.json";
        private const string PSW = "1234";

        public List<Product> ReadProductsFromJson(){
            string json = File.ReadAllText(PRODUCTSPATH);
            return JsonConvert.DeserializeObject<List<Product>>(json);
        }
        public void WriteToJson(List<Product> listToWrite){
            File.WriteAllText(PRODUCTSPATH,JsonConvert.SerializeObject(listToWrite, Formatting.Indented));
        }
        public List<string> ReadCategoriesFromJson(){
            string json = File.ReadAllText(CATEGORIESPATH);
            return JsonConvert.DeserializeObject<List<string>>(json);
        }
        public List<string> ReadPicturesFromJson(){
            string json = File.ReadAllText(PICTURESPATH);
            return JsonConvert.DeserializeObject<List<string>>(json);
        }
        public void WritePicturesFromJson(){
            List<string?> imgFiles = Directory.GetFiles("wwwroot/img/")
                                  .Select(Path.GetFileName) // Estrae solo il nome del file
                                  .ToList();
            System.IO.File.WriteAllText(PICTURESPATH,JsonConvert.SerializeObject(imgFiles, Formatting.Indented));
        }
        public bool CheckPassword(string pswToCheck){
            if(pswToCheck == PSW)
                return true;
            else
                return false;
        }
        public int CalculateId(List<Product> products){
            int id = 0;
            foreach(Product prod in products){
                if(id <= prod.Id)
                    id = prod.Id + 1;
            }
            return id;
        }
        public List<Product> RemoveProductFromList(List<Product> products, int id){
            foreach(Product prod in products){
                if(prod.Id == id){
                    products.Remove(prod);
                    break;
                }
            }
            return products;
        }
        public List<Product> FilterProducts(List<Product> productsToFilter, decimal? minPrice, decimal? maxPrice){
            List<Product> filtredProducts = new List<Product>();
            bool addToList;
            if(productsToFilter != null){
                foreach(Product prod in productsToFilter){
                    addToList = true;

                    if(minPrice.HasValue && prod.Price < minPrice)
                        addToList = false;
                    
                    if(maxPrice.HasValue && prod.Price > maxPrice)
                        addToList = false;
                    
                    if(addToList)
                        filtredProducts.Add(prod);
                }
            }
            return filtredProducts;
        }

        public Product AssignProduct(List<Product> products, int id){
            foreach(Product prod in products){
                if(prod.Id == id){
                    return prod;
                }
            }
            return new Product();
        }
    }
}