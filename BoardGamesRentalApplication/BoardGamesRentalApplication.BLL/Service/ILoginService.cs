using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.DAL.Models;

namespace BoardGamesRentalApplication.BLL.Service
{
    public interface ILoginService
    {
        LoginServiceResponse Login(User user);
    }
}
