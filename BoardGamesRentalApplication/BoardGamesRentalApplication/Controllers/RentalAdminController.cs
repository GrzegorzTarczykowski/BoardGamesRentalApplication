using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class RentalAdminController : Controller
    {
        private readonly IUserTypeService userTypeService;
        private readonly IRepository<Reservation> reservationRepository;

        public RentalAdminController(IRepository<Reservation> resrvationRepository)
        {
            this.reservationRepository = resrvationRepository;
            userTypeService = new UserTypeService(this, RedirectToAction("Index", "Home"));
        }

        // GET: RentalAdmin
        public ActionResult Index()
        {
            return userTypeService.Authorize(() =>
            {
                return View(reservationRepository.GetAll(nameof(Reservation.User), nameof(Reservation.ReservationStatus), nameof(Reservation.BoardGame)).ToList());
            }, UserType.Administrator);
        }
    }
}