using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class BoardGameDetailsOfferController : Controller
    {
        IRepository<BoardGame> boardGameRepository;

        public BoardGameDetailsOfferController(IRepository<BoardGame> boardGameRepository)
        {
            this.boardGameRepository = boardGameRepository;
        }

        [HttpGet]
        public ActionResult Details(int boardGameId)
        {
            BoardGame boardGame = boardGameRepository.FindBy(bg => bg.BoardGameId == boardGameId
                                                            , nameof(BoardGameCategory)
                                                            , nameof(BoardGamePublisher)
                                                            , nameof(BoardGameState))
                                                            .FirstOrDefault();
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
                Image = boardGame.Image
            });
        }

        [HttpGet]
        public ActionResult Rental(DateTime rental_from, DateTime rental_to, int boardGameId)
        {
            return RedirectToAction("Index", "Rental", new { rental_from, rental_to, boardGameId });
        }
    }
}