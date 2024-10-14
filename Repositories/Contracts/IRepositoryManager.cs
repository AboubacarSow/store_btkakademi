namespace Repositories.Contracts;

public interface IRepositoryManager:IProductRepository
{
    IProductRepository Product {get;}
    ICategoryRepository Category{get;}
    IOrderRepository Order{get;}
    void Save();
}