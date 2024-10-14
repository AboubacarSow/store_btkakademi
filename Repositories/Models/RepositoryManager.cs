using System.Linq.Expressions;
using Entities.Models;
using Entities.RequestParameters;
using Repositories.Contracts;

namespace Repositories.Models;


public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _context;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IOrderRepository _orderRepository;

    public RepositoryManager(IProductRepository productRepository, RepositoryContext context, ICategoryRepository categoryRepository,IOrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _context = context;
        _categoryRepository = categoryRepository;
        _orderRepository=orderRepository;
    }
    public IProductRepository Product =>_productRepository;

    public ICategoryRepository Category => _categoryRepository;

    public IOrderRepository Order => _orderRepository;

     #region Implementation Incomprise
    public IQueryable<Product> FindAll(bool TrackChange)=> throw new NotImplementedException();
    public Product? FindByCondition(Expression<Func<Product, bool>> expression, bool trackChanges)=>throw new NotImplementedException();
    public IQueryable<Product> GetAllProducts(bool trackChange) => throw new NotImplementedException();
    public IQueryable<Product> GetShowcaseProducts(bool trackChange) => throw new NotImplementedException();
    public IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters parameter) => throw new NotImplementedException();
    public Product? GetOneProduct(int id, bool trackChanges)=> throw new NotImplementedException();
    public void CreateProduct(Product product)=> throw new NotImplementedException();
    public void Create(Product product) => throw new NotImplementedException();
    public void Remove(Product product) => throw new NotImplementedException();
    public void DeleteOneProduct(Product product) => throw new NotImplementedException();
    public void UpdateOneProduct(Product product) => throw new NotImplementedException();
    public void Update(Product product) => throw new NotImplementedException();

     #endregion 
   
    public void Save()=>_context.SaveChanges();


   
   
}