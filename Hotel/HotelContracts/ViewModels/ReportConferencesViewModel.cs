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

        public string ConferenceName { get; set; }

        public List<(SeminarViewModel, List<LunchViewModel>)> SeminarLunches { get; set; }
    }
}