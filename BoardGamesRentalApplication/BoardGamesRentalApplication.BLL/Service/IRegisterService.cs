using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.DAL.Models;

namespace BoardGamesRentalApplication.BLL.Service
{
    public interface IRegisterService
    {
        RegisterServiceResponse Register(User user);
    }
}
