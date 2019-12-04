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

        public void AddPublisher(BoardGamePublisher boardGamePublisher)
        {
            repository.Add(boardGamePublisher);
            repository.SaveChanges();
        }

        public BoardGamePublisher FindById(int id)
        {
            return repository.FindById(id);
        }

        public IQueryable<BoardGamePublisher> GetAll()
        {
            return repository.GetAll();
        }

        public void RemovePublisher(int id)
        {
            BoardGamePublisher publisherBeingDeleted = repository.FindById(id);
            if (publisherBeingDeleted != null)
            {
                repository.Remove(new object[] { id });
                repository.SaveChanges();
            }
        }

        public void UpdatePublisher(int id, BoardGamePublisher boardGamePublisher)
        {
            BoardGamePublisher edited = repository.FindById(id);
            if (edited != null)
            {
                edited.Name = boardGamePublisher.Name;
                repository.Edit(edited);
                repository.SaveChanges();
            }
        }
    }
}
