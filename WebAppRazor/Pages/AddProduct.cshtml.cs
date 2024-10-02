using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


public class AddProductModel : PageModel
{
    private readonly ILogger<AddProductModel> _logger;
    public List<string> Categories{get;set;}
    public List<string> Pictures{get;set;}
    [BindProperty]
    public string psw{get;set;}
    //public static string password = "abcd";

    public AddProductModel(ILogger<AddProductModel> logger){
        _logger = logger;
    }

    public void OnGet(){
        string json = System.IO.File.ReadAllText("wwwroot/json/categories.json");
        Categories = JsonConvert.DeserializeObject<List<string>>(json);
        json = System.IO.File.ReadAllText("wwwroot/json/pictures.json");
        Pictures = JsonConvert.DeserializeObject<List<string>>(json);
    }
    public IActionResult OnPost(string name, decimal price, string details, string picture, int amount, string category/*, string psw*/){
        if(psw != "abcd" || !ModelState.IsValid){
            return RedirectToPage("error");
        }
        string json = System.IO.File.ReadAllText("wwwroot/json/products.json");
        List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
        int id = 0;
        foreach(Product prod in products){
            if(id < prod.Id)
                id = prod.Id + 1;
        }
        products.Add(new Product(id, name, price, details, picture, amount, category));
        System.IO.File.WriteAllText("wwwroot/json/products.json",JsonConvert.SerializeObject(products, Formatting.Indented));
        
        return RedirectToPage("products");
    }
}
