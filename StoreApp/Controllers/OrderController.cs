using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using Entities.Models;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Controllers;

public class OrderController : Controller
{
    private readonly IServiceManager _manager;
    private readonly Cart _cart;


    public OrderController( IServiceManager manager,Cart cart)
    {
        _manager= manager;
        _cart= cart;
    }
    [HttpGet]
    [Authorize]
    public ViewResult Checkout() => View(new Order());


    [HttpPost]

    public IActionResult Checkout([FromForm] Order order)
    {
        
        if(_cart.Lines.Count() == 0){

            ModelState.AddModelError("", "Sorry, actually your cart's empty");
        }
        
        if(ModelState.IsValid)
        {
            order.Lines =_cart.Lines.ToArray();
            _manager.OrderService.SaveOrder(order);
            _cart.Clear();
            return RedirectToPage("/Complete", new{ Name=order.Name,OrderId= order.OrderId});
            
        }
        else
        {
            return View();//Retourne sur la même page
        }
    }
   
}