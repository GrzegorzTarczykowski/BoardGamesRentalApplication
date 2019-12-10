using System;
using System.ComponentModel.DataAnnotations;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Display(Name = "Nazwa użytkownika")]
        public string Username { get; set; }
        public byte[] Salt { get; set; }
        public string Password { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }
        [Display(Name = "Rodzaj użytkownika")]
        public UserType UserType { get; set; }
        [Display(Name = "Nr telefonu")]
        public string PhoneNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
