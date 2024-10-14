using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace StoreApp.Infrastructure.TagHelpers;
[HtmlTargetElement("td", Attributes="user-role")]
public class UserRoleTagHelper : TagHelper
{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    [HtmlAttributeName("userName")]
    public string? UserName { get; set;}

    public UserRoleTagHelper(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
     {
        _userManager = userManager;
        _roleManager = roleManager;
     }

     public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
     {
        var user =await  _userManager.FindByNameAsync(UserName);
        //code permettant de recuperer tous les roles de notre IdentityUser
        //C'est pourquoi il y a une différence entre UserRoles et Roles

        TagBuilder ul = new TagBuilder("ul");

        var roles = _roleManager.Roles.ToList();
        foreach (var role in roles)
        {
            TagBuilder li = new TagBuilder("li");
            var isInRole= await _userManager.IsInRoleAsync(user, role.Name);
            li.InnerHtml.Append($"{role,-8} :  {isInRole}");

            ul.InnerHtml.AppendHtml(li);
        }


        output.Content.AppendHtml(ul);


     }
    
}