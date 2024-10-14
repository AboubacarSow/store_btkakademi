using Entities.Models;
using Entities.Dtos;
using Entities.RequestParameters;
using AutoMapper;
using Services.Contracts;
using Repositories.Contracts;
namespace Services.Models;

public class ProductManager : IProductService
{
    private readonly IRepositoryManager _manager;

    private readonly IMapper _mapper;


    public ProductManager( IRepositoryManager manager, IMapper mapper)
    {
        _manager=manager;
        _mapper=mapper;
    }
    public IEnumerable<Product> GetAllProducts(bool trackChanges)
    {
        return _manager.Product.GetAllProducts(trackChanges);
    }
    public IEnumerable<Product> GetLatestProducts(int n ,bool trackChanges)
    {
        return _manager
                .Product
                .FindAll(trackChanges)
                .OrderByDescending(p => p.ProductId)
                .Take(n);
    }
    public IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters parameter)
    {
        return _manager.Product.GetAllProductsWithDetails(parameter);
    }
    public IEnumerable<Product> GetShowcaseProducts(bool trackChanges)
    {
        return _manager.Product.GetShowcaseProducts(trackChanges);
    }
    public Product ? GetOneProduct(int id, bool trackChanges)
    {
        Product? product=_manager.Product.GetOneProduct(id, trackChanges);
         if(product.Equals(null))
           throw new Exception("This product is not founded");
        else
            return product;
    }
    public ProductDtoForUpdate GetOneProductForUpdate(int id, bool trackChanges)
    {
        return _mapper.Map<ProductDtoForUpdate>(GetOneProduct(id,trackChanges));
    }
    public void CreateProduct(ProductDtoForInsertion productDto)
    {
        Product product= _mapper.Map<Product>(productDto);
        _manager.Product.CreateProduct(product);
        _manager.Save();
    }
    public void UpdateOneProduct(ProductDtoForUpdate productDto)
    {
        Product ? product= _mapper.Map<Product>(productDto);
        _manager.Product.UpdateOneProduct(product);
        _manager.Save();
    }
    public void DeleteOneProduct(int id)
    {
        _manager.Product.DeleteOneProduct(
                             _manager.Product
                                     .GetOneProduct(id,false));
        _manager.Save();
    }

}
