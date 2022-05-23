using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HotelContracts.ViewModels
{
    public class ConferenceViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название конференции")]
        public string Name { get; set; }

        [DisplayName("Дата проведения")]
        public DateTime DateOf { get; set; }

        public int OrganizerId { get; set; }

        public Dictionary<int, string> ConferenceRooms { get; set; }

        //public Dictionary<int, string> ConferenceSeminars { get; set; }
    }
}
