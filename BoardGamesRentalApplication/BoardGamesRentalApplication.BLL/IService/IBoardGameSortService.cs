using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace BoardGamesRentalApplication.BLL.IService
{
    public interface IBoardGameSortService
    {
        ICollection<string> GetAllSortingOptions();
        IQueryable<BoardGame> SortBy(IQueryable<BoardGame> boardGamesQuery, BoardGameSortOption sortByOption);
    }
}
