using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Services.Contracts;
using Entities.Dtos;

namespace  StoreApp.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles="Admin")]
public class UserController : Controller
{
    private readonly IServiceManager _manager;

    public UserController(IServiceManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index()
    {
        return View(_manager.AuthService.GetAllUsers);
    }

    public IActionResult Create()
    {
        return View(new UserDtoForCreation()
        {
            //Ici on recueille les roles afin de permettre à l'utilisateur de choisir son rôle sur notre interface
            Roles = new HashSet<string>(_manager
            .AuthService
            .Roles
            .Select(role => role.Name)
            .ToList()),
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] UserDtoForCreation userDto)
    {
        if(ModelState.IsValid)
        {
          IdentityResult userResult =await _manager.AuthService.CreateUserAsync(userDto);

         if(userResult.Succeeded)
         {
            return RedirectToAction("Index");
         }
         else
         {
            foreach(var err in userResult.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
        
         }
        }
        return View();

    }
    [HttpGet]
     public async Task<IActionResult> Update([FromRoute(Name="id")] string id)
     {
        var model= await _manager.AuthService.GetOneUserForUpdateAsync(id);

        return View(model);
     } 

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromForm] UserDtoForUpdate userDto) 
    {
        if(ModelState.IsValid)
        {
          await _manager.AuthService.UpdateAsync(userDto);
          return RedirectToAction("Index");
        }

        return View();
    }

    public async Task<IActionResult> ResetPassword ( [FromRoute] string id)
    {
        return View(new ResetPasswordDto(){ UserName = id});
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword ( [FromForm] ResetPasswordDto resetModel)
    {
        IdentityResult result = await _manager.AuthService.ResetPasswordAsync(resetModel);

        if(result.Succeeded)
        {

            return RedirectToAction("Index");
        }
        return View();    
    }
    [HttpPost]
    public async Task<IActionResult> Delete ([FromForm(Name="UserName")] string userName)
    {
       var result= await _manager
                .AuthService
                .DeleteOneUserAsync(userName);
        if(result.Succeeded)
        {
            return RedirectToAction("Index");
        }
        return View();
    }

}