﻿using System;
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
        public DateTime CreateDate { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
