using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BLL.IService
{
    public interface ICryptographyService
    {
        byte[] GenerateRandomSalt();
        byte[] GenerateSHA512(string explicitText, byte[] salt);
    }
}
