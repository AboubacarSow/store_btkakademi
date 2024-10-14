using System.ComponentModel.DataAnnotations;
namespace  Entities.Dtos;

public record ProductDto
{
    public int ProductId{get; init; }
    [Required(ErrorMessage="ProductName is required")]
    public string? ProductName{ get; init; }
    [Required(ErrorMessage="Price is required")]
    public decimal Price {get; init; }
    
    public string? Summary{ get; init;}
     
    public string? ImageUrl{get; set;}

    public int? CategoryId {get; init;}  // Foreign key 

}