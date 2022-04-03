using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace HotelContracts.ViewModels
{
    public class RoomerViewModel
    {
        public int Id { get; set; }

        [DisplayName("Полное имя")]
        public string FullName { get; set; }

        [DisplayName("Номер телефона")]
        public string PhoneNumber { get; set; }

        [DisplayName("Дата бронирования")]
        public DateTime DateBooking { get; set; }

        public int HeadwaiterId { get; set; }
    }
}
