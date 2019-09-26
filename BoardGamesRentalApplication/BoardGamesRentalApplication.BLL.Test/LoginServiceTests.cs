using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.BLL.Service;
using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using FluentAssertions;
using Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BoardGamesRentalApplication.BLL.Test
{
    [TestFixture]
    public class LoginServiceTests
    {
        private LoginService loginService;
        private Mock<IRepository<User>> repository = new Mock<IRepository<User>>();
        private CryptographyService cryptographyService = new CryptographyService();

        [SetUp]
        public void Setup()
        {
            loginService = new LoginService(repository.Object, cryptographyService);
        }

        [Test]
        public void Login_WhenUserNameIsNotInDatabase_ReturnUserDoesntExist()
        {
            string usernameNotInDb = It.IsNotIn(new[] { "meAmnestic" });
            Expression<Func<User, bool>> expression = u => u.Username == usernameNotInDb;
            repository.Setup(repo => repo.Any(It.Is<Expression<Func<User, bool>>>(x => LambdaCompare.Eq(x, expression))))
               .Returns(false);
            repository.Setup(repo => repo.FindBy(It.Is<Expression<Func<User, bool>>>(x => LambdaCompare.Eq(x, expression))))
                .Returns<IQueryable<User>>(null);

            LoginServiceResponse response = loginService.Login(new User() { Username = usernameNotInDb });

            response.Should().Be(LoginServiceResponse.UserDoesntExist);
        }

        [TestCase("why")]
        [TestCase("cannot")]
        public void Login_WhenUserIsInDatabaseAndPasswordDoesntMatch_ReturnsIncorrectPassword
            (string password)
        {
            const string ExplicitPassword = "remember";
            string usernameInDb = It.IsAny<string>();
            Expression<Func<User, bool>> expression = u => u.Username == usernameInDb;
            byte[] salt = cryptographyService.GenerateRandomSalt();
            IQueryable<User> queryable = (new User[] { new User { Username = usernameInDb, Password = Convert.ToBase64String(cryptographyService.GenerateSHA512(ExplicitPassword, salt)), Salt = salt } }).AsQueryable();
            repository.Setup(repo => repo.Any(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(true);
            repository.Setup(repo => repo.FindBy(It.Is<Expression<Func<User, bool>>>(x => LambdaCompare.Eq(x, expression))))
                .Returns(() => queryable);

            LoginServiceResponse response = loginService.Login(new User() { Username = usernameInDb, Password = password });

            response.Should().Be(LoginServiceResponse.IncorrectPassword);
        }

        [Test]
        public void Login_WhenUserIsInDatabaseAndPasswordsMatch_ReturnsLoginSuccessful()
        {
            const string ExplicitPassword = "remember";
            const string ExistingUsername = "meAmnestic";
            string usernameInDb = It.IsAny<string>();
            Expression<Func<User, bool>> expression = u => u.Username == usernameInDb;
            byte[] salt = cryptographyService.GenerateRandomSalt();
            IQueryable<User> queryable = (new User[] { new User { Username = ExistingUsername, Password = Convert.ToBase64String(cryptographyService.GenerateSHA512(ExplicitPassword, salt)), Salt = salt } }).AsQueryable();
            repository.Setup(urm => urm.Any(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(true);
            repository.Setup(repo => repo.FindBy(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(() => queryable);

            LoginServiceResponse response = loginService.Login(new User() { Username = ExistingUsername, Password = ExplicitPassword });

            response.Should().Be(LoginServiceResponse.LoginSuccessful);
        }
    }
}
