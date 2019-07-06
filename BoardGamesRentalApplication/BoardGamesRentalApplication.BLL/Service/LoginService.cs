using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.DAL.UnitOfWork;

namespace BoardGamesRentalApplication.BLL.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICryptographyService cryptographyService;

        public LoginService(IUnitOfWork unitOfWork, ICryptographyService cryptographyService)
        {
            this.unitOfWork = unitOfWork;
            this.cryptographyService = cryptographyService;
        }

        public LoginServiceResponse Login(User user)
        {
            try
            {
                User matchingUser = unitOfWork.UserRepository.FindBy(u => u.Username == user.Username).FirstOrDefault();
                if (matchingUser == null)
                {
                    return LoginServiceResponse.UserDoesntExist;
                }
                else
                {
                    byte[] hash = cryptographyService.GenerateSHA512(user.Password, matchingUser.Salt);
                    byte[] hashForComparison = Convert.FromBase64String(matchingUser.Password);
                    for (int i = 0; i < hashForComparison.Length; i++)
                    {
                        if (hash[i] != hashForComparison[i])
                        {
                            return LoginServiceResponse.IncorrectPassword;
                        }
                    }
                    matchingUser.LastLogin = DateTime.Now;
                    unitOfWork.Save();
                    return LoginServiceResponse.LoginSuccessful;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
