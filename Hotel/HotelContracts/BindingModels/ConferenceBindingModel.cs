using System;
using System.Collections.Generic;

namespace HotelContracts.BindingModels
{
    public class ConferenceBindingModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOf { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int OrganizerId { get; set; }

        public Dictionary<int, string> ConferenceRooms { get; set; }

        public Dictionary<int, string> ConferenceSeminars { get; set; }
    }
}
