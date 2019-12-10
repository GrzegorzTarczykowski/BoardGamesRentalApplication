using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class BoardGamePublisher
    {
        public int BoardGamePublisherId { get; set; }
        [Display(Name = "Wydawca")]
        public string Name { get; set; }

        public virtual ICollection<BoardGame> BoardGames { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
