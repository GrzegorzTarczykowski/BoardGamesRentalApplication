using BoardGamesRentalApplication.BLL.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGamesRentalApplication.Models
{
    public class BoardGamesCollectionPageData
    {
        public IPagedList<BoardGame> BoardGames { get; set; }
        public ICollection<string> SortingOptions { get; set; }
        public IList<Filter> FilterParameters { get; set; }
    }
}