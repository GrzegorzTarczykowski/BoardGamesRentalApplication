using BoardGamesRentalApplication.DAL.Models;
using System.Linq;

namespace BoardGamesRentalApplication.BLL.IService
{
    public interface IBoardGamePublishersService
    {
        IQueryable<BoardGamePublisher> GetAll();
    }

}
