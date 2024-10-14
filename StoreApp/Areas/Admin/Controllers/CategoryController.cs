using Microsoft.AspNetCore.Mvc;
namespace StoreApp.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Services.Contracts;

[Area("Admin")]
[Authorize(Roles="Admin")]
public class CategoryController : Controller
{
    private readonly IServiceManager _manager;

    public CategoryController(IServiceManager manager)
    {
        _manager = manager;
    }
    public IActionResult Index()
    {
        return View(_manager.CategoryService.GetAllCategories(false));
    }
}