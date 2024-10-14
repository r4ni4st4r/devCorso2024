using Newtonsoft.Json;
using MvcAuth.Models;

namespace MvcAuth.ViewModels
{
    public class DeleteViewModel
    {
        public Product ProductToDelete{get;set;}
        public DeleteViewModel(int id){
            string json = File.ReadAllText("wwwroot/json/products.json");
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
            foreach(Product prod in products){
                if(prod.Id == id){
                    ProductToDelete = prod;
                }
            }
        }
    }
}