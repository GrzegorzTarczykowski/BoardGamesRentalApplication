using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }
        public int Count { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int BoardGameId { get; set; }
        public BoardGame BoardGame { get; set; }
        public int ReservationStatusId { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
    }
}
