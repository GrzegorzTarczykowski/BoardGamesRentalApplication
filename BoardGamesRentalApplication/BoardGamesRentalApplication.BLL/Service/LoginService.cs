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

        public LoginService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
                    string password = user.Password;
                    byte[] saltedPassword = Encoding.UTF8.GetBytes(password).Concat(matchingUser.Salt).ToArray();
                    using (SHA256 sha = SHA256.Create())
                    {
                        byte[] hash = sha.ComputeHash(saltedPassword);
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
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
