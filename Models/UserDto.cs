using System.ComponentModel.DataAnnotations;

namespace TwoDo.Models
{
    public class UserDTO
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
