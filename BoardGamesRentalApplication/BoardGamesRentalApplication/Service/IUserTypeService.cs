using BoardGamesRentalApplication.DAL.Models;
using System;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Service
{
    public interface IUserTypeService
    {
        ActionResult Authorize(Func<ActionResult> actionToAuthorize, params UserType[] allowedUserTypes);
    }
}
