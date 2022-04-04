using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelContracts.ViewModels
{
    public class ReportLunchesViewModel
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Dish { get; set; }
        public string Drink { get; set; }
        public string Seminar { get; set; }
        public string Room { get; set; }
    }
}
