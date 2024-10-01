using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


public class ModifyProductModel : PageModel
{
    private readonly ILogger<ModifyProductModel> _logger;

    public Product ProductToModify{get;set;}

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
    }
    public IActionResult OnPost(string? name, decimal? price, string? details){
        string nameToModify;
        decimal? priceToModify;
        string detailsToModify;
        string json = System.IO.File.ReadAllText("wwwroot/json/products.json");
        List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
        if(ProductToModify!=null){
            for(int i = 0; i < products.Count; i++){
                    if(products[i].Id == ProductToModify.Id){
                        products[i] = ProductToModify;
                    }
            }
            if(name != null)
                nameToModify = name;
            else
                nameToModify = ProductToModify.Name;
            if(price != null)
                priceToModify = price;
            else
                priceToModify = ProductToModify.Price;
            if(details != null)
                detailsToModify = details;
            else
                detailsToModify = ProductToModify.Details;
                   
            products.Add(new Product(ProductToModify.Id, nameToModify, priceToModify, detailsToModify, "./img/scarpe.jpg"));
            System.IO.File.WriteAllText("wwwroot/json/products.json",JsonConvert.SerializeObject(products, Formatting.Indented));
        }
        return RedirectToPage("products");
    }
}
