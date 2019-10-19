using System.Collections.Generic;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Service
{
    public interface ISelectListService
    {
        SelectList Retrieve(List<string> values, List<string> textForValues);
    }
}
