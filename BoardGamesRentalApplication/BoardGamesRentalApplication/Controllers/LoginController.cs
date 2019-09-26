using System;
using System.Web.Mvc;
using BoardGamesRentalApplication.BLL.Service;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.DAL.UnitOfWork;
using BoardGamesRentalApplication.BLL.IService;

namespace BoardGamesRentalApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(Exception), View = "Error")]
        public ActionResult Login(User userEntity)
        {
            switch (loginService.Login(userEntity))
            {
                case LoginServiceResponse.LoginSuccessful:
                    ModelState.Clear();
                    Session["Username"] = userEntity.Username;
                    Session["UserType"] = userEntity.UserType;
                    ViewBag.LoginSuccessfulMessage = $"Zalogowano jako {userEntity.Username}.";
                    return RedirectToAction("Index", "Home");
                case LoginServiceResponse.UserDoesntExist:
                    ViewBag.UserDoesntExistMessage = $"Użytkownik {userEntity.Username} nie istnieje.";
                    return View();
                case LoginServiceResponse.IncorrectPassword:
                    ViewBag.IncorrectPasswordMessage = "Hasło jest niepoprawne.";
                    return View();
            }
            return View(userEntity);
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}