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
    public class DiscountAdminController : Controller
    {
        private readonly IUserTypeService userTypeService;
        private readonly IRepository<DiscountCode> discountCodeRepository;
        private readonly IRepository<BoardGame> boardGameRepository;

        public DiscountAdminController(IRepository<DiscountCode> discountCodeRepository, IRepository<BoardGame> boardGameRepository)
        {
            this.userTypeService = new UserTypeService(this, RedirectToAction(nameof(Index), "Home"));
            this.discountCodeRepository = discountCodeRepository;
            this.boardGameRepository = boardGameRepository;
            ViewBag.BoardGame = boardGameRepository.GetAll().Select(bg => new SelectListItem { Text = bg.Name, Value = bg.BoardGameId.ToString() });
        }


        // GET: DiscountAdmin
        public ActionResult Index()
        {
            return userTypeService.Authorize(() => View(discountCodeRepository.GetAll(nameof(BoardGame))), UserType.Administrator);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return userTypeService.Authorize(() => View(), UserType.Administrator);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            return userTypeService.Authorize(() =>
            {
                discountCodeRepository.Add(new DiscountCode
                {
                    Code = collection[nameof(DiscountCode.Code)],
                    CodeStatus = collection[nameof(DiscountCode.CodeStatus)],
                    BoardGameId = int.Parse(collection.GetValue(nameof(BoardGame)).AttemptedValue)
                });
                discountCodeRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }, UserType.Administrator);
        }
    }
}