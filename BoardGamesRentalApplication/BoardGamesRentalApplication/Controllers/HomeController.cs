using BoardGamesRentalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void CreateDb()
        {
            using (MySqlDbContext db = new MySqlDbContext())
            {
                User user = new User() { Id = 1, Name = "Grzegorz", Password = "12345" };
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }
}