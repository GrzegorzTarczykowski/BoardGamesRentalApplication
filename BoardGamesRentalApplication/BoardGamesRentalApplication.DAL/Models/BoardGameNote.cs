using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.DAL.Models
{
    public class BoardGameNote
    {
        public int BoardGameNoteId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }

        public int BoardGameId { get; set; }
        public BoardGame BoardGame { get; set; }
    }
}
