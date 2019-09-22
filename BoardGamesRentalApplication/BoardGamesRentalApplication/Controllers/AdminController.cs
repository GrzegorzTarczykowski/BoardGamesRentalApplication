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
    public class AdminController : Controller
    {
        private readonly IBoardGameCategoryService categoryService;

        public AdminController(IBoardGameCategoryService service)
        {
            this.categoryService = service;
        }

        public ActionResult Index()
        {
            return View(categoryService.GetAll().AsEnumerable());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View(categoryService.FindById(id));
        }
        
        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                categoryService.AddCategory(new BoardGameCategory
                {
                    Name = collection["Name"]
                });
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        
        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                categoryService.UpdateCategory(id, new BoardGameCategory { Name = collection["Name"] });
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        
        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                categoryService.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
