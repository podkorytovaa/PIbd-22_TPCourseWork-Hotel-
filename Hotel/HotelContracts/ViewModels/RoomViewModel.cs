using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace HotelContracts.ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }

        [DisplayName("Номер помещения")]
        public string Number { get; set; }

        [DisplayName("Этаж")]
        public int Level { get; set; }

        public int HeadwaiterId { get; set; }

        public int RoomerID { get; set; }

        public Dictionary<int, string> RoomLunches { get; set; }
    }
}
