using Newtonsoft.Json;
using MvcAuth.Models;

namespace MvcAuth.ViewModels
{
    public class DetailsViewModel
    {
        public Product Product{get;set;}
        public DetailsViewModel(int id){
            string json = System.IO.File.ReadAllText("wwwroot/json/products.json");
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
            foreach(Product prod in products){
                if(prod.Id == id){
                    Product = prod;
                }
            }
            Console.WriteLine("picture string -> "+Product.Picture);
        }
    }
}