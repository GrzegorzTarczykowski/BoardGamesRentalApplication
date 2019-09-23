using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.DAL.MySqlDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBoardGameCategoryService categoryService;

        public CategoryController(IBoardGameCategoryService service)
        {
            this.categoryService = service;
        }

        public ActionResult Index()
        {
            return HandleUserType(ListCategories);
        }

        private ActionResult HandleUserType(Func<ActionResult> actionToAuthorize)
        {
            try
            {
                if (Session["UserType"] is UserType && (UserType)Session["UserType"] == UserType.Administrator)
                {
                    return actionToAuthorize();
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private ActionResult ListCategories()
        {
            return View("Index", categoryService.GetAll().AsEnumerable());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return HandleUserType(() => View(categoryService.FindById(id)));
        }
        
        // GET: Admin/Create
        public ActionResult Create()
        {
            return HandleUserType(View);
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            return HandleUserType(() =>
            {
                categoryService.AddCategory(new BoardGameCategory { Name = collection["Name"] });
                return RedirectToAction("Index");
            });
        }
        
        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return HandleUserType(View);
        }
        
        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            return HandleUserType(() =>
            {
                categoryService.UpdateCategory(id, new BoardGameCategory { Name = collection["Name"] });
                return RedirectToAction("Index");
            });
        }
        
        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return HandleUserType(() => View(categoryService.FindById(id)));
        }
        
        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            return HandleUserType(() =>
            {
                categoryService.DeleteCategory(id);
                return RedirectToAction("Index");
            });
        }
    }
}
