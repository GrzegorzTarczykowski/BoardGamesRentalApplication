using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class BoardGame
    {
        public int BoardGameId { get; set; }
        [Display(Name = "Gra planszowa")]
        public string Name { get; set; }
        [Display(Name = "Opis gry")]
        public string Description { get; set; }
        [Display(Name = "Zawartość")]
        public string Content { get; set; }
        public byte [] Image { get; set; }
        [Display(Name = "Minimum graczy")]
        public int MinPlayerCount { get; set; }
        [Display(Name = "Maskimum graczy")]
        public int MaxPlayerCount { get; set; }
        [Display(Name = "Czas gry w minutach")]
        public int GameTimeInMinutes { get; set; }
        [Display(Name = "Minimalny wiek")]
        public int MinimumAge { get; set; }
        [Display(Name = "Cena wypożyczenia / Dzień")]
        public decimal RentalCostPerDay { get; set; }
        [Display(Name = "Ilość")]
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
