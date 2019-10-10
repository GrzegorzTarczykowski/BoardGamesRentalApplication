using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Models;
using System.Linq;
using System.Web.Mvc;
using BoardGame = BoardGamesRentalApplication.Models.BoardGame;

namespace BoardGamesRentalApplication.Controllers
{
    public class BoardGamesCollectionController : Controller
    {
        private readonly IBoardGamesService boardGamesService;

        public BoardGamesCollectionController(IBoardGamesService boardGamesService)
        {
            this.boardGamesService = boardGamesService;
        }

        // GET: BoardGamesCollection
        public ActionResult BoardGamesCollection()
        {
            if (string.IsNullOrEmpty(Session["Username"] as string) || (Session["UserType"] is UserType && (UserType)Session["UserType"] != UserType.Administrator))
                return RedirectToAction("Index", "Home");
            return View(boardGamesService.GetAll().Select(bg => new BoardGame()
            {
                BoardGameId = bg.BoardGameId,
                Name = bg.Name,
                Description = bg.Description,
                Content = bg.Content,
                Image = bg.Image,
                PlayerCount = bg.PlayerCount,
                MinimumAge = bg.MinimumAge,
                BoardGameStateName = bg.BoardGameState.Name,
                BoardGamePublisherName = bg.BoardGamePublisher.Name
            }).ToList());
        }
    }
}