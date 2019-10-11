using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using System.Linq;

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

        public IQueryable<BoardGame> GetAll()
        {
            return boardGameRepository.GetAll(nameof(BoardGame.BoardGamePublisher)
                                            , nameof(BoardGame.BoardGameState));
        }

        public IQueryable<BoardGame> GetFourRecommendedBoardGames()
        {
            return boardGameRepository.GetAll().Take(4);
        }
    }
}
