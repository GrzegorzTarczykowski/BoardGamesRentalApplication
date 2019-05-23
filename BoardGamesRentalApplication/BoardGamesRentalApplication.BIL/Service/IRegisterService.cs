using BoardGamesRentalApplication.BIL.Enums;
using BoardGamesRentalApplication.DAL.Models;

namespace BoardGamesRentalApplication.BIL.Service
{
    public interface IRegisterService
    {
        RegisterServiceResponse Register(User user);
    }
}
