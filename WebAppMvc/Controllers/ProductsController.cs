using Microsoft.AspNetCore.Mvc;
using WebAppMvc.Models;
using WebAppMvc.ViewModels;
using Newtonsoft.Json;
using WebAppMvc.Services;


namespace WebAppMvc.Controllers
{
    public class ProductsController : Controller
    {
        private List<Product> _products;
        private ProductsService _service;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
            _products = new List<Product>();
            _service = new ProductsService();
            _service.WritePicturesFromJson();
        }

        [HttpGet]
        public IActionResult Index(decimal? maxPrice, decimal? minPrice, int? pageIndex = 1)
        {
            var model = new IndexViewModel();
            model.MinPrice = minPrice;
            model.MaxPrice = maxPrice;
            model.AllProducts = _service.FilterProducts(model.AllProducts, maxPrice, minPrice);
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
            _products = _service.ReadProductsFromJson();
            model.ProductToModify = _service.AssignProduct(_products, id);
            model.Categories = _service.ReadCategoriesFromJson();
            model.Pictures = _service.ReadPicturesFromJson();
            return View(model);
        }

        [HttpPost, ActionName("Modify")]
        public IActionResult ModifyPost(ModifyViewModel model)
        {
            _products = _service.ReadProductsFromJson();
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
            
            _service.WriteToJson(_products);

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
            _products = _service.ReadProductsFromJson();
            _products = _service.RemoveProductFromList(_products, id);
            _service.WriteToJson(_products);
            return RedirectToAction("Index", "Products");
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            AddViewModel model = new AddViewModel();
            model.Categories = _service.ReadCategoriesFromJson();
            model.Pictures = _service.ReadPicturesFromJson();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {

            if(_service.CheckPassword(model.Psw)){
                return RedirectToAction("Error", "Shared");
            }
            _products = _service.ReadProductsFromJson();
            model.ProductToAdd.Id = _service.CalculateId(_products);
            model.ProductToAdd.Picture = "/img/" + model.ProductToAdd.Picture;
            _products.Add(model.ProductToAdd);
            _service.WriteToJson(_products);
            
            return RedirectToAction("Index", "Products");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}