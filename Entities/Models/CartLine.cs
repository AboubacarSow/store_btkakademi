namespace Entities.Models;

public class CartLine
{
    public int CartLineId{get; set;}
    //Pourquoi l'on fait directement une instanciation à ce point
    public Product Product {get;set;} =new();
    public int Quantity {get; set;}
}