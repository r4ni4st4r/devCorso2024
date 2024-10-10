using WebAppMvc.Models;
using Newtonsoft.Json;

namespace WebAppMvc.ViewModels
{
    public class IndexViewModel
    {
        public List<Product> AllProducts { get; set; }
        public int PageNumber { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public IndexViewModel(){
            if(File.Exists("wwwroot/json/products.json")){
                string json = File.ReadAllText("wwwroot/json/products.json");
                AllProducts = JsonConvert.DeserializeObject<List<Product>>(json);
            }
        }
    }
}