using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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