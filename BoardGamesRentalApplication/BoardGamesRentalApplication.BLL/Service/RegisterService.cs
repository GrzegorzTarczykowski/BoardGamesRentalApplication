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
        private readonly ICryptographyService cryptographyService;

        public RegisterService(IUnitOfWork unitOfWork, ICryptographyService cryptographyService)
        {
            this.unitOfWork = unitOfWork;
            this.cryptographyService = cryptographyService;
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
                    byte[] salt = cryptographyService.GenerateRandomSalt();
                    byte[] hashedPassword = cryptographyService.GenerateSHA512(user.Password, salt);
                    user.Salt = salt;
                    user.Password = Convert.ToBase64String(hashedPassword);
                    user.CreateDate = DateTime.Now;
                    unitOfWork.UserRepository.Add(user);
                    unitOfWork.UserRepository.SaveChanges();
                    return RegisterServiceResponse.SuccessRegister;
                }
            }
            catch (Exception)
            {
                throw new Exception("Błąd zapisu nowego użytkownika do bazy.");
            }
        }
    }
}

