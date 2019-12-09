using BoardGamesRentalApplication.DAL.Models;
using System.Linq;

namespace BoardGamesRentalApplication.BLL.IService
{
    public interface IBoardGamePublishersService
    {
        IQueryable<BoardGamePublisher> GetAll();
        void AddPublisher(BoardGamePublisher boardGamePublisher);
        void UpdatePublisher(int id, BoardGamePublisher boardGamePublisher);
        void RemovePublisher(int id);
        BoardGamePublisher FindById(int id);
    }

}
