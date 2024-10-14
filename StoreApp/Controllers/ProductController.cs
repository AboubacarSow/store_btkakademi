using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entities.RequestParameters;
using Repositories.Models;
using Repositories.Contracts;
using Services.Contracts;
using  StoreApp.Models;
namespace StoreApp.Controllers;

public class ProductController : Controller
{
    private readonly IServiceManager _manager;

    public ProductController(IServiceManager manager)
    {
        _manager = manager;
    }


    public IActionResult Index(ProductRequestParameters parameter)
    { 
        var products=_manager.ProductService.GetAllProductsWithDetails(parameter).ToList();
        Pagination pagination = new Pagination()
        {
            TotalItems = _manager.ProductService.GetAllProducts(false).Count(),
            ItemsPerPage = parameter.PageSize,
            CurrentPage =parameter.PageNumber
        };
        return View(new ProductListViewModel()
        {
            Products = products,
            Pagination = pagination
        });
    }
    public IActionResult Get([FromRoute(Name="id")]int id)
    {
      var model= _manager.ProductService.GetOneProduct(id,false);
      return View(model);
    } 
}
