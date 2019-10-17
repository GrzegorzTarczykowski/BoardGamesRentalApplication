using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BLL.Models
{
    public class Filter
    {
        public int ID { set; get; }
        public string Header { set; get; }
        public List<FilterOption> FilterOptions { set; get; }
        public int SelectedFilterOption { set; get; }

        public Filter()
        {
            FilterOptions = new List<FilterOption>();
        }
    }
}
