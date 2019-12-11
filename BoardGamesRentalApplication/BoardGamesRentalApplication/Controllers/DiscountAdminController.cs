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
        private readonly IRepository<DiscountCodeStatus> discountCodeStatusRepository;

        public DiscountAdminController(IRepository<DiscountCode> discountCodeRepository, IRepository<BoardGame> boardGameRepository, IRepository<DiscountCodeStatus> discountCodeStatusRepository)
        {
            this.userTypeService = new UserTypeService(this, RedirectToAction(nameof(Index), "Home"));
            this.discountCodeRepository = discountCodeRepository;
            this.boardGameRepository = boardGameRepository;
            this.discountCodeStatusRepository = discountCodeStatusRepository;
            ViewBag.BoardGames = new SelectList(boardGameRepository.GetAll().Select(bg => new SelectListItem { Text = bg.Name, Value = bg.BoardGameId.ToString() }), "Value", "Text");
            ViewBag.DiscountCodeStatuses = new SelectList(discountCodeStatusRepository.GetAll().Select(dcs => new SelectListItem { Text = dcs.Name, Value = dcs.DiscountCodeStatusId.ToString() }), "Value", "Text");
        }


        // GET: DiscountAdmin
        public ActionResult Index()
        {
            return userTypeService.Authorize(() => View(discountCodeRepository.GetAll(nameof(BoardGame), nameof(DiscountCodeStatus))), UserType.Administrator, UserType.Employee);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return userTypeService.Authorize(() => View(), UserType.Administrator, UserType.Employee);
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
                    DiscountCodeStatusId = int.Parse(collection.GetValue(nameof(DiscountCode.DiscountCodeStatus)).AttemptedValue),
                    BoardGameId = int.Parse(collection.GetValue(nameof(BoardGame)).AttemptedValue)
                });
                discountCodeRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }, UserType.Administrator, UserType.Employee);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return userTypeService.Authorize(() =>
            {
                var model = discountCodeRepository.FindBy(dc => dc.DiscountCodeStatusId == id, nameof(BoardGame)).Single();
                ViewBag.DiscountCodeStatues = new SelectList(ViewBag.DiscountCodeStatuses, "Value", "Text", model.DiscountCodeStatusId);
                return View(model);
            }, UserType.Administrator, UserType.Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            return userTypeService.Authorize(() =>
            {
                var edited = discountCodeRepository.FindById(id);
                edited.DiscountCodeStatusId = int.Parse(collection.GetValue(nameof(DiscountCode.DiscountCodeStatus)).AttemptedValue);
                discountCodeRepository.Edit(edited);
                discountCodeRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }, UserType.Administrator, UserType.Employee);
        }
    }
}