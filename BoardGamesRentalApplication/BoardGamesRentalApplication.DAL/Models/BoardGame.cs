using System.Collections.Generic;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class BoardGame
    {
        public int BoardGameId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BoardGameEvaluation> BoardGameEvaluations { get; set; }
        public virtual ICollection<BoardGameCategory> BoardGameCategories { get; set; }
        public virtual BoardGamePublisher Publisher { get; set; }
    }
}
