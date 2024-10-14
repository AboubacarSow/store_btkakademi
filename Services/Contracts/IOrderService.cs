using Entities.Models;
namespace Services.Contracts;

public interface IOrderService 
{
    //Properties
    IQueryable<Order> Orders {get;}
    int NumberOfInProcess {get;}
    //Methods
    Order ? GetOneOrder(int id);
    void Complete(int id);
    void SaveOrder(Order order);
}