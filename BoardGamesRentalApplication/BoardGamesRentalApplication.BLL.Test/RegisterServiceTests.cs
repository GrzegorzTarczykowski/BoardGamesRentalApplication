using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.BLL.Service;
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
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BLL.Test
{
    [TestFixture]
    class RegisterServiceTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<ICryptographyService> cryptographyServiceMock;
        private RegisterService registerService;

        [SetUp]
        public void Setup()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            cryptographyServiceMock = new Mock<ICryptographyService>();
            registerService = new RegisterService(unitOfWorkMock.Object, cryptographyServiceMock.Object);
        }

        [Test]
        public void Register_TryAddNewUserWithAlreadyUseUsername_ReturnResponseDuplicateUsername()
        {
            //Arrange
            string alreadyUsername = It.IsAny<string>();
            Expression<Func<User, bool>> expr = u => u.Username == alreadyUsername;
            unitOfWorkMock.Setup(uowm => uowm.UserRepository
                          .Any(It.Is<Expression<Func<User, bool>>>(x => LambdaCompare.Eq(x, expr))))
                          .Returns(true);
            //Act
            RegisterServiceResponse result = registerService.Register(new User() { Username = alreadyUsername });
            //Assert
            result.Should().Be(RegisterServiceResponse.DuplicateUsername);
        }
    }
}
