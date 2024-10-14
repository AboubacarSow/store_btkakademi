using Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Entities.Dtos;
using AutoMapper;

namespace Services.Models;

public class AuthManager : IAuthService
{
    private readonly RoleManager<IdentityRole > _roleManager ;

    private readonly UserManager<IdentityUser > _userManager ;

    private readonly IMapper _mapper; 

    public AuthManager(RoleManager<IdentityRole> roleManager,
   UserManager<IdentityUser> userManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _mapper = mapper;
    }

    public IEnumerable<IdentityRole> Roles => _roleManager.Roles;

    public IEnumerable<IdentityUser> GetAllUsers => _userManager.Users;

    public async Task<IdentityResult> CreateUserAsync(UserDtoForCreation userDto) 
    {
        IdentityUser user = _mapper.Map<IdentityUser>(userDto);
        var userResult= await _userManager.CreateAsync(user , userDto.Password);

        if(!userResult.Succeeded)
        {
            throw new Exception("User couldn't be created:" + String.Join(", ", userResult.Errors.Select(e =>e.Description)));
        }
        //Code permettant d'ajouter le role de l'utilisateur
        if(userDto.Roles.Count> 0)
        {
            var roleResult = await _userManager.AddToRolesAsync(user , userDto.Roles);
            if(!roleResult.Succeeded)
            {
                throw new Exception("The System has problems with the Roles definitions:");
            }
        }

        return userResult;

    }
    public async Task<IdentityUser> GetOneUserAsync(string userName)
    {
        IdentityUser user = await _userManager.FindByNameAsync(userName);
        if(user is not null)
            return user;
        throw new Exception("User could not found");
        
         
    }

    public async Task UpdateAsync(UserDtoForUpdate userDto)
    {
        //
        IdentityUser user= await GetOneUserAsync(userDto.UserName);
        user = _mapper.Map<IdentityUser>(userDto);

        await _userManager.UpdateAsync(user);

        if(userDto.Roles.Count > 0)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user,userDto.Roles);
        }
        
    }

    public  async Task<UserDtoForUpdate> GetOneUserForUpdateAsync(string userName)
     {
        //Recuperation de l'utilisateur qui est du type IdentityUser;
        IdentityUser user = await GetOneUserAsync(userName);

         UserDtoForUpdate userDto = _mapper.Map<UserDtoForUpdate>(user);
         userDto.Roles = new HashSet<string>(Roles.Select(r =>r.Name).ToList());
         userDto.UserRoles=new HashSet<string>(await _userManager.GetRolesAsync(user));

         return userDto;
     }
    public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto ressetdto)
    {
        IdentityUser user = await GetOneUserAsync(ressetdto.UserName);

        //Suppression de Password de l'utilisateur
        await _userManager.RemovePasswordAsync(user);
        var userResult = await _userManager.AddPasswordAsync(user , ressetdto.Password);

        return userResult;
    }
    public async Task<IdentityResult> DeleteOneUserAsync(string userName)
     {
        IdentityUser user = await GetOneUserAsync(userName);

        var result= await _userManager.DeleteAsync(user);

        return result;
        
     }
}