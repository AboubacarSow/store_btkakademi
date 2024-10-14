using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using StoreApp.Infrastructure.Extensions;
using Entities.Models;

namespace StoreApp.Models;

public class SessionCart : Cart
{
    [JsonIgnore] //Cela éviterait que cette propriété ce réecrit d'elle-même
    public ISession?  Session {get;set;}

    

    public static SessionCart GetCart(IServiceProvider services)
    {
        //Creation de notre session
        ISession ? session= services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
        //Attention !!!!
       /* if (session == null)
        {
            throw new InvalidOperationException("Session is not available.");
        }*/
        //Creation de notre sessioncart qui se comporte de la meme maniere qu'une cart
        SessionCart cart =session?.GetJson<SessionCart>("cart") ?? new SessionCart();
        cart.Session=session;
        
        return cart;
    }
    public override void AddItem(Product product, int quantity)
    {
        base.AddItem(product, quantity);

        Session?.SetJson<SessionCart>("cart",this);
    }

    public override void Clear()
    {
        base.Clear();
        //Je me dis que Remove est une méthode de l'objet Session
        Session?.Remove("cart");
    }
    public override void RemoveItem(Product product)
    {
        base.RemoveItem(product);

        Session?.SetJson<SessionCart>("cart",this);
    }

}