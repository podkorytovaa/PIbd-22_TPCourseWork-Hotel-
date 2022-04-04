using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelContracts.ViewModels;

namespace HotelContracts.BindingModels
{
    public class ReportRoomBindingModel
    {
        public string FileName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<RoomViewModel> Rooms { get; set; }
    }
}
