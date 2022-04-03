using System;
using System.Collections.Generic;
using System.Text;

namespace HotelContracts.BindingModels
{
    public class RoomerBindingModel
    {
        public int? Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateBooking { get; set; }
        public int RoomId { get; set; }

        public int HeadwaiterId { get; set; }
    }
}
