using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using webappmvc.Models;
using WebAppMvc.Models;
using WebAppMvc.ViewModels;
using Newtonsoft.Json;


namespace WebAppMvc.Controllers
{
    
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index(decimal? minPrice, decimal? maxPrice, int? pageIndex = 1)
        {
            var model = new IndexViewModel();
            model.MaxPrice = maxPrice;
            model.MinPrice = minPrice;
            List<Product> tempProducts = new List<Product>();
            bool addToList;

            foreach(Product prod in model.AllProducts){
                addToList = true;

                if(minPrice.HasValue && prod.Price < minPrice)
                    addToList = false;
                
                if(maxPrice.HasValue && prod.Price > maxPrice)
                    addToList = false;
                
                if(addToList)
                    tempProducts.Add(prod);
            }
            tempProducts = model.AllProducts;
            /*model.AllProducts = _allProducts.Where(p => (!minPrice.HasValue || p.Price >= minPrice) && 
                                (!maxPrice.HasValue || p.Price <= maxPrice))
                                .Skip((pageIndex - 1) * 6) // Adjust the number for pagination
                                .Take(6)
                                .ToList();*/
            model.AllProducts  = tempProducts.OrderBy(p => p.Name).ToList();
            model.PageNumber = (int)Math.Ceiling(model.AllProducts.Count / 6.0);
            model.AllProducts = model.AllProducts.Skip(((pageIndex ?? 1) - 1) * 6).Take(6).ToList();
            // model.PageNumber = (int)Math.Ceiling((double)model.AllProducts.Count / 6); // Adjust based on your page size
            // Console.WriteLine("model.AllProducts -> " + model.AllProducts.Count);
            // Console.WriteLine("model.MaxPrice -> " + model.MaxPrice);
            // Console.WriteLine("model.MinPrice -> " + model.MinPrice);
            // Console.WriteLine("model.PageNumber -> " + model.PageNumber);
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = new DetailsViewModel(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Modify(int id)
        {
            var model = new ModifyViewModel();
            string json = System.IO.File.ReadAllText("wwwroot/json/products.json");
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
            foreach(Product prod in products){
                if(prod.Id == id){
                    model.ProductToModify = prod;
                }
            }
            json = System.IO.File.ReadAllText("wwwroot/json/categories.json");
            model.Categories = JsonConvert.DeserializeObject<List<string>>(json);
            return View(model);
        }

        [HttpPost]
        public IActionResult Modify(ModifyViewModel model)
        {
            string json = System.IO.File.ReadAllText("wwwroot/json/products.json");
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
            foreach(Product prod in products){
            if(prod.Id == model.ProductToModify.Id){
                    prod.Name = model.ProductToModify.Name;
                    prod.Price = model.ProductToModify.Price;
                    prod.Details = model.ProductToModify.Details;
                    prod.Amount = model.ProductToModify.Amount;
                    prod.Category = model.ProductToModify.Category;
                    break;
                }
            }
            System.IO.File.WriteAllText("wwwroot/json/products.json",JsonConvert.SerializeObject(products, Formatting.Indented));
            return RedirectToAction("Index", "Products");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}