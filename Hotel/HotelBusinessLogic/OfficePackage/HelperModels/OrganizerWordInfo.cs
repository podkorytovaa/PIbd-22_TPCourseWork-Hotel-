using HotelContracts.ViewModels;
using System.Collections.Generic;

namespace HotelBusinessLogic.OfficePackage.HelperModels
{
    public class OrganizerWordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportConferenceLunchesViewModel> ConferenceLunches { get; set; }
    }
}
