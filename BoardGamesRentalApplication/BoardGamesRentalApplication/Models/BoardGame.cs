using System.ComponentModel.DataAnnotations;

namespace BoardGamesRentalApplication.Models
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

        public byte[] Image { get; set; }

        [Display(Name = "Minimum graczy")]
        public int MinPlayerCount { get; set; }

        [Display(Name = "Maksimum graczy")]
        public int MaxPlayerCount { get; set; }

        [Display(Name = "Czas gry w minutach")]
        public int GameTimeInMinutes { get; set; }

        [Display(Name = "Minimalny wiek")]
        public int MinimumAge { get; set; }

        public string BoardGameCategoryName { get; set; }

        public string BoardGamePublisherName { get; set; }

        public string BoardGameStateName { get; set; }
    }
}