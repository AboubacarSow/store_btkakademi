using Entities.Models;
using Repositories.Contracts;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Repositories.Extensions;

namespace Repositories.Models;

//Cette classe doit avoir comme héritage : RepositoryBase, IProductRepository
public class ProductRepository : RepositoryBase<Product> ,IProductRepository
{
    public ProductRepository(RepositoryContext context) : base(context)
    {

    }

    public IQueryable<Product> GetAllProducts(bool trackChanges)
    {
        return FindAll(trackChanges);
    }
    public IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters param)
    {
        return _context
                .Products
                .FilterByCategoryId(param.CategoryId)
                .FilterBySearchTerm(param.SearchTerm)
                .FilterByPrice(param.MinPrice,param.MaxPrice,param.IsValidPrice)
                .ToPaginate(param.PageNumber, param.PageSize);
    }
    public IQueryable<Product> GetShowcaseProducts(bool trackChanges)
    {
        return FindAll(trackChanges)
                .Where(p=>p.ShowCase.Equals(true));
    }

    public Product? GetOneProduct(int id, bool trackChanges)
    {
        return FindByCondition(p=>p.ProductId.Equals(id),trackChanges);
    }
    public void CreateProduct(Product product)=> Create(product);

    public void DeleteOneProduct(Product product)=> Remove(product);

    public void UpdateOneProduct(Product product) => Update(product);
    
    
}