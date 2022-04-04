using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelContracts.BusinessLogicsContracts;
using HotelBusinessLogic.OfficePackage;

namespace HotelBusinessLogic.BusinessLogics
{
    public class OrganizerReportLogic : IOrganizerReportLogic
    {
        private readonly IConferenceStorage _conferenceStorage;
        private readonly ISeminarStorage _seminarStorage;
        private readonly ILunchStorage _lunchStorage;
        private readonly OrganizerAbstractSaveToWord _saveToWord;
        private readonly OrganizerAbstractSaveToExcel _saveToExcel;
        private readonly OrganizerAbstractSaveToPdf _saveToPdf;

        public OrganizerReportLogic(IConferenceStorage conferenceStorage, ISeminarStorage seminarStorage, ILunchStorage lunchStorage,
            OrganizerAbstractSaveToWord saveToWord, OrganizerAbstractSaveToExcel saveToExcel, OrganizerAbstractSaveToPdf saveToPdf)
        {
            _conferenceStorage = conferenceStorage;
            _seminarStorage = seminarStorage;
            _lunchStorage = lunchStorage;
            _saveToWord = saveToWord;
            _saveToExcel = saveToExcel;
            _saveToPdf = saveToPdf;
        }

        public List<ReportConferenceLunchesViewModel> GetConferenceLunches(List<ConferenceViewModel> selectedConferences)
        {
            return null;
        }

        public List<ReportConferencesViewModel> GetConferences(ReportBindingModel model)
        {
            return null;
        }

        public void SaveConferenceLunchesToWord(ReportBindingModel model)
        {
        }

        public void SaveConferenceLunchesToExcel(ReportBindingModel model)
        {
        }

        public void SaveConferencesToPdf(ReportBindingModel model)
        {
        }
    }
}
