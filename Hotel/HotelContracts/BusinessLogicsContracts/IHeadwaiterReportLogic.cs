﻿using System;
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
        List<ReportLunchesViewModel> GetLunches(ReportBindingModel model);
        void SaveRoomSeminarsToWord(ReportBindingModel model);
        void SaveRoomSeminarsToExcel(ReportBindingModel model);
        void SaveLunchesToPdf(ReportBindingModel model);
    }
}
