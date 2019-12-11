using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class DiscountCode
    {
        public int DiscountCodeId { get; set; }
        [Display(Name = "Kod")]
        [RegularExpression(@"^[A-z0-9]{5,10}$")]
        public string Code { get; set; }
        public int DiscountCodeStatusId { get; set; }
        [Display(Name = "Status kodu")]
        public DiscountCodeStatus DiscountCodeStatus { get; set; }
        public int BoardGameId { get; set; }
        public BoardGame BoardGame { get; set; }
    }
}
