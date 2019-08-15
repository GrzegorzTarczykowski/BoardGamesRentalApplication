using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.BLL.Service;
using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using BoardGamesRentalApplication.DAL.UnitOfWork;
using FluentAssertions;
using Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BoardGamesRentalApplication.BLL.Test
{
    [TestFixture]
    class RegisterServiceTests
    {
        private Mock<IRepository<User>> userRepositoryMock;
        private Mock<ICryptographyService> cryptographyServiceMock;
        private RegisterService registerService;

        [SetUp]
        public void Setup()
        {
            userRepositoryMock = new Mock<IRepository<User>>();
            cryptographyServiceMock = new Mock<ICryptographyService>();
            registerService = new RegisterService(userRepositoryMock.Object, cryptographyServiceMock.Object);
        }

        [Test]
        public void Register_TryAddNewUserWithAlreadyUseUsername_ReturnResponseDuplicateUsername()
        {
            //Arrange
            string alreadyUsername = It.IsAny<string>();
            Expression<Func<User, bool>> expr = u => u.Username == alreadyUsername;
            userRepositoryMock.Setup(urm => urm.Any(It.Is<Expression<Func<User, bool>>>(x => LambdaCompare.Eq(x, expr))))
                                               .Returns(true);
            //Act
            RegisterServiceResponse result = registerService.Register(new User() { Username = alreadyUsername });
            //Assert
            result.Should().Be(RegisterServiceResponse.DuplicateUsername);
        }
    }
}
