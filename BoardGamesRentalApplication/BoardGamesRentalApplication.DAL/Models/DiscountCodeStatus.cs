using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class DiscountCodeStatus
    {
        public int DiscountCodeStatusId { get; set; }
        [Display(Name = "Status kodu")]
        public string Name { get; set; }
    }
}
