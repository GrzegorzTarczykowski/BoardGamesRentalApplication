using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class BoardGameState
    {
        public int BoardGameStateId { get; set; }
        [Display(Name = "Stan")]
        public string Name { get; set; }

        public virtual ICollection<BoardGame> BoardGames { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
