using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.Models;
namespace StoreApp.Components;

public class CategorySummaryViewComponent : ViewComponent
{
    private readonly IServiceManager _manager;

    public CategorySummaryViewComponent(IServiceManager manager)
    {
        _manager=manager;
    }

    public string Invoke()
    {
        return _manager.CategoryService.GetAllCategories(false).Count().ToString();
    }
}

