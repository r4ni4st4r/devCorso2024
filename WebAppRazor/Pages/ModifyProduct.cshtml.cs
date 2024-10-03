using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


public class ModifyProductModel : PageModel
{
    private readonly ILogger<ModifyProductModel> _logger;

    public Product ProductToModify{get;set;}
    public List<string> Categories{get;set;}

    public ModifyProductModel(ILogger<ModifyProductModel> logger)
    {
        _logger = logger;
    }


    public void OnGet(int id){
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
    public IActionResult OnPost(int id, string name, decimal price, string details, int amount, string category){    
        string json = System.IO.File.ReadAllText("wwwroot/json/products.json");
        List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
        foreach(Product prod in products){
            if(prod.Id == id){
                prod.Name = name;
                prod.Price = price;
                prod.Details = details;
                prod.Amount = amount;
                prod.Category = category;
                break;
            }
        }
        System.IO.File.WriteAllText("wwwroot/json/products.json",JsonConvert.SerializeObject(products, Formatting.Indented));
        return RedirectToPage("products");
    }
}
