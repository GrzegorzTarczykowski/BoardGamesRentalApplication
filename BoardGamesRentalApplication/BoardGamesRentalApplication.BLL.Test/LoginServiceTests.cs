using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.BLL.Service;
using BoardGamesRentalApplication.BLL.Test.Mocks;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.DAL.Repository;
using BoardGamesRentalApplication.DAL.UnitOfWork;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BoardGamesRentalApplication.BLL.Test
{
    [TestFixture]
    public class LoginServiceTests
    {
        private LoginService loginService;
        [SetUp]
        public void Setup()
        {
            IRepository<User> repository = new MockGenericRepository<User>();
            Mock<IUnitOfWork> unit = new Mock<IUnitOfWork>();
            Mock<ICryptographyService> cryptographyServiceMock = new Mock<ICryptographyService>();

            byte[] salt = new byte[32];
            for (byte i = 0; i < 32; i++)
            {
                salt[i] = i;
            }

            using (SHA256 sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes("remember").Concat(salt).ToArray());
                User amnestic = new User() { Username = "meAmnestic", Password = Convert.ToBase64String(hash), Salt = salt };
                repository.Add(amnestic);
            }

            unit.SetupGet(u => u.UserRepository).Returns(repository);
            loginService = new LoginService(unit.Object, cryptographyServiceMock.Object);
        }

        [Test]
        [TestCase("*.*")]
        [TestCase("!@#$%^&*")]
        public void Login_WhenUserNameIsNotInDatabase_ReturnUserDoesntExist(string username)
        {
            User loggingUser = new User() { Username = username };
            LoginServiceResponse response = loginService.Login(loggingUser);
            Assert.AreEqual(LoginServiceResponse.UserDoesntExist, response);
        }

        [Test]
        [TestCase("meAmnestic", "why")]
        [TestCase("meAmnestic", "cannot")]
        public void Login_WhenUserIsInDatabaseAndPasswordDoesntMatch_ReturnsIncorrectPassword
            (string username, string password)
        {
            User loggingUser = new User() { Username = username, Password = password };
            LoginServiceResponse response = loginService.Login(loggingUser);
            Assert.AreEqual(LoginServiceResponse.IncorrectPassword, response);
        }

        [Test]
        public void Login_WhenUserIsInDatabaseAndPasswordsMatch_ReturnsLoginSuccessful()
        {
            User logginUser = new User() { Username = "meAmnestic", Password = "remember" };
            LoginServiceResponse response = loginService.Login(logginUser);
            Assert.AreEqual(LoginServiceResponse.LoginSuccessful, response);
        }
    }
}
