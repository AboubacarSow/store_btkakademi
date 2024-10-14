using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Services.Contracts;
using Entities.Models;
using StoreApp.Models;
using Entities.RequestParameters;
using Entities.Dtos;
namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles="Admin")]
public class ProductController : Controller
{
    private readonly IServiceManager _manager; 

    public ProductController(IServiceManager manager)
    {
        _manager= manager;
    }
    [Area("Admin")]
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
    
    [Area("Admin")]
    public IActionResult Create()
    {
        ViewBag.Categories=new SelectList(_manager.CategoryService.GetAllCategories(false),
        "CategoryId","CategoryName","1");
        return View();
    }
    //Pourquoi cette methode doit être executer de façon asynchrone
    [Area("Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto,IFormFile file)
    {
       var model= _manager.ProductService.GetAllProducts(false).ToList();

            
        if(ModelState.IsValid)
        {
            //file operation
            //Construction du path de l'image
            string path = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot","Images",file.FileName);
            //Telechargement de l'image physique dans notre fichier 
            using(var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            productDto.ImageUrl=String.Concat("/images/",file.FileName);

            if(!model.Any(p=>p.ProductName.Equals(productDto.ProductName))){
                _manager.ProductService.CreateProduct(productDto);
                TempData["success"] =$"{productDto.ProductName} has been successfully created";
                return RedirectToAction("Index");
            }
            else
            {
                string message ="This product has already been created";
                ModelState.AddModelError("",message);
                return View();
            }
            
        }
         return View();
            
    }
    
    [Area("Admin")]
    public IActionResult Update([FromRoute(Name="id")]int id)
    {
        ViewBag.Categories=new SelectList(_manager.CategoryService.GetAllCategories(false),
        "CategoryId","CategoryName","1");
        var model= _manager.ProductService.GetOneProductForUpdate(id,false);
        return View(model);
    }
    [Area("Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromForm] ProductDtoForUpdate productDto, IFormFile file)
    {
       if(ModelState.IsValid)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot","Images",file.FileName);
            //Telechargement de l'image physique dans notre fichier 
            using(var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            productDto.ImageUrl= String.Concat("/images/",file.FileName);
             _manager.ProductService.UpdateOneProduct(productDto);
             TempData["warning"] =$"The product {productDto.ProductName} has been successfully update";
            return RedirectToAction("Index");
            
        }
         return View();       
    }
    [Area("Admin")]

    public IActionResult Delete([FromRoute(Name="id")] int id)
    {
        _manager.ProductService.DeleteOneProduct(id);
        TempData["danger"] =$"The product {id} has been deleted";
        return RedirectToAction("Index");
    }




}
