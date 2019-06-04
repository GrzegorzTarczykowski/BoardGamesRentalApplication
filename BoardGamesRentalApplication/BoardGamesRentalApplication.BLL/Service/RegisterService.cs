using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.DAL.UnitOfWork;

namespace BoardGamesRentalApplication.BLL.Service
{
    public class RegisterService : IRegisterService
    {
        private readonly IUnitOfWork unitOfWork;

        public RegisterService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public RegisterServiceResponse Register(User user)
        {
            try
            {
                if (unitOfWork.UserRepository.Any(u => u.Username == user.Username))
                {
                    return RegisterServiceResponse.DuplicateUsername;
                }
                else if (unitOfWork.UserRepository.Any(u => u.Email == user.Email))
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
                            unitOfWork.UserRepository.Add(user);
                            unitOfWork.UserRepository.Save();
                            return RegisterServiceResponse.SuccessRegister;
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

