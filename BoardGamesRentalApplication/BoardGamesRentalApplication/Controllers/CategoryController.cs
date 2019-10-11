using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.Service;
using System.Linq;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBoardGameCategoryService categoryService;
        private readonly IUserTypeService userTypeService;

        public CategoryController(IBoardGameCategoryService service)
        {
            this.categoryService = service;
            this.userTypeService = new UserTypeService(this, RedirectToAction("Index", "Home"));
        }

        public ActionResult Index()
        {
            return userTypeService.Authorize(ListCategories, UserType.Administrator);
        }

        private ActionResult ListCategories()
        {
            return View("Index", categoryService.GetAll().AsEnumerable());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return userTypeService.Authorize(() => View(categoryService.FindById(id)), UserType.Administrator);
        }
        
        // GET: Admin/Create
        public ActionResult Create()
        {
            return userTypeService.Authorize(View, UserType.Administrator);
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            return userTypeService.Authorize(() =>
            {
                categoryService.AddCategory(new BoardGameCategory { Name = collection["Name"] });
                return RedirectToAction("Index");
            }, UserType.Administrator);
        }
        
        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return userTypeService.Authorize(View, UserType.Administrator);
        }
        
        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            return userTypeService.Authorize(() =>
            {
                categoryService.UpdateCategory(id, new BoardGameCategory { Name = collection["Name"] });
                return RedirectToAction("Index");
            }, UserType.Administrator);
        }
        
        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return userTypeService.Authorize(() => View(categoryService.FindById(id)), UserType.Administrator);
        }
        
        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            return userTypeService.Authorize(() =>
            {
                categoryService.DeleteCategory(id);
                return RedirectToAction("Index");
            }, UserType.Administrator);
        }
    }
}
