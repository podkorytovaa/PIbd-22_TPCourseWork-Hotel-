using System;
using System.Collections.Generic;

namespace HotelContracts.ViewModels
{
    public class ReportConferenceLunchesViewModel
    {
        public string ConferenceName { get; set; }
        public string RoomNumber { get; set; }
        public List<Tuple<string>> Lunches { get; set; }
    }
}
