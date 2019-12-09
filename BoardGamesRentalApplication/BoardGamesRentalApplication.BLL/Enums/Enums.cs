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

    public enum ReservationServiceResponse
    {
        SuccessReservation,
        NotEnoughBoardGame,
        DuplicateEmail
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

    public enum FilterByPlayerCount
    {
        [Description("1")]
        OnePlayer = 1,
        [Description("2")]
        TwoPlayers,
        [Description("3")]
        ThreePlayers,
        [Description("4")]
        FourPlayers,
        [Description("5")]
        FivePlayers,
        [Description("6")]
        SixPlayers
    }

    public enum FilterByGameTime
    {
        [Description("< 15min")]
        LessThanFifteenMin = 1,
        [Description("< 30min")]
        LessThanThirtyMin,
        [Description("< 1 godzina")]
        LessThanAnHour,
        [Description("> 1 godzina")]
        ForMoreThanAnHour
    }

    public enum FilterByAge
    {
        [Description("5 - 7 lat")]
        FromFiveToSevenYears = 1,
        [Description("8 - 11 lat")]
        FromEightToElevenYears,
        [Description("12 - 17 lat")]
        FromTwelveToSeventeenYears,
        [Description("18 +")]
        ForEighteenYears
    }
}
