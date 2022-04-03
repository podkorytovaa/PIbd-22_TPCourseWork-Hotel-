using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace HotelContracts.ViewModels
{
    public class LunchViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Блюдо")]
        public string Dish { get; set; }

        [DisplayName("Напиток")]
        public string Drink { get; set; }

        public int HeadwaiterId { get; set; }

        public Dictionary<int, string> LunchSeminars { get; set; }
    }
}
