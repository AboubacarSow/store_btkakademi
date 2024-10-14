using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

namespace StoreApp.Controllers;

public class CategoryController : Controller
{
    private readonly IServiceManager _manager;

    public CategoryController(IServiceManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index()
    {
        var model= _manager.CategoryService.GetAllCategories(false).ToList();
        return View(model);
    }
    public IActionResult Get([FromRoute(Name="id")]int id)
    {
        var model= _manager.CategoryService.GetOneCategory(id,false);
        return View(model);
    }


}