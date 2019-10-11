using BoardGamesRentalApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class AdminPanelController : Controller
    {
        // GET: AdminPanel
        public ActionResult Index()
        {
            if (!UserType.Administrator.Equals(Session["UserType"]))
                return RedirectToAction("Index", "Home");
            return View();
        }
    }
}