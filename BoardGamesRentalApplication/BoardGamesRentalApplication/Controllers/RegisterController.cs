using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.BLL.Service;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.DAL.UnitOfWork;
using BoardGamesRentalApplication.Models;
using System;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(Exception), View = "Error")]
        public ActionResult Register(RegisterUser registerUser)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    RegisterService registerService = new RegisterService(unitOfWork);
                    User user = new User()
                    {
                        Username = registerUser.Username,
                        FirstName = registerUser.FirstName,
                        LastName = registerUser.LastName,
                        Email = registerUser.Email,
                        Password = registerUser.Password
                    };
                    switch (registerService.Register(user))
                    {
                        case RegisterServiceResponse.SuccessRegister:
                            ModelState.Clear();
                            TempData["SuccessRegisterNewUserMessage"] = $"Zapisano z powodzeniem użytkownika: {registerUser.Username}.";
                            return RedirectToAction("Login", "Login");
                        case RegisterServiceResponse.DuplicateUsername:
                            ViewBag.DuplicateUsernameMessage = "Nazwa użytkownika jest używana.";
                            return View();
                        case RegisterServiceResponse.DuplicateEmail:
                            ViewBag.DuplicateEmailMessage = "Email jest używany.";
                            return View();
                        default:
                            break;
                    }
                }  
            }
            return View(registerUser);
        }
    }
}