namespace Entities.Dtos;


public record ProductDtoForUpdate : ProductDto
{
    public bool ShowCase {get; set;}
}