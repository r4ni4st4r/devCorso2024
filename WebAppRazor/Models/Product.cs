using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

public class Product{
    private int _id;
    //private string _name;
    private decimal _price;
    private string _details;
    private string _image;
    
    public int Id{get{return _id;}}
    [Required(ErrorMessage = "required field")] 
    public string Name{get;set;}
    public decimal Price{get{return _price;}}
    public string Details{get{return _details;}}
    public string Image{get{return _image;}}
    public int Amount{get;set;}
    public string Category{get;set;}

    [JsonConstructor]
    public Product(int id, string name, 
                            decimal price,[StringLength(50,MinimumLength =3,ErrorMessage ="field from 3 to 50 chars")] string details, 
                            string image, int amount, string category){
        _id = id;
        Name = name;
        _price = price;
        _details = details;
        _image = image;
        Amount = amount;
        Category = category;
    }
    public Product(int id){
        _id = id;
    }
}

