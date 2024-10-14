namespace Entities.Models;

public class Cart
{
    public List<CartLine> Lines {get;set;}
    public Cart()
    {
        Lines=new List<CartLine>();
    }
    //Methode permettant d'ajouter un item(un item contient un produit et la quantité de ce produit)
    public virtual void AddItem(Product product, int quantity)
    {
        CartLine? line= Lines.Where(l=>l.Product.ProductName.Equals(product.ProductName)).FirstOrDefault();
        
        if(line is null)
            Lines.Add(new CartLine(){Product=product,Quantity=quantity});
        else
            line.Quantity += quantity;      
    }
    //Methode permettant de supprimer un item(une CartLine)
    public virtual void RemoveItem(Product product) => 
        Lines.RemoveAll(line=>line.Product.ProductId.Equals(product.ProductId));  
    //Methode permettant de calculer la somme totale des items
    public decimal ComputeTotalValue()=>
        Lines.Sum(s => s.Product.Price* s.Quantity);
    
    //Methode permettant de supprimer tous les items de la cart
    public virtual void Clear()=>Lines.Clear();
}