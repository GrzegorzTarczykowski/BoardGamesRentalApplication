using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using System.Linq;
using System.Linq.Expressions;

namespace BoardGamesRentalApplication.BLL.Service
{
    public class BoardGamesService : IBoardGamesService
    {
        private readonly IRepository<BoardGame> boardGameRepository;

        public BoardGamesService(IRepository<BoardGame> boardGameRepository)
        {
            this.boardGameRepository = boardGameRepository;
        }

        public void AddBoardGame(BoardGame boardGame)
        {
            boardGameRepository.Add(boardGame);
            boardGameRepository.SaveChanges();
        }

        public BoardGame FindById(int id)
        {
            return boardGameRepository.FindById(id);
        }

        public IQueryable<BoardGame> GetAll()
        {
            return boardGameRepository.GetAll(nameof(BoardGame.BoardGamePublisher)
                                            , nameof(BoardGame.BoardGameState)
                                            , nameof(BoardGame.BoardGameCategory));
        }

        public IQueryable<BoardGame> GetFourRecommendedBoardGames()
        {
            return boardGameRepository.GetAll().Take(4);
        }

        public void UpdateBoardGame(int id, BoardGame boardGame)
        {
            BoardGame edited = boardGameRepository.FindById(id);
            if (edited != null)
            {
                edited.Name = boardGame.Name;
                edited.Description = boardGame.Description;
                edited.Content = boardGame.Content;
                edited.BoardGameStateId = boardGame.BoardGameStateId;
                edited.BoardGamePublisherId = boardGame.BoardGamePublisherId;
                edited.MinimumAge = boardGame.MinimumAge;
                edited.MinPlayerCount = boardGame.MinPlayerCount;
                edited.MaxPlayerCount = boardGame.MaxPlayerCount;
                edited.GameTimeInMinutes = boardGame.GameTimeInMinutes;
                edited.BoardGameCategoryId = boardGame.BoardGameCategoryId;

                boardGameRepository.Edit(edited);
                boardGameRepository.SaveChanges();
            }
        }

        public void RemoveBoardGame(int id)
        {
            BoardGame gameBeingDeleted = boardGameRepository.FindById(id);
            if (gameBeingDeleted != null)
            {
                boardGameRepository.Remove(new object[] { id });
                boardGameRepository.SaveChanges();
            }
        }

        public IQueryable<BoardGame> FindBy(Expression<System.Func<BoardGame, bool>> predicate, params string[] includeProperties)
        {
            return boardGameRepository.FindBy(predicate, includeProperties);
        }
    }
}
