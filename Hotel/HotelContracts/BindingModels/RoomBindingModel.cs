using System;
using System.Collections.Generic;
using System.Text;

namespace HotelContracts.BindingModels
{
    public class RoomBindingModel
    {
        public int? Id { get; set; }
        public string Number { get; set; }
        public int Level { get; set; }

        public int HeadwaiterId { get; set; }

        //public int RoomerID { get; set; }
        public Dictionary<int, string> Roomers { get; set; }

        public Dictionary<int, string> RoomLunches { get; set; }
    }
}
