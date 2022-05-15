using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.BusinessLogicsContracts
{
    public interface IHeadwaiterReportLogic
    {
        List<ReportRoomSeminarsViewModel> GetRoomSeminars(List<RoomViewModel> rooms);
        List<ReportLunchesViewModel> GetLunches(ReportBindingModel model, int headwaiterId);
        void SaveRoomSeminarsToWord(ReportRoomBindingModel model);
        void SaveRoomSeminarsToExcel(ReportRoomBindingModel model);
        void SaveLunchesToPdf(ReportBindingModel model, int headwaiterId);
    }
}
