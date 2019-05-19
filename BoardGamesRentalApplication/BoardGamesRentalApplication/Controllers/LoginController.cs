using System;
using System.Web.Mvc;
using BoardGamesRentalApplication.BIL.Service;
using BoardGamesRentalApplication.DAL.Models;

namespace BoardGamesRentalApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(Exception), View = "Error")]
        public ActionResult Login(User userEntity)
        {
            var loginService = new LoginService();
            switch (loginService.Login(userEntity))
            {
                case BIL.Enums.LoginServiceResponse.LoginSuccessful:
                    Session["Username"] = userEntity.Username;
                    ViewBag.LoginSuccessfulMessage = $"Zalogowano jako {userEntity.Username}.";
                    return RedirectToAction("Index", "Home");
                case BIL.Enums.LoginServiceResponse.UserDoesntExist:
                    ViewBag.UserDoesntExistMessage = $"Użytkownik {userEntity.Username} nie istnieje.";
                    return View();
                case BIL.Enums.LoginServiceResponse.IncorrectPassword:
                    ViewBag.IncorrectPasswordMessage = "Hasło jest niepoprawne.";
                    return View();
            }
            return View(userEntity);
        }
    }
}