using System;
using System.Linq;
using System.Web.Mvc;
using BoardGamesRentalApplication.DAL.Models;

namespace BoardGamesRentalApplication.Service
{
    public class UserTypeService : IUserTypeService
    {
        private readonly Controller controller;
        private readonly ActionResult defaultAction;

        public UserTypeService(Controller controller, ActionResult defaultAction)
        {
            this.controller = controller;
            this.defaultAction = defaultAction;
        }

        public ActionResult Authorize(Func<ActionResult> actionToAuthorize, params UserType[] allowedUserTypes)
        {
            if (!(controller.Session["UserType"] is UserType) || string.IsNullOrEmpty(controller.Session["Username"] as string))
                return defaultAction;

            UserType type = (UserType)controller.Session["UserType"];
            if (allowedUserTypes.Contains(type))
                return actionToAuthorize();

            return defaultAction;
        }
    }
}