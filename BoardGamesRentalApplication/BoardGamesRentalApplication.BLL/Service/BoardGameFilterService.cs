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

        public IQueryable<BoardGame> FilterBy(IQueryable<BoardGame> boardGamesQuery, params BoardGameFilterParameter[] filterByParameter)
        {
            if (filterByParameter.Contains(BoardGameFilterParameter.FilterByBoardGameCategory))
            {

            }

            if (filterByParameter.Contains(BoardGameFilterParameter.FilterByMinPlayerCount))
            {

            }

            if (filterByParameter.Contains(BoardGameFilterParameter.FilterByMaxPlayerCount))
            {

            }

            if (filterByParameter.Contains(BoardGameFilterParameter.FilterByGameTime))
            {

            }

            if (filterByParameter.Contains(BoardGameFilterParameter.FilterByAge))
            {

            }

            return boardGamesQuery;
        }

        public IList<Filter> GetAllFilterParameters()
        {
            IList<Filter> filters = new List<Filter>();
            Filter filter = new Filter();
            filter.ID = 1;
            filter.Header = BoardGameFilterParameter.FilterByBoardGameCategory.GetDescription();
            foreach (var boardGameCategory in boardGameCategoryRepository.GetAll())
            {
                filter.FilterOptions.Add(new FilterOption() { FilterOptionId = boardGameCategory.BoardGameCategoryId
                                                            , FilterOptionText = boardGameCategory.Name
                });
            }
            filters.Add(filter);
            filter = new Filter();
            filter.ID = 2;
            filter.Header = BoardGameFilterParameter.FilterByMinPlayerCount.GetDescription();
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 1, FilterOptionText = "1" });
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 2, FilterOptionText = "2" });
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 3, FilterOptionText = "3" });
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 4, FilterOptionText = "4" });
            filters.Add(filter);
            filter = new Filter();
            filter.ID = 3;
            filter.Header = BoardGameFilterParameter.FilterByMaxPlayerCount.GetDescription();
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 1, FilterOptionText = "2" });
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 2, FilterOptionText = "3" });
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 3, FilterOptionText = "4" });
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 4, FilterOptionText = "5" });
            filters.Add(filter);
            filter = new Filter();
            filter.ID = 4;
            filter.Header = BoardGameFilterParameter.FilterByGameTime.GetDescription();
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 1, FilterOptionText = "< 15min" });
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 2, FilterOptionText = "< 30min" });
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 3, FilterOptionText = "< 1 godzina" });
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 4, FilterOptionText = "> 1 godzina" });
            filters.Add(filter);
            filter = new Filter();
            filter.ID = 5;
            filter.Header = BoardGameFilterParameter.FilterByAge.GetDescription();
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 1, FilterOptionText = "5 - 7 lat" });
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 2, FilterOptionText = "8 - 11 lat" });
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 3, FilterOptionText = "12 - 18 lat" });
            filter.FilterOptions.Add(new FilterOption() { FilterOptionId = 4, FilterOptionText = "18 +" });
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

        public void SetSelectedFilterOptionInFilterParameters(IList<Filter> filterParameters, string selectedFilterOption)
        {
            if (!string.IsNullOrWhiteSpace(selectedFilterOption))
            {
                var selectedFilterOptionArray = selectedFilterOption.Split(';');
                foreach (var selectedFilterOptionPair in selectedFilterOptionArray)
                {
                    var keyValuePair = selectedFilterOptionPair.Split('=');
                    filterParameters[int.Parse(keyValuePair[0]) - 1].SelectedFilterOption = int.Parse(keyValuePair[1]);
                }
            }
        }
    }
}
