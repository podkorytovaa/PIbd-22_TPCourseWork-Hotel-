using System.Collections.Generic;

namespace HotelContracts.BindingModels
{
    public class LinkSeminarToConferencesBindingModel
    {
        public int SeminarId { get; set; }
        public List<int> ConferenceId { get; set; }
    }
}
