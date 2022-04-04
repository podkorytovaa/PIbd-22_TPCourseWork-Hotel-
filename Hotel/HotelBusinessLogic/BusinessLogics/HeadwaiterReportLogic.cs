using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelContracts.BusinessLogicsContracts;
using HotelBusinessLogic.OfficePackage;

namespace HotelBusinessLogic.BusinessLogics
{
    public class HeadwaiterReportLogic : IHeadwaiterReportLogic
    {
        private readonly IConferenceStorage _conferenceStorage;
        private readonly ISeminarStorage _seminarStorage;
        private readonly ILunchStorage _lunchStorage;
        private readonly IRoomStorage _roomStorage;
        private readonly HeadwaiterAbstractSaveToWord _saveToWord;
        private readonly HeadwaiterAbstractSaveToExcel _saveToExcel;
        private readonly HeadwaiterAbstractSaveToPdf _saveToPdf;

        public HeadwaiterReportLogic(IConferenceStorage conferenceStorage, ISeminarStorage seminarStorage, ILunchStorage lunchStorage, IRoomStorage roomStorage,
            HeadwaiterAbstractSaveToWord saveToWord, HeadwaiterAbstractSaveToExcel saveToExcel, HeadwaiterAbstractSaveToPdf saveToPdf)
        {
            _conferenceStorage = conferenceStorage;
            _seminarStorage = seminarStorage;
            _lunchStorage = lunchStorage;
            _roomStorage = roomStorage;
            _saveToWord = saveToWord;
            _saveToExcel = saveToExcel;
            _saveToPdf = saveToPdf;
        }

        public List<ReportRoomSeminarsViewModel> GetRoomSeminars(List<RoomViewModel> rooms)
        {
            return null;
        }

        public List<ReportLunchesViewModel> GetLunches(ReportBindingModel model)
        {
            return null;
        }

        public void SaveRoomSeminarsToWord(ReportBindingModel model)
        {
        }

        public void SaveRoomSeminarsToExcel(ReportBindingModel model)
        {
        }

        public void SaveLunchesToPdf(ReportBindingModel model)
        {
        }
    }
}
