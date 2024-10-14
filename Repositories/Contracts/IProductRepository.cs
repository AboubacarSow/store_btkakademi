using Entities.Models;
using Entities.RequestParameters;

namespace Repositories.Contracts;

//Cette interface ne doit pas être du type générique parce que ici le type avec lequel nous travaillons est déjà connu
public interface IProductRepository : IRepositoryBase<Product>
{
    IQueryable<Product> GetAllProducts(bool trackChange);
    IQueryable<Product> GetShowcaseProducts(bool trackChange);
    IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters parameter);
    Product ? GetOneProduct(int id, bool trackChanges);
    void CreateProduct(Product product);
    void DeleteOneProduct(Product product);
    void UpdateOneProduct(Product product);
}