using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class RentalController : Controller
    {
        private readonly IReservationService reservationService;

        public RentalController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet]
        public ActionResult Index(DateTime rentalStartDate, DateTime rentalEndDate, int boardGameId, string boardGameName, int count)
        {
            Models.Reservation reservation = new Models.Reservation
            {
                RentalStartDate = rentalStartDate,
                RentalEndDate = rentalEndDate,
                BoardGameId = boardGameId,
                Count = count,
                BoardGameName = boardGameName
            };
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Models.Reservation model)
        {
            if (ModelState.IsValid)
            {
                DAL.Models.Reservation reservation = new DAL.Models.Reservation()
                {
                    RentalStartDate = model.RentalStartDate,
                    RentalEndDate = model.RentalEndDate,
                    Count = model.Count,
                    UserId = (int)Session["UserId"],
                    BoardGameId = model.BoardGameId,
                    ReservationStatusId = 1
                };
                switch (reservationService.AddReservation(reservation))
                {
                    case ReservationServiceResponse.SuccessReservation:
                        ModelState.Clear();
                        TempData["SuccessReservation"] = $"Z powodzeniem dokonano rezerwacji: {model.BoardGameName}.";
                        return RedirectToAction("Login", "Login");
                    case ReservationServiceResponse.NotEnoughBoardGame:
                        ViewBag.NotEnoughBoardGameMessage = "Nie posiadamy wystarczającej liczby gier.";
                        return RedirectToAction("Details", "BoardGameDetailsOffer", new { boardGameId = model.BoardGameId });
                    default:
                        break;
                }
            }
            return View(model);
        }
    }
}