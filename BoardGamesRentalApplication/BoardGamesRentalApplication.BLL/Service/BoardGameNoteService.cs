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
    public class BoardGameNoteService : IBoardGameNoteService
    {
        private readonly IRepository<BoardGameNote> repository;

        public BoardGameNoteService(IRepository<BoardGameNote> repository)
        {
            this.repository = repository;
        }

        public IQueryable<BoardGameNote> GetFristThreeBoardGameNote()
        {
            return repository.GetAll().Take(3);
        }
    }
}
