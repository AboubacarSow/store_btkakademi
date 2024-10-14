using  Entities.Models;
using Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Models;


public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(RepositoryContext context) : base(context){}


    public IQueryable<Order> Orders => 
        _context.Orders.Include(o=>o.Lines)
                       .ThenInclude(c=>c.Product)
                       .OrderBy(o => o.Shipped)
                       .ThenByDescending(o => o.OrderId);

    public int NumberOfInProcess => _context.Orders.Count(o=>o.Shipped.Equals(false));
    public Order? GetOneOrder(int id)
    {
       return FindByCondition(o=>o.OrderId.Equals(id),false);
    }
    public void Complete(int id)
    {
       var order= FindByCondition(o=>o.OrderId.Equals(id),true);
        if(order is  null)
        {
            throw new Exception("The order is not founded");
        }
        order.Shipped=true;
        
    }
    public void SaveOrder(Order order)
    {
        //Ce bout de code est incompris pour le moment
        _context.AttachRange(order.Lines.Select(l=>l.Product));
        if(order.OrderId==0)
            _context.Orders.Add(order);
    }
}
