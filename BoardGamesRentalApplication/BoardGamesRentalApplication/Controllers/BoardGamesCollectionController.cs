using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.Service;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BoardGame = BoardGamesRentalApplication.Models.BoardGame;

namespace BoardGamesRentalApplication.Controllers
{
    public class BoardGamesCollectionController : Controller
    {
        private readonly IBoardGamesService boardGamesService;
        private readonly IUserTypeService userTypeService;
        private readonly IBoardGameFilterService boardGameFilterService;
        private readonly IBoardGameSortService boardGameSortService;

        public BoardGamesCollectionController(IBoardGamesService boardGamesService, IBoardGameFilterService boardGameFilterService, IBoardGameSortService boardGameSortService)
        {
            this.boardGamesService = boardGamesService;
            this.boardGameFilterService = boardGameFilterService;
            this.boardGameSortService = boardGameSortService;
            this.userTypeService = new UserTypeService(this, RedirectToAction("Index", "Home"));
        }

        // GET: BoardGamesCollection
        public ActionResult BoardGamesCollection(int? page)
        {
            IQueryable<DAL.Models.BoardGame> boardGamesQuery = boardGamesService.GetAll();
            return userTypeService.Authorize(() => View(boardGameSortService.SortBy(boardGamesQuery, BLL.Enums.BoardGameSortOption.SortAscendingByName).Select(bg => new BoardGame()
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
            }).ToPagedList(page ?? 1, 5)), UserType.Administrator);
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
                return RedirectToAction(nameof(BoardGamesCollection));
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