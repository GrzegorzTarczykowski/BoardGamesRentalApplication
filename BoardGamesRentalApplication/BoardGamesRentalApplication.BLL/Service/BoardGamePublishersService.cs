using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BLL.Service
{
    public class BoardGamePublishersService : IBoardGamePublishersService
    {
        private readonly IRepository<BoardGamePublisher> repository;

        public BoardGamePublishersService(IRepository<BoardGamePublisher> repository)
        {
            this.repository = repository;
        }

        public IQueryable<BoardGamePublisher> GetAll()
        {
            return repository.GetAll();
        }
    }
}
