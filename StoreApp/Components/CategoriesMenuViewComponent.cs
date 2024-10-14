using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
namespace StoreApp.Components;
//Elle se comporte comme un controlleur partiel et non une partiel view
public class CategoriesMenuViewComponent : ViewComponent
{
    private readonly IServiceManager _manager;

    public CategoriesMenuViewComponent( IServiceManager manager)
    {
        _manager=manager;
    }

    public IViewComponentResult Invoke()
    {
        var categories=_manager.CategoryService.GetAllCategories(false);

        return View(categories);
    }
}