using BoardGamesRentalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User userEntity)
        {
            using (MySqlDbContext db = new MySqlDbContext())
            {
                var user = db.Users.Where(u => u.Username == userEntity.Username).FirstOrDefault();
                var pass = userEntity.Password;
                using (var rng = RandomNumberGenerator.Create())
                {
                    byte[] saltedPassword = Encoding.UTF8.GetBytes(pass).Concat(user.Salt).ToArray();
                    using (var sha = SHA256.Create())
                    {
                        byte[] hash = sha.ComputeHash(saltedPassword);
                        byte[] hashForComparison = Convert.FromBase64String(user.Password);
                        for (int i = 0; i < hashForComparison.Length; i++)
                            if (hash[i] != hashForComparison[i])
                                throw new UnauthorizedAccessException();
                        //access granted
                        Session["logged"] = true;
                    }
                }
            }
            return View();
        }
    }
}