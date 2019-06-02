using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BLL.Enums
{
    public enum RegisterServiceResponse
    {
        SuccessRegister,
        DuplicateUsername,
        DuplicateEmail
    }

    public enum LoginServiceResponse
    {
        LoginSuccessful,
        UserDoesntExist,
        IncorrectPassword
    }
}
