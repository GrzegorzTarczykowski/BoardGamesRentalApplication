using System.Linq;
using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;

namespace BoardGamesRentalApplication.BLL.Service
{
    class BoardGameStatesService : IBoardGameStatesService
    {
        private readonly IRepository<BoardGameState> repository;

        public BoardGameStatesService(IRepository<BoardGameState> repository)
        {
            this.repository = repository;
        }
        public IQueryable<BoardGameState> GetAll()
        {
            return repository.GetAll();
        }
    }
}
