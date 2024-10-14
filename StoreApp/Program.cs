using Microsoft.EntityFrameworkCore;
using StoreApp.Infrastructure.Extensions;

//Ici nous avons l'initialisation et la construction de notre application
var builder = WebApplication.CreateBuilder(args);
//Configuration de notre Api
builder.Services.AddControllers()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

//Ici on fait usage d'un service nous permettant de travailler avec des controlleurs et des vues
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

//Usage d'un service nous permettant de créer une connection avec la base de données
/*builder.Services.AddDbContext<RepositoryContext>(options=>
    {
        options.UseSqlite(
            builder.Configuration.GetConnectionString("sqlconnection"),
             b => b.MigrationsAssembly("StoreApp"));
    }
);*/
//Extensions de la methode qui est commenté en dessus
builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.ConfigureIdentity();

// Ajout de services pour les sessions

/*
  builder.Services.AddDistributedMemoryCache();
 builder.Services.AddSession(options =>
 {
    options.Cookie.Name="StoreApp.Session";
    options.IdleTimeout=TimeSpan.FromMinutes(10);
 });
 builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
*/
//Extensions de la methode qui est commenté en dessus
builder.Services.ConfigureSession();
builder.Services.ConfigureApplicationCookie();

//Ici on indique à notre builder s'il rencontre nos interfaces suivantes qu'il les cooresponde à nos classes indiquées
//Ici c'est comme un enregistrement que l'on fait( registre)
/*builder.Services.AddScoped<IRepositoryManager,RepositoryManager>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();*/

builder.Services.ConfigureRepositoryRegistration();

//Cas des Services
/*builder.Services.AddScoped<IServiceManager,ServiceManager>();
builder.Services.AddScoped<IProductService,ProductManager>();
builder.Services.AddScoped<ICategoryService,CategoryManager>();
builder.Services.AddScoped<IOrderService,OrderManager>();*/

builder.Services.ConfigureServiceRegistration();

//Services pour l'instantiation de la cart
/*builder.Services.AddScoped<SessionCart>();
builder.Services.AddScoped<Cart>(cart => SessionCart.GetCart(cart));*/ //Avec Scoped la carte sera différente selon le user
                                    //Contrairement le cas Single
builder.Services.ConfigureSessionCard();
builder.Services.ConfigureRouting();

//Pour appliquer l'auto mapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

/*MapGet est une fonction qui permet d'afficher du contenu sans la configuration MVC
C'est le model le plus simple possible en Web
app.MapGet("/", () => "Hello World!");

Afficher par exemple Bonjour le monde 
app.MapGet("/fr", ()=> "Bonjour le monde");*/
app.UseStaticFiles(); //For static files
app.UseSession();  //For session
app.UseHttpsRedirection();
app.UseRouting();
//Mehodes pour la configuration de Identity
app.UseAuthentication();
app.UseAuthorization();
/*Etant donné que nous allons travailler sous le modèle MVC
Il est indispensable de définir le routage du controlleur
C'est pourquoi nous n'allons pas faire usage de le méthode MapGet
Cette méthode prend donc deux paramètres: name et pattern */
    //Definition de Controlleur route pour une area
app.MapAreaControllerRoute(
        name :"AdminArea",
        areaName:"Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");


    //Définition de ControllerRoute par defaut
app.MapControllerRoute(
        name:"default",
        pattern:"{controller=Home}/{action=Index}/{id?}");

    //A présent nous utilisons des Razor Page
app.MapRazorPages();
app.MapControllers();



app.ConfigureAndCheckMigrations();
app.ConfigureLocalisation();
await app.ConfigureDefaultAdminUserAsync();
app.Run();
    

