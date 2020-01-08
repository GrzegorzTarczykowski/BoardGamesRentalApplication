using BoardGamesRentalApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BLL.IService
{
    public interface IBoardGameNoteService
    {
        IQueryable<BoardGameNote> GetFristThreeBoardGameNote();
    }
}
