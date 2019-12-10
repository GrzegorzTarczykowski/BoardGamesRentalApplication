using BoardGamesRentalApplication.BLL.Enums;
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
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> reservatioRepository;
        private readonly IRepository<BoardGame> boardGameRepository;
        private readonly IDiscountCodeService discountCodeService;

        public ReservationService(IRepository<Reservation> reservatioRepository, IRepository<BoardGame> boardGameRepository, IDiscountCodeService discountCodeService)
        {
            this.reservatioRepository = reservatioRepository;
            this.boardGameRepository = boardGameRepository;
            this.discountCodeService = discountCodeService;
        }

        public ReservationServiceResponse AddReservation(Reservation reservation)
        {
            try
            {
                if (!boardGameRepository.Any(bg => bg.BoardGameId == reservation.BoardGameId && bg.Quantity >= reservation.Count))
                {
                    return ReservationServiceResponse.NotEnoughBoardGame;
                }
                else
                {
                    reservation.ReservationStatusId = 1;
                    reservatioRepository.Add(reservation);
                    reservatioRepository.SaveChanges();
                    BoardGame boardGame = boardGameRepository.FindById(reservation.BoardGameId);
                    boardGame.Quantity -= reservation.Count;
                    boardGameRepository.SaveChanges();
                    return ReservationServiceResponse.SuccessReservation;
                }
            }
            catch (Exception)
            {
                throw new Exception("Błąd tworzenia nowej rezerwacji.");
            }
        }

        public decimal CaculateTotalCostByBoardGameDiscountCode(string boardGameDiscountCode, int boardGameId, DateTime rentalStartDate, DateTime rentalEndDate, decimal rentalCostPerDay)
        {
            double numberOfDayRental = (rentalEndDate - rentalStartDate).TotalDays;
            decimal totalCost = (decimal)numberOfDayRental * rentalCostPerDay;
            if (discountCodeService.CheckDiscountCode(boardGameDiscountCode, boardGameId))
            {
                decimal discount = (totalCost * 10M / 100M);
                return totalCost - discount;
            }
            else
            {
                return totalCost;
            }
        }
    }
}
