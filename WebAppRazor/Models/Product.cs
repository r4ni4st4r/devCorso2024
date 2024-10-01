using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Product{
    private int _id;
    private string _name;
    private decimal? _price;
    private string _details;
    private string _image;
    
    public int Id{get{return _id;}}
    public string Name{get{return _name;}}
    public decimal? Price{get{return _price;}}
    public string Details{get{return _details;}}
    public string Image{get{return _image;}}

    [JsonConstructor]
    public Product(int id, string name, decimal? price, string details, string image){
        _id = id;
        _name = name;
        _price = price;
        _details = details;
        _image = image;
    }
    public Product(int id){
        _id = id;
    }
}

