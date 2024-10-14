using Microsoft.AspNetCore.Mvc;
using MvcAuth.Models;

namespace MvcAuth.ViewModels
{
    public class AddViewModel
    { 
        //[BindProperty]
        public Product ProductToAdd{get;set;}
        public List<string> Categories{get;set;}
        public List<string> Pictures{get;set;}
        public string Psw{get;set;}
    }
}