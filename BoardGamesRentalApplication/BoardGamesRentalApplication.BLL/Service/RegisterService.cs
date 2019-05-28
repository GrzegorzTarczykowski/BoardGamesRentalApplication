using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.DAL.UnitWork;

namespace BoardGamesRentalApplication.BLL.Service
{
    public class RegisterService : IRegisterService
    {
        public RegisterServiceResponse Register(User user)
        {
            try
            {
                using (UnitOfWork unit = new UnitOfWork())
                {
                    if (unit.UserRepository.Any(u => u.Username == user.Username))
                    {
                        return RegisterServiceResponse.DuplicateUsername;
                    }
                    else if (unit.UserRepository.Any(u => u.Email == user.Email))
                    {
                        return RegisterServiceResponse.DuplicateEmail;
                    }
                    else
                    {
                        using (SHA256 sha = SHA256.Create())
                        {
                            using (var rng = RandomNumberGenerator.Create())
                            {
                                byte[] salt = new byte[32];
                                rng.GetBytes(salt);
                                byte[] password = Encoding.UTF8.GetBytes(user.Password);
                                byte[] saltedPassword = password.Concat(salt).ToArray();
                                byte[] hashedPassword = sha.ComputeHash(saltedPassword);
                                user.Salt = salt;
                                user.Password = Convert.ToBase64String(hashedPassword);
                                user.CreateDate = DateTime.Now;
                                unit.UserRepository.Add(user);
                                unit.UserRepository.Save();
                                return RegisterServiceResponse.SuccessRegister;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Błąd zapisu nowego użytkownika do bazy.");
            }
        }
    }
}

