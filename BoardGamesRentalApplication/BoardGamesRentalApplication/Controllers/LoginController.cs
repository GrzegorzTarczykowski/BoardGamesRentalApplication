using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using BoardGamesRentalApplication.Models;

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
            using (MySqlDbContext db = new MySqlDbContext())
            {
                var user = db.Users.Where(u => u.Username == userEntity.Username).FirstOrDefault();
                if (user == null)
                {
                    ViewBag.Message = "Taki użytkownik nie istnieje.";
                    return View();
                }

                var pass = userEntity.Password;
                byte[] saltedPassword = Encoding.UTF8.GetBytes(pass).Concat(user.Salt).ToArray();
                using (var sha = SHA256.Create())
                {
                    byte[] hash = sha.ComputeHash(saltedPassword);
                    byte[] hashForComparison = Convert.FromBase64String(user.Password);
                    for (int i = 0; i < hashForComparison.Length; i++)
                        if (hash[i] != hashForComparison[i])
                        {
                            ViewBag.Message = "Niepoprawne hasło.";
                            return View();
                        }
                    //access granted
                    Session["UserId"] = user.Id;
                    Session["Username"] = user.Username;
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}