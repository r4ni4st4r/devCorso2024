using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


public class AddProductModel : PageModel
{
    private readonly ILogger<AddProductModel> _logger;

    public AddProductModel(ILogger<AddProductModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
    public IActionResult OnPost(string name, decimal price, string details){
        string json = System.IO.File.ReadAllText("wwwroot/json/products.json");
        List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
        int id = 0;
        if(products.Count > 0){
            id = products[products.Count-1].Id+1;

        }
        products.Add(new Product(id, name, price, details, "./img/scarpe.jpg"));
        System.IO.File.WriteAllText("wwwroot/json/products.json",JsonConvert.SerializeObject(products, Formatting.Indented));
        
        return RedirectToPage("products");
    }
}
