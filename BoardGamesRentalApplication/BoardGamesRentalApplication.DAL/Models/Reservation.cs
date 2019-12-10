using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        [Display(Name = "Data rozpoczęcia")]
        public DateTime RentalStartDate { get; set; }
        [Display(Name = "Data zakończenia")]
        public DateTime RentalEndDate { get; set; }
        [Display(Name = "Wypożyczona ilość")]
        public int Count { get; set; }
        public decimal TotalCost { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BoardGameId { get; set; }
        public BoardGame BoardGame { get; set; }
        public int ReservationStatusId { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
    }
}
