using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppRazor.Pages;

public class ProductsModel : PageModel
{
    private readonly ILogger<ProductsModel> _logger;
    public List<Product> AllProducts {get;set;}

    public List<Product> TempProducts {get;set;}

    public ProductsModel(ILogger<ProductsModel> logger)
    {
        _logger = logger;
    }

    public void OnGet(decimal? minPrice, decimal? maxPrice)
    {
        //TempProducts = new List<Product>();
        AllProducts = new List<Product>();
        AllProducts.Add(new Product(1, "scarpa", 100, "serve per camminare","./img/scarpe.jpg"));
        AllProducts.Add(new Product(2, "giacca", 200, "serve per coprirsi","./img/giacca.jpg"));
        AllProducts.Add(new Product(3, "cappello", 300, "si mette sulla testa","./img/cappello.jpg"));
        /*
        foreach(Product prod in AllProducts){
            if(prod.Price >= minPrice && prod.Price <= maxPrice)
                TempProducts.Add(prod);
        }
        AllProducts  =TempProducts;*/
    }
}