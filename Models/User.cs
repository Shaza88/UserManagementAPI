using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models
{
    public class User
    {
        
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters are allowed in the name.")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters are allowed in the name.")]
        [StringLength(50, ErrorMessage = "Middle name can't be longer than 50 characters.")]
        public string? MiddleName { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters are allowed in the name.")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set;}

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
        [EmailAddress(ErrorMessage ="Invalid email address. Please enter a valid email.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [Range(typeof(DateTime), "1/1/1920", "1/1/2100", ErrorMessage = "Date of Birth must be a valid date.")]
        public DateTime DateOfBirth { get; set; }
    }
}
