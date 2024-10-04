using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

public class Product{
    public int Id{get;set;}
    [Required(ErrorMessage ="required field!")]
    [StringLength(20, MinimumLength =2, ErrorMessage ="Name must be between 2 and 20 chars!")]
    public string Name{get;set;}
    [Required(ErrorMessage ="required field!")]
    [Range(0.01, 2000, ErrorMessage ="Price must be between 0.01 and 2000!")]
    public decimal Price{get;set;}
    [Required(ErrorMessage ="not an email required field!")]
    [StringLength(50, MinimumLength =3, ErrorMessage ="Details must be between 3 and 50 chars!")]
    public string Details{get;set;}
    public string Image{get;set;}
    [Required(ErrorMessage ="required field!")]
    [Range(0, 500, ErrorMessage ="Amount must be between 0 and 500!")]
    public int Amount{get;set;}
    [Required(ErrorMessage ="required field!")]
    public string Category{get;set;}
    //[RegularExpression(@"^[0-9]{3}-[0-9]{2}-[0-9]{4}$")]
    //[Compare("Name",ErrorMessage ="they don't match")]
    //[CreditCard(ErrorMessage ="it is not a valid credit card")]
}

