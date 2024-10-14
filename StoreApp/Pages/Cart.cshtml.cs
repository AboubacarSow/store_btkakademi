using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.Models;
using StoreApp.Infrastructure.Extensions;

namespace StoreApp.Pages;

public class CartModel : PageModel
{
    private readonly IServiceManager _manager;
    public Cart _cart {get;set;}

    public string? ReturnUrl{get; set;}

    public CartModel(IServiceManager manager, Cart cart)
    {
        _manager = manager;
        _cart= cart;
    }
    //A quoi sert cette methode et quand on l'utilise?
    public void OnGet(string returnUrl)
    {
        ReturnUrl=returnUrl ?? "/";
       
    }
    public IActionResult OnPost(int productId, string returnUrl)
    {
        Product ? product =_manager.ProductService.GetOneProduct(productId,false);
        if(product is not null)
        {
            _cart.AddItem(product,1);
        }
        return RedirectToPage(new {returnUrl=returnUrl});
    }
    public IActionResult OnPostRemove(int id, string returnUrl)
    {
        //les paramètres id et returnUrl viennent de notre forme dans Cart.cshtml ayant pour 
        //asp-page-handler="OnPostRemove"
        _cart.RemoveItem(_cart.Lines.First(l=>l.Product.ProductId.Equals(id)).Product);
        return Page();
    }
}