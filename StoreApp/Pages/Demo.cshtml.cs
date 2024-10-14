using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Pages;

public class DemoModel : PageModel
{
    public string FullName =>HttpContext?.Session?.GetString("name") ?? "";

    public void OnGet()
    {

    }

    public void OnPost([FromForm]string name)
    {
        HttpContext.Session.SetString("name",name);
    }
}

