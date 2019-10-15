using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.Service;
using System.Collections.Generic;
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

        public BoardGamesCollectionController(IBoardGamesService boardGamesService, IBoardGamePublishersService publishersService, IBoardGameStatesService statesService)
        {
            this.boardGamesService = boardGamesService;
            this.publishersService = publishersService;
            this.statesService = statesService;
            this.userTypeService = new UserTypeService(this, RedirectToAction("Index", "Home"));

            var allPublishers = publishersService.GetAll().AsEnumerable();
            List<SelectListItem> listOfPublishers = new List<SelectListItem>();
            foreach (var publisher in allPublishers)
                listOfPublishers.Add(new SelectListItem { Value = publisher.BoardGamePublisherId.ToString(), Text = publisher.Name });
            ViewBag.Publishers = new SelectList(listOfPublishers, "Value", "Text");

            var allStates = statesService.GetAll().AsEnumerable();
            List<SelectListItem> listOfStates = new List<SelectListItem>();
            foreach (var state in allStates)
                listOfStates.Add(new SelectListItem { Value = state.BoardGameStateId.ToString(), Text = state.Name });
            ViewBag.States = new SelectList(listOfStates, "Value", "Text");
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
                PlayerCount = bg.PlayerCount,
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
                    PlayerCount = int.Parse(collection["PlayerCount"]),
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
                    PlayerCount = int.Parse(collection["PlayerCount"]),
                    BoardGamePublisherId = int.Parse(collection.GetValue("BoardGamePublisher").AttemptedValue),
                    BoardGameStateId = int.Parse(collection.GetValue("BoardGameState").AttemptedValue)
                });
                return RedirectToAction(nameof(BoardGamesCollection));
            }, UserType.Administrator);
        }
    }
}