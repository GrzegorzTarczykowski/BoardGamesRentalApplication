using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGamesRentalApplication.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }
        public int Count { get; set; }
        public decimal TotalCost { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BoardGameId { get; set; }
        public string BoardGameName { get; set; }
        public int ReservationStatusId { get; set; }
        public bool BoardGameIsAvailable { get; set; }
        public string ImagePath { get; set; }
    }
}