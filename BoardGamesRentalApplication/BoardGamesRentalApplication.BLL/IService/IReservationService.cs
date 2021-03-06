﻿using BoardGamesRentalApplication.BLL.Enums;
using BoardGamesRentalApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BLL.IService
{
    public interface IReservationService
    {
        ReservationServiceResponse AddReservation(Reservation reservation);
        decimal CaculateTotalCostByBoardGameDiscountCode(string boardGameDiscountCode, int boardGameId, DateTime rentalStartDate, DateTime rentalEndDate, decimal rentalCostPerDay);
    }
}
