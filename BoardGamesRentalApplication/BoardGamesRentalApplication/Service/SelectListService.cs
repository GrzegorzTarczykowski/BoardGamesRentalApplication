using System.Collections.Generic;
using System.Web.Mvc;

namespace BoardGamesRentalApplication.Service
{
    public class SelectListService : ISelectListService
    {
        public SelectList Retrieve(List<string> values, List<string> textForValues)
        {
            if (values.Count != textForValues.Count) return new SelectList(new[]{ new SelectListItem() });
            List<SelectListItem> items = new List<SelectListItem>();
            for (int i = 0; i < values.Count; i++)
                items.Add(new SelectListItem { Value = values[i], Text = textForValues[i] });
            return new SelectList(items, "Value", "Text");
        }
    }
}