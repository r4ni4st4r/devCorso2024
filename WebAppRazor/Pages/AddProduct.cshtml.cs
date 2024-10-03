using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


public class AddProductModel : PageModel
{
    private readonly ILogger<AddProductModel> _logger;
    [BindProperty]
    public Product ProductToAdd{get;set;}
    public List<string> Categories{get;set;}
    public List<string> Pictures{get;set;}
    [BindProperty]
    [Required(ErrorMessage = "required field")]
    public string Psw{get;set;}
    public AddProductModel(ILogger<AddProductModel> logger){
        _logger = logger;
    }
    public void OnGet(){
        string json = System.IO.File.ReadAllText("wwwroot/json/categories.json");
        Categories = JsonConvert.DeserializeObject<List<string>>(json);
        json = System.IO.File.ReadAllText("wwwroot/json/pictures.json");
        Pictures = JsonConvert.DeserializeObject<List<string>>(json);
    }
    public IActionResult OnPost(){
        if(Psw != "abcd"){
            return RedirectToPage("error", new {message = "Code is not valid!"});
        }
        string json = System.IO.File.ReadAllText("wwwroot/json/products.json");
        List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
        int id = 0;
        foreach(Product prod in products){
            if(id < prod.Id)
                id = prod.Id + 1;
        }
        ProductToAdd.Id = id;
        products.Add(ProductToAdd);
        System.IO.File.WriteAllText("wwwroot/json/products.json",JsonConvert.SerializeObject(products, Formatting.Indented));
        
        return RedirectToPage("products");
    }
}
