﻿using BoardGamesRentalApplication.BIL.Enums;
using BoardGamesRentalApplication.DAL.Models;

namespace BoardGamesRentalApplication.BIL.Service
{
    public interface ILoginService
    {
        LoginServiceResponse Login(User user);
    }
}
