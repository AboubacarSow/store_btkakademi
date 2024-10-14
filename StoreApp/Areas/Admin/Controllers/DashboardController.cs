using Microsoft.AspNetCore.Mvc;
namespace StoreApp.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;

[Area("Admin")]
[Authorize(Roles="Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        TempData["info"]="Welcome Back";
        ViewData["Title"] = "DashBoard";
        return View();
    }
}