using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

public class ProductDetails : PageModel
{
    private readonly ILogger<ProductDetails> _logger;
    public Product Product{get;set;}
    public ProductDetails(ILogger<ProductDetails> logger){
        _logger = logger;
    }
    public void OnGet(int id){
        string json = System.IO.File.ReadAllText("wwwroot/json/products.json");
        List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
        foreach(Product prod in products){
            if(prod.Id == id){
                Product = prod;
            }
        }
    }
}
