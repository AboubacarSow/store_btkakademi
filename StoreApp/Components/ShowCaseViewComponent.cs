using  Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace  StoreApp.Components;

public class ShowCaseViewComponent : ViewComponent
{
    public readonly IServiceManager _manager;

    public ShowCaseViewComponent(IServiceManager manager)
    {
        _manager= manager;
    }

    public IViewComponentResult Invoke()
    {
        return View(_manager.ProductService.GetShowcaseProducts(false));
    }
}