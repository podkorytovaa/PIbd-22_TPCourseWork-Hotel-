using System;
using System.Collections.Generic;
using HotelContracts.ViewModels;

namespace HotelBusinessLogic.OfficePackage.HelperModels
{
    public class OrganizerPdfInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public List<ReportConferencesViewModel> Conferences { get; set; }
    }
}
