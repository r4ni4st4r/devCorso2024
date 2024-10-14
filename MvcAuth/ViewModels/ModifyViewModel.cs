using MvcAuth.Models;

namespace MvcAuth.ViewModels
{
    public class ModifyViewModel
    {
        public Product ProductToModify{get;set;}
        public List<string> Categories{get;set;}
        public List<string> Pictures{get;set;}
    }
}