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
        /*
        new Product{Id =1, Name = "scarpa", Price = 100, Detail = "serve per camminare", Image="./img/scarpe.jpg"},
        new Product{Id =2, Name = "cappello", Price = 200, Detail = "serve per camminare", Image="./img/scarpe.jpg"},
        new Product{Id =3, Name = "calza", Price = 300, Detail = "serve per camminare", Image="./img/scarpe.jpg"},
        new Product{Id =4, Name = "maglia", Price = 150, Detail = "serve per camminare", Image="./img/scarpe.jpg"},
        new Product{Id =5, Name = "canotta", Price = 250, Detail = "serve per camminare", Image="./img/scarpe.jpg"},
        new Product{Id =6, Name = "ciabatta", Price = 350, Detail = "serve per camminare", Image="./img/scarpe.jpg"},
        new Product{Id =7, Name = "costume", Price = 600, Detail = "serve per camminare", Image="./img/scarpe.jpg"},
        new Product{Id =8, Name = "cerchietto", Price = 450, Detail = "serve per camminare", Image="./img/scarpe.jpg"},
        new Product{Id =9, Name = "bermuda", Price = 80, Detail = "serve per camminare", Image="./img/scarpe.jpg"},
        new Product{Id =10, Name = "felpa", Price = 260, Detail = "serve per camminare", Image="./img/scarpe.jpg"},
        new Product{Id =11, Name = "bracciale", Price = 110, Detail = "serve per camminare", Image="./img/scarpe.jpg"}};
        
        AllProducts.Add(new Product(2, "giacca", 200, "serve per coprirsi","./img/giacca.jpg"));
        AllProducts.Add(new Product(3, "cappello", 300, "si mette sulla testa","./img/cappello.jpg"));
        AllProducts.Add(new Product(1, "calza", 150, "serve per camminare","./img/scarpe.jpg"));
        AllProducts.Add(new Product(2, "maglia", 250, "serve per coprirsi","./img/giacca.jpg"));
        AllProducts.Add(new Product(3, "canotta", 350, "si mette sulla testa","./img/cappello.jpg"));
        AllProducts.Add(new Product(1, "ciabatta", 400, "serve per camminare","./img/scarpe.jpg"));
        AllProducts.Add(new Product(2, "costume", 600, "serve per coprirsi","./img/giacca.jpg"));
        AllProducts.Add(new Product(3, "cerchietto", 450, "si mette sulla testa","./img/cappello.jpg"));
        AllProducts.Add(new Product(1, "bermuda", 80, "serve per camminare","./img/scarpe.jpg"));
        AllProducts.Add(new Product(2, "felpa", 260, "serve per coprirsi","./img/giacca.jpg"));
        AllProducts.Add(new Product(3, "bracciale", 110, "si mette sulla testa","./img/cappello.jpg"));*/
        
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

        AllProducts  = TempProducts;
        PageNumber = (int)Math.Ceiling(AllProducts.Count / 6.0);
        AllProducts = AllProducts.Skip(((pageIndex ?? 1) - 1) * 6).Take(6).ToList();
    }
}