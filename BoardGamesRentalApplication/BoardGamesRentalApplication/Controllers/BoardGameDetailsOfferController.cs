using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class BoardGameDetailsOfferController : Controller
    {
        readonly IRepository<BoardGame> boardGameRepository;
        private readonly IUserTypeService userTypeService;

        public BoardGameDetailsOfferController(IRepository<BoardGame> boardGameRepository)
        {
            this.boardGameRepository = boardGameRepository;
            this.userTypeService = new UserTypeService(this, RedirectToAction("Index", "Home"));
        }

        [HttpGet]
        public ActionResult Details(int boardGameId)
        {
            BoardGame boardGame = boardGameRepository.FindBy(bg => bg.BoardGameId == boardGameId
                                                            , nameof(BoardGameCategory)
                                                            , nameof(BoardGamePublisher)
                                                            , nameof(BoardGameState))
                                                            .FirstOrDefault();
            TempData["ImagePath"] = boardGame.ImagePath;
            return View(new Models.BoardGame
            {
                BoardGameId = boardGame.BoardGameId,
                Name = boardGame.Name,
                Content = boardGame.Content,
                Description = boardGame.Description,
                MinimumAge = boardGame.MinimumAge,
                GameTimeInMinutes = boardGame.GameTimeInMinutes,
                MinPlayerCount = boardGame.MinPlayerCount,
                MaxPlayerCount = boardGame.MaxPlayerCount,
                BoardGameCategoryName = boardGame.BoardGameCategory.Name,
                BoardGamePublisherName = boardGame.BoardGamePublisher.Name,
                BoardGameStateName = boardGame.BoardGameState.Name,
                Image = boardGame.Image,
                Quantity = boardGame.Quantity,
                RentalCostPerDay = boardGame.RentalCostPerDay,
                ImagePath = boardGame.ImagePath,
                DetailsImagePath = boardGame.DetailsImagePath
            });
        }

        [HttpGet]
        public ActionResult Rental(DateTime rentalStartDate, DateTime rentalEndDate, int boardGameId, string boardGameName, int count, string discountCode, string rentalCostPerDay)
        {
            return userTypeService.Authorize(() =>
            {
                return RedirectToAction("Index", "Rental", new { rentalStartDate, rentalEndDate, boardGameId, boardGameName, count, discountCode, rentalCostPerDay });
            }, UserType.Regular);
        }
    }
}