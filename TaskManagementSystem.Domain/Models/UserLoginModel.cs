using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Domain.Models
{
    public class UserLoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
