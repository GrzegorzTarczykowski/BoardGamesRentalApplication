using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class BoardGameType
    {
        public int BoardGameTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BoardGame> BoardGames { get; set; }
    }
}
