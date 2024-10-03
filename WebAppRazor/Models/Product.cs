using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class Product{
    public int Id{get;set;}
    [BindProperty]
    [Required(ErrorMessage ="required field!")]
    [StringLength(50,MinimumLength =3,ErrorMessage ="Name must be between 3 and 50 chars!")]
    public string Name{get;set;}
    /*[Required(ErrorMessage ="required field!")]
    [Range(0.01,1500,ErrorMessage ="Price must be between 3 and 50 chars!")]*/
    public decimal Price{get;set;}
    public string Details{get;set;}
    public string Image{get;set;}
    public int Amount{get;set;}
    public string Category{get;set;}
}

