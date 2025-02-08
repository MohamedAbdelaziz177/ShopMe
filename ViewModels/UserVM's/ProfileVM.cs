using System.ComponentModel.DataAnnotations;

namespace E_Commerce2.ViewModels.UserVM_s
{
    public class ProfileVM
    {
        [Required(ErrorMessage = "The First Name field is required"), MaxLength(100)]
        public string FirstName { get; set; } 

        [Required(ErrorMessage = "The Last Name field is required"), MaxLength(100)]
        public string LastName { get; set; } 

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } 

        [Phone(ErrorMessage = "The format of the Phone Number is not valid"), MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Required, MaxLength(200)]
        public string Address { get; set; } 
    }
}
