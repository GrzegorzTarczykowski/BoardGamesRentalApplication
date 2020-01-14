using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            DAL.Models.User userToUpdate = userService.GetById(id);
            User user = new User
            {
                UserId = userToUpdate.UserId,
                FirstName = userToUpdate.FirstName,
                LastName = userToUpdate.LastName,
                PhoneNumber = userToUpdate.PhoneNumber,
                Street = userToUpdate.Street,
                HouseNumber = userToUpdate.HouseNumber,
                ApartmentNumber = userToUpdate.ApartmentNumber,
                PostalCode = userToUpdate.PostalCode,
                City = userToUpdate.City
            };
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            DAL.Models.User userToUpdate = userService.GetById(id);
            User user = new User
            {
                UserId = userToUpdate.UserId,
                FirstName = userToUpdate.FirstName,
                LastName = userToUpdate.LastName,
                PhoneNumber = userToUpdate.PhoneNumber,
                Street = userToUpdate.Street,
                HouseNumber = userToUpdate.HouseNumber,
                ApartmentNumber = userToUpdate.ApartmentNumber,
                PostalCode = userToUpdate.PostalCode,
                City = userToUpdate.City
            };
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                DAL.Models.User userToUpdate = new DAL.Models.User
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Street = user.Street,
                    HouseNumber = user.HouseNumber,
                    ApartmentNumber = user.ApartmentNumber,
                    PostalCode = user.PostalCode,
                    City = user.City
                };
                userService.Update(userToUpdate);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
