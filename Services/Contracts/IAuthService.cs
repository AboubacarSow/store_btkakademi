using Microsoft.AspNetCore.Identity;
using Entities.Dtos;

namespace Services.Contracts;

public interface IAuthService
{
    IEnumerable< IdentityRole > Roles {get;}

    IEnumerable< IdentityUser > GetAllUsers {get;}

    Task<IdentityResult> CreateUserAsync(UserDtoForCreation userDto);
    Task<IdentityUser> GetOneUserAsync(string user);
    Task<UserDtoForUpdate> GetOneUserForUpdateAsync(string id);
    Task UpdateAsync(UserDtoForUpdate userDto);

    Task<IdentityResult> ResetPasswordAsync (ResetPasswordDto passwordDto);

    Task<IdentityResult> DeleteOneUserAsync(string userName);


}
