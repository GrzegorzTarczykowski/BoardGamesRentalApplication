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
        public ActionResult Index(int? sortByOptionId, int? page, string selectedFilterOption)
        {
            BoardGamesCollectionPageData boardGamesCollectionPageData = new BoardGamesCollectionPageData();

            ViewBag.SelectedFilterOption = selectedFilterOption;
            boardGamesCollectionPageData.FilterParameters = boardGameFilterService.GetAllFilterParameters();

            Dictionary<int, int> filterParametersDict = boardGameFilterService.GetFilterParametersDictByString(selectedFilterOption);
            boardGameFilterService.SetSelectedFilterOptionInFilterParameters(boardGamesCollectionPageData.FilterParameters, filterParametersDict);
            
            ViewBag.CurrentSortOptionId = sortByOptionId;
            boardGamesCollectionPageData.SortingOptions = boardGameSortService.GetAllSortingOptions();

            IQueryable<DAL.Models.BoardGame> boardGamesQuery = boardGamesService.GetAll();
            boardGamesQuery = boardGameFilterService.FilterBy(boardGamesQuery, filterParametersDict);
            boardGamesCollectionPageData.BoardGames = boardGameSortService.SortBy(boardGamesQuery, (BoardGameSortOption)(sortByOptionId ?? 0))
                                                                          .Select(bg => new BoardGame()
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
                                                                              BoardGameCategoryName = bg.BoardGameCategory.Name,
                                                                              BoardGameStateName = bg.BoardGameState.Name,
                                                                              BoardGamePublisherName = bg.BoardGamePublisher.Name,
                                                                              Quantity = bg.Quantity,
                                                                              RentalCostPerDay = bg.RentalCostPerDay
                                                                          })
                                                                          .ToPagedList(page ?? 1, 3);
            return View(boardGamesCollectionPageData);
        }

        [HttpPost]
        public ActionResult Index(BoardGamesCollectionPageData boardGamesCollectionPageData)
        {
            if (ModelState.IsValid)
            {
                string selectedFilterOption = boardGameFilterService.GetSelectedFilterOptionByFilterParameters(boardGamesCollectionPageData.FilterParameters);
                ViewBag.SelectedFilterOption = selectedFilterOption;
                return RedirectToAction(nameof(Index), new { selectedFilterOption });
            }
            return View(boardGamesCollectionPageData);
        }
    }
}