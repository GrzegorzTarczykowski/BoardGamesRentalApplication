using BoardGamesRentalApplication.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGamesRentalApplication.Models
{
    public class HomePageData
    {
        public IEnumerable<BoardGame> RecommendedBoardGames { get; set; }
        public IList<Filter> FilterParameters { get; set; }
    }
}