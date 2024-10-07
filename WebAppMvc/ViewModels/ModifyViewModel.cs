using WebAppMvc.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;


namespace WebAppMvc.ViewModels
{
    public class ModifyViewModel
    {
        [BindProperty]
        public Product ProductToModify{get;set;}
        public List<string> Categories{get;set;}
        public ModifyViewModel(int id){
            string json = System.IO.File.ReadAllText("wwwroot/json/products.json");
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
            foreach(Product prod in products){
                if(prod.Id == id){
                    ProductToModify = prod;
                }
            }
            json = System.IO.File.ReadAllText("wwwroot/json/categories.json");
            Categories = JsonConvert.DeserializeObject<List<string>>(json);
        }
        public ModifyViewModel(){}
    }
}