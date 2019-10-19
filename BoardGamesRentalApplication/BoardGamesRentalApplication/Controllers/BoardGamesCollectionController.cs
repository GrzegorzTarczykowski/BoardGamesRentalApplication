using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.Service;
using System.Linq;
using System.Web.Mvc;
using BoardGame = BoardGamesRentalApplication.Models.BoardGame;

namespace BoardGamesRentalApplication.Controllers
{
    public class BoardGamesCollectionController : Controller
    {
        private readonly IBoardGamesService boardGamesService;
        private readonly IBoardGamePublishersService publishersService;
        private readonly IBoardGameStatesService statesService;
        private readonly IUserTypeService userTypeService;
        private readonly ISelectListService selectListService;

        public BoardGamesCollectionController(IBoardGamesService boardGamesService, IBoardGamePublishersService publishersService, IBoardGameStatesService statesService, ISelectListService selectListService)
        {
            this.boardGamesService = boardGamesService;
            this.publishersService = publishersService;
            this.statesService = statesService;
            this.selectListService = selectListService;
            this.userTypeService = new UserTypeService(this, RedirectToAction("Index", "Home"));

            var allPublishers = publishersService.GetAll();
            var publisherIds = allPublishers.Select(p => p.BoardGamePublisherId.ToString()).ToList();
            var publisherNames = allPublishers.Select(p => p.Name).ToList();
            ViewBag.Publishers = selectListService.Retrieve(publisherIds, publisherNames);

            var allStates = statesService.GetAll();
            var stateIds = allStates.Select(s => s.BoardGameStateId.ToString()).ToList();
            var stateNames = allStates.Select(s => s.Name).ToList();
            ViewBag.States = selectListService.Retrieve(stateIds, stateNames);
        }

        // GET: BoardGamesCollection
        public ActionResult BoardGamesCollection()
        {
            return userTypeService.Authorize(() => View(boardGamesService.GetAll().Select(bg => new BoardGame()
            {
                BoardGameId = bg.BoardGameId,
                Name = bg.Name,
                Description = bg.Description,
                Content = bg.Content,
                Image = bg.Image,
                GameTimeInMinutes = bg.GameTimeInMinutes,
                MinPlayerCount = bg.MinPlayerCount,
                MaxPlayerCount = bg.MaxPlayerCount,
                MinimumAge = bg.MinimumAge,
                BoardGameStateName = bg.BoardGameState.Name,
                BoardGamePublisherName = bg.BoardGamePublisher.Name
            }).ToList()), UserType.Administrator);
        }

        public ActionResult Create()
        {
            return userTypeService.Authorize(() =>
            {
                return View();
            }, UserType.Administrator);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            return userTypeService.Authorize(() =>
            {
                boardGamesService.AddBoardGame(new DAL.Models.BoardGame()
                {
                    Name = collection["Name"],
                    Description = collection["Description"],
                    Content = collection["Content"],
                    MinimumAge = int.Parse(collection["MinimumAge"]),
                    GameTimeInMinutes = int.Parse(collection["GameTimeInMinutes"]),
                    MinPlayerCount = int.Parse(collection["MinPlayerCount"]),
                    MaxPlayerCount = int.Parse(collection["MaxPlayerCount"]),
                    BoardGamePublisherId = int.Parse(collection.GetValue("BoardGamePublisher").AttemptedValue),
                    BoardGameStateId = int.Parse(collection.GetValue("BoardGameState").AttemptedValue)
                });
                return RedirectToAction("BoardGamesCollection");
            }, UserType.Administrator);
        }

        public ActionResult Edit(int id)
        {
            return userTypeService.Authorize(() =>
            {
                return View(boardGamesService.FindById(id));
            }, UserType.Administrator);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            return userTypeService.Authorize(() =>
            {
                boardGamesService.UpdateBoardGame(id, new DAL.Models.BoardGame()
                {
                    Name = collection["Name"],
                    Description = collection["Description"],
                    Content = collection["Content"],
                    MinimumAge = int.Parse(collection["MinimumAge"]),
                    GameTimeInMinutes = int.Parse(collection["GameTimeInMinutes"]),
                    MinPlayerCount = int.Parse(collection["MinPlayerCount"]),
                    MaxPlayerCount = int.Parse(collection["MaxPlayerCount"]),
                    BoardGamePublisherId = int.Parse(collection.GetValue("BoardGamePublisher").AttemptedValue),
                    BoardGameStateId = int.Parse(collection.GetValue("BoardGameState").AttemptedValue)
                });
                return RedirectToAction(nameof(BoardGamesCollection));
            }, UserType.Administrator);
        }
    }
}