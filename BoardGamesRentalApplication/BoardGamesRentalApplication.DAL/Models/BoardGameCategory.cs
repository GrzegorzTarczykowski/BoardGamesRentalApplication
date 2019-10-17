using System.Collections.Generic;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class BoardGameCategory
    {
        public int BoardGameCategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BoardGame> BoardGames { get; set; }
    }
}