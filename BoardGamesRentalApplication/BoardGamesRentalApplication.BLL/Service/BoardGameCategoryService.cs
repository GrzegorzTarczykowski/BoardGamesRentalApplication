using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using System.Linq;

namespace BoardGamesRentalApplication.BLL.Service
{
    public class BoardGameCategoryService : IBoardGameCategoryService
    {
        private readonly IRepository<BoardGameCategory> repository;

        public BoardGameCategoryService(IRepository<BoardGameCategory> repository)
        {
            this.repository = repository;
        }

        public void AddCategory(BoardGameCategory category)
        {
            if (!repository.Any(bgc => bgc.Name == category.Name))
            {
                repository.Add(category);
                repository.SaveChanges();
            }
        }

        public void DeleteCategory(int id)
        {
            BoardGameCategory entity = repository.FindById(id);
            if (entity != null)
            {
                repository.Remove(entity);
                repository.SaveChanges();
            }
        }

        public BoardGameCategory FindById(int id)
        {
            return repository.FindById(id);
        }

        public IQueryable<BoardGameCategory> GetAll()
        {
            return repository.GetAll();
        }

        public void UpdateCategory(int id, BoardGameCategory boardGameCategory)
        {
            var edited = repository.FindById(id);
            if (edited != null)
            {
                edited.Name = boardGameCategory.Name;
                repository.Edit(edited);
                repository.SaveChanges();
            }
        }
    }
}
