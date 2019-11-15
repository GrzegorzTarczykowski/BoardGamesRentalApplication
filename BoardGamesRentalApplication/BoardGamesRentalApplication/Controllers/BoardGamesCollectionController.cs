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
        private readonly IBoardGameCategoryService categoryService;

        public BoardGamesCollectionController(IBoardGamesService boardGamesService, IBoardGamePublishersService publishersService, IBoardGameStatesService statesService, IBoardGameCategoryService categoryService)
        {
            this.boardGamesService = boardGamesService;
            this.publishersService = publishersService;
            this.statesService = statesService;
            this.categoryService = categoryService;
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

            var allCategories = categoryService.GetAll().AsEnumerable();
            List<SelectListItem> listOfCategories = new List<SelectListItem>();
            foreach (var category in allCategories)
                listOfCategories.Add(new SelectListItem { Value = category.BoardGameCategoryId.ToString(), Text = category.Name });
            ViewBag.Categories = new SelectList(listOfCategories, "Value", "Text");
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
                BoardGamePublisherName = bg.BoardGamePublisher.Name,
                BoardGameCategoryName = bg.BoardGameCategory.Name
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
                    BoardGameStateId = int.Parse(collection.GetValue("BoardGameState").AttemptedValue),
                    BoardGameCategoryId = int.Parse(collection.GetValue("BoardGameCategory").AttemptedValue)
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
                    BoardGamePublisherId = int.Parse(collection.GetValue("BoardGamePublisherId").AttemptedValue),
                    BoardGameStateId = int.Parse(collection.GetValue("BoardGameStateId").AttemptedValue),
                    BoardGameCategoryId = int.Parse(collection.GetValue("BoardGameCategoryId").AttemptedValue)
                });
                return RedirectToAction(nameof(BoardGamesCollection));
            }, UserType.Administrator);
        }

        public ActionResult Delete(int id)
        {
            return userTypeService.Authorize(() =>
            {
                return View(boardGamesService.FindById(id));
            }, UserType.Administrator);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            return userTypeService.Authorize(() => {
                boardGamesService.RemoveBoardGame(id);
                return RedirectToAction(nameof(BoardGamesCollection));
                }, UserType.Administrator);
        }
    }
}