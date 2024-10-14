using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Entities.Models;
using Repositories.Config;
namespace Repositories.Models;

//RepositoryContext constitue le répresent de notre base de données du coté de notre code
//Dans ce cas il contient nos tableaux: ici le tableau Products qui contient Product
public class RepositoryContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Product> Products{get; set;}
    public DbSet<Category> Categories{get; set;}
    public DbSet<Order> Orders{get; set;}
    public RepositoryContext()
    {

    }
    //Pourquoi ce constructeur???
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductConfig());
        modelBuilder.ApplyConfiguration(new CategoryConfig());
        modelBuilder.ApplyConfiguration(new IdentityRoleConfig());

        //Moyen alternatif
       // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
}