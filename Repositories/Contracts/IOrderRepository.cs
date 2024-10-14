using Entities.Models;
namespace Repositories.Contracts;

public interface IOrderRepository 
{
    //Properties
    IQueryable<Order> Orders {get;}
    //Propriété permettant d'indiquer le nombre d'ordres qui ne sont pas encore en cours de livraison
    int NumberOfInProcess {get;}
    //Methods
    Order ? GetOneOrder(int id);
    void Complete(int id);
    void SaveOrder(Order order);
}