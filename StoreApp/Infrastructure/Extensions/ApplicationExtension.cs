using Repositories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace StoreApp.Infrastructure.Extensions;


public static class ApplicationExtension
{
    public static void ConfigureAndCheckMigrations(this IApplicationBuilder app)
    {
        RepositoryContext context = app
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<RepositoryContext>();

        if(context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
        
    }
    public static void ConfigureLocalisation(this WebApplication app)
    {
        app.UseRequestLocalization(options =>
        {
            options.AddSupportedCultures("tr-TR","en-US")
                    .AddSupportedUICultures("tr-TR","en-US") //Pour l'interface design
                    .SetDefaultCulture("tr-TR");
        });
    }
    //Methode permettant de configurer un administrateur par defaut
    public static async Task ConfigureDefaultAdminUserAsync(this IApplicationBuilder app)
    {
         const string adminUser = "Admin";
         const string adminPassword = "admin!1234";

        //Definition d'un UserManager 
        UserManager<IdentityUser> userManager = app
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();


        //Definition d'un UserRole
        RoleManager<IdentityRole> roleManager = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

        //Definition d'un IdentityUser
        IdentityUser user = await userManager.FindByNameAsync(adminUser);
        if(user is null)
        {
            user = new IdentityUser()
            {
                UserName= adminUser,
                Email = "aboubacarsow2004@gmail.com",
                PhoneNumber = "05344095872"
            };

            var userResult = await userManager.CreateAsync(user, adminPassword);

            if(!userResult.Succeeded)
               throw new Exception("Admin user couldn't be created :"  + string.Join(", ", userResult.Errors.Select(e => e.Description)));

            var roleResult = await userManager.AddToRolesAsync(user,
            roleManager
                .Roles
                .Select(role => role.Name)
                .ToList());
            if(!roleResult.Succeeded)
              throw new Exception("The System has problems with roles definition for admin");
        }


    }
}