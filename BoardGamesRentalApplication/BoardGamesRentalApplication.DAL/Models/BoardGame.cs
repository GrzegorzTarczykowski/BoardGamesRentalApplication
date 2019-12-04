using System.Collections.Generic;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class BoardGame
    {
        public int BoardGameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public byte [] Image { get; set; }
        public int MinPlayerCount { get; set; }
        public int MaxPlayerCount { get; set; }
        public int GameTimeInMinutes { get; set; }
        public int MinimumAge { get; set; }
        public int Quantity { get; set; }

        public int BoardGameCategoryId { get; set; }
        public BoardGameCategory BoardGameCategory { get; set; }
        public int BoardGamePublisherId { get; set; }
        public BoardGamePublisher BoardGamePublisher { get; set; }
        public int BoardGameStateId { get; set; }
        public BoardGameState BoardGameState { get; set; }

        public virtual ICollection<BoardGameEvaluation> BoardGameEvaluations { get; set; }
        public virtual ICollection<BoardGameNote> BoardGameNotes { get; set; }
    }
}
