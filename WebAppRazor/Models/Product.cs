using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Product{
    private int _id;
    private string _name;
    private decimal _price;
    private string _detail;
    private string _image;
    public int Id{get{return _id;}}
    public string Name{get{return _name;}}
    public decimal Price{get{return _price;}}
    public string Detail{get{return _detail;}}
    public string Image{get{return _image;}}
    public Product(int id, string name, decimal price, string detail, string image){
        _id = id;
        _name = name;
        _price = price;
        _detail = detail;
        _image = image;
    }
}

