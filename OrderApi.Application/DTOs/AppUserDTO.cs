using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
