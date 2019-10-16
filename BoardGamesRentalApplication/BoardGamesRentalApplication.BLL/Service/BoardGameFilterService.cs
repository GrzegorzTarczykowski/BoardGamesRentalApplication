using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.BLL.Extensions;
using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.BLL.Models;
using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BLL.Service
{
    public class BoardGameFilterService : IBoardGameFilterService
    {
        private readonly IRepository<BoardGameCategory> boardGameCategoryRepository;

        public BoardGameFilterService(IRepository<BoardGameCategory> boardGameCategoryRepository)
        {
            this.boardGameCategoryRepository = boardGameCategoryRepository;
        }

        public IQueryable<BoardGame> FilterBy(IQueryable<BoardGame> boardGamesQuery, Dictionary<int, int> filterParametersDict)
        {
            if (filterParametersDict.TryGetValue((int)BoardGameFilterParameter.FilterByBoardGameCategory, out int boardGameCategoryValue))
            {
                boardGamesQuery.Where(bg => bg.BoardGameCategoryId == boardGameCategoryValue);
            }

            if (filterParametersDict.TryGetValue((int)BoardGameFilterParameter.FilterByMinPlayerCount, out int minPlayerCountValue))
            {
                boardGamesQuery.Where(bg => bg.MinPlayerCount >= minPlayerCountValue);
            }

            if (filterParametersDict.TryGetValue((int)BoardGameFilterParameter.FilterByMaxPlayerCount, out int maxPlayerCountValue))
            {
                boardGamesQuery.Where(bg => bg.MaxPlayerCount <= maxPlayerCountValue);
            }

            if (filterParametersDict.TryGetValue((int)BoardGameFilterParameter.FilterByGameTime, out int gameTimeValue))
            {
                switch ((FilterByGameTime)gameTimeValue)
                {
                    case FilterByGameTime.LessThanFifteenMin:
                        boardGamesQuery = boardGamesQuery.Where(bg => bg.GameTimeInMinutes < 15);
                        break;
                    case FilterByGameTime.LessThanThirtyMin:
                        boardGamesQuery = boardGamesQuery.Where(bg => bg.GameTimeInMinutes < 30);
                        break;
                    case FilterByGameTime.LessThanAnHour:
                        boardGamesQuery = boardGamesQuery.Where(bg => bg.GameTimeInMinutes < 60);
                        break;
                    case FilterByGameTime.ForMoreThanAnHour:
                        boardGamesQuery = boardGamesQuery.Where(bg => bg.GameTimeInMinutes >= 60);
                        break;
                }

            }

            if (filterParametersDict.TryGetValue((int)BoardGameFilterParameter.FilterByAge, out int ageValue))
            {
                switch ((FilterByAge)ageValue)
                {
                    case FilterByAge.FromFiveToSevenYears:
                        boardGamesQuery = boardGamesQuery.Where(bg => bg.MinimumAge >= 5 && bg.MinimumAge <= 7);
                        break;
                    case FilterByAge.FromEightToElevenYears:
                        boardGamesQuery = boardGamesQuery.Where(bg => bg.MinimumAge >= 8 && bg.MinimumAge <= 11);
                        break;
                    case FilterByAge.FromTwelveToSeventeenYears:
                        boardGamesQuery = boardGamesQuery.Where(bg => bg.MinimumAge >= 12 && bg.MinimumAge <= 17);
                        break;
                    case FilterByAge.ForEighteenYears:
                        boardGamesQuery = boardGamesQuery.Where(bg => bg.MinimumAge >= 18);
                        break;
                }
            }

            return boardGamesQuery;
        }

        public IList<Filter> GetAllFilterParameters()
        {
            IList<Filter> filters = new List<Filter>();

            Filter filter = new Filter();
            filter.ID = (int)BoardGameFilterParameter.FilterByBoardGameCategory;
            filter.Header = BoardGameFilterParameter.FilterByBoardGameCategory.GetDescription();
            foreach (var boardGameCategory in boardGameCategoryRepository.GetAll())
            {
                filter.FilterOptions.Add(new FilterOption()
                {
                    FilterOptionId = boardGameCategory.BoardGameCategoryId
                                                            ,
                    FilterOptionText = boardGameCategory.Name
                });
            }
            filters.Add(filter);

            filter = new Filter();
            filter.ID = (int)BoardGameFilterParameter.FilterByMinPlayerCount;
            filter.Header = BoardGameFilterParameter.FilterByMinPlayerCount.GetDescription();
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByPlayerCount.OnePlayer
                ,
                FilterOptionText = FilterByPlayerCount.OnePlayer.GetDescription()
            });
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByPlayerCount.TwoPlayers
                ,
                FilterOptionText = FilterByPlayerCount.TwoPlayers.GetDescription()
            });
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByPlayerCount.ThreePlayers
                ,
                FilterOptionText = FilterByPlayerCount.ThreePlayers.GetDescription()
            });
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByPlayerCount.FourPlayers
                ,
                FilterOptionText = FilterByPlayerCount.FourPlayers.GetDescription()
            });
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByPlayerCount.FivePlayers
                ,
                FilterOptionText = FilterByPlayerCount.FivePlayers.GetDescription()
            });
            filters.Add(filter);

            filter = new Filter();
            filter.ID = (int)BoardGameFilterParameter.FilterByMaxPlayerCount;
            filter.Header = BoardGameFilterParameter.FilterByMaxPlayerCount.GetDescription();
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByPlayerCount.TwoPlayers
                ,
                FilterOptionText = FilterByPlayerCount.TwoPlayers.GetDescription()
            });
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByPlayerCount.ThreePlayers
                ,
                FilterOptionText = FilterByPlayerCount.ThreePlayers.GetDescription()
            });
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByPlayerCount.FourPlayers
                ,
                FilterOptionText = FilterByPlayerCount.FourPlayers.GetDescription()
            });
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByPlayerCount.FivePlayers
                ,
                FilterOptionText = FilterByPlayerCount.FivePlayers.GetDescription()
            });
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByPlayerCount.SixPlayers
                ,
                FilterOptionText = FilterByPlayerCount.SixPlayers.GetDescription()
            });
            filters.Add(filter);

            filter = new Filter();
            filter.ID = (int)BoardGameFilterParameter.FilterByGameTime;
            filter.Header = BoardGameFilterParameter.FilterByGameTime.GetDescription();
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByGameTime.LessThanFifteenMin
                ,
                FilterOptionText = FilterByGameTime.LessThanFifteenMin.GetDescription()
            });
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByGameTime.LessThanThirtyMin
                ,
                FilterOptionText = FilterByGameTime.LessThanThirtyMin.GetDescription()
            });
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByGameTime.LessThanAnHour
                ,
                FilterOptionText = FilterByGameTime.LessThanAnHour.GetDescription()
            });
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByGameTime.ForMoreThanAnHour
                ,
                FilterOptionText = FilterByGameTime.ForMoreThanAnHour.GetDescription()
            });
            filters.Add(filter);

            filter = new Filter();
            filter.ID = (int)BoardGameFilterParameter.FilterByAge;
            filter.Header = BoardGameFilterParameter.FilterByAge.GetDescription();
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByAge.FromFiveToSevenYears
                ,
                FilterOptionText = FilterByAge.FromFiveToSevenYears.GetDescription()
            });
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByAge.FromEightToElevenYears
                ,
                FilterOptionText = FilterByAge.FromEightToElevenYears.GetDescription()
            });
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByAge.FromTwelveToSeventeenYears
                ,
                FilterOptionText = FilterByAge.FromTwelveToSeventeenYears.GetDescription()
            });
            filter.FilterOptions.Add(new FilterOption()
            {
                FilterOptionId = (int)FilterByAge.ForEighteenYears
                ,
                FilterOptionText = FilterByAge.ForEighteenYears.GetDescription()
            });
            filters.Add(filter);
            return filters;
        }

        public string GetSelectedFilterOptionByFilterParameters(IList<Filter> filterParameters)
        {
            Dictionary<int, int> keyFilterIdValueSelectedFilterOption = new Dictionary<int, int>();
            foreach (var filter in filterParameters)
            {
                keyFilterIdValueSelectedFilterOption.Add(filter.ID, filter.SelectedFilterOption);
            }
            return string.Join(";", keyFilterIdValueSelectedFilterOption.Select(x => $"{x.Key}={x.Value}"));
        }

        public Dictionary<int, int> GetFilterParametersDictByString(string selectedFilterOption)
        {
            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
            if (!string.IsNullOrWhiteSpace(selectedFilterOption))
            {
                var selectedFilterOptionArray = selectedFilterOption.Split(';');
                foreach (var selectedFilterOptionPair in selectedFilterOptionArray)
                {
                    var keyValuePair = selectedFilterOptionPair.Split('=');
                    keyValuePairs.Add(int.Parse(keyValuePair[0]), int.Parse(keyValuePair[1]));
                }
            }
            return keyValuePairs;
        }

        public void SetSelectedFilterOptionInFilterParameters(IList<Filter> filterParameters, Dictionary<int, int> filterParametersDict)
        {
            foreach (var keyValuePair in filterParametersDict)
            {
                filterParameters[keyValuePair.Key].SelectedFilterOption = keyValuePair.Value;
            }
        }
    }
}
