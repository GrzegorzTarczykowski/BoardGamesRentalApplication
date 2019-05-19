using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BIL.Enums
{
    public enum RegisterServiceResponse
    {
        SuccessRegister = 1,
        DuplicateUsername = 2,
        DuplicateEmail = 3
    }
}
