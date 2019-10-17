using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.BLL.Extensions;
using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BLL.Service
{
    public class BoardGameSortService : IBoardGameSortService
    {
        public ICollection<string> GetAllSortingOptions()
        {
            ICollection<string> sortingOptions = new List<string>();
            foreach (BoardGameSortOption boardGameSortOption in Enum.GetValues(typeof(BoardGameSortOption)))
            {
                sortingOptions.Add(boardGameSortOption.GetDescription());
            }
            return sortingOptions;
        }

        public IQueryable<BoardGame> SortBy(IQueryable<BoardGame> boardGamesQuery, BoardGameSortOption sortByOption)
        {
            switch (sortByOption)
            {
                case BoardGameSortOption.SortAscendingByName:
                    boardGamesQuery = boardGamesQuery.OrderBy(bg => bg.Name);
                    break;
                case BoardGameSortOption.SortDescendingByName:
                    boardGamesQuery = boardGamesQuery.OrderByDescending(bg => bg.Name);
                    break;
                case BoardGameSortOption.SortAscendingByNumberOfPlayers:
                    boardGamesQuery = boardGamesQuery.OrderBy(bg => bg.MinPlayerCount);
                    break;
                case BoardGameSortOption.SortDescendingByNumberOfPlayers:
                    boardGamesQuery = boardGamesQuery.OrderByDescending(bg => bg.MinPlayerCount);
                    break;
                default:
                    boardGamesQuery = boardGamesQuery.OrderBy(bg => bg.Name);
                    break;
            }

            return boardGamesQuery;
        }
    }
}
