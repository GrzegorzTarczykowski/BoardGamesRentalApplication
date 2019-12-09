using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class BoardGamePublisherController : Controller
    {
        private readonly IUserTypeService userTypeService;
        private readonly IBoardGamePublishersService boardGamePublishersService;

        public BoardGamePublisherController(IBoardGamePublishersService boardGamePublishersService)
        {
            this.userTypeService = new UserTypeService(this, RedirectToAction("Index", "Home"));
            this.boardGamePublishersService = boardGamePublishersService;
        }

        // GET: BoardGamePublisher
        public ActionResult Index()
        {
            return userTypeService.Authorize(() => View(boardGamePublishersService.GetAll().ToList()), UserType.Administrator);
        }

        // GET: BoardGamePublisher/Create
        public ActionResult Create()
        {
            return userTypeService.Authorize(View, UserType.Administrator);
        }

        // POST: BoardGamePublisher/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            return userTypeService.Authorize(() =>
            {
                boardGamePublishersService.AddPublisher(new BoardGamePublisher { Name = collection["Name"] });
                return RedirectToAction(nameof(Index));
            }, UserType.Administrator);
        }

        // GET: BoardGamePublisher/Edit/5
        public ActionResult Edit(int id)
        {
            return userTypeService.Authorize(() => View(boardGamePublishersService.FindById(id)), UserType.Administrator);
        }

        // POST: BoardGamePublisher/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            return userTypeService.Authorize(() =>
            {
                boardGamePublishersService.UpdatePublisher(id, new BoardGamePublisher { Name = collection["Name"] });
                return RedirectToAction(nameof(Index));
            }, UserType.Administrator);
        }

        // GET: BoardGamePublisher/Delete/5
        public ActionResult Delete(int id)
        {
            return userTypeService.Authorize(() => View(boardGamePublishersService.FindById(id)), UserType.Administrator);
        }

        // POST: BoardGamePublisher/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            return userTypeService.Authorize(() =>
            {
                boardGamePublishersService.RemovePublisher(id);
                return RedirectToAction(nameof(Index));
            }, UserType.Administrator);
        }
    }
}
