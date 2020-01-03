using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.Service;
using PagedList;
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
        private readonly IRepository<ReservationStatus> reservationStatusRepository;
        private readonly IRepository<BoardGame> boardGameRepository;

        public RentalAdminController(IRepository<Reservation> reservationRepository, IRepository<ReservationStatus> reservationStatusRepository, IRepository<BoardGame> boardGameRepository)
        {
            this.reservationRepository = reservationRepository;
            this.reservationStatusRepository = reservationStatusRepository;
            this.boardGameRepository = boardGameRepository;
            userTypeService = new UserTypeService(this, RedirectToAction("Index", "Home"));
        }

        // GET: RentalAdmin
        public ActionResult Index(int? page)
        {
            return userTypeService.Authorize(() =>
            {
                IQueryable<Reservation> reservationsQuery = reservationRepository.GetAll(nameof(Reservation.User), nameof(Reservation.ReservationStatus), nameof(Reservation.BoardGame));
                return View(reservationsQuery.OrderBy(r => r.RentalEndDate).ToPagedList(page ?? 1, 10));
            }, UserType.Administrator, UserType.Employee);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return userTypeService.Authorize(() =>
            {
                Reservation model = reservationRepository.FindBy(r => r.ReservationId == id, nameof(Reservation.ReservationStatus), nameof(Reservation.BoardGame), nameof(Reservation.User)).Single();
                ViewBag.ReservationStatus = new SelectList(reservationStatusRepository.GetAll().Select(rs => new SelectListItem { Text = rs.Name, Value = rs.ReservationStatusId.ToString() }),
                    "Value", "Text", model.ReservationStatusId);
                return View(model);
            }, UserType.Administrator, UserType.Employee);
        }

        public ActionResult Edit(int id, FormCollection collection)
        {
            return userTypeService.Authorize(() =>
            {
                var editedReservation = reservationRepository.FindById(id);
                editedReservation.ReservationStatusId = int.Parse(collection[nameof(Reservation.ReservationStatusId)]);
                if (editedReservation.ReservationStatusId == (int)DefaultReservationStatus.Zakonczona)
                {
                    var game = boardGameRepository.FindById(id);
                    game.Quantity += editedReservation.Count;
                    boardGameRepository.Edit(game);
                    boardGameRepository.SaveChanges();
                }
                reservationRepository.Edit(editedReservation);
                reservationRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }, UserType.Administrator, UserType.Employee);
        }
    }
}