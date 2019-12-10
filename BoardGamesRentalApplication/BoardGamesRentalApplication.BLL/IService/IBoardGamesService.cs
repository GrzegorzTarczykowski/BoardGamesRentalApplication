using BoardGamesRentalApplication.DAL.Models;
using System.Linq;
using System.Linq.Expressions;

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
        IQueryable<BoardGame> FindBy(Expression<System.Func<BoardGame, bool>> predicate, params string[] includeProperties);
    }
}
