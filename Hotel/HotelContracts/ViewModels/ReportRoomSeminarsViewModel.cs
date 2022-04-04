using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelContracts.ViewModels
{
    public class ReportRoomSeminarsViewModel
    {
        public string RoomNumber { get; set; }
        public string LunchName { get; set; }
        public List<Tuple<string>> Seminars { get; set; }
    }
}
