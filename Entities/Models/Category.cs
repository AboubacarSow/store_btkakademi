using System.ComponentModel.DataAnnotations;
namespace Entities.Models;

public class Category
{
    public int CategoryId{get; set;}
     [Required(ErrorMessage="This field is required")]
    public string? CategoryName{get; set;}

    public ICollection<Product> ? Products {get; set;} //Collection de naviguation
}