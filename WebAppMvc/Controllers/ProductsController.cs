using Microsoft.AspNetCore.Mvc;
using WebAppMvc.Models;
using WebAppMvc.ViewModels;
using Newtonsoft.Json;


namespace WebAppMvc.Controllers
{
    public class ProductsController : Controller
    {
        private const string PSW = "1234";
        private const string JSONPATH = "wwwroot/json/products.json";
        private const string CATEGORIESPATH = "wwwroot/json/categories.json";
        private  string PICTURESPATH = "wwwroot/json/pictures.json";
        //[BindProperty]
        //public Product ProductToModify{get;set;}
        //[BindProperty]
        //public Product ProductToAdd{get;set;}
        private List<Product> _products;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
            _products = new List<Product>();
            WrirePicturesFromJson(PICTURESPATH);
        }

        [HttpGet]
        public IActionResult Index(decimal? minPrice, decimal? maxPrice, int? pageIndex = 1)
        {
            var model = new IndexViewModel();
            model.MinPrice = minPrice;
            model.MaxPrice = maxPrice;
            List<Product> filtredProducts = new List<Product>();
            bool addToList;
            if(model.AllProducts!=null){
                foreach(Product prod in model.AllProducts){
                    addToList = true;

                    if(minPrice.HasValue && prod.Price < minPrice)
                        addToList = false;
                    
                    if(maxPrice.HasValue && prod.Price > maxPrice)
                        addToList = false;
                    
                    if(addToList)
                        filtredProducts.Add(prod);
                }
            }
            model.AllProducts = filtredProducts;

            model.AllProducts  = model.AllProducts.OrderBy(p => p.Name).ToList();
            model.PageNumber = (int)Math.Ceiling(model.AllProducts.Count / 6.0);
            model.AllProducts = model.AllProducts.Skip(((pageIndex ?? 1) - 1) * 6).Take(6).ToList();
            
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
            _products = ReadProductsFromJson(JSONPATH);
            foreach(Product prod in _products){
                if(prod.Id == id){
                    model.ProductToModify = prod;
                }
            }
            model.Categories = ReadCategoriesPicturesFromJson(CATEGORIESPATH);
            model.Pictures = ReadCategoriesPicturesFromJson(PICTURESPATH);
            return View(model);
        }

        [HttpPost, ActionName("Modify")]
        public IActionResult ModifyPost(ModifyViewModel model)
        {
            _products = ReadProductsFromJson(JSONPATH);
            foreach(Product prod in _products){
            if(prod.Id == model.ProductToModify.Id){
                    prod.Name = model.ProductToModify.Name;
                    prod.Price = model.ProductToModify.Price;
                    prod.Details = model.ProductToModify.Details;
                    prod.Amount = model.ProductToModify.Amount;
                    prod.Category = model.ProductToModify.Category;
                    prod.Picture = "/img/" + model.ProductToModify.Picture;
                    break;
                }
            }
            
            WriteToJson(_products);

            return RedirectToAction("Index", "Products");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = new DeleteViewModel(id);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            _products = ReadProductsFromJson(JSONPATH);
            foreach(Product prod in _products){
            if(prod.Id == id){
                    _products.Remove(prod);
                    break;
                }
            }
            WriteToJson(_products);
            return RedirectToAction("Index", "Products");
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            AddViewModel model = new AddViewModel();
            model.Categories = ReadCategoriesPicturesFromJson(CATEGORIESPATH);
            model.Pictures = ReadCategoriesPicturesFromJson(PICTURESPATH);
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            if(model.Psw != PSW){
                return RedirectToAction("Error", "Shared");
            }
            _products = ReadProductsFromJson(JSONPATH);
            int id = 0;
            foreach(Product prod in _products){
                if(id <= prod.Id)
                    id = prod.Id + 1;
            }
            model.ProductToAdd.Id = id;
            model.ProductToAdd.Picture = "/img/" + model.ProductToAdd.Picture;
            _products.Add(model.ProductToAdd);
            WriteToJson(_products);
            
            return RedirectToAction("Index", "Products");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<Product> ReadProductsFromJson(string path){
            string json = System.IO.File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<Product>>(json);
        }
        private void WriteToJson(List<Product> listToWrite){
            System.IO.File.WriteAllText("wwwroot/json/products.json",JsonConvert.SerializeObject(listToWrite, Formatting.Indented));
        }
        private List<string> ReadCategoriesPicturesFromJson(string path){
            string json = System.IO.File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<string>>(json);
        }
        private void WrirePicturesFromJson(string path){
            List<string?> imgFiles = Directory.GetFiles("wwwroot/img/")
                                  .Select(Path.GetFileName) // Estrae solo il nome del file
                                  .ToList();
            System.IO.File.WriteAllText("wwwroot/json/pictures.json",JsonConvert.SerializeObject(imgFiles, Formatting.Indented));
        }
    }
}