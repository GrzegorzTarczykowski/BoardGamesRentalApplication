using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BLL.IService
{
    public interface IBoardGameCategoryService
    {
        void AddCategory(BoardGameCategory category);
        IQueryable<BoardGameCategory> GetAll();
        void DeleteCategory(int id);
        void UpdateCategory(int id, BoardGameCategory boardGameCategory);
        BoardGameCategory FindById(int id);
    }
}
