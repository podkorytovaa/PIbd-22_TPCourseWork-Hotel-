using System.Collections.Generic;
using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.BusinessLogicsContracts
{
    public interface IOrganizerReportLogic
    {
        List<ReportConferenceLunchesViewModel> GetConferenceLunches(List<ConferenceViewModel> conferences);

        List<ReportConferencesViewModel> GetConferences(ReportConferenceBindingModel model);

        void SaveConferenceLunchesToWord(ReportConferenceBindingModel model);

        void SaveConferenceLunchesToExcel(ReportConferenceBindingModel model);

        void SaveConferencesToPdf(ReportConferenceBindingModel model);
    }
}
