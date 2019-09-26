using System.Collections.Generic;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class BoardGamePublisher
    {
        public int BoardGamePublisherId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BoardGame> BoardGames { get; set; }
    }
}
