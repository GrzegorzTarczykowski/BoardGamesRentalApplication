using System;
using System.Web.Mvc;
using BoardGamesRentalApplication.BLL.Service;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.DAL.UnitOfWork;

namespace BoardGamesRentalApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService loginService;

        public LoginController(/*ILoginService loginService*/) //TODO Implement Container
        {
            this.loginService = new LoginService(new UnitOfWork(), new CryptographyService());
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
                    Session["Username"] = userEntity.Username;
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
    }
}