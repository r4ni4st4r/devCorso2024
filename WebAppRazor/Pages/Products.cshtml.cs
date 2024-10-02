using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebAppRazor.Pages;

public class ProductsModel : PageModel
{
    private readonly ILogger<ProductsModel> _logger;
    public List<Product> AllProducts {get;set;}

    public int PageNumber{get;set;}

    public ProductsModel(ILogger<ProductsModel> logger)
    {
        _logger = logger;
    }

    public void OnGet(decimal? minPrice, decimal? maxPrice, int? pageIndex)
    {
        string json = System.IO.File.ReadAllText("wwwroot/json/products.json");

        List<Product> TempProducts = new List<Product>();
        AllProducts = JsonConvert.DeserializeObject<List<Product>>(json);
        
        bool addToList;

        foreach(Product prod in AllProducts){
            addToList = true;

            if(minPrice.HasValue && prod.Price < minPrice)
                addToList = false;
            
            if(maxPrice.HasValue && prod.Price > maxPrice)
                addToList = false;
            
            if(addToList)
                TempProducts.Add(prod);
        }

        AllProducts  = TempProducts.OrderBy(p => p.Name).ToList();
        PageNumber = (int)Math.Ceiling(AllProducts.Count / 6.0);
        AllProducts = AllProducts.Skip(((pageIndex ?? 1) - 1) * 6).Take(6).ToList();
    }
}