using Repositories.Models;
using Repositories.Contracts;
using Services.Models;
using Services.Contracts;
using StoreApp.Models;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace StoreApp.Infrastructure.Extensions;

public static  class  ServiceExtension
{
    //Methode d'extension qui configure DbContext
    public static void ConfigureDbContext(this IServiceCollection services, 
    IConfiguration configuration)
     {
        services.AddDbContext<RepositoryContext>(options=>
        {
            options.UseSqlServer(configuration.GetConnectionString("mssqlconnection"),
             b => b.MigrationsAssembly("StoreApp"));
            options.EnableSensitiveDataLogging(true);
        });
     }
    //Methode pour la configuration de Identity
    public static void ConfigureIdentity(this IServiceCollection services)
     {
        services.AddIdentity<IdentityUser, IdentityRole>(options=>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail= true;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;

        })
        .AddEntityFrameworkStores<RepositoryContext>();
     }
    //Methode d'extension qui configure Session
    public static void ConfigureSession(this IServiceCollection services)
     {
        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.Cookie.Name="StoreApp.Session";
            options.IdleTimeout=TimeSpan.FromMinutes(10);
        });
        services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
     }
    //Methode d'extension qui configure Repository Registration

    public static void ConfigureRepositoryRegistration(this IServiceCollection services)
     {
        services.AddScoped<IRepositoryManager,RepositoryManager>();
        services.AddScoped<IProductRepository,ProductRepository>();
        services.AddScoped<ICategoryRepository,CategoryRepository>();
        services.AddScoped<IOrderRepository,OrderRepository>();
     }
    //Methode d'extension qui configure Service Registration

    public static void ConfigureServiceRegistration(this IServiceCollection services)
     {
        services.AddScoped<IServiceManager,ServiceManager>();
        services.AddScoped<IProductService,ProductManager>();
        services.AddScoped<ICategoryService,CategoryManager>();
        services.AddScoped<IOrderService,OrderManager>();
        services.AddScoped<IAuthService,AuthManager>();
     }
   public static void ConfigureApplicationCookie(this IServiceCollection services)
    {
      services.ConfigureApplicationCookie(options =>
      { 
         options.LoginPath = new PathString("/Account/Login");
         options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
         options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
         options.AccessDeniedPath = new PathString("/Account/AccessDenied");
      }
      );
    }

    //Methode qui configure Session
    public static void ConfigureSessionCard(this IServiceCollection services)
     {
        services.AddScoped<SessionCart>();
        services.AddScoped<Cart>(cart => SessionCart.GetCart(cart));
     }
    //Configuration de l'apparition des routes sur la barre 
    public static void ConfigureRouting(this IServiceCollection services)
     {
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.AppendTrailingSlash = true;        
        });
     }

}