using System.ComponentModel.DataAnnotations;
namespace OrderApi.Application.DTOs
{
    public record AppUserDTO
   (
        int id,
        [Required] string Name,
        [Required] string TelephoneNumber,
        [Required] string Addres,
        [Required ,EmailAddress] string Email,
        [Required] string Password,
        [Required, EmailAddress] string Role
        );
}
