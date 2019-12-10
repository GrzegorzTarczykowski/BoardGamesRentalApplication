using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class BoardGameCategory
    {
        public int BoardGameCategoryId { get; set; }
        [Display(Name = "Kategoria")]
        public string Name { get; set; }

        public virtual ICollection<BoardGame> BoardGames { get; set; }
    }
}