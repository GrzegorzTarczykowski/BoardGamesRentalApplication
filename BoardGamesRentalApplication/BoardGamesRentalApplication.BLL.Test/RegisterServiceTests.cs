using BoardGamesRentalApplication.BLL.Service;
using BoardGamesRentalApplication.DAL.UnitOfWork;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BLL.Test
{
    [TestFixture]
    class RegisterServiceTests
    {
        private RegisterService registerService;

        [SetUp]
        public void Setup()
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            Mock<ICryptographyService> cryptographyServiceMock = new Mock<ICryptographyService>();
            registerService = new RegisterService(mock.Object, cryptographyServiceMock.Object);
        }

        [Test]
        public void Register_TryAddNewUserWithAlreadyUseUsername_ReturnResponseDuplicateUsername()
        {
            //Arrange
            //Act
            //Assert
        }
    }
}
