using System;
using System.Collections.Generic;

namespace HotelContracts.ViewModels
{
    public class ReportConferenceLunchesViewModel
    {
        public string ConferenceName { get; set; }
        public string SeminarName { get; set; } // or RoomNumber
        public List<Tuple<string>> Lunches { get; set; }
    }
}
