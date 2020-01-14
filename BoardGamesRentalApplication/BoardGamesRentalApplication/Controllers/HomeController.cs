using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.Models;
using System.Linq;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBoardGamesService boardGamesService;
        private readonly IBoardGameFilterService boardGameFilterService;
        private readonly IBoardGameNoteService boardGameNoteService;

        public HomeController(IBoardGamesService boardGamesService, IBoardGameFilterService boardGameFilterService, IBoardGameNoteService boardGameNoteService)
        {
            this.boardGamesService = boardGamesService;
            this.boardGameFilterService = boardGameFilterService;
            this.boardGameNoteService = boardGameNoteService;
        }

        public ActionResult Index(HomePageData homePageData)
        {
            homePageData.FilterParameters = boardGameFilterService.GetAllFilterParameters();

            homePageData.RecommendedBoardGames = boardGamesService
                                                .GetFourRecommendedBoardGames()
                                                .Select(bg => new BoardGame()
                                                {
                                                    BoardGameId = bg.BoardGameId,
                                                    Name = bg.Name,
                                                    Image = bg.Image,
                                                    GameTimeInMinutes = bg.GameTimeInMinutes,
                                                    MinPlayerCount = bg.MinPlayerCount,
                                                    MaxPlayerCount = bg.MaxPlayerCount,
                                                    MinimumAge = bg.MinimumAge,
                                                    BoardGameCategoryName = bg.BoardGameCategory.Name,
                                                    BoardGamePublisherName = bg.BoardGamePublisher.Name,
                                                    BoardGameStateName = bg.BoardGameState.Name,
                                                    Content = bg.Content,
                                                    Description = bg.Description,
                                                    Quantity = bg.Quantity,
                                                    RentalCostPerDay = bg.RentalCostPerDay,
                                                    ImagePath = bg.ImagePath,
                                                    DetailsImagePath = bg.DetailsImagePath
                                                });

            homePageData.FristThreeBoardGameNote = boardGameNoteService.GetFristThreeBoardGameNote()
                                                                       .Select(bgn => new BoardGameNote()
                                                                       {
                                                                           BoardGameId = bgn.BoardGameId,
                                                                           Author = bgn.Author,
                                                                           BoardGameNoteId = bgn.BoardGameNoteId,
                                                                           Content = bgn.Content
                                                                       });

            return View(homePageData);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }
    }
}