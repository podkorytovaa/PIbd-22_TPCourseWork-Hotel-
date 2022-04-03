using System;
using System.Collections.Generic;
using System.Text;

namespace HotelContracts.BindingModels
{
    public class LunchBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Dish { get; set; }
        public string Drink { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public int HeadwaiterId { get; set; }

        public Dictionary<int, string> LunchSeminars { get; set; }
    }
}
