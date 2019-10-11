using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class BoardGameController : Controller
    {
        IRepository<BoardGame> boardGameRepository;
        IRepository<BoardGamePublisher> boardGamePublisherRepository;
        IRepository<BoardGameState> boardGameStateRepository;

        public BoardGameController(IRepository<BoardGame> boardGameRepository, IRepository<BoardGamePublisher> boardGamePublisherRepository, IRepository<BoardGameState> boardGameStateRepository)
        {
            this.boardGameRepository = boardGameRepository;
            this.boardGamePublisherRepository = boardGamePublisherRepository;
            this.boardGameStateRepository = boardGameStateRepository;
        }

        // GET: BoardGame
        public ActionResult Details(int id)
        {
            BoardGame boardGame = boardGameRepository.FindById(id);
            BoardGamePublisher publisher = boardGamePublisherRepository.FindById(boardGame.BoardGamePublisherId);
            BoardGameState state = boardGameStateRepository.FindById(boardGame.BoardGameStateId);
            return View(new Models.BoardGame
            {
                BoardGameId = boardGame.BoardGameId,
                Name = boardGame.Name,
                Content = boardGame.Content,
                Description = boardGame.Description,
                MinimumAge = boardGame.MinimumAge,
                PlayerCount = boardGame.PlayerCount,
                BoardGamePublisherName = publisher.Name,
                BoardGameStateName = state.Name,
                Image = boardGame.Image
            });
        }
    }
}