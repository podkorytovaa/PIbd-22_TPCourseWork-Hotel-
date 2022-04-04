using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.BusinessLogicsContracts
{
    public interface IOrganizerReportLogic
    {
        List<ReportConferenceLunchesViewModel> GetConferenceLunches(List<ConferenceViewModel> selectedConferences);

        List<ReportConferencesViewModel> GetConferences(ReportBindingModel model);

        void SaveConferenceLunchesToWord(ReportBindingModel model);

        void SaveConferenceLunchesToExcel(ReportBindingModel model);

        void SaveConferencesToPdf(ReportBindingModel model);
    }
}
