using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


    public class DeleteProductModel : PageModel
    {
        private readonly ILogger<DeleteProductModel> _logger;
        public Product ProductToDelete{get;set;}

        public DeleteProductModel(ILogger<DeleteProductModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int id){
            string json = System.IO.File.ReadAllText("wwwroot/json/products.json");
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
            foreach(Product prod in products){
                if(prod.Id == id){
                    ProductToDelete = prod;
                }
            }
        }
        public IActionResult OnPost(int id){
            string json = System.IO.File.ReadAllText("wwwroot/json/products.json");
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
            for(int i = 0;i < products.Count;i++){
                if(products[i].Id == id){
                    products.RemoveAt(i);
                    break;
                }
            }
            System.IO.File.WriteAllText("wwwroot/json/products.json",JsonConvert.SerializeObject(products, Formatting.Indented));
            return RedirectToPage("products");
        }
    }
