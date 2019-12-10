using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class ReservationStatus
    {
        public int ReservationStatusId { get; set; }
        [Display(Name = "Status rezerwacji")]
        public string Name { get; set; }
    }
}
