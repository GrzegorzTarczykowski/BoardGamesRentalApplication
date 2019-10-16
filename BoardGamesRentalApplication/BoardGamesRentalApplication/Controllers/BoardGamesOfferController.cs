using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class BoardGamesOfferController : Controller
    {
        private readonly IBoardGamesService boardGamesService;
        private readonly IBoardGameSortService boardGameSortService;
        private readonly IBoardGameFilterService boardGameFilterService;

        public BoardGamesOfferController(IBoardGamesService boardGamesService, IBoardGameSortService boardGameSortService, IBoardGameFilterService boardGameFilterService)
        {
            this.boardGamesService = boardGamesService;
            this.boardGameSortService = boardGameSortService;
            this.boardGameFilterService = boardGameFilterService;
        }

        [HttpGet]
        public ActionResult BoardGamesOffer(int? sortByOptionId, int? page, string selectedFilterOption)
        {
            BoardGamesCollectionPageData boardGamesCollectionPageData = new BoardGamesCollectionPageData();

            ViewBag.SelectedFilterOption = selectedFilterOption;
            boardGamesCollectionPageData.FilterParameters = boardGameFilterService.GetAllFilterParameters();
            boardGameFilterService.SetSelectedFilterOptionInFilterParameters(boardGamesCollectionPageData.FilterParameters, selectedFilterOption);
            
            ViewBag.CurrentSortOptionId = sortByOptionId;
            boardGamesCollectionPageData.SortingOptions = boardGameSortService.GetAllSortingOptions();

            boardGamesCollectionPageData.BoardGames = boardGameSortService.SortBy(boardGamesService.GetAll(), (BoardGameSortOption)(sortByOptionId ?? 0))
                                                                          .Select(bg => new BoardGame()
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
                                                                          })
                                                                          .ToPagedList(page ?? 1, 3);
            return View(boardGamesCollectionPageData);
        }

        [HttpPost]
        public ActionResult BoardGamesOffer(BoardGamesCollectionPageData boardGamesCollectionPageData)
        {
            if (ModelState.IsValid)
            {
                string selectedFilterOption = boardGameFilterService.GetSelectedFilterOptionByFilterParameters(boardGamesCollectionPageData.FilterParameters);
                ViewBag.SelectedFilterOption = selectedFilterOption;
                return RedirectToAction(nameof(BoardGamesOffer), new { selectedFilterOption });
            }
            return View(boardGamesCollectionPageData);
        }
    }
}