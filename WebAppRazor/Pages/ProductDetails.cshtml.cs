using Microsoft.AspNetCore.Mvc.RazorPages;


    public class ProductDetails : PageModel
    {
        private readonly ILogger<ProductDetails> _logger;
        public Product ProductDet{get;set;}

        public ProductDetails(ILogger<ProductDetails> logger){
            _logger = logger;
        }

        public void OnGet(int id, string name, decimal price, string detail, string image){
            ProductDet = new Product(id, name, price, detail, image);
        }
    }
