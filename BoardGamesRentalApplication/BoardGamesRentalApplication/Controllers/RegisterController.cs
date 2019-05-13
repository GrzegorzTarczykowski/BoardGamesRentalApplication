using BoardGamesRentalApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
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
                try
                {
                    using (MySqlDbContext db = new MySqlDbContext())
                    {
                        if(db.Users.Any(u=>u.Username == registerUser.Username))
                        {
                            ViewBag.DuplicateMessage = "Username already exist.";
                            return View();
                        }
                        else
                        {
                            using (SHA256 sha = SHA256.Create())
                            {
                                using (var rng = RNGCryptoServiceProvider.Create())
                                {
                                    byte[] salt = new byte[32];
                                    rng.GetBytes(salt);
                                    byte[] password = Encoding.UTF8.GetBytes(registerUser.Password);
                                    byte[] saltedPassword = password.Concat(salt).ToArray();
                                    byte[] hashedPassword = sha.ComputeHash(saltedPassword);
                                    User user = new User()
                                    {
                                        Salt = salt,
                                        Password = Convert.ToBase64String(hashedPassword),
                                        Username = registerUser.Username,
                                        FirstName = registerUser.FirstName,
                                        LastName = registerUser.LastName,
                                        Email = registerUser.Email
                                    };
                                    db.Users.Add(user);
                                    db.SaveChanges();
                                    ModelState.Clear();
                                    ViewBag.Message = $"Zapisano z powodzeniem użytkownika: {registerUser.Username}.";
                                }
                            }
                        }
                    }
                }
                catch (DbEntityValidationException e)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        sb.AppendLine($"Entity of type: {eve.Entry.Entity.GetType().Name} in state: {eve.Entry.State} has the following validation errors:");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            sb.AppendLine($"- Property: {ve.PropertyName}, Error: {ve.ErrorMessage}");
                        }
                    }
                    throw new Exception(sb.ToString());
                }
                catch (Exception)
                {
                    throw new Exception("Błąd zapisu nowego użytkownika do bazy.");
                }
            }
            return View(registerUser);
        }
    }
}