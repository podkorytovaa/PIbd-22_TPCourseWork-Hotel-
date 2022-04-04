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
        public List<ConferenceViewModel> Conferences { get; set; }
    }
}
