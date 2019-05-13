using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BoardGamesRentalApplication.Models
{
    
    public class User
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("User name")]
        public string Username { get; set; }
        
        public byte[] Salt { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}