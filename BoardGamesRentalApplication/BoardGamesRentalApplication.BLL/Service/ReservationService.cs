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

        public ReservationService(IRepository<Reservation> reservatioRepository, IRepository<BoardGame> boardGameRepository)
        {
            this.reservatioRepository = reservatioRepository;
            this.boardGameRepository = boardGameRepository;
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
    }
}
