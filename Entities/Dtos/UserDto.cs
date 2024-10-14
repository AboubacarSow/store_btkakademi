using System.ComponentModel.DataAnnotations;


namespace Entities.Dtos;

public record UserDto
{
    [DataType(DataType.Text)]
    [Required(ErrorMessage="UserName is required")]
    public string ? UserName { get; init;}

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage="Email is required")]
    public string ? Email { get; init;}

    public string ? PhoneNumber { get; init;}

    public HashSet<string> Roles { get; set;} = new HashSet<string>();
}