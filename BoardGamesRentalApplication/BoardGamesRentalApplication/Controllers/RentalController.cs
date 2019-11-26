using BoardGamesRentalApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class RentalController : Controller
    {
        [HttpGet]
        public ActionResult Index(DateTime rental_from, DateTime rental_to, int boardGameId)
        {
            return View(new Reservation
            {
                RentalStartDate = rental_from,
                RentalEndDate = rental_to,
                BoardGameId = boardGameId,
                UserId = 1, //(int)Session["UserId"],
                Count = 1,
                ReservationStatusId = 1
            });
        }

        [HttpPost]
        public ActionResult Rental(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                //Reservation reservation = new Reservation
                //{
                //    RentalStartDate = rental_from,
                //    RentalEndDate = rental_to,
                //    BoardGameId = boardGameId,
                //    UserId = (int)Session["UserId"],
                //    Count = 1,
                //    ReservationStatusId = 1
                //};
            }
            return View(reservation);
        }
    }
}