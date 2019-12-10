using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Abstraction;
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
        private readonly IRepository<BoardGameCategory> categoryRepository;
        private readonly IRepository<BoardGameState> stateRepository;
        private readonly IRepository<BoardGamePublisher> publisherRepository;


        public BoardGamesCollectionController(IBoardGamesService boardGamesService, IBoardGameFilterService boardGameFilterService, IBoardGameSortService boardGameSortService, IRepository<BoardGameCategory> categoryRepository, IRepository<BoardGameState> stateRepository, IRepository<BoardGamePublisher> publisherRepository)
        {
            this.boardGamesService = boardGamesService;
            this.boardGameFilterService = boardGameFilterService;
            this.boardGameSortService = boardGameSortService;
            this.categoryRepository = categoryRepository;
            this.stateRepository = stateRepository;
            this.publisherRepository = publisherRepository;
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
                ViewBag.BoardGamePublisher = new SelectList(publisherRepository.GetAll().Select(bgp => new SelectListItem { Text = bgp.Name, Value = bgp.BoardGamePublisherId.ToString() }), "Value", "Text");
                ViewBag.BoardGameState = new SelectList(stateRepository.GetAll().Select(bgs => new SelectListItem { Text = bgs.Name, Value = bgs.BoardGameStateId.ToString() }), "Value", "Text");
                ViewBag.BoardGameCategory = new SelectList(categoryRepository.GetAll().Select(bgc => new SelectListItem { Text = bgc.Name, Value = bgc.BoardGameCategoryId.ToString() }), "Value", "Text");
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
                DAL.Models.BoardGame model = boardGamesService.FindBy(bg => bg.BoardGameId == id, nameof(BoardGamePublisher), nameof(BoardGameState), nameof(BoardGameCategory)).Single();
                ViewBag.BoardGamePublisher = new SelectList(publisherRepository.GetAll().Select(bgp => new SelectListItem { Text = bgp.Name, Value = bgp.BoardGamePublisherId.ToString() }), "Value", "Text", model.BoardGamePublisherId);
                ViewBag.BoardGameState = new SelectList(stateRepository.GetAll().Select(bgs => new SelectListItem { Text = bgs.Name, Value = bgs.BoardGameStateId.ToString() }), "Value", "Text", model.BoardGameStateId);
                ViewBag.BoardGameCategory = new SelectList(categoryRepository.GetAll().Select(bgc => new SelectListItem { Text = bgc.Name, Value = bgc.BoardGameCategoryId.ToString() }), "Value", "Text", model.BoardGameCategoryId);
                //ViewBag.BoardGamePublisher.Text = model.BoardGamePublisher.Name;
                //ViewBag.BoardGameState.Text = model.BoardGameState.Name;
                //ViewBag.BoardGameCategory.Text = model.BoardGameCategory.Name;
                //ViewBag.BoardGamePublisher.Value = model.BoardGamePublisher.BoardGamePublisherId.ToString();
                //ViewBag.BoardGameState.Value = model.BoardGameState.BoardGameStateId.ToString();
                //ViewBag.BoardGameCategory.Value = model.BoardGameCategory.BoardGameCategoryId.ToString();
                return View(model);
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
                    BoardGamePublisherId = int.Parse(collection.GetValue("BoardGamePublisher.Name").AttemptedValue),
                    BoardGameStateId = int.Parse(collection.GetValue("BoardGameState.Name").AttemptedValue),
                    BoardGameCategoryId = int.Parse(collection.GetValue("BoardGameCategory.Name").AttemptedValue)
                });
                return RedirectToAction(nameof(BoardGamesCollection));
            }, UserType.Administrator);
        }

        public ActionResult Delete(int id)
        {
            return userTypeService.Authorize(() =>
            {
                return View(boardGamesService.FindBy(bg => bg.BoardGameId == id, nameof(BoardGamePublisher), nameof(BoardGameState), nameof(BoardGameCategory)).Single());
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