using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BLL.Enums
{
    public enum RegisterServiceResponse
    {
        SuccessRegister,
        DuplicateUsername,
        DuplicateEmail
    }

    public enum LoginServiceResponse
    {
        LoginSuccessful,
        UserDoesntExist,
        IncorrectPassword
    }

    public enum BoardGameSortOption
    {
        [Description("Sortuj rosnąco po nazwie")]
        SortAscendingByName,
        [Description("Sortuj malejąco po nazwie")]
        SortDescendingByName,
        [Description("Sortuj rosnąco po ilości graczy")]
        SortAscendingByNumberOfPlayers,
        [Description("Sortuj malejąco po ilości graczy")]
        SortDescendingByNumberOfPlayers
    }

    public enum BoardGameFilterParameter
    {
        [Description("Kategorie")]
        FilterByBoardGameCategory,
        [Description("Minimum graczy")]
        FilterByMinPlayerCount,
        [Description("Maximum graczy")]
        FilterByMaxPlayerCount,
        [Description("Czas gry")]
        FilterByGameTime,
        [Description("Wiek gracza")]
        FilterByAge
    }
}
