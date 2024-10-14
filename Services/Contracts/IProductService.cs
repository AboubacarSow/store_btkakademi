using Entities.Models;
using Entities.Dtos;
using Entities.RequestParameters;

namespace  Services.Contracts;

public interface IProductService 
{
    IEnumerable<Product> GetAllProducts(bool trackChanges);
    IEnumerable<Product> GetLatestProducts(int n, bool trackChanges);
    IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters parameter);
    IEnumerable<Product> GetShowcaseProducts(bool trackChanges);
    Product ? GetOneProduct(int id, bool trackChanges);
    void CreateProduct(ProductDtoForInsertion productDto);
    void UpdateOneProduct(ProductDtoForUpdate productDto);
    void DeleteOneProduct(int id);
    ProductDtoForUpdate GetOneProductForUpdate(int i , bool trackChanges);
}
    
