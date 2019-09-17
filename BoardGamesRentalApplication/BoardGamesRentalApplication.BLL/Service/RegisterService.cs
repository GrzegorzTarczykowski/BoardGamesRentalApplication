using System;
using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;

namespace BoardGamesRentalApplication.BLL.Service
{
    public class RegisterService : IRegisterService
    {
        private readonly IRepository<User> userRepository;
        private readonly ICryptographyService cryptographyService;

        public RegisterService(IRepository<User> userRepository, ICryptographyService cryptographyService)
        {
            this.userRepository = userRepository;
            this.cryptographyService = cryptographyService;
        }

        public RegisterServiceResponse Register(User user)
        {
            try
            {
                if (userRepository.Any(u => u.Username == user.Username))
                {
                    return RegisterServiceResponse.DuplicateUsername;
                }
                else if (userRepository.Any(u => u.Email == user.Email))
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
                    userRepository.Add(user);
                    userRepository.SaveChanges();
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

