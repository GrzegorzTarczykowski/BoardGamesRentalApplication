using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamesRentalApplication.Models
{
    public class RegisterUser
    {
        [Required]
        [DisplayName("Nazwa użytkownika")]
        [RegularExpression(@"^[a-zA-Z0-9]{8,20}$"
                            , ErrorMessage = "Dozwolone są znaki: a-z A-Z 0-9 Liczba znaków min: 8 max: 20")]
        public string Username { get; set; }

        [DisplayName("Imię")]
        [RegularExpression(@"^[a-zA-Z]{1,20}$"
                            , ErrorMessage = "Dozwolone są znaki: a-z A-Z Liczba znaków min: 1 max: 20")]
        public string FirstName { get; set; }

        [DisplayName("Nazwisko")]
        [RegularExpression(@"^[a-zA-Z]{1,20}$"
                            , ErrorMessage = "Dozwolone są znaki: a-z A-Z Liczba znaków min: 1 max: 20")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DisplayName("Hasło")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)[a-zA-Z0-9]{8,20}$"
                            , ErrorMessage = "Wymagane i dozwolone są znaki: a-z A-Z 0-9 Liczba znaków min: 8 max: 20")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Powtórz hasło")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Hasła muszą być takie same.")]
        public string ConfirmPassword { get; set; }
    }
}