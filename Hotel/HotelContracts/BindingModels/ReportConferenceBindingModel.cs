using System;
using System.Collections.Generic;
using HotelContracts.ViewModels;

namespace HotelContracts.BindingModels
{
    public class ReportConferenceBindingModel
    {
        public string FileName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<ConferenceViewModel>? Conferences { get; set; }
        public List<LunchViewModel>? Lunches { get; set; }
        public int OrganizerId { get; set; }
    }
}
