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
    public class DiscountCodeService : IDiscountCodeService
    {
        private readonly IRepository<DiscountCode> discountCodeRepository;

        public DiscountCodeService(IRepository<DiscountCode> discountCodeRepository)
        {
            this.discountCodeRepository = discountCodeRepository;
        }

        public bool CheckDiscountCode(string code, int boardGameId)
        {
            return discountCodeRepository.Any(dc => dc.Code == code && dc.BoardGameId == boardGameId);
        }
    }
}
