using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardGamesRentalApplication.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }
        [Display(Name = "Nr telefonu")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Ulica")]
        public string Street { get; set; }
        [Display(Name = "Nr lokalu")]
        public int HouseNumber { get; set; }
        [Display(Name = "Nr mieszkania")]
        public int ApartmentNumber { get; set; }
        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }
        [Display(Name = "Miejscowość")]
        public string City { get; set; }
    }
}