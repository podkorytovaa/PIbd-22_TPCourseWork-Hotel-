using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelContracts.ViewModels
{
    public class ReportConferencesViewModel
    {
        public DateTime DateOf { get; set; }
        public string Name { get; set; }
        public string Seminar { get; set; }
        public string Lunch { get; set; }
    }
}
