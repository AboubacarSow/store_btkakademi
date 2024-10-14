using Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles="Admin")]
public class OrderController : Controller
{
    private readonly IServiceManager _manager;

    public OrderController(IServiceManager manager)
    {
        _manager = manager;
    }
    public IActionResult Index()
    {
        return View(_manager.OrderService.Orders);
    }

    [Area("Admin")]
    [HttpPost]
    public IActionResult Complete([FromForm(Name="id")]int id)
    {
          _manager.OrderService.Complete(id);
           return RedirectToAction("Index");
    }
}