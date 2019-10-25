using BoardGamesRentalApplication.DAL.Models;
using System.Linq;

namespace BoardGamesRentalApplication.BLL.IService
{
    public interface IBoardGamesService
    {
        IQueryable<BoardGame> GetFourRecommendedBoardGames();
        IQueryable<BoardGame> GetAll();
        void AddBoardGame(BoardGame boardGame);
        BoardGame FindById(int id);
        void UpdateBoardGame(int id, BoardGame boardGame);
        void RemoveBoardGame(int id);
    }
}
