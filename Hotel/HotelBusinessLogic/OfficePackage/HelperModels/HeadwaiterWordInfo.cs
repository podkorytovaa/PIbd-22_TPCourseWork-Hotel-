using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelContracts.ViewModels;

namespace HotelBusinessLogic.OfficePackage.HelperModels
{
    public class HeadwaiterWordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportRoomSeminarsViewModel> RoomSeminars { get; set; }
    }
}
