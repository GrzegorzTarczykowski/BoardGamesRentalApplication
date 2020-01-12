using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BLL.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public User GetById(int id)
        {
            return userRepository.FindById(id);
        }

        public bool Update(User user)
        {
            var userToUpdate = userRepository.FindById(user.UserId);
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.PhoneNumber = user.PhoneNumber;
            userToUpdate.Street = user.Street;
            userToUpdate.HouseNumber = user.HouseNumber;
            userToUpdate.ApartmentNumber = user.ApartmentNumber;
            userToUpdate.PostalCode = user.PostalCode;
            userToUpdate.City = user.City;
            userRepository.Edit(userToUpdate);
            userRepository.SaveChanges();
            return true;
        }
    }
}
